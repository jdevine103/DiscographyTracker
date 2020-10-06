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

            // Get the note
            var detail = service.GetUserAlbumById(userAlbumId);

            // Create the NoteEdit model instance with the new star state
            var updatedUserAlbum =
                new UserAlbumEdit
                {
                    UserAlbumID = detail.UserAlbumID,
                    IsFavorited = newState
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
    }
}
