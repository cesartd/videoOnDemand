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
        #region ActionResult Serie
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

        #endregion

        #region ActionResult Episodios
        // GET: Serie/EpisodesIndex/5
        public ActionResult EpisodesIndex(int id)
        {
            EpisodioRepository repository = new EpisodioRepository(context);
            //consulte los episodios del repositorio

            var lst = repository.Query(t => t.SerieId == id);

            ViewBag.SerieId = id;

            //mapeamos la lista de individuos con una lista de EpisodioViewModel
            var models = MapHelper.Map<IEnumerable<EpisodioViewModel>>(lst);

            return View(models);
        }

        // GET: Episodio
        public ActionResult CreateEpisodes(int id)
        {
            EpisodioViewModel model = new EpisodioViewModel();
            model.SerieId = id;
             
            return View(model);
        }

        // POST: Serie/EpisodesIndex/5
        [HttpPost]
        public ActionResult CreateEpisodes(EpisodioViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //llamado al repositorio
                    SerieRepository repository2 = new SerieRepository(context);

                    EpisodioRepository repository = new EpisodioRepository(context);

                    #region validaciones
                    //validar nombre unico
                    var episodioQry = new Episodio { Nombre = model.Nombre };
                    //consulto los episodios con el nombre y valido si existe un elemento 
                    bool existeEpisodio = repository.QueryByExample(episodioQry).Count > 0;

                    if (existeEpisodio)
                    {
                        ModelState.AddModelError("Nombre", "El capitulo ya existe!.");
                        return View(model);
                    }
                    #endregion

                    //mapear el modelo de vista a una entidad episodio
                    Episodio episodio = MapHelper.Map<Episodio>(model);
                    episodio.Serie = repository2.Query(t => t.Id == episodio.SerieId).First();
                    repository.Insert(episodio);
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

        // GET: Serie/EpisodesIndex/EditEpisode/5
        public ActionResult EditEpisode(int id)
        {
            EpisodioRepository repo = new EpisodioRepository(context);
            var episode = repo.Query(t => t.Id == id).First();
            var model = MapHelper.Map<EpisodioViewModel>(episode);

            return View(model);
        }

        // POST: Serie/EpisodesIndex/EditEpisode/5
        [HttpPost]
        public ActionResult EditEpisode(int id, EpisodioViewModel model)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    //llamado al repositorio
                    EpisodioRepository repository = new EpisodioRepository(context);

                    #region validaciones
                    //validar nombre unico
                    var episodioQry = new Episodio { Nombre = model.Nombre };
                    //consulto los temas con el nombre y valido si existe un elemento 
                    bool existeSerie = repository.Query(x => x.Nombre == model.Nombre && x.Id != model.Id).Count > 0;

                    if (existeSerie)
                    {
                        ModelState.AddModelError("Nombre", "La serie ya existe!");
                        return View(model);
                    }
                    #endregion

                    //mapear el modelo de vista a una entidad topic
                    Episodio serie = MapHelper.Map<Episodio>(model);
                    repository.Update(serie);
                    context.SaveChanges();
                }
                return RedirectToAction("EpisodesIndex");
            }

            catch (Exception ex)
            {
                ModelState.AddModelError(" ", ex.Message);
                return View();
            }
        }


        #endregion
    }
}
