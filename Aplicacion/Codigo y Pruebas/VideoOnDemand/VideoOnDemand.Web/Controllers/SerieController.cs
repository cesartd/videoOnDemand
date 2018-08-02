using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoOnDemand.Entities;
using VideoOnDemand.Repositories;
using VideoOnDemand.Web.Helpers;
using VideoOnDemand.Web.Models;

namespace VideoOnDemand.Web.Controllers
{
    public class SerieController : BaseController
    {
        // GET: Serie
        public ActionResult Index()
        {
            SerieRepository repository = new SerieRepository(context);
            //consulte los individuos del repositorio
            var lst = repository.GetAll();
            //mapeamos la lista de individuos con una lista de IndividualViewModel
            var models = MapHelper.Map<IEnumerable<SerieViewModel>>(lst);
            return View(models);
        }

        // GET: Serie/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Serie/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Serie/Create
        [HttpPost]
        public ActionResult Create(SerieViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //llamado al repositorio
                    SerieRepository repository = new SerieRepository(context);

                    #region validaciones
                    //validar nombre unico
                    var serieQry = new Serie { Nombre = model.Nombre };
                    //consulto los temas con el nombre y valido si existe un elemento 
                    bool existeSerie = repository.QueryByExample(serieQry).Count > 0;
                    if (existeSerie)
                    {
                        ModelState.AddModelError("Nombre", "La serie ya existe!.");
                        return View(model);
                    }
                    #endregion

                    //mapear el modelo de vista a una entidad topic
                    Serie serie = MapHelper.Map<Serie>(model);
                    repository.Insert(serie);
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            catch (Exception ex)
            {
                ModelState.AddModelError(" ", ex.Message);
                return View();
            }
        }

        // GET: Serie/Edit/5
        public ActionResult Edit(int id)
        {
            SerieRepository repository = new SerieRepository(context);
            var topic = repository.Query(t => t.Id == id).First();
            var model = MapHelper.Map<SerieViewModel>(topic);
            return View(model);
        }

        // POST: Serie/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, SerieViewModel model)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    //llamado al repositorio
                    SerieRepository repository = new SerieRepository(context);

                    #region validaciones
                    //validar nombre unico
                    var serieQry = new Serie { Nombre = model.Nombre };
                    //consulto los temas con el nombre y valido si existe un elemento 
                    bool existeSerie = repository.Query(x => x.Nombre == model.Nombre && x.Id != model.Id).Count > 0;
                    if (existeSerie)
                    {
                        ModelState.AddModelError("Nombre", "La serie ya existe!");
                        return View(model);
                    }
                    #endregion

                    //mapear el modelo de vista a una entidad topic
                    Serie serie = MapHelper.Map<Serie>(model);
                    repository.Update(serie);
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }

            catch (Exception ex)
            {
                ModelState.AddModelError(" ", ex.Message);
                return View();
            }
        }

        // GET: Serie/Delete/5
        public ActionResult Delete(int id)
        {
            SerieRepository repository = new SerieRepository(context);
            var serie = repository.Query(t => t.Id == id).First();
            var model = MapHelper.Map<SerieViewModel>(serie);
            return View(model);
        }

        // POST: Serie/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, SerieViewModel model)
        {
            try
            {
                SerieRepository repository = new SerieRepository(context);
                Serie serie = MapHelper.Map<Serie>(model);
                repository.Delete(serie);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
