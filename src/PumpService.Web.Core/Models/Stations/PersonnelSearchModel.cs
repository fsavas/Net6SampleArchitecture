using PumpService.Core.Defaults;
using System.ComponentModel;

namespace PumpService.Web.Core.Models.Stations
{
    public partial class PersonnelSearchModel : BaseSearchModel
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Stations_PersonnelSearchModel_Name_DisplayName)]
        public string Name { get; set; }

        #endregion Properties
    }
}