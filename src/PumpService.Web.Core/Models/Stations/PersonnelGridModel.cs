using PumpService.Core.Defaults;
using System.ComponentModel;

namespace PumpService.Web.Core.Models.Stations
{
    public partial class PersonnelGridModel : BaseGridModel
    {
        #region Properties

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Stations_PersonnelGridModel_PersonnelIdNumber_DisplayName)]
        public string PersonnelIdNumber { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Stations_PersonnelGridModel_Name_DisplayName)]
        public string Name { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Stations_PersonnelGridModel_PositionType_Description_DisplayName)]
        public string PositionType_Description { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Stations_PersonnelGridModel_DiscountRate_DisplayName)]
        public decimal? DiscountRate { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Stations_PersonnelGridModel_CardId_DisplayName)]
        public string CardId { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Stations_PersonnelGridModel_NationalIdNumber_DisplayName)]
        public string NationalIdNumber { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Stations_PersonnelGridModel_IsActive_DisplayName)]
        public bool IsActive { get; set; }

        #endregion Properties
    }
}