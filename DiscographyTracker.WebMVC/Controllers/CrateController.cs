using DiscographyTracker.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiscographyTracker.WebMVC.Controllers
{
    public class CrateController : Controller
    {
        // GET: Crate
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new UserArtistService(userId);
            var model = service.GetCrate();
            return View(model);
        }
    }
}