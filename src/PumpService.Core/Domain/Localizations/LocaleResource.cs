namespace PumpService.Core.Domain.Localizations
{
    public partial class LocaleResource : BaseDomainEntity
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public virtual Language Language { get; set; }
        public long LanguageId { get; set; }
    }
}