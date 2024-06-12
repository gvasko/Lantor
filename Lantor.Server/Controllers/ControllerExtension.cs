using Lantor.Server.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Lantor.Server.Controllers
{
    public static class ControllerExtension
    {
        public static bool HasRole(this ControllerBase controller, string roleName)
        {
            List<string> roleClaims = controller.HttpContext.User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList();
            return roleClaims.Any(r => r == roleName);
        }
    }
}
