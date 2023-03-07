using Microsoft.Extensions.DependencyInjection;
using PumpService.Services.Tanks;
using Serilog;

namespace PumpService.Services.Channel.Tanks
{
    public class TankChannel : ITankChannel
    {
        #region Fields

        private ChannelData _channelData;
        private readonly ITankService _tankService;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        #endregion Fields

        #region Constructor

        public TankChannel(ChannelData channelData, ITankService tankService, IServiceScopeFactory serviceScopeFactory)
        {
            _channelData = channelData;
            _tankService = tankService;
            _serviceScopeFactory = serviceScopeFactory;
        }

        #endregion Constructor

        #region Methods

        public void StartTanks()
        {
            try
            {
                var dbTanks = _tankService.GetAllTanks();

                if (dbTanks != null && dbTanks.Count > 0)
                {
                    _channelData.AddTanks(dbTanks);

                    foreach (var tank in _channelData.Tanks)
                    {
                        var tankContainer = new TankContainer(tank);
                        var probe = tankContainer.InitializeTank();
                        ThreadPool.QueueUserWorkItem(tankContainer.RunTankThread, new object[] { probe, _serviceScopeFactory });
                    }
                }
            }
            catch (Exception e)
            {
                Log.Logger.Error("Message=" + e.Message + " StackTrace=" + e.StackTrace);
            }
        }

        #endregion Methods
    }
}