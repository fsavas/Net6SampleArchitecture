using PumpService.Core.Defaults;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PumpService.Web.Core.Models.Stations
{
    public partial class StationModel : BaseEntityModel
    {
        #region Properties

        //[DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = MemoryCacheKeys.PumpService_Web_Core_Models_Stations_StationModel_Name_Required)]
        [DisplayName(MemoryCacheKeys.PumpService_Web_Core_Models_Stations_StationModel_Name_DisplayName)]
        public string Name { get; set; }

        #endregion Properties

        //Example method for custom validation
        //public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    //var entity = (Entity)validationContext.ObjectInstance;
        //    if (Id < 0)
        //    {
        //        yield return new ValidationResult(
        //            $"{Id} " + MemoryCacheKeys.ModelIdNegative,
        //            new[] { nameof(Id) });
        //    }
        //}
    }
}