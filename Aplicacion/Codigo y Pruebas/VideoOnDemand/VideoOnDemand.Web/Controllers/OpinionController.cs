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
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Opinion/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Opinion/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Opinion/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Opinion/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        public ActionResult GetList(int id){
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
            catch(Exception e) {

                return Json(new
                {
                    Success = false,

                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
