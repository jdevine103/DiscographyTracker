using DiscographyTracker.Models;
using DiscographyTracker.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DiscographyTracker.WebMVC.Controllers.WebAPI
{
    [Authorize]
    [RoutePrefix("api/UserAlbum")]
    public class UserAlbumController : ApiController
    {
        private bool SetStarState(int userAlbumId, bool newState)
        {
            // Create the service
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new UserAlbumService(userId);

            // Get the UserAlbum
            var detail = service.GetUserAlbumById(userAlbumId);

            // Create the UserAlbumEdit model instance with the new star state
            var updatedUserAlbum =
                new UserAlbumEdit
                {
                    AlbumID = detail.AlbumID,
                    UserID = detail.UserID,
                    UserAlbumID = detail.UserAlbumID,
                    HaveListened = detail.HaveListened,
                    IsFavorited = newState
                };

            // Return a value indicating whether the update succeeded
            return service.UpdateUserAlbum(updatedUserAlbum);
        }        
        private bool SetListenState(int userAlbumId, bool newState)
        {
            // Create the service
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new UserAlbumService(userId);

            // Get the UserAlbum
            var detail = service.GetUserAlbumById(userAlbumId);

            // Create the UserAlbumEdit model instance with the new Listen state
            var updatedUserAlbum =
                new UserAlbumEdit
                {
                    AlbumID = detail.AlbumID,
                    UserID = detail.UserID,
                    UserAlbumID = detail.UserAlbumID,
                    IsFavorited = detail.IsFavorited,
                    HaveListened = newState
                };

            // Return a value indicating whether the update succeeded
            return service.UpdateUserAlbum(updatedUserAlbum);
        }

        [Route("{id}/Star")]
        [HttpPut]
        public bool ToggleStarOn(int id) => SetStarState(id, true);

        [Route("{id}/Star")]
        [HttpDelete]
        public bool ToggleStarOff(int id) => SetStarState(id, false);        
        [Route("{id}/Listen")]
        [HttpPut]
        public bool ToggleListenOn(int id) => SetListenState(id, true);

        [Route("{id}/Listen")]
        [HttpDelete]
        public bool ToggleListenOff(int id) => SetListenState(id, false);
    }
}
