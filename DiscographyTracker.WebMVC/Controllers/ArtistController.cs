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
            var service = new ArtistService();
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
        public ActionResult AddAlbum(int id)
        {
            AlbumCreate model = new AlbumCreate();
            model.ArtistID = id;
            var svc = CreateAlbumService();
            Album newAlbum = svc.CreateAlbum(model);
            //this does not pass through to POST
            id = newAlbum.AlbumID;
            
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAlbum(int id, AlbumCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.ArtistID != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            AlbumEdit editModel = model.ToAlbumEdit();
            
            //assign editModel.AlbumID to most recet entry?? seems improper 
            
            var service = CreateAlbumService();
            if (service.UpdateAlbum(editModel))
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult AddToCrate(int id)
        {
            var svc = CreateArtistService();
            var model = svc.GetArtistById(id);

            var newModel = model.ToUserArtistCreate();

            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new UserArtistService(userId);

            if (service.CreateUserArtist(newModel))
            {
                TempData["SaveResult"] = $" {model.ArtistName} was added to your crate.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["SaveResult"] = $" {model.ArtistName} is already in your crate.";
                return RedirectToAction("Index");
            }
        }
        public ActionResult Details(int id)
        {
            var service = new ArtistService();
            var model = service.GetArtistById(id);

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
        private AlbumService CreateAlbumService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new AlbumService(userId);
            return service;
        }
        private UserArtistService CreateUserArtistService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new UserArtistService(userId);
            return service;
        }
    }
}