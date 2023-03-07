using PumpService.Core.Domain.Lookups;

namespace PumpService.Core.Domain.Stations
{
    public partial class Personnel : BaseDomainEntity
    {
        public string PersonnelIdNumber { get; set; }
        public string Name { get; set; }
        public virtual LookupTable PositionType { get; set; }
        public decimal? DiscountRate { get; set; }
        public string CardId { get; set; }
        public string NationalIdNumber { get; set; }
        public bool IsActive { get; set; }
        //public bool IsDeleted { get; set; }
        public List<SelectListItem> PositionTypes { get; set; }
        public long PositionTypeId { get; set; }
    }
}