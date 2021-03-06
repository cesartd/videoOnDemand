﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoOnDemand.Data;
using VideoOnDemand.Entities;
using VideoOnDemand.Repositories;
using VideoOnDemand.Web.Helpers;
using VideoOnDemand.Web.Models;

namespace VideoOnDemand.Web.Controllers
{
    public class MovieController : Controller
    {

        VideoOnDemandContext context = new VideoOnDemandContext();

        // GET: Movie
        public ActionResult Index()
        {
            MovieRepository repository = new MovieRepository(context);

            var list = repository.GetAll();
            var models = MapHelper.Map<IEnumerable<MovieViewModel>>(list);
            return View(models);
        }

        // GET: Movie/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Movie/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movie/Create
        [HttpPost]
        public ActionResult Create(MovieViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //llamado al repositorio                   
                    MovieRepository repository = new MovieRepository(context);

                    #region validaciones
                    //validar nombre unico
                    var MovieQry = new Movie { Nombre = model.Nombre };
                    //consulto los temas con el nombre y valido si existe un elemento 
                    bool existeMovie = repository.QueryByExample(MovieQry).Count > 0;
                    if (existeMovie)
                    {
                        ModelState.AddModelError("Name", "El nombre de la película ya existe.");
                        return View(model);
                    }
                    #endregion

                    //mapear el modelo de vista a una entidad topic
                    Movie movie = MapHelper.Map<Movie>(model);
                    movie.FechaDeRegistro = DateTime.Now;
                    repository.Insert(movie);
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

        // GET: Movie/Edit/5
        public ActionResult Edit(int id)
        {
            MovieRepository repository = new MovieRepository(context);
            var movie = repository.Query(t => t.Id == id).First();
            var model = MapHelper.Map<MovieViewModel>(movie);
            return View(model);
        }

        // POST: Movie/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, MovieViewModel model)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    //llamado al repositorio
                    MovieRepository repository = new MovieRepository(context);

                    #region validaciones
                    //validar nombre unico
                    var movieQry = new Movie { Nombre = model.Nombre };
                    //consulto los temas con el nombre y valido si existe un elemento 
                    bool existeMovie = repository.Query(x => x.Nombre == model.Nombre && x.Id != model.Id).Count > 0;
                    if (existeMovie)
                    {
                        ModelState.AddModelError("Name", "El nombre de la película ya existe.");
                        return View(model);
                    }
                    #endregion

                    //mapear el modelo de vista a una entidad topic
                    Movie movie = MapHelper.Map<Movie>(model);
                    repository.Update(movie);
                    context.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Movie/Delete/5
        public ActionResult Delete(int id)
        {
            MovieRepository repository = new MovieRepository(context);
            var movie = repository.Query(t => t.Id == id).First();
            var model = MapHelper.Map<MovieViewModel>(movie);
            return View(model);
        }

        // POST: Movie/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, MovieViewModel model)
        {
            try
            {
                MovieRepository repository = new MovieRepository(context);
                Movie movie = MapHelper.Map<Movie>(model);
                repository.Delete(movie);
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
