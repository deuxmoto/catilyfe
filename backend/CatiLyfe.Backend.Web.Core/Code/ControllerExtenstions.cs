namespace CatiLyfe.Backend.Web.Core.Code
{
    using System.Security.Claims;

    using CatiLyfe.DataLayer.Models;

    using Microsoft.AspNetCore.Mvc;

    internal static class ControllerExtenstions
    {
        /// <summary>
        /// Gets the current user id.
        /// </summary>
        /// <param name="self">The controller.</param>
        /// <returns>The user id.</returns>
        public static int GetUserId(this Controller self)
        {
            return int.Parse(self.User.FindFirstValue(ClaimTypes.Sid));
        }

        /// <summary>
        /// Gets the current user token.
        /// </summary>
        /// <param name="self">The controller.</param>
        /// <returns>The user token.</returns>
        public static string GetUserToken(this Controller self)
        {
            return self.User.FindFirstValue(ClaimTypes.Hash);
        }

        /// <summary>
        /// Creates user access details for the signed in user.
        /// </summary>
        /// <param name="self">The controller.</param>
        /// <returns>The user access details.</returns>
        public static UserAccessDetails GetUserAccessDetails(this Controller self)
        {
            return new UserAccessDetails(self.GetUserId(), self.GetUserToken());
        }
    }
}
