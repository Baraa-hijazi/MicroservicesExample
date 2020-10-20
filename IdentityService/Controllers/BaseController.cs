using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class BaseController : Controller
    {
        [NonAction]
        protected string GetCurrentUserName()
        {
            return User.Claims.First(i => i.Type == "Username").Value;
        }
    }
}