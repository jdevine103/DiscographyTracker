using DiscographyTracker.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiscographyTracker.WebMVC.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new UserService(userId);
            var model = service.GetCrate();

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
    }
}