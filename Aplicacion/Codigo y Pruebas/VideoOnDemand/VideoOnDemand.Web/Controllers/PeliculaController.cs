using System;
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
    public class PeliculaController : Controller
    {
        VideoOnDemandContext context = new VideoOnDemandContext();
        // GET: Pelicula
        public ActionResult Index(int page = 1, string Search = null, string genero = null, int pageSize = 7)
        {


            MovieRepository repository = new MovieRepository(context);
            GeneroRepository generoRepository = new GeneroRepository(context);
            var includes = new Expression<Func<Movie, object>>[] { m => m.Generos };

            int paginasTotales;
            int filasTotales;

            ICollection<Movie> movies;

            movies = repository.QueryPageByNombreAndGeneroIncluding(Search, genero, includes, out paginasTotales, out filasTotales, "Nombre", page -1, pageSize);

            ViewBag.Busqueda = Search;
            ViewBag.Genero = genero;
            ViewBag.ListaGeneros = generoRepository.GetAll().Where(g=>g.Activo == true).Select(g => g.Nombre).Where(g => g != genero).ToList();

            var paginador = new PaginatorViewModel<MovieViewModel>();
            paginador.Page = page;
            paginador.PageSize = pageSize;
            paginador.Results = MapHelper.Map<ICollection<MovieViewModel>>(movies);
            paginador.TotalPages = paginasTotales;
            paginador.TotalRows = filasTotales;

            return View(paginador);





            #region Old

            // var lst = generoRepository.GetAll();

            // Movie movie = new Movie();
            // movie.Nombre = Search;

            // ICollection<Movie> list = null;

            // if (!String.IsNullOrEmpty(Search))
            // {
            //     list = repository.QueryByExample(movie);

            // }
            // else
            // {

            //     list = repository.GetAll().ToList();
            // }

            // var models = MapHelper.Map<IEnumerable<MovieViewModel>>(list);
            // var MovieQry = models.Where(m => m.Estatus.Equals(EEstatusMedia.VISIBLE));


            //MovieQry.First().GenerosDisponibles = MapHelper.Map<ICollection<GeneroViewModel>>(lst);
            // return View(MovieQry);

            #endregion


        }

        // GET: Pelicula/Details/5
        public ActionResult Details(int id)
        {
            MovieRepository repository = new MovieRepository(context);
            var topic = repository.Query(t => t.Id == id).First();
            var model = MapHelper.Map<MovieViewModel>(topic);
            return View(model);
        }

        // GET: Pelicula/Lista
        public ActionResult Lista(int page = 1, string Search = null, string genero = null, int pageSize = 10)
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

    }
}
