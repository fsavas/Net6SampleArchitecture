using PumpService.Core;
using PumpService.Core.Domain.Localizations;

namespace PumpService.Services.Localizations
{
    public partial interface ILanguageService : IBaseService
    {
        void DeleteLanguage(long languageId);

        List<Language> GetAllLanguages();

        Language GetLanguageById(long languageId);

        void InsertLanguage(Language language);

        void UpdateLanguage(Language language);

        IPagedList<Language> SearchLanguages(LanguageSearch languageSearch);

        bool SelectLanguage(long languageId);

        string ExportLanguages(LanguageSearch languageSearch);
    }
}