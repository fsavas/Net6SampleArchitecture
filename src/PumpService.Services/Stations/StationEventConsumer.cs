using Microsoft.Extensions.Caching.Memory;
using PumpService.Core.Domain.Stations;
using PumpService.Core.Events;
using PumpService.Services.Events;
using Serilog;

namespace PumpService.Services.Stations
{
    public partial class StationEventConsumer :

        IConsumer<EntityInsertedEvent<Station>>,
        IConsumer<EntityUpdatedEvent<Station>>,
        IConsumer<EntityDeletedEvent<Station>>
    {
        #region Fields

        private readonly IMemoryCache _memoryCache;
        private readonly MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions().SetPriority(CacheItemPriority.Normal);

        #endregion Fields

        #region Constructor

        public StationEventConsumer(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        #endregion Constructor

        #region Methods

        public void HandleEvent(EntityInsertedEvent<Station> eventMessage)
        {
            Log.Logger.Information("Entity Inserted");
        }

        public void HandleEvent(EntityDeletedEvent<Station> eventMessage)
        {
        }

        public void HandleEvent(EntityUpdatedEvent<Station> eventMessage)
        {
            Log.Logger.Information("Entity Updated");
        }

        #endregion Methods
    }
}