using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VideoOnDemand.Data;

namespace VideoOnDemand.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        protected VideoOnDemandContext context = new VideoOnDemandContext();

        protected override void Dispose(bool disposing)
        {
            context.Dispose();

            base.Dispose(disposing);
        }
    }
}