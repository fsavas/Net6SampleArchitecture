using PumpService.Core.Domain.Lookups;
using PumpService.Core.Domain.Products;
using PumpService.Core.Domain.Stations;

namespace PumpService.Core.Domain.Tanks
{
    public partial class Tank : BaseDomainEntity
    {
        public string Code { get; set; }
        public int ProbeAddress { get; set; }
        public virtual SerialPortDefinition SerialPortDefinition { get; set; }
        public long SerialPortDefinitionId { get; set; }
        public decimal Capacity { get; set; }
        public int Diameter { get; set; }
        public virtual Product Product { get; set; }
        public long ProductId { get; set; }
        public virtual LookupTable ProbeType { get; set; }
        public int? MeasurementPeriod { get; set; }//zorunlu alan;kaç saniyede bir ölçüm yapılacağı bilgisi
        public int? ProbeLength { get; set; } //zorunlu alan; probe_uzunlugu - mm olarak belirtilecek - (tankda takılı olan probe'un uzunluğu, tank çapından daha büyük olmalıdır;bunu kontrol edelim,
        public string TankGroupNo { get; set; }//tank eğer gruplanmış tanklardan biri ise, burada grup numarası tutulacaktır, aynı gruptaki tankların grup numarası aynı olacaktır
        public bool GruptakiAktifTank { get; set; } //todo adını değiştir tank eğer bir grup içinde yer alıyorsa, tankın aktif tank olup olmadığını gösterir; true:aktif tank false:pasif tank; gruptaki tanklar sırayla aktif ve pasif hale getirilmektedir
        public decimal? LowFuelAlarm { get; set; }//zorunlu alan;mm olarak belirtilecek - yakıt seviyesi bu değerin altına düşünce yakıt bitiyor alarmı üretilecek;
        public decimal? HeightLimitBetweenTwoTankStatus { get; set; } //zorunlu alan;iki peşpeşe tank ölçümü arasındaki yükseklik farkı bu değeri aşarsa tank ölçümü kaydedilmektedir;mm değeridir; 0'dan büyük 1'den küçük bir değer olmalıdır.
        public bool IsDetectAutoFilling { get; set; }//zorunlu alan;otomatik_dolum_algılama - A:Aktif P:Pasif;  Tanka dolum yapılmaya başlandığının otomatik olarak algılanıp algılanmayacağı bilgisi;
        public decimal? WaterOffset { get; set; }//mm olarak belirtilir - eğer bu alana değer girilirse probe'dan gelen su seviyesine bu değer eklenir(veya çıkartılır) ve bulunan değer üzerinden hareket edilir;
        public decimal? FuelOffset { get; set; }//mm olarak belirtilir - eğer bu alana değer girilirse probe'dan gelen yakıt seviyesine bu değer eklenir(veya çıkartılır)
        public string ProbeSerialNumber { get; set; }//tankın içindeki asis probe'un seri numarası 999-999-999-999 şeklinde 4 parça nümerik olarak girilecek,bunu kontrol edelim )
        public bool ProbeSerialNumberApplied { get; set; }//asis probe'larda seri numaranın kullanılarak probe'un adresinin atanıp atanmadığı bilgisi true:atandı false:atanmadı
        public short? ProbeAddressAsis { get; set; } //probe asis ise zorunlu alan; asis probe'lar için kullanılacak adrestir; eğer probe tipi asis değilse bu alanın seçilmemesi gerekiyor);
        public long ProbeTypeId { get; set; }
        public bool IsActive { get; set; }
        //public bool IsDeleted { get; set; }

        #region Not Persist

        public TankStatus LastPersistedPeriyodikTankOlcum { get; set; }//todo adını değiştir persist edilmeyecektir,tankla ilgili yapılan ölçümlerden database'e kaydedilen en son periyodik ölçümü tutar
        public DateTime? NotPersistStartTime { get; set; }//todo adını değiştir persist edilmeyecektir, tank ölçüm sonucunun hangi zaman itibariyle database'e yazılmadığını gösterir
        public decimal? EnSonOlculenTankYakitSeviyesi { get; set; }//todo adını değiştir persist edilmeyecektir;tankla ilgili ölçülen en son tank yakıt seviyesi,
        public MaxStack<TankStatus> SessionTankOlcumler { get; set; }//todo adını değiştir persist edilmeyecektir, session bünyesinde tank ölçümlerini tutmak için kullanılır
        public int TankSeviyesiKacOlcumdurDegismiyor { get; set; }//todo adını değiştir persist edilmeyecektir;autofilling ile ilgili kullanılır,tank seviyesinin kaç peşpeşe ölçümde  değişmediği tutulacaktır
        public int NotPersistIntervalMinute { get; set; }//zorunlu alan;tankda birbirini takip eden ölçümlerde aynı seviye okunduğu zaman, kaç dakika süreyle DB'ye kayıt atılmayacağını gösterir
        public int TankSeviyesiKacOlcumdurArtiyor { get; set; }//todo adını değiştir persist edilmeyecektir;autofilling ile ilgili kullanılır,tank seviyesinin kaç peşpeşe ölçümde  arttığı tutulacaktır
        public int TankSeviyesiKacOlcumdurAzaliyor { get; set; }//todo adını değiştir persist edilmeyecektir;autofilling ile ilgili kullanılır,tank seviyesinin kaç peşpeşe ölçümde  azaldigi tutulacaktır
        public int AutoFillingNotRisingConstant { get; set; } //otomatik dolum algılanacağı belirtilmişse(AutoFillingSensor=true)zorunlu alan; autofilling ile ilgili kontrol sırasında  tank seviyesinin kaç peşpeşe ölçümde yükselip/yükselmediğini kontrol etmek için kullanılan parametre;eğer otomatik dolum algılanması seçilmişse girilmesi gerekir; 10 uygun bir değerdir
        public bool AutoFillingStarted { get; set; }//persist edilmeyecektir; autofilling başlatıldığı bilgisi; true:başlatıldı
        public object Probe { get; set; } //persist edilmeyecektir;

        #endregion Not Persist

        #region Methods

        public virtual decimal HeightLimitForTwoTankStatus()
        {
            decimal ikiOlcumArasindakiYukseklikFarki = 0.1m;

            if (HeightLimitBetweenTwoTankStatus != null
                && HeightLimitBetweenTwoTankStatus > 0
                && HeightLimitBetweenTwoTankStatus < 1)
            {
                return (decimal)HeightLimitBetweenTwoTankStatus;
            }
            else
                return ikiOlcumArasindakiYukseklikFarki;
        }

        #endregion Methods
    }
}