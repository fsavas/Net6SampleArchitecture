using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PumpService.Core;
using PumpService.Core.Defaults;
using PumpService.Core.Domain.Security;
using PumpService.Services.Roles;
using PumpService.Web.Core.Models;
using PumpService.Web.Core.Models.Security;

namespace PumpService.Web.Controllers.Roles
{
    public class RoleController : BaseController
    {
        #region Fields

        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        #endregion Fields

        #region Constructor

        public RoleController(IRoleService roleService, IMapper mapper, IMemoryCache memoryCache)
        {
            _roleService = roleService;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        #endregion Constructor

        #region Base Methods

        // POST: api/Role
        [HttpPost("Search")]
        public ServiceResult PostSearch([FromBody] RoleSearchModel value)
        {
            try
            {
                var roleSearch = _mapper.Map<RoleSearch>(value);
                var rolePagedList = (PagedList<Role>)_roleService.SearchRoles(roleSearch);
                var data = _mapper.Map<PagedList<Role>, PagedList<RoleGridModel>>(rolePagedList);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // POST: api/Role
        [HttpPost("Export")]
        public ServiceResult PostExport([FromBody] RoleSearchModel value)
        {
            try
            {
                var roleSearch = _mapper.Map<RoleSearch>(value);
                var data = _roleService.ExportRoles(roleSearch);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // POST: api/Language
        [HttpPost("New")]
        public ServiceResult PostNew()
        {
            try
            {
                var data = InitializeRole();

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        //GET: api/Role/5
        [HttpGet("{id}")]
        public ServiceResult Get(long id)
        {
            try
            {
                var role = _roleService.GetRoleById(id);
                var data = _mapper.Map<RoleModel>(role);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // POST: api/Role
        [HttpPost]
        public ServiceResult Post([FromBody] RoleModel value)
        {
            try
            {
                var role = _mapper.Map<Role>(value);

                if (role.Id > 0)
                    _roleService.UpdateRole(role);
                else
                    _roleService.InsertRole(role);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = null };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // DELETE: api/Role/5
        [HttpDelete("{id}")]
        public ServiceResult Delete(long id)
        {
            try
            {
                _roleService.DeleteRole(id);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = null };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        #endregion Base Methods

        #region Methods

        private RoleModel InitializeRole()
        {
            return new RoleModel();
        }

        #endregion Methods
    }
}