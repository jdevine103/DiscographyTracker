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
            var service = new SongService();
            var model = service.GetSongs();

            return View(model);
        }
        //GET: Song/Create/?
        public ActionResult Create(int? id)
        {
            if (id.HasValue)
                PopulateAlbums(id.Value);
            else
                PopulateAlbums();

            return View();
        }
        private void PopulateAlbums()
        {
            ViewBag.AlbumID = new SelectList(new AlbumService(Guid.Parse(User.Identity.GetUserId())).GetAlbums(), "AlbumID", "AlbumTitle");
        }
        private void PopulateAlbums(int id)
        {
            ViewBag.AlbumID = new SelectList(new AlbumService(Guid.Parse(User.Identity.GetUserId())).GetAlbums(), "AlbumID", "AlbumTitle", id);
        }
        //POST: Song/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SongCreate model)
        {
            if (!ModelState.IsValid)
            {
                //ViewBag
                //ViewBag.Albums = new SelectList(_db.Albums.ToList(), "AlbumID", "AlbumTitle");
                PopulateAlbums();
                return View(model);
            }

            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new SongService(userId);

            service.CreateSong(model);

            return RedirectToAction("Details", "Album", new { id = model.AlbumID });



            //var userId = Guid.Parse(User.Identity.GetUserId());
            //var service = new SongService(userId);

            //service.CreateSong(model);

            //return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            var service = new SongService();
            var model = service.GetSongById(id);

            return View(model);
        }
        public ActionResult Edit(int id)
        {
            //ViewBag
            ViewBag.Albums = new SelectList(_db.Albums.ToList(), "AlbumID", "AlbumID");
            var service = CreateSongService();
            var detail = service.GetSongById(id);
            var model =
                new SongEdit
                {
                    SongID = detail.SongID,
                    AlbumID = detail.AlbumID,
                    SongName = detail.SongName
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
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateSongService();
            var model = svc.GetSongById(id);

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var svc = CreateSongService();

            svc.DeleteSong(id);

            TempData["SaveResult"] = "The song was deleted";

            return RedirectToAction("Index");
        }
        private SongService CreateSongService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new SongService(userId);
            return service;
        }
    }
}
