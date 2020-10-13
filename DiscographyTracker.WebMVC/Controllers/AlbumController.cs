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
            var service = new AlbumService();
            var model = service.GetAlbums();

            return View(model);
        }
        //GET: Album/Create/?
        public ActionResult Create(int? id)
        {
            if (id.HasValue)
                PopulateArtists(id.Value);
            else
                PopulateArtists();

            return View();
        }

        private void PopulateArtists()
        {
            ViewBag.ArtistID = new SelectList(new ArtistService(Guid.Parse(User.Identity.GetUserId())).GetArtists(), "ArtistID", "ArtistName");
        }
        private void PopulateArtists(int id)
        {
            ViewBag.ArtistID = new SelectList(new ArtistService(Guid.Parse(User.Identity.GetUserId())).GetArtists(), "ArtistID", "ArtistName", id);
        }
        //POST: Album/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AlbumCreate model)
        {
            if (!ModelState.IsValid)
            {
                PopulateArtists();
                return View(model);
            }
            

            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new AlbumService(userId);

            service.CreateAlbum(model);

            return RedirectToAction("Details", "Artist", new { id = model.ArtistID });
        }        
        public ActionResult Details(int id)
        {
            var service = new AlbumService();
            var model = service.GetAlbumById(id);

            return View(model);
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
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateAlbumService();
            var model = svc.GetAlbumById(id);

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var svc = CreateAlbumService();

            svc.DeleteAlbum(id);

            TempData["SaveResult"] = "The album was deleted";

            return RedirectToAction("Index");
        }
        private AlbumService CreateAlbumService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new AlbumService(userId);
            return service;
        }
    }
}
