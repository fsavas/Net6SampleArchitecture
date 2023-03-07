using PumpService.Core.Defaults;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PumpService.Web.Core.Models.Stations
{
    public partial class PersonnelModel : BaseEntityModel
    {
        #region Properties

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Stations_PersonnelModel_PersonnelIdNumber_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Stations_PersonnelModel_PersonnelIdNumber_DisplayName)]
        public string PersonnelIdNumber { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Stations_PersonnelModel_Name_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Stations_PersonnelModel_Name_DisplayName)]
        public string Name { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Stations_PersonnelModel_PositionTypeId_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Stations_PersonnelModel_PositionTypeId_DisplayName)]
        public long? PositionTypeId { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Stations_PersonnelModel_DiscountRate_DisplayName)]
        public decimal? DiscountRate { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Stations_PersonnelModel_CardId_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Stations_PersonnelModel_CardId_DisplayName)]
        public string CardId { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Stations_PersonnelModel_NationalIdNumber_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Stations_PersonnelModel_NationalIdNumber_DisplayName)]
        public string NationalIdNumber { get; set; }

        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Stations_PersonnelModel_IsActive_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Stations_PersonnelModel_IsActive_DisplayName)]
        public bool IsActive { get; set; }

        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Stations_PersonnelModel_PositionTypes_DisplayName)]
        public List<SelectListItemModel> PositionTypes { get; set; }

        #endregion Properties
    }
}