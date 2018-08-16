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
            SerieRepository repository = new SerieRepository(context);
            //consulte los individuos del repositorio
            var lst = repository.GetAll();
            //mapeamos la lista de individuos con una lista de IndividualViewModel
            var models = MapHelper.Map<IEnumerable<SerieViewModel>>(lst);
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


            var usuarioId = User.Identity.GetUserId();
            var repositoryUser = new UsuarioRepository(context);
            var usuario = repositoryUser.Query(c => c.IdentityId == usuarioId).FirstOrDefault();
            if (usuario != null)
            {
               ViewBag.UsuarioId = usuario.Id;
            }
            return View(temporadas);

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