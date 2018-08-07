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
    public class GeneroController : BaseController
    {
        // GET: Genero
        public ActionResult Index()
        {
            GeneroRepository repository = new GeneroRepository(context);
            var lst = repository.Query(n => n.Activo == true);

            var models = MapHelper.Map<IEnumerable<GeneroViewModel>>(lst);
            
            return View(models);
        }

        // GET: Genero/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Genero/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genero/Create
        [HttpPost]
        public ActionResult Create(GeneroViewModel model)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    GeneroRepository repository = new GeneroRepository(context);

                    #region validaciones
                    var generoQry = new Genero {Nombre = model.Nombre };
                    bool existeGenero = repository.QueryByExample(generoQry).Count > 0;

                    if (existeGenero)
                    {
                        ModelState.AddModelError("Nombre", "El nombre ya existe.");
                        return View(model);
                    }
                    #endregion

                    Genero genero = MapHelper.Map<Genero>(model);
                    genero.Activo = true;
                    repository.Insert(genero);

                    context.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }


        // GET: Topic/Edit/5
        public ActionResult Edit(int id)
        {
            GeneroRepository repository = new GeneroRepository(context);

            var genero = repository.Query(t => t.Id == id).First();

            var model = MapHelper.Map<GeneroViewModel>(genero);

            return View(model);
        }

        // POST: Topic/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, GeneroViewModel model)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    GeneroRepository repository = new GeneroRepository(context);

                    #region Validaciones
                    // Consulto los generos con el nombre
                    bool existeGenero = repository.Query(x => x.Nombre == model.Nombre && x.Id != model.Id)
                        .Count > 0;

                    if (existeGenero)
                    {
                        ModelState.AddModelError("Nombre", "El nombre del genero ya existe.");
                        return View(model);
                    }
                    #endregion

                    // Variable auxiliar que sirve de mapeo de GeneroViewModel a Genero
                    Genero genero = MapHelper.Map<Genero>(model);

                    // Se modifica el valor.
                    repository.Update(genero);

                    // Guardar y registrar cambios.
                    context.SaveChanges();

                    return RedirectToAction("Index");
                }

                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(model);
            }
        }

        // GET: Topic/Delete/5
        public ActionResult Delete(int id)
        {
            GeneroRepository repository = new GeneroRepository(context);
            var topic = repository.Query(t => t.Id == id).First();
            var model = MapHelper.Map<GeneroViewModel>(topic);

            return View(model);
        }

        // POST: Topic/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, GeneroViewModel model)
        {
            try
            {
                // TODO: Add delete logic here
                GeneroRepository repository = new GeneroRepository(context);

                var topic = repository.Query(n => n.Id == id).First();
                topic.Activo = false;
                                
                repository.Update(topic);
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
