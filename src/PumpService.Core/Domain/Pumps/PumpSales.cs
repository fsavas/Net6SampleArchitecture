using PumpService.Core.Domain.Products;

namespace PumpService.Core.Domain.Pumps
{
    public partial class PumpSales : BaseDomainEntity
    {
        public virtual decimal Amount { get; set; }//parasal tutar
        public virtual decimal PumpQuantity { get; set; }//pompada gösterilen satılan litre
        public virtual decimal NetQuantity { get; set; }//satışın yapıldığı sıradaki sıcaklığa göre düzenlenmiş satılan litre
        public virtual decimal UnitPrice { get; set; }
        public virtual DateTime? TransactionStartTime { get; set; }//pompa satışın başladığı zaman
        public virtual DateTime? TransactionEndTime { get; set; }//pompa satışın tamamlandığı zaman
        public virtual FillingPoint FillingPoint { get; set; }
        public long FillingPointId { get; set; }
        public virtual Nozzle Nozzle { get; set; }
        public long NozzleId { get; set; }
        public virtual Product Product { get; set; }
        public long ProductId { get; set; }
    }
}