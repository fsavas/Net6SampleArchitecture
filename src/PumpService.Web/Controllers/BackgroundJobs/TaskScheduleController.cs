using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using PumpService.Core;
using PumpService.Core.Defaults;
using PumpService.Core.Domain.BackgroundJobs;
using PumpService.Services.BackgroundJobs;
using PumpService.Web.Core.Models;
using PumpService.Web.Core.Models.BackgroundJobs;

namespace PumpService.Web.Controllers.BackgroundJobs
{
    public class TaskScheduleController : BaseController
    {
        #region Fields

        private readonly ITaskScheduleService _TaskScheduleService;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        #endregion Fields

        #region Constructor

        public TaskScheduleController(ITaskScheduleService TaskScheduleService, IMapper mapper, IMemoryCache memoryCache)
        {
            _TaskScheduleService = TaskScheduleService;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        #endregion Constructor

        #region Base Methods

        // POST: api/TaskSchedule
        [HttpPost("Search")]
        public ServiceResult PostSearch([FromBody] TaskScheduleSearchModel value)
        {
            try
            {
                var TaskScheduleSearch = _mapper.Map<TaskScheduleSearch>(value);
                var TaskSchedulePagedList = (PagedList<TaskSchedule>)_TaskScheduleService.SearchTaskSchedules(TaskScheduleSearch);
                var data = _mapper.Map<PagedList<TaskSchedule>, PagedList<TaskScheduleGridModel>>(TaskSchedulePagedList);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // POST: api/TaskSchedule
        [HttpPost("Export")]
        public ServiceResult PostExport([FromBody] TaskScheduleSearchModel value)
        {
            try
            {
                var TaskScheduleSearch = _mapper.Map<TaskScheduleSearch>(value);
                var data = _TaskScheduleService.ExportTaskSchedules(TaskScheduleSearch);

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
                var data = InitializeTaskSchedule();

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        //GET: api/TaskSchedule/5
        [HttpGet("{id}")]
        public ServiceResult Get(long id)
        {
            try
            {
                var TaskSchedule = _TaskScheduleService.GetTaskScheduleById(id);
                var data = _mapper.Map<TaskScheduleModel>(TaskSchedule);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = data };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // POST: api/TaskSchedule
        [HttpPost]
        public ServiceResult Post([FromBody] TaskScheduleModel value)
        {
            try
            {
                var TaskSchedule = _mapper.Map<TaskSchedule>(value);

                if (TaskSchedule.Id > 0)
                    _TaskScheduleService.UpdateTaskSchedule(TaskSchedule);
                else
                    _TaskScheduleService.InsertTaskSchedule(TaskSchedule);

                if (_memoryCache.TryGetValue(MemoryCacheKeys.ControllerActionSuccess, out string message))
                    successMessage = message;

                return new ServiceResult { Success = true, Message = successMessage, Data = null };
            }
            catch (Exception e)
            {
                return new ServiceResult { Success = false, Message = e.Message, Data = null };
            }
        }

        // DELETE: api/TaskSchedule/5
        [HttpDelete("{id}")]
        public ServiceResult Delete(long id)
        {
            try
            {
                _TaskScheduleService.DeleteTaskSchedule(id);

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

        private TaskScheduleModel InitializeTaskSchedule()
        {
            return new TaskScheduleModel();
        }

        #endregion Methods
    }
}