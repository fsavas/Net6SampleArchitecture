using PumpService.Core.Domain.Localizations;

namespace PumpService.Core.Repository.Localizations
{
    public partial interface ILanguageRepository : IBaseRepository<Language>
    {
        #region Methods

        IPagedList<Language> SearchLanguages(LanguageSearch languageSearch);

        List<Language> GetAllLanguages();

        IList<Language> SearchAllLanguages(LanguageSearch languageSearch);

        #endregion Methods
    }
}