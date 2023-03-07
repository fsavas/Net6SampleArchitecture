using PumpService.Core.Domain.Lookups;
using PumpService.Core.Domain.Pumps;

namespace PumpService.Core.Domain.Tanks
{
    public partial class TankStatus : BaseDomainEntity
    {
        public decimal FuelLevelVolume { get; set; }
        public decimal FuelLevelLength { get; set; }
        public decimal WaterLevelVolume { get; set; }
        public decimal WaterLevelLength { get; set; }
        public decimal? Temperature { get; set; }
        public DateTime StatusInfoDate { get; set; }
        public virtual LookupTable OlcumSebebi { get; set; }//todo adını değiştir
        public long OlcumSebebiId { get; set; }
        public decimal? SatisMiktari { get; set; } //todo adını değiştir eğer ölçüm pompa satışı sırasında yapılmışsa,ölçümün yapıldığı ana kadar yapılmış olan satış miktari(Quantity)
        public decimal FuelLevel_LT_Kalibrasyon { get; set; }//todo adını değiştir fuellevel_ltNet ile aynı amaçlı görünüyor, bir ara silinebilir #sümer#todo#
        public decimal? FuelLevel_LTNet { get; set; } //todo adını değiştir
        public virtual PumpSales PompaSatis { get; set; }//todo adını değiştir eğer ölçüm bir pompa satışı sonrasında yapılmışsa,satışı gösterir
        public long PompaSatisId { get; set; }
        public virtual TankFilling TankDolum { get; set; } //todo adını değiştir eğer ölçüm bir tank filling sonrasında yapılmışsa,dolumu gösterir
        public long TankDolumId { get; set; }
        public virtual Tank Tank { get; set; }
        public long TankId { get; set; }
        public bool IsActive { get; set; }
        //public bool IsDeleted { get; set; }

        #region Not Persist

        public int? TankSeviyesiKacOlcumdurDegismiyor { get; set; }//todo adını değiştir persist edilmeyecektir
        public int? TankSeviyesiKacOlcumdurAzaliyor { get; set; }//todo adını değiştir persist edilmeyecektir
        public int? TankSeviyesiKacOlcumdurArtiyor { get; set; }//todo adını değiştir persist edilmeyecektir
        public int EnFazlaTekrarEdenOlcum { get; set; } //todo adını değiştir persist edilmeyecektir
        public int FuelLevelMMIntegerDifferenceWithPreviousTankStatus { get; set; } //todo adını değiştir persist edilmeyecektir
        public int FuelLevelMMInteger { get; set; } //todo adını değiştir persist edilmeyecektir

        #endregion Not Persist
    }
}