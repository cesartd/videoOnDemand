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
    public class SeriePrincipalController : BaseController
    {
        // GET: SeriePrincipal
        public ActionResult Index()
        {
            SerieRepository repository = new SerieRepository(context);
            //consulte los individuos del repositorio
            var lst = repository.GetAll();
            //mapeamos la lista de individuos con una lista de IndividualViewModel
            var models = MapHelper.Map<IEnumerable<SerieViewModel>>(lst);
            return View(models);
        }

        // GET: SeriePrincipal/Details
        public ActionResult Details(int id)
        {
            EpisodioRepository repository = new EpisodioRepository(context);

            SerieRepository repositorySerie = new SerieRepository(context);

            var includes1 = new Expression<Func<Serie, object>>[] { x => x.Generos, x => x.Actores };

            //var includes2 = new Expression<Func<Serie, object>>[] { x => x.Actores };

            var serie = repositorySerie.QueryIncluding(n => n.Id == id && n.Estatus == EEstatusMedia.VISIBLE, includes1).FirstOrDefault();
       

            if (serie == null)
            {
                return RedirectToAction("Index");
            }

            SerieActorGeneroViewModel serieModel = MapHelper.Map<SerieActorGeneroViewModel>(serie);

            //consulte los episodios del repositorio
            var lst = repository.Query(t => t.SerieId == id && t.Estatus == EEstatusMedia.VISIBLE).GroupBy(n => n.Temporada).OrderBy(n=> n.Key);

            //lst.GroupBy(n => n.Temporada);

            List<TemporadaViewModel> temporadas = new List<TemporadaViewModel>();

            foreach (var item in lst )
            {
                TemporadaViewModel temp = new TemporadaViewModel();
                temp.Temporada = item.Key;
                temp.Episodios = MapHelper.Map<List<EpisodioViewModel>>(item.ToList());

                temporadas.Add(temp);
            }

            ViewBag.SerieId = id;
            ViewBag.SerieDetalle = serieModel;

            //mapeamos la lista de individuos con una lista de EpisodioViewModel
            //var models = MapHelper.Map<IEnumerable<EpisodioViewModel>>(lst);


            return View(temporadas);
        }
       
    }
}