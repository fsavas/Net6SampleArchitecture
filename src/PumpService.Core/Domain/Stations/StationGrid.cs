using PumpService.Core.Defaults;
using System.ComponentModel;

namespace PumpService.Core.Domain.Stations
{
    public partial class StationGrid : BaseGrid
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Stations_StationGridModel_Name_DisplayName)]
        public string Name { get; set; }

        #endregion Properties
    }
}