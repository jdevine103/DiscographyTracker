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
            var service = new CrateService(userId);
            var model = service.GetCrate();
            return View(model);
        }
        public ActionResult AddToCrate(int id)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var svc = new CrateService(userId);

            if (svc.AddToCrate(id))
            {
                TempData["SaveResult"] = $" was added to your crate.";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["SaveResult"] = $" is already in your crate.";
                return RedirectToAction("Index");
            }
        }
        public ActionResult CrateAlbums(int id)
        {
            var albumService = CreateCrateService();
            var model = albumService.GetUserAlbums(id);

            return View(model);
        }        
        public ActionResult CrateSongs(int id)
        {
            var songService = CreateUserSongService();
            var model = songService.GetUserSongs(id);

            return View(model);
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
        private CrateService CreateCrateService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new CrateService(userId);
            return service;
        }
    }
}