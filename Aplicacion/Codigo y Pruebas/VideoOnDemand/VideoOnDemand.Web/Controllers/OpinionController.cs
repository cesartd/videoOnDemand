using Newtonsoft.Json;
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
    public class OpinionController : BaseController
    {
        // GET: Opinion
        public ActionResult Index()
        {
            return View();
        }

        // GET: Opinion/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Opinion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Opinion/Create
        [HttpPost]
        public ActionResult Create(OpinionViewModel model)
        {
            try
            {
                var usuarioId = User.Identity.GetUserId();
                var repositoryUser = new UsuarioRepository(context);
                var usuario = repositoryUser.Query(c => c.IdentityId == usuarioId).FirstOrDefault();
                if (usuario == null)
                {
                    throw new ArgumentNullException("Usuario nulo");
                }

                //Validar la descripción max 200 carac.
                //Puntuacipon 0-5
                //Reseña no repetida por usuario en el media
                var repository = new OpinionRepository(context);
                var opinion = repository.Query(x => x.MediaId == model.MediaId && x.UsuarioId == usuario.Id).FirstOrDefault();
                if (opinion != null)
                {
                    throw new ArgumentNullException("Opinión repetida");
                }
                var registroOpinion = MapHelper.Map<Opinion>(model);
                registroOpinion.FechaRegistro = DateTime.Now;
                registroOpinion.UsuarioId = usuario.Id;
                repository.Insert(registroOpinion);
                context.SaveChanges();
                return Json(new
                {
                    Success = true,

                }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                return Json(new
                {
                    Success = false,

                }, JsonRequestBehavior.AllowGet);
            }
        }

       
        [HttpPost]
        public ActionResult Delete(OpinionViewModel model)
        {
            try
            {
                if (model==null) {
                    throw new ArgumentNullException("El modelo no puede ser nulo");
                }
                if (model.Id==null) {
                    throw new ArgumentNullException("Id nulo");
                }
                var usuarioId = User.Identity.GetUserId();
                var repositoryUser = new UsuarioRepository(context);
                var usuario = repositoryUser.Query(c => c.IdentityId == usuarioId).FirstOrDefault();
                if (usuario == null)
                {
                    throw new ArgumentNullException("Usuario nulo");
                }
                var repository = new OpinionRepository(context);
                var opinion = repository.Query(x => x.Id == model.Id && x.UsuarioId == usuario.Id).FirstOrDefault();
                if (opinion == null)
                {
                    throw new ArgumentNullException("Opinión nula");
                }
                repository.Delete(opinion);
                context.SaveChanges();
                return Json(new
                {
                    Success = true,

                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e) {
                return Json(new
                {
                    Success = false,

                }, JsonRequestBehavior.AllowGet);
            }
        }

        
        public ActionResult GetList(int id) {
            try
            {

                var repository = new OpinionRepository(context);
                var includes1 = new Expression<Func<Opinion, object>>[] { x => x.Usuario };
                var opinion = repository.QueryIncluding(x => x.MediaId == id, includes1);
                var models = MapHelper.Map<ICollection<OpinionViewModel>>(opinion);

                return Json(new
                {
                    Success = true,
                    Opiniones = JsonConvert.SerializeObject(opinion)
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e) {

                return Json(new
                {
                    Success = false,

                }, JsonRequestBehavior.AllowGet);
            }
        }
        
    }
}
