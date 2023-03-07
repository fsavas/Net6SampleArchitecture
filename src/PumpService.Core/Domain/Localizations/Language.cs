namespace PumpService.Core.Domain.Localizations
{
    public partial class Language : BaseDomainEntity
    {
        public string Name { get; set; }
        public string Culture { get; set; }
        //public bool IsDeleted { get; set; }
    }
}