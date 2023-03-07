using Microsoft.AspNetCore.SignalR;

namespace PumpService.Services.Hubs
{
    public class TankHub : Hub
    {
        #region Fields

        private decimal? _Amount { get; set; }

        #endregion Fields

        #region Methods

        public void GetAmount(string tankNumber)
        {
            Clients.All.SendAsync("GetAmount", _Amount);
        }

        public void SetAmount(decimal? amount)
        {
            _Amount = amount;
        }

        #endregion Methods
    }
}