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
    public class PeliculaController : Controller
    {
        VideoOnDemandContext context = new VideoOnDemandContext();
        // GET: Pelicula
        public ActionResult Index()
        {
             MovieRepository repository = new MovieRepository(context);

            var list = repository.GetAll();
            var models = MapHelper.Map<IEnumerable<MovieViewModel>>(list);

            var MovieQry = models.Where(m => m.Estatus.Equals(EEstatusMedia.VISIBLE));

            return View(MovieQry);
        }

        // GET: Pelicula/Details/5
        public ActionResult Details(int id)
        {
            MovieRepository repository = new MovieRepository(context);
            var topic = repository.Query(t => t.Id == id).First();
            var model = MapHelper.Map<MovieViewModel>(topic);
            return View(model);
        }
       
    }
}
