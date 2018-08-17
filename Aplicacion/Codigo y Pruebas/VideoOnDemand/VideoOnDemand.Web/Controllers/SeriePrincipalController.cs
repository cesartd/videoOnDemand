using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using VideoOnDemand.Entities;
using VideoOnDemand.Repositories;
using VideoOnDemand.Web.Helpers;
using VideoOnDemand.Web.Models;
using Microsoft.AspNet.Identity;

namespace VideoOnDemand.Web.Controllers
{
    public class SeriePrincipalController : BaseController
    {
        // GET: SeriePrincipal
        public ActionResult Index()
        {
            #region conseguir Usuario Id
            UsuarioRepository usrep = new UsuarioRepository(context); //se llama al repository de usuario
            string idsesion = User.Identity.GetUserId(); //codigo magico para hacer algo, importante incluir la libreria
            Usuario usuario = usrep.Query(x => x.IdentityId == idsesion).FirstOrDefault(); //se ejecuta el query para conseguir un objeto del usuario
            #endregion

            SerieRepository repository = new SerieRepository(context);
            FavoritoRepository favrepo = new FavoritoRepository(context); //se llama al repositorio de favoritos
            //consulte los individuos del repositorio
            var lst = repository.Query(x => x.Estatus != EEstatusMedia.INVISIBLE); //consigue todos los elementos de la tabla de series
            var lst2 = favrepo.GetAll(); //consigue todos los elementos de la tabla de favoritos
            //mapeamos la lista de individuos con una lista de de sus modelos
            var models = MapHelper.Map<IEnumerable<SerieViewModel>>(lst); //se convierte los elementos conseguidos del repositorio a una lista de objetos SerieViewModel
            var favmodel = MapHelper.Map<IEnumerable<FavoritoViewModel>>(lst2); //se convierte los elementos conseguidos del repositorio a una lista de objetos FavoritoViewModel


            /*Este query hace un join entre las dos tablas de Series y Favoritos para sacar los elementos que
              comparten el mismo ID*/
            /*De esta forma podemos ver cuales series estan en la tabla de favoritos y ademas podemos filtrar 
              solo las series que corresponden al usuario que esta usando el sistema*/
            var query = from serie in models.AsEnumerable() //'serie' contiene los elementos de la tabla Series
                        join favo in favmodel.AsEnumerable() //y se hace un join con 'favo' que contiene los elementos de la tabla Favoritos
                        on serie.Id equals favo.MediaId //se filtra solo las series que estan en la tabla de Favoritos
                        where favo.UsuarioId == usuario.Id //y de estas series se sacan las que el usuario agrego a su lista de favoritos
                        select serie; //los cuales se guardan en 'query'

            foreach (SerieViewModel mod in query) 
            {
                //Es importante agregar a su ViewModel un variable booleano (eg. public bool isAdded = false)
                //este booleano se vuelve true cuando se encuentra una serie que el usuario agrego a su lista
                var modelo = models.FirstOrDefault(x => x.Id == mod.Id);
                modelo.isAdded = true; //se cambia el valor a true
            }

            return View(models);
        }

        // GET: SeriePrincipal/Details
        public ActionResult Details(int id)
        {
            EpisodioRepository repository = new EpisodioRepository(context);

            SerieRepository repositorySerie = new SerieRepository(context);

            var includes1 = new Expression<Func<Serie, object>>[] { x => x.Generos, x => x.Actores };

            //var includes2 = new Expression<Func<Serie, object>>[] { x => x.Actores };

            var serie = repositorySerie.QueryIncluding(n => n.Id == id && n.Estatus == EEstatusMedia.VISIBLE, includes1).FirstOrDefault();


            if (serie == null)
            {
                return RedirectToAction("Index");
            }

            SerieActorGeneroViewModel serieModel = MapHelper.Map<SerieActorGeneroViewModel>(serie);

            //consulte los episodios del repositorio
            var lst = repository.Query(t => t.SerieId == id && t.Estatus == EEstatusMedia.VISIBLE).GroupBy(n => n.Temporada).OrderBy(n => n.Key);

            //lst.GroupBy(n => n.Temporada);

            List<TemporadaViewModel> temporadas = new List<TemporadaViewModel>();
            //lst.GroupBy(n => n.Temporada);
            foreach (var item in lst)
            {
                TemporadaViewModel temp = new TemporadaViewModel();
                temp.Temporada = item.Key;
                temp.Episodios = MapHelper.Map<List<EpisodioViewModel>>(item.ToList());

                temporadas.Add(temp);
            }

            ViewBag.SerieId = id;
            ViewBag.SerieDetalle = serieModel;

            return View(temporadas);
        }

            //mapeamos la lista de individuos con una lista de EpisodioViewModel
            //var models = MapHelper.Map<IEnumerable<EpisodioViewModel>>(lst);


           /* var usuarioId = User.Identity.GetUserId();
            var repositoryUser = new UsuarioRepository(context);
            var usuario = repositoryUser.Query(c => c.IdentityId == usuarioId).FirstOrDefault();
            if (usuario != null)
            {
               ViewBag.UsuarioId = usuario.Id;
            }
            return View(temporadas);*/

        [HttpPost]
        public ActionResult AgregarFavorito(FavoritoViewModel model)
        {
            #region conseguir Usuario Id
            UsuarioRepository usrep = new UsuarioRepository(context);
            string idsesion = User.Identity.GetUserId();
            Usuario usuario = usrep.Query(x => x.IdentityId == idsesion).FirstOrDefault();
            #endregion

            //validar que el mediaid del model no sea null
            //consultar en repository si ya esta agregado la serie a favoritos


            FavoritoRepository repository = new FavoritoRepository(context);
            Favorito fave = MapHelper.Map<Favorito>(model);
            fave.FechaAgregado = DateTime.Now;
            fave.UsuarioId = usuario.Id;

            repository.Insert(fave);
            context.SaveChanges();

            return Json(new { Success = true},JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult DeleteFavorito(FavoritoViewModel model)
        {
            #region conseguir Usuario Id
            UsuarioRepository usrep = new UsuarioRepository(context);
            string idsesion = User.Identity.GetUserId();
            Usuario usuario = usrep.Query(x => x.IdentityId == idsesion).FirstOrDefault();
            #endregion

            //validar que el mediaid del model no sea null
            //consultar en repository si ya esta agregado la serie a favoritos


            FavoritoRepository repository = new FavoritoRepository(context);
            Favorito fave = repository.Query(x => x.MediaId == model.MediaId && x.UsuarioId == usuario.Id).FirstOrDefault();
            if(fave != null)
            {
                repository.Delete(fave);
                context.SaveChanges();
                return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
            }
          
        }       
    }
}