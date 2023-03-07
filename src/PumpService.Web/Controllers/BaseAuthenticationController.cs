using Microsoft.AspNetCore.Mvc;
using PumpService.Core.Defaults;

namespace PumpService.Web.Controllers
{
    [Route("api/[controller]")]
    public abstract class BaseAuthenticationController : ControllerBase
    {
        protected string successMessage = MemoryCacheKeys.ControllerActionSuccess;
    }
}