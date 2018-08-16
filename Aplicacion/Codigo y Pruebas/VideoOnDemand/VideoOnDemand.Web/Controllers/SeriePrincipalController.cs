using System;
using System.Collections.Generic;
using System.Linq;
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
            //consulte los episodios del repositorio

            var lst = repository.Query(t => t.SerieId == id && t.Estatus == EEstatusMedia.VISIBLE);

            ViewBag.SerieId = id;

            //mapeamos la lista de individuos con una lista de EpisodioViewModel
            var models = MapHelper.Map<IEnumerable<EpisodioViewModel>>(lst);

            return View(models);
        }

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