using System;

using Microsoft.AspNetCore.Mvc;

namespace TeamJob.Services.Profile.Controllers
{
    public class BaseController : ControllerBase
    {
        protected bool IsAdmin
            => User.IsInRole("admin");

        protected string UserId
            => string.IsNullOrWhiteSpace(User?.Identity?.Name) ?
                string.Empty :
                User.Identity.Name;
    }
}
