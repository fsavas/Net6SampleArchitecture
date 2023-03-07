using PumpService.Core;
using PumpService.Core.Domain.Localizations;

namespace PumpService.Services.Localizations
{
    public partial interface ILocaleResourceService : IBaseService
    {
        void DeleteLocaleResource(long localeResourceId);

        List<LocaleResource> GetAllLocaleResources();

        LocaleResource GetLocaleResourceById(long localeResourceId);

        void InsertLocaleResource(LocaleResource localeResource);

        void UpdateLocaleResource(LocaleResource localeResource);

        IPagedList<LocaleResource> SearchLocaleResources(LocaleResourceSearch localeResourceSearch);

        string ExportLocaleResources(LocaleResourceSearch localeResourceSearch);
    }
}