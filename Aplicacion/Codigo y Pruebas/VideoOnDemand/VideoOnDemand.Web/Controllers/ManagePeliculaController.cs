﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using VideoOnDemand.Data;
using VideoOnDemand.Entities;
using VideoOnDemand.Repositories;
using VideoOnDemand.Web.Helpers;
using VideoOnDemand.Web.Models;

namespace VideoOnDemand.Web.Controllers
{
    public class ManagePeliculaController : Controller
    {

        VideoOnDemandContext context = new VideoOnDemandContext();

        // GET: Movie
        public ActionResult Index(int page = 1, string Search = null, string genero = null, int pageSize = 100)
        {


            MovieRepository repository = new MovieRepository(context);
            GeneroRepository generoRepository = new GeneroRepository(context);
            var includes = new Expression<Func<Movie, object>>[] { m => m.Generos };

            int paginasTotales;
            int filasTotales;

            ICollection<Movie> movies;

            movies = repository.QueryPageByNombreAndGeneroIncluding(Search, genero, includes, out paginasTotales, out filasTotales, "Nombre", page - 1, pageSize);

            ViewBag.Busqueda = Search;
            ViewBag.Genero = genero;
            ViewBag.ListaGeneros = generoRepository.GetAll().Where(g => g.Activo == true).Select(g => g.Nombre).Where(g => g != genero).ToList();

            var paginador = new PaginatorViewModel<MovieViewModel>();
            paginador.Page = page;
            paginador.PageSize = pageSize;
            paginador.Results = MapHelper.Map<ICollection<MovieViewModel>>(movies);
            paginador.TotalPages = paginasTotales;
            paginador.TotalRows = filasTotales;

            return View(paginador);



        }

        // GET: Movie/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Movie/Create
        public ActionResult Create()
        {
            var model = new MovieViewModel();
            GeneroRepository generoRepository = new GeneroRepository(context);
            var lst = generoRepository.GetAll().Where(g => g.Activo == true).ToList();
            model.GenerosDisponibles = MapHelper.Map<ICollection<GeneroViewModel>>(lst);

            PersonaRepository personaRepository = new PersonaRepository(context);
            var lst2 = personaRepository.GetAll().Where(g => g.Activo == true).ToList(); 
            model.ActoresDisponibles = MapHelper.Map<ICollection<PersonaViewModel>>(lst2);

            return View(model);
        }

        // POST: Movie/Create
        [HttpPost]
        public ActionResult Create(MovieViewModel model)
        {
            GeneroRepository generoRepository = new GeneroRepository(context);
            var lst = generoRepository.GetAll().Where(g=>g.Activo == true).ToList();
            model.GenerosDisponibles = MapHelper.Map<ICollection<GeneroViewModel>>(lst);

            PersonaRepository personaRepository = new PersonaRepository(context);
            var lst2 = personaRepository.GetAll().Where(g => g.Activo == true).ToList();
            model.ActoresDisponibles = MapHelper.Map<ICollection<PersonaViewModel>>(lst2);


            try
            {
                MovieRepository repository = new MovieRepository(context);

                if (ModelState.IsValid)
                {            
                    

                    #region validaciones
                    //validar nombre unico
                    var MovieQry = new Movie { Nombre = model.Nombre };
                    //consulto las movies con el nombre y valido si existe un elemento 
                    bool existeMovie = repository.QueryByExample(MovieQry).Count > 0;
                    if (existeMovie)
                    {
                        ModelState.AddModelError("Name", "El nombre de la película ya existe.");
                        return View(model);
                    }
                    #endregion

                    //mapear el modelo de vista a una entidad movie
                    var movie = MapHelper.Map<Movie>(model);
                    movie.FechaDeRegistro = DateTime.Now;
                    repository.InsertComplete(movie, model.ActoresSeleccionados, model.GenerosSeleccionados);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }

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
            var repository = new MovieRepository(context);
            var generoRepo = new GeneroRepository(context);
            var actorRepo = new PersonaRepository(context);

            //Expreson lambda para incluir las relaciones
            var includes1 = new Expression<Func<Movie, object>>[] { x => x.Generos};
            var includes2 = new Expression<Func<Movie, object>>[] { x => x.Actores };
            var movie = repository.QueryIncluding(x => x.Id == id, includes1).SingleOrDefault();
            movie = repository.QueryIncluding(x => x.Id == id, includes2).SingleOrDefault();
            var model = MapHelper.Map<MovieViewModel>(movie);

            //Consultando topic ordenados por Name
            var genero = generoRepo.Query(null, "Nombre").Where(g => g.Activo == true).ToList();
            var actor = actorRepo.Query(null, "Nombre").Where(g => g.Activo == true).ToList();

            //map de topic a topic view model
            model.GenerosDisponibles = MapHelper.Map<ICollection<GeneroViewModel>>(genero);
            model.GenerosSeleccionados = movie.Generos.Select(x => x.Id.Value).ToArray();
            model.ActoresDisponibles = MapHelper.Map<ICollection<PersonaViewModel>>(actor);
            model.ActoresSeleccionados = movie.Actores.Select(x => x.Id.Value).ToArray();
            return View(model);
        }

        // POST: Movie/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, MovieViewModel model)
        {
            var generoRepo = new GeneroRepository(context);
            var actorRepo = new PersonaRepository(context);
            try
            {
                var repository = new MovieRepository(context);
                if (ModelState.IsValid)
                {
                    #region validaciones
                    //validar nombre unico
                    var movieQry = new Movie { Nombre = model.Nombre };
                    //consulto las movceis con el nombre y valido si existe un elemento 
                    bool existeMovie = repository.Query(x => x.Nombre == model.Nombre && x.Id != model.Id).Count > 0;
                    if (existeMovie)
                    {
                        ModelState.AddModelError("Nombre", "El nombre de la película ya existe.");
                        return View(model);
                    }
                    #endregion

                    //mapear el modelo de vista a una entidad movie
                    Movie movie = MapHelper.Map<Movie>(model);
                    repository.UpdateComplete(movie, model.ActoresSeleccionados,model.GenerosSeleccionados);
                    context.SaveChanges();
                    return RedirectToAction("Index");
                }
                var genero = generoRepo.Query(null, "Nombre").Where(g => g.Activo == true).ToList();
                var actor = actorRepo.Query(null, "Nombre").Where(g => g.Activo == true).ToList();
                model.GenerosDisponibles = MapHelper.Map<ICollection<GeneroViewModel>>(genero);
                model.ActoresDisponibles = MapHelper.Map<ICollection<PersonaViewModel>>(actor);
                return View(model);
            }
            catch (Exception ex)
            {
                var genero = generoRepo.Query(null, "Nombre").Where(g => g.Activo == true).ToList();
                var actor = actorRepo.Query(null, "Nombre").Where(g => g.Activo == true).ToList();
                model.GenerosDisponibles = MapHelper.Map<ICollection<GeneroViewModel>>(genero);
                model.ActoresDisponibles = MapHelper.Map<ICollection<PersonaViewModel>>(actor);
                model.FechaDeRegistro = DateTime.Now;
                return View(model);
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
                var pelicula = repository.Query(e => e.Id == id).First();
                pelicula.Estatus = EEstatusMedia.ELIMINADO;
                repository.Update(pelicula);
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
