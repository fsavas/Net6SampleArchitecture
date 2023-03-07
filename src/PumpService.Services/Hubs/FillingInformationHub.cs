using Microsoft.AspNetCore.SignalR;
using PumpService.Core.Defaults;
using PumpService.Core.Results;
using PumpService.Services.Channel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumpService.Services.Hubs
{
    public class FillingInformationHub : Hub
    {
        #region Fields

        private readonly ChannelData _channelData;

        #endregion Fields

        #region Constructor

        public FillingInformationHub(ChannelData channelData)
        {
            _channelData = channelData;
        }

        #endregion Constructor

        #region Methods

        public void GetFillingInformationWithTimer(int abuAddress, int cpuId)
        {
            var timer = new System.Timers.Timer(1000);
            timer.Elapsed += (sender, eventArgs) => LoadFillingInformationAsync(abuAddress, cpuId);
            timer.Start();            
        }

        private async Task LoadFillingInformationAsync(int abuAddress, int cpuId)
        {
            if (_channelData.FillingInformations != null && _channelData.FillingInformations.ContainsKey(string.Join(ChannelKeys.KeySeperator, abuAddress.ToString(), cpuId.ToString())))
            {
                var data = _channelData.FillingInformations[string.Join(ChannelKeys.KeySeperator, abuAddress.ToString(), cpuId.ToString())];

                await Clients.All.SendAsync("ListenFillingInformation", data);
            }
        }

        public void GetFillingInformation(int abuAddress, int cpuId)
        {
            if (_channelData.FillingInformations != null)
            {
                var data = _channelData.FillingInformations[string.Join(ChannelKeys.KeySeperator, abuAddress.ToString(), cpuId.ToString())];

                Clients.All.SendAsync("ListenFillingInformation", data);
            }
        }

        #endregion Methods
    }
}
