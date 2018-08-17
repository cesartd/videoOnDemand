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
using Newtonsoft.Json;

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

            MediaRepository mediaObject = new MediaRepository(context);
            var ob = mediaObject.Query(n => n.Id == model.MediaId && n.Estatus == EEstatusMedia.VISIBLE).FirstOrDefault();
            

            //validar que el mediaid del model no sea null
            //consultar en repository si ya esta agregado la serie a favoritos
            MediaOnPlayRepository repository = new MediaOnPlayRepository(context);
            MediaOnPlay fave = MapHelper.Map<MediaOnPlay>(model);
            fave.UsuarioId = usuario.Id;
            fave.Activo = true;
            fave.Media = ob;

            

            var chklist = repository.Query(n => n.MediaId == fave.MediaId && n.UsuarioId == fave.UsuarioId).FirstOrDefault();
            if (chklist != null)
            {
                MediaOnPlay up = MapHelper.Map<MediaOnPlay>(chklist);
                up.Media = ob;
                up.Milisegundo = model.Milisegundo;
                repository.Update(up);
                context.SaveChanges();
            }else
            {
                repository.Insert(fave);
                context.SaveChanges();
            }
         
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetOnPlay(int id)
        {
            try
            {
                #region conseguir Usuario Id
                UsuarioRepository usrep = new UsuarioRepository(context);
                string idsesion = User.Identity.GetUserId();
                Usuario usuario = usrep.Query(x => x.IdentityId == idsesion).FirstOrDefault();
                #endregion

                

                MediaOnPlayRepository repository = new MediaOnPlayRepository(context);
                var onPlay = repository.Query(x => x.MediaId == id && x.UsuarioId == usuario.Id).FirstOrDefault();

                MediaRepository mediaObject = new MediaRepository(context);
                var ob = mediaObject.Query(n => n.Id == id && n.Estatus == EEstatusMedia.VISIBLE).FirstOrDefault();

                var models = MapHelper.Map<Media>(ob);
                onPlay.Media = ob;

                return Json(new
                {
                    Success = true,
                    MediaOnPlay = JsonConvert.SerializeObject(onPlay)
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                return Json(new
                {
                    Success = false,

                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetListOnPlay()
        {

            try
            {
                #region conseguir Usuario Id
                UsuarioRepository usrep = new UsuarioRepository(context);
                string idsesion = User.Identity.GetUserId();
                Usuario usuario = usrep.Query(x => x.IdentityId == idsesion).FirstOrDefault();
                #endregion

               
      
                MediaOnPlayRepository repository = new MediaOnPlayRepository(context);
                var list = repository.Query(n => n.UsuarioId == usuario.Id);

                foreach (var item in list)
                {
                    MediaRepository mediaObject = new MediaRepository(context);
                    var ob = mediaObject.Query(n => n.Id == item.MediaId && n.Estatus == EEstatusMedia.VISIBLE).FirstOrDefault();

                    var models = MapHelper.Map<Media>(ob);
                    item.Media = ob;
                }

                //var models = MapHelper.Map<ICollection<OpinionViewModel>>(opinion);

                return Json(new
                {
                    Success = true,
                    ListOnPlay = JsonConvert.SerializeObject(list)
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                return Json(new
                {
                    Success = false,

                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
