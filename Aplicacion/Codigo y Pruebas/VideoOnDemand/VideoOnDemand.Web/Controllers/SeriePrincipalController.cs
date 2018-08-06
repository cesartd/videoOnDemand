using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

    }
}