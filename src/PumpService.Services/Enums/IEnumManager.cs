using PumpService.Core;

namespace PumpService.Services.Enums
{
    public interface IEnumManager
    {
        List<SelectListItem> GetEnums<T>();

        string GetDescription(Enum value);

        string GetDescription(object value);
    }
}