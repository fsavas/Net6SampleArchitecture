using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PumpService.Core;
using PumpService.Core.Defaults;
using PumpService.Core.Domain.Stations;
using PumpService.Services.Lookups;
using PumpService.Services.Stations;
using PumpService.Web.Core.Models;
using PumpService.Web.Core.Models.Stations;
using Serilog;

namespace PumpService.Web.Controllers.Stations
{
    public class PersonnelController : BaseController
    {
        #region Fields

        private readonly IPersonnelService _personnelService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly ILookupTableService _lookupTableService;

        #endregion Fields

        #region Constructor

        public PersonnelController(IPersonnelService personnelService, IMapper mapper, IMemoryCache memoryCache, ILookupTableService lookupTableService)
        {
            _personnelService = personnelService;
            _mapper = mapper;
            _memoryCache = memoryCache;
            _lookupTableService = lookupTableService;
        }

        #endregion Constructor

        #region Base Methods

        // POST: api/Personnel
        [HttpPost("Search")]
        public ServiceResult PostSearch([FromBody] PersonnelSearchModel value)
        {
            try
            {
                var personnelSearch = _mapper.Map<PersonnelSearch>(value);
                var personnelPagedList = (PagedList<Personnel>)_personnelService.SearchPersonnels(personnelSearch);
                var data = _mapper.Map<PagedList<Personnel>, PagedList<PersonnelGridModel>>(personnelPagedList);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // POST: api/Personnel
        [HttpPost("Export")]
        public ServiceResult PostExport([FromBody] PersonnelSearchModel value)
        {
            try
            {
                var personnelSearch = _mapper.Map<PersonnelSearch>(value);
                var data = _personnelService.ExportPersonnels(personnelSearch);

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
                var data = InitializePersonnel();

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        //GET: api/Personnel/5
        [HttpGet("{id}")]
        public ServiceResult Get(long id)
        {
            try
            {
                var personnel = _personnelService.GetPersonnelById(id);
                personnel.PositionTypes = GetPositionTypes();
                var data = _mapper.Map<PersonnelModel>(personnel);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // POST: api/Personnel
        [HttpPost]
        public ServiceResult Post([FromBody] PersonnelModel value)
        {
            try
            {
                Log.ForContext("User", "Fatih").Information("Update Personnel Controller Started");

                var personnel = _mapper.Map<Personnel>(value);

                if (personnel.Id > 0)
                    _personnelService.UpdatePersonnel(personnel);
                else
                    _personnelService.InsertPersonnel(personnel);

                Log.ForContext("User", "Fatih").Information("Update Personnel Controller Ended");

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = null };
            }
            catch (Exception e)
            {
                Log.ForContext("User", "Fatih").Information("Update Personnel Exception");
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // DELETE: api/Personnel/5
        [HttpDelete("{id}")]
        public ServiceResult Delete(long id)
        {
            try
            {
                _personnelService.DeletePersonnel(id);

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

        private PersonnelModel InitializePersonnel() => new PersonnelModel()
        {
            PositionTypes = _mapper.Map<List<SelectListItem>, List<SelectListItemModel>>(GetPositionTypes())
        };

        private List<SelectListItem> GetPositionTypes() => _lookupTableService.GetByType(EnumClasses.LookupTypes.PersonnelPositionTypes);

        #endregion Methods
    }
}