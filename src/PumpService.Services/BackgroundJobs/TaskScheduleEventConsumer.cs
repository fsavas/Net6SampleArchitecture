using Microsoft.Extensions.Caching.Memory;
using PumpService.Core.Domain.BackgroundJobs;
using PumpService.Core.Events;
using PumpService.Services.Events;
using Serilog;

namespace PumpService.Services.BackgroundJobs
{
    public partial class TaskScheduleEventConsumer :

        IConsumer<EntityInsertedEvent<TaskSchedule>>,
        IConsumer<EntityUpdatedEvent<TaskSchedule>>,
        IConsumer<EntityDeletedEvent<TaskSchedule>>
    {
        #region Fields

        private readonly IMemoryCache _memoryCache;
        private readonly MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.Normal);

        #endregion Fields

        #region Constructor

        public TaskScheduleEventConsumer(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        #endregion Constructor

        #region Methods

        public void HandleEvent(EntityInsertedEvent<TaskSchedule> eventMessage)
        {
            Log.Logger.Information("Entity Inserted");
        }

        public void HandleEvent(EntityDeletedEvent<TaskSchedule> eventMessage)
        {
        }

        public void HandleEvent(EntityUpdatedEvent<TaskSchedule> eventMessage)
        {
            Log.Logger.Information("Entity Updated");
        }

        #endregion Methods
    }
}