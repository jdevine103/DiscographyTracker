using DiscographyTracker.Models;
using Microsoft.AspNet.Identity;
using DiscographyTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DiscographyTracker.Data;

namespace DiscographyTracker.WebMVC.Controllers
{
    public class SongController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Song
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new SongService(userId);
            var model = service.GetSongs();

            return View(model);
        }
        
        public ActionResult Create(SongCreate model)
        {
            if (!ModelState.IsValid)
            {
                //ViewBag
                ViewBag.Albums = new SelectList(_db.Albums.ToList(), "AlbumID", "AlbumTitle");
                return View(model);
            }

            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new SongService(userId);

            service.CreateSong(model);

            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            //ViewBag
            ViewBag.Albums = new SelectList(_db.Albums.ToList(), "AlbumID", "AlbumTitle");
            var service = CreateSongService();
            var detail = service.GetSongById(id);
            var model =
                new SongEdit
                {
                    SongID = detail.SongID,
                    AlbumID = detail.AlbumID,
                    SongName = detail.SongName,
                    HaveListened = detail.HaveListened
                };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SongEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.SongID != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateSongService();

            if (service.UpdateSong(model))
            {
                TempData["SaveResult"] = $" {model.SongName} was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", $" {model.SongName} could not be updated.");
            return View(model);
        }
        private SongService CreateSongService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new SongService(userId);
            return service;
        }
    }
}
