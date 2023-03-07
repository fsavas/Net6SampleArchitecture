using PumpService.Core.Domain.Products;

namespace PumpService.Core.Domain.Pumps
{
    public partial class Nozzle : BaseDomainEntity
    {
        public int Address { get; set; }
        public virtual FillingPoint FillingPoint { get; set; }
        public long FillingPointId { get; set; }
        public virtual Product Product { get; set; }
        public long ProductId { get; set; }
        public bool IsActive { get; set; }
        //public bool IsDeleted { get; set; }
    }
}