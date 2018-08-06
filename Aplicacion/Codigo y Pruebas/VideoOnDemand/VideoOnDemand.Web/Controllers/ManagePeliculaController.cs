using System;
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
    public class ManagePeliculaController : Controller
    {

        VideoOnDemandContext context = new VideoOnDemandContext();

        // GET: Movie
        public ActionResult Index()
        {
            MovieRepository repository = new MovieRepository(context);
            var list = repository.GetAll();
            var models = MapHelper.Map<IEnumerable<MovieViewModel>>(list);

            var MovieQry = models.Where(m=>m.Estatus.Equals(EEstatusMedia.VISIBLE));

            return View(MovieQry);
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
            var lst = generoRepository.GetAll();
            model.GenerosDisponibles = MapHelper.Map<ICollection<GeneroViewModel>>(lst);

            PersonaRepository personaRepository = new PersonaRepository(context);
            var lst2 = personaRepository.GetAll();
            model.ActoresDisponibles = MapHelper.Map<ICollection<PersonaViewModel>>(lst2);

            return View(model);
        }

        // POST: Movie/Create
        [HttpPost]
        public ActionResult Create(MovieViewModel model)
        {
            GeneroRepository generoRepository = new GeneroRepository(context);
            var lst = generoRepository.GetAll();
            model.GenerosDisponibles = MapHelper.Map<ICollection<GeneroViewModel>>(lst);

            PersonaRepository personaRepository = new PersonaRepository(context);
            var lst2 = personaRepository.GetAll();
            model.ActoresDisponibles = MapHelper.Map<ICollection<PersonaViewModel>>(lst2);

            try
            {
                if (ModelState.IsValid)
                {
                    //llamado al repositorio                   
                    MovieRepository repository = new MovieRepository(context);

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
                    Movie movie = MapHelper.Map<Movie>(model);
                    movie.FechaDeRegistro = DateTime.Now;
                    movie.Estatus = EEstatusMedia.VISIBLE;
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
                    //consulto las movceis con el nombre y valido si existe un elemento 
                    bool existeMovie = repository.Query(x => x.Nombre == model.Nombre && x.Id != model.Id).Count > 0;
                    if (existeMovie)
                    {
                        ModelState.AddModelError("Name", "El nombre de la película ya existe.");
                        return View(model);
                    }
                    #endregion

                    //mapear el modelo de vista a una entidad movie
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
