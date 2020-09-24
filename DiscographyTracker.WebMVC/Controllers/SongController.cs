using DiscographyTracker.Models;
using Microsoft.AspNet.Identity;
using DiscographyTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiscographyTracker.WebMVC.Controllers
{
    public class SongController : Controller
    {
        // GET: Song
        public ActionResult Index()
        {
            var model = new SongListItem[0];
            return View(model);
        }
        
        public ActionResult Create(SongCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new SongService(userId);

            service.CreateSong(model);

            return RedirectToAction("Index");
        }
    }
}