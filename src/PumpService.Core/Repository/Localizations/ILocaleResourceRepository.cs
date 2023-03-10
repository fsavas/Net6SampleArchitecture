using PumpService.Core.Domain.Localizations;

namespace PumpService.Core.Repository.Localizations
{
    public partial interface ILocaleResourceRepository : IBaseRepository<LocaleResource>
    {
        #region Methods

        IPagedList<LocaleResource> SearchLocaleResources(LocaleResourceSearch localeResourceSearch);

        IList<LocaleResource> SearchAllLocaleResources(LocaleResourceSearch localeResourceSearch);

        List<LocaleResource> GetAllLocaleResources();

        List<LocaleResource> GetByLanguage(long languageId);

        List<LocaleResource> GetByNamePrefixLanguage(string name, long languageId);

        #endregion Methods
    }
}