using Microsoft.AspNetCore.SignalR;

namespace PumpService.Services.Hubs
{
    public class FillingPointHub : Hub
    {
        #region Fields

        private decimal? _Amount { get; set; }

        #endregion Fields

        #region Methods

        public void GetAmount(string fillingPointNumber)
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