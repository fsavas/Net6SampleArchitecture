namespace PumpService.Core.Domain.Pumps
{
    public partial class PumpSalesSearch : BaseSearch
    {
        #region Properties

        public DateTime? TransactionStartTime { get; set; }//pompa satışın başladığı zaman
        public DateTime? TransactionEndTime { get; set; }//pompa satışın tamamlandığı zaman

        #endregion Properties
    }
}