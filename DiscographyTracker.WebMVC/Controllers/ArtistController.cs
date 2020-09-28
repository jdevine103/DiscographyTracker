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
    public class ArtistController : Controller
    {
        // GET: Artist
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ArtistService(userId);
            var model = service.GetArtists();

            return View(model);
        }
        public ActionResult Create(ArtistCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ArtistService(userId);

            service.CreateArtist(model);

            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            var svc = CreateArtistService();
            var model = svc.GetArtistById(id);

            return View(model);
        }
        public ActionResult CreateWithAlbum(ArtistAlbumCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ArtistService(userId);
            var albumService = new AlbumService(userId);

            Artist newArtist = service.CreateArtist(model.ToArtistCreate(model));
            albumService.CreateAlbum(model.ToAlbumCreate(model, newArtist.ArtistID));

            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            var service = CreateArtistService();
            var detail = service.GetArtistById(id);
            var model =
                new ArtistEdit
                {
                    ArtistID = detail.ArtistID,
                    ArtistName = detail.ArtistName
                };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ArtistEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.ArtistID != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateArtistService();

            if (service.UpdateArtist(model))
            {
                TempData["SaveResult"] = $" {model.ArtistName} was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", $" {model.ArtistName} could not be updated.");
            return View(model);
        }
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateArtistService();
            var model = svc.GetArtistById(id);

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var svc = CreateArtistService();

            svc.DeleteArtist(id);

            TempData["SaveResult"] = "The artist was deleted";

            return RedirectToAction("Index");
        }
        private ArtistService CreateArtistService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ArtistService(userId);
            return service;
        }
    }
}