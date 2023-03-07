using PumpService.Web.Core.Mvc.ModelValidation;
using System.ComponentModel.DataAnnotations;

namespace PumpService.Web.Core.Models
{
    public partial class BaseEntityModel : IValidatableObject
    {
        [IdValidationAttribute]
        public virtual long Id { get; set; }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield return ValidationResult.Success;
        }
    }
}