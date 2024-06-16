using Lantor.DomainModel;
using Lantor.Server.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lantor.Server.Controllers
{
    public static class ControllerExtensions
    {
        public static bool HasRole(this ControllerBase controller, string roleName)
        {
            List<string> roleClaims = controller.HttpContext.User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();
            return roleClaims.Any(r => r == roleName);
        }

        public static User GetUserData(this ControllerBase controller)
        {
            string? name = controller.HttpContext.User.FindAll("name").Select(r => r.Value).ToList().FirstOrDefault();
            string? userName = controller.HttpContext.User.FindAll("preferred_username").Select(r => r.Value).ToList().FirstOrDefault();
            string? email = controller.HttpContext.User.FindAll("email").Select(r => r.Value).ToList().FirstOrDefault();
            string? externalId = controller.HttpContext.User.FindAll("http://schemas.microsoft.com/identity/claims/objectidentifier").Select(r => r.Value).ToList().FirstOrDefault();
            return new User(name ?? "", userName ?? "", email ?? "", externalId ?? "");
        }
    }
}
