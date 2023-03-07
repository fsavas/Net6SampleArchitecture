using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PumpService.Core.Defaults;
using PumpService.Web.Mvc.Filters;

namespace PumpService.Web.Controllers
{
    [Route("api/[controller]")]
    [ServiceFilter(typeof(MemoryCacheFilter))]
    //[Authorize]
    public abstract class BaseController : ControllerBase
    {
        protected string successMessage = MemoryCacheKeys.ControllerActionSuccess;
    }
}