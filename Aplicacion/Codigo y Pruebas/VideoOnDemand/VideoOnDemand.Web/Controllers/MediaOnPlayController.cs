using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using VideoOnDemand.Web.Models;
using VideoOnDemand.Repositories;
using VideoOnDemand.Entities;
using VideoOnDemand.Web.Helpers;

namespace VideoOnDemand.Web.Controllers
{
    public class MediaOnPlayController : BaseController
    {
        // GET: MediaOnPlay
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AgregarOnPlay(MediaOnPlayViewModel model)
        {
            #region conseguir Usuario Id
            UsuarioRepository usrep = new UsuarioRepository(context);
            string idsesion = User.Identity.GetUserId();
            Usuario usuario = usrep.Query(x => x.IdentityId == idsesion).FirstOrDefault();
            #endregion

            //validar que el mediaid del model no sea null
            //consultar en repository si ya esta agregado la serie a favoritos


            MediaOnPlayRepository repository = new MediaOnPlayRepository(context);
            MediaOnPlay fave = MapHelper.Map<MediaOnPlay>(model);
            fave.UsuarioId = usuario.Id;

            repository.Insert(fave);
            context.SaveChanges();

            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);

        }
    }
}
