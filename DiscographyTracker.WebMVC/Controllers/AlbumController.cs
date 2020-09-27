using DiscographyTracker.Data;
using DiscographyTracker.Models;
using DiscographyTracker.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiscographyTracker.WebMVC.Controllers
{
    public class AlbumController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Album
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new AlbumService(userId);
            var model = service.GetAlbums();

            return View(model);
        }
        public ActionResult Create(AlbumCreate model)
        {
            if (!ModelState.IsValid)
            {
                //ViewBag
                ViewBag.Artists = new SelectList(_db.Artists.ToList(), "ArtistID", "ArtistName");
                return View(model);
            }
            

            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new AlbumService(userId);

            service.CreateAlbum(model);

            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            //ViewBag
            ViewBag.Artists = new SelectList(_db.Artists.ToList(), "ArtistID", "ArtistName");
            var service = CreateAlbumService();
            var detail = service.GetAlbumById(id);
            var model =
                new AlbumEdit
                {
                    AlbumID = detail.AlbumID,
                    ArtistID = detail.ArtistID,
                    AlbumTitle = detail.AlbumTitle,
                    ReleaseDate = detail.ReleaseDate
                };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AlbumEdit model)
        {
            if (!ModelState.IsValid)
            {
                //ViewBag
                ViewBag.Artists = new SelectList(_db.Artists.ToList(), "ArtistID", "ArtistName");
                return View(model);
            }

            if (model.AlbumID != id)
            {
                //ViewBag
                ViewBag.Artists = new SelectList(_db.Artists.ToList(), "ArtistID", "ArtistName");
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateAlbumService();

            if (service.UpdateAlbum(model))
            {
                TempData["SaveResult"] = $" {model.AlbumTitle} was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", $" {model.AlbumTitle} could not be updated.");
            return View(model);
        }
        private AlbumService CreateAlbumService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new AlbumService(userId);
            return service;
        }
    }
}
