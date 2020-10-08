using DiscographyTracker.Data;
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
        public ActionResult AddToCrate(int id)
        {
            var svc = CreateArtistService();
            var model = svc.GetArtistById(id);

            var newModel = model.ToUserArtistCreate();

            var artistService = CreateUserArtistService();
            var albumService = CreateUserAlbumService();
            var songService = CreateUserSongService();

            bool artistAdded = artistService.CreateUserArtist(newModel);
            bool albumsAdded = albumService.CreateUserAlbums(id);
            bool songsAdded = songService.CreateUserSongs(id);

            if (artistAdded && albumsAdded && songsAdded)
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
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateUserArtistService();
            var model = svc.GetUserArtistById(id);

            return View(model);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var svc = CreateUserArtistService();

            svc.DeleteUserArtist(id);

            TempData["SaveResult"] = "The artist was deleted";

            return RedirectToAction("Index");
        }
        public ActionResult CrateAlbums(int id)
        {
            var albumService = CreateUserAlbumService();
            var model = albumService.GetUserAlbums(id);

            return View(model);
        }

        private UserArtistService CreateUserArtistService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new UserArtistService(userId);
            return service;
        }
        private UserAlbumService CreateUserAlbumService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new UserAlbumService(userId);
            return service;
        }
        private UserSongService CreateUserSongService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new UserSongService(userId);
            return service;
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
    }
}