using Microsoft.Extensions.Caching.Memory;
using PumpService.Core;
using PumpService.Core.Defaults;
using PumpService.Core.Domain.Lookups;
using PumpService.Core.Domain.Pumps;
using PumpService.Core.Domain.Tanks;
using PumpService.Core.Repository.Lookups;
using PumpService.Core.Repository.Tanks;
using PumpService.Services.Channel.Utility;
using PumpService.Services.Tanks;
using Serilog;

namespace PumpService.Services.Channel
{
    public class TankAndPumpOps
    {
        #region Fields

        //todo constructor dene
        private ILookupTableRepository _lookupTableRepository = (ILookupTableRepository)ServiceContainer.Scope.ServiceProvider.GetService(typeof(ILookupTableRepository));

        private IMemoryCache _memoryCache = (IMemoryCache)ServiceContainer.Scope.ServiceProvider.GetService(typeof(IMemoryCache));
        private ChannelData _channelData = (ChannelData)ServiceContainer.Scope.ServiceProvider.GetService(typeof(ChannelData));
        private ITankStatusRepository _tankStatusRepository = (ITankStatusRepository)ServiceContainer.Scope.ServiceProvider.GetService(typeof(ITankStatusRepository));
        private IUnitOfWork _unitOfWork = (IUnitOfWork)ServiceContainer.Scope.ServiceProvider.GetService(typeof(IUnitOfWork));
        private ITankService _tankService = (ITankService)ServiceContainer.Scope.ServiceProvider.GetService(typeof(ITankService));

        #endregion Fields

        //Tank_Service tankServis = CurrentContainer.Container.Build<Tank_Service>();
        //Pump_Service pompaServis = CurrentContainer.Container.Build<Pump_Service>();
        //CommonTaskServices taskService = CurrentContainer.Container.Build<CommonTaskServices>();

        //TarpetSession currentSession = CurrentContainer.Container.Build<TarpetSession>();
        //ILog log = LogManager.GetLogger(typeof(TankAndPumpOps));

        //public void ProcessNozzleFilling(DateTime pEventTime, Nozzle pTabanca, double pAmount, double pVolume, double pUnitPrice, PumpSales pPompaSatis)
        //{
        //    FillingPoint DolumNoktasi = pTabanca.FillingPoint;

        //    double myunitPriceOfProduct = 1;
        //    double myVolume = 0;

        //    if (pVolume > 0)
        //        myVolume = pVolume;
        //    if (pUnitPrice > 0)
        //        myunitPriceOfProduct = pUnitPrice;

        //    #region Kalibrasyon ve Tank seviyesinin düşüklüğü kontrolü

        //    int kontrolVolume = (int)pVolume;

        //    int kalibrasyonAdim = 50;

        //    if (pTabanca.Tank.OtmCalibrationLitreAdimi != null && pTabanca.Tank.OtmCalibrationLitreAdimi >= 50)//eğer tank tanımında kalibrasyon adımı belirtilmişse ve bu değer 50'den büyükse bu kullanılır
        //    {
        //        kalibrasyonAdim = (int)pTabanca.Tank.OtmCalibrationLitreAdimi;
        //    }

        //    if ((kontrolVolume - DolumNoktasi.AnlikVerilenLitre) >= kalibrasyonAdim)//Kalibrasyon adımı tanımında belirtilen litrede bir kalibrasyon için gereken tank ölçümü gerçekleştirilir.
        //    {
        //        ReadTankStatusFromSession(pEventTime: DateTime.Now, pTank: pTabanca.Tank, pOlcumSebebi: EnumClasses.TankStatusOlcumSebebi.KalibrasyonIcin, pPumpSale: pPompaSatis, pTankDolum: null, pSatisMiktari: Convert.ToDecimal(pVolume));//tabancanın tankının statusünün okunması sağlanır

        //        DolumNoktasi.AnlikVerilenLitre = kontrolVolume;
        //    }
        //    else
        //    {
        //        ReadTankStatusFromSession(pEventTime: DateTime.Now, pTank: pTabanca.Tank, pOlcumSebebi: EnumClasses.TankStatusOlcumSebebi.SatisSirasinda, pPumpSale: pPompaSatis, pTankDolum: null, pSatisMiktari: Convert.ToDecimal(pVolume));//tabancanın tankının statusünün okunması sağlanır
        //    }

        //    #endregion

        //    #region Ekran Güncelleme
        //    DomainEventPublisher.Raise<UpdatePumpScreenEvent>(new UpdatePumpScreenEvent() { EventTime = DateTime.Now, DolumNoktasi = pTabanca.FillingPoint, Amount = pAmount, Volume = myVolume, UnitPrice = myunitPriceOfProduct, PompaStatus = EnumClasses.PumpStatus.NozzleFilling });
        //    #endregion

        //}

        //public void ProcessNozzleFillingCompleted(DateTime pEventTime, Nozzle pTabanca, double pAmount, double pVolume, double pUnitPrice, PumpSales pPompaSatis, decimal pPreSalesElkTotalizer, decimal pPostSalesElkTotalizer)
        //{
        //    #region pompa görselleri güncellenir
        //    DomainEventPublisher.Raise<UpdatePumpScreenEvent>(new UpdatePumpScreenEvent() { EventTime = DateTime.Now, DolumNoktasi = pTabanca.FillingPoint, Amount = pAmount, Volume = pVolume, UnitPrice = pUnitPrice, PompaStatus = EnumClasses.PumpStatus.NozzleFillingCompleted });
        //    #endregion

        //    #region Eğer bu satışla ilgili tüm state'ler işlenmediyse, gelen pumpsale parametresi null görüneceği için işlem yapılır
        //    PumpSales pompasatis = null;

        //    if (pPompaSatis != null)
        //    {
        //        pompasatis = pPompaSatis;
        //    }
        //    else
        //    {
        //        taskService.SaveTarpetTaskLogs("TankAndPumpOps", pAmount.ToString(), (int)TarpetLogCodes.DcrBugOccurred);
        //        log.Warn("pPompaSatis is null");
        //        return; //eğer pumpsales bilgisi null geldiyse, bir işlem yapmıyoruz, arada teosis unpaid konumda geliyor nedense, bu bir bug gibi görünüyor
        //    }
        //    #endregion

        //    TarPet.Data.Domain.Pump mypompa = pTabanca.Pump;

        //    FillingPoint dolumNoktasi = pTabanca.FillingPoint;

        //    Nozzle tabancaDBden = pompaServis.GetNozzleById((long)pTabanca.Id);

        //    #region satışla ilgili hesaplanmış totalizer işlemi için tabancanın hesaplanmış totalizer bilgileri kullanılır
        //    decimal preSalesTotalizer = 0;

        //    if (tabancaDBden.NozzleTotalizerValue != null)
        //        preSalesTotalizer = Convert.ToDecimal(tabancaDBden.NozzleTotalizerValue);
        //    else
        //    {
        //        if (tabancaDBden.NozzleTotalizerStartValue != null)
        //            preSalesTotalizer = Convert.ToDecimal(tabancaDBden.NozzleTotalizerStartValue);
        //        else
        //            preSalesTotalizer = 0;
        //    }
        //    #endregion

        //    pompasatis.Amount = Convert.ToDecimal(pAmount);
        //    pompasatis.Quantity = Convert.ToDecimal(pVolume);
        //    pompasatis.UnitPrice = Convert.ToDecimal(pUnitPrice);
        //    pompasatis.CreateDate = DateTime.Now;

        //    if (pompasatis.PumpTrnxTime == null)
        //        pompasatis.PumpTrnxTime = DateTime.Now;

        //    pompasatis.Nozzle = tabancaDBden;
        //    pompasatis.Product = tabancaDBden.Product;

        //    pompasatis.Pump = mypompa;
        //    pompasatis.Status = EnumClasses.AcikKapali.Kapali;
        //    pompasatis.SendToDistributor = false;
        //    pompasatis.PumpTrnxEndTime = DateTime.Now;

        //    #region satışla ilgili elektronik totalizer bilgileri güncellenir
        //    if (pompasatis.NozzleTotalizerPrevElk == null)
        //        pompasatis.NozzleTotalizerPrevElk = pPreSalesElkTotalizer;

        //    if (pompasatis.NozzleTotalizerAfterElk == null)
        //        pompasatis.NozzleTotalizerAfterElk = pPostSalesElkTotalizer;
        //    #endregion

        //    #region satışla ilgili hesaplanmış totalizer'ler güncellenir
        //    pompasatis.NozzleTotalizerPrev = preSalesTotalizer;
        //    pompasatis.NozzleTotalizerAfter = preSalesTotalizer + Convert.ToDecimal(pVolume);
        //    #endregion

        //    #region eğer satış normal olarak kapatılmamışsa(sistem tarafından kapatılmışsa) ve quantity 0 görünüyorsa, elektronik totalizer üzerinden quantity ve amount hesaplanmasına çalışılır
        //    if (pompasatis.StatusClosedBySystem != null && (bool)pompasatis.StatusClosedBySystem && pompasatis.Quantity == 0 && pompasatis.NozzleTotalizerAfterElk != null && pompasatis.NozzleTotalizerPrevElk != null && pompasatis.NozzleTotalizerAfterElk > pompasatis.NozzleTotalizerPrevElk && pompasatis.NozzleTotalizerAfterElk >= 0 && pompasatis.NozzleTotalizerPrevElk >= 0)
        //    {
        //        pompasatis.Quantity = (decimal)(pompasatis.NozzleTotalizerAfterElk - pompasatis.NozzleTotalizerPrevElk);
        //        pompasatis.Amount = pompasatis.Quantity * pompasatis.UnitPrice;
        //    }
        //    #endregion

        //    #region PumpSale bilgisi persist edilir
        //    String saveResult = null;

        //    if (pPompaSatis == null || pPompaSatis.Id == null)
        //        saveResult = pompaServis.SavePumpSale(pompasatis);
        //    else
        //        saveResult = pompaServis.UpdatePumpSales(pompasatis);
        //    #endregion

        //    #region tabanca totalizer tarihçesi için kayıt oluşturulur
        //    tabancaDBden.NozzleTotalizerValue = pompasatis.NozzleTotalizerAfter;

        //    tabancaDBden.NozzleTotalizerElkValue = pPostSalesElkTotalizer;

        //    pompaServis.UpdateNozzle(tabancaDBden);//tabanca totalizer bilgileri güncellenir
        //    #endregion

        //    #region tabancanın tankının statusünün okunması sağlanır
        //    ReadTankStatusFromSession(pEventTime: DateTime.Now, pTank: tabancaDBden.Tank, pOlcumSebebi: EnumClasses.TankStatusOlcumSebebi.SatisSonrasi, pPumpSale: pPompaSatis, pTankDolum: null, pSatisMiktari: 0);
        //    #endregion

        //    #region pumpview'daki plaka alanı ve diğer alanların sıfırlanması sağlanır
        //    if (pPompaSatis != null)
        //        SatisTamamlandi(pPompaSatis:pPompaSatis);
        //    #endregion

        //}

        ////periyodik tank ölçümleri dışında satışla ilgili yada dolumla ilgili tank ölçümleri, periyodik yapılan ve TarpetSession bünyesinde saklanan ölçümlerin en sonuncusu kullanılarak belirlenir, bunlar için ayrı bir tank ölçümü gerçekleştirilmez
        //public void ReadTankStatusFromSession(DateTime pEventTime, TarPet.Data.Domain.Tank pTank, EnumClasses.TankStatusOlcumSebebi pOlcumSebebi, PumpSales pPumpSale, TankFilling pTankDolum, decimal pSatisMiktari)
        //{
        //    List<Tank> gruplanmisTanklar = new List<Tank>();

        //    //eğer gönderilen tank gruplanmış bir tank ise, gruptaki diğer tanklarında aynı parametrelerle okunmasını sağlayalım,çünkü gruptaki hangi tankın aktif olduğundan emin olamayız eğer güncel tanımlama yapılmadıysa
        //    if (!String.IsNullOrWhiteSpace(pTank.TankGroupNo))
        //        gruplanmisTanklar = tankServis.ListTanksInGroup(pTank.TankGroupNo);
        //    else //eğer tank gruplanmış bir tank değilse, sadece kendisinin okunması sağlanır
        //        gruplanmisTanklar.Add(pTank);

        //    foreach (var gruplanmisTank in gruplanmisTanklar)
        //    {
        //        TankStatus enSonEklenenOlcum = ListeyeEnSonEklenenTankOlcumu(gruplanmisTank);

        //        if (enSonEklenenOlcum != null)
        //        {
        //            ProcessTankStatus(
        //                pEventTime: DateTime.Now,
        //                pFuelVolumeLt: enSonEklenenOlcum.FuelLevel_LT,
        //                pFuelHeightMm: enSonEklenenOlcum.FuelLevel_MM,
        //                pWaterVolumeLt: enSonEklenenOlcum.WaterLevel_LT,
        //                pWaterHeightMm: enSonEklenenOlcum.WaterLevel_MM,
        //                pTemperature: (decimal)enSonEklenenOlcum.Temperature,
        //                pTank: enSonEklenenOlcum.Tank,
        //                pOlcumSebebi: pOlcumSebebi,
        //                pSatisMiktari: pSatisMiktari,
        //                pPumpSale: pPumpSale,
        //                pTankDolum: pTankDolum
        //            );
        //        }
        //    }

        //}

        //for TankStatusOkunduEvent

        #region Methods

        public void ProcessTankStatus(DateTime pEventTime,
            decimal pFuelVolumeLt,
            decimal pFuelHeightMm,
            decimal pWaterVolumeLt,
            decimal pWaterHeightMm,
            decimal pTemperature,
            Tank pTank,
            LookupTable pOlcumSebebi,
            decimal pSatisMiktari,
            PumpSales pPumpSale,
            TankFilling pTankDolum
            )
        {
            if (pPumpSale != null)
            {
                Log.Logger.ForContext("LogKey", LogKeys.TankMeasurementReason).Information("Tank=" + pTank.Code + " TankMeasurementReason=" + pOlcumSebebi.Name + " FuelLevel_MM=" + pFuelHeightMm + " PumpSaleTime=" + pPumpSale.TransactionStartTime);
                //log.Warn("OlcumSebebi=" + pOlcumSebebi + "FuelLevel_MM=" + pFuelHeightMm + "PumpSaleTime="+pPumpSale.PumpTrnxTime);
            }
            else if (pTankDolum != null)
            {
                Log.Logger.ForContext("LogKey", LogKeys.TankMeasurementReason).Information("Tank=" + pTank.Code + " TankMeasurementReason=" + pOlcumSebebi.Name + " FuelLevel_MM=" + pFuelHeightMm + " TankFillingTime=" + pTankDolum.FillingStartTime);
                //log.Warn("OlcumSebebi=" + pOlcumSebebi + "FuelLevel_MM=" + pFuelHeightMm + "TankFillingTime=" + pTankDolum.FillingStartTime);
            }
            else
            {
                Log.Logger.ForContext("LogKey", LogKeys.TankMeasurementReason).Information("Tank=" + pTank.Code + " TankMeasurementReason=" + pOlcumSebebi.Name + " FuelLevel_MM=" + pFuelHeightMm);
                //log.Warn("OlcumSebebi=" + pOlcumSebebi + "FuelLevel_MM=" + pFuelHeightMm );
            }

            #region Tankstatus bilgileri oluşturulur

            TankStatus tankOlcumu = new TankStatus();

            #region sıcaklık ile ilgili kontrol

            tankOlcumu.Temperature = Convert.ToDecimal((double)pTemperature);

            //todo 0 setleme doğru mu
            if (tankOlcumu.Temperature != null && (tankOlcumu.Temperature <= -90 || tankOlcumu.Temperature >= 60))
            {
                Log.Logger.ForContext("LogKey", LogKeys.WrongTankMeasurementTemperature).Warning("Tank=" + pTank.Code + " Temperature=" + tankOlcumu.Temperature.ToString());
                //taskService.SaveTarpetTaskLogs("TankAndPumpOps", tankOlcumu.Temperature.ToString(), (int)TarpetLogCodes.TankStatusTemperatureError);
                tankOlcumu.Temperature = 0;
            }

            #endregion sıcaklık ile ilgili kontrol

            tankOlcumu.StatusInfoDate = pEventTime;

            tankOlcumu.Tank = pTank;

            tankOlcumu.OlcumSebebi = pOlcumSebebi;
            tankOlcumu.SatisMiktari = pSatisMiktari;
            tankOlcumu.PompaSatis = pPumpSale;
            tankOlcumu.TankDolum = pTankDolum;

            if (!_memoryCache.TryGetValue(MemoryCacheKeys.EnumClasses_LookupTypes_MeasurementReasons_Periodic, out LookupTable periodicTankMeasurementReason))
                periodicTankMeasurementReason = _lookupTableRepository.GetByTypeName(EnumClasses.LookupTypes.TankMeasurementReasons, nameof(EnumClasses.TankMeasurementReasons.Periodic));

            if (!_memoryCache.TryGetValue(MemoryCacheKeys.EnumClasses_LookupTypes_MeasurementReasons_DuringPumpSales, out LookupTable duringPumpSalesTankMeasurementReason))
                duringPumpSalesTankMeasurementReason = _lookupTableRepository.GetByTypeName(EnumClasses.LookupTypes.TankMeasurementReasons, nameof(EnumClasses.TankMeasurementReasons.DuringPumpSales));

            if (pOlcumSebebi == periodicTankMeasurementReason)
            {
                #region yükseklikle ilgili hesaplamalar yapılır

                tankOlcumu.FuelLevelLength = Convert.ToDecimal(_tankService.FindProcessedHeightOfTankStatus(pTank, (double)pFuelHeightMm, EnumClasses.LiquidType.Fuel));
                tankOlcumu.WaterLevelLength = Convert.ToDecimal(_tankService.FindProcessedHeightOfTankStatus(pTank, (double)pWaterHeightMm, EnumClasses.LiquidType.Water));

                #endregion yükseklikle ilgili hesaplamalar yapılır

                #region Probe'larda(özellikle Teosis) 6,7 ölçümde bir yakıt seviyesi 0 ölçülmektedir, bunu operasyonda sorun yaratmaması için ignore edilmesi sağlanır

                bool hataVar = false;

                if (tankOlcumu.FuelLevelLength <= 0 || tankOlcumu.FuelLevelLength > pTank.ProbeLength)
                {
                    Log.Logger.ForContext("LogKey", LogKeys.WrongTankMeasurementFuelLevel).Warning("Tank=" + pTank.Code + "FuelHeightMm=" + tankOlcumu.FuelLevelLength.ToString());
                    //taskService.SaveTarpetTaskLogs("ProcessTankStatus", "FuelHeightMm=" + tankOlcumu.FuelLevel_MM , (int)TarpetLogCodes.TankStatusFuelLevelError);
                    hataVar = true;
                }
                if (tankOlcumu.WaterLevelLength <= 0 || tankOlcumu.WaterLevelLength > pTank.ProbeLength)
                {
                    Log.Logger.ForContext("LogKey", LogKeys.WrongTankMeasurementWaterLevel).Warning("Tank=" + pTank.Code + "WaterHeightMm=" + tankOlcumu.WaterLevelLength.ToString());
                    //taskService.SaveTarpetTaskLogs("ProcessTankStatus", "WaterHeightMm=" + tankOlcumu.WaterLevel_MM , (int)TarpetLogCodes.TankStatusWaterLevelError);
                    hataVar = true;
                }
                if (hataVar)
                    return;

                #endregion Probe'larda(özellikle Teosis) 6,7 ölçümde bir yakıt seviyesi 0 ölçülmektedir, bunu operasyonda sorun yaratmaması için ignore edilmesi sağlanır

                #region ölçüm milimetresi kullanılarak hacim belirlenir

                tankOlcumu.FuelLevelVolume = (decimal)_tankService.FindVolumesOfLiquid(tankOlcumu.Tank, (double)tankOlcumu.FuelLevelLength, EnumClasses.LiquidType.Fuel);
                tankOlcumu.WaterLevelVolume = (decimal)_tankService.FindVolumesOfLiquid(tankOlcumu.Tank, (double)tankOlcumu.WaterLevelLength, EnumClasses.LiquidType.Water);

                #endregion ölçüm milimetresi kullanılarak hacim belirlenir
            }
            else
            {
                #region periyodik olmayan ölçümler, daha önce yapılmış olan periyodik ölçümler kullanılarak oluşturulduğu için, yukarıdaki hesaplamalar tekrar gerçekleştirilmez

                tankOlcumu.FuelLevelLength = pFuelHeightMm;
                tankOlcumu.WaterLevelLength = pWaterHeightMm;
                tankOlcumu.FuelLevelVolume = pFuelVolumeLt;
                tankOlcumu.WaterLevelVolume = pWaterVolumeLt;

                #endregion periyodik olmayan ölçümler, daha önce yapılmış olan periyodik ölçümler kullanılarak oluşturulduğu için, yukarıdaki hesaplamalar tekrar gerçekleştirilmez
            }

            #region kesafet oranı uygulanarak yakıtın net hacmi belirlenir

            tankOlcumu.FuelLevel_LTNet = tankOlcumu.FuelLevelVolume * (decimal)KesafetDizel.KesafetOraniBul(Convert.ToDouble(tankOlcumu.Temperature));
            tankOlcumu.FuelLevel_LT_Kalibrasyon = (decimal)tankOlcumu.FuelLevel_LTNet;

            #endregion kesafet oranı uygulanarak yakıtın net hacmi belirlenir

            #endregion Tankstatus bilgileri oluşturulur

            #region tank status bilgilerinin ekranda güncellenmesi için event üretilir

            if (tankOlcumu.OlcumSebebi == periodicTankMeasurementReason)
            {
                //todo signalr
                //DomainEventPublisher.Raise<UpdateTankScreenEvent>(new UpdateTankScreenEvent() { EventTime = DateTime.Now, TankOlcumu = tankOlcumu, StatusOfTank = EnumClasses.StatusOfTank.TankConnectionOpened });
            }

            #endregion tank status bilgilerinin ekranda güncellenmesi için event üretilir

            #region ölçüm bir satış sırasında yapılmışsa ve yakıt seviyesi tank tanımında belirtilen seviyenin altına düşmüşse, yakıt verme işlemi kesilir

            if (pPumpSale != null)
            {
                if (!string.IsNullOrEmpty(pTank.TankGroupNo) && pTank.GruptakiAktifTank && pTank.LowFuelAlarm != null && tankOlcumu.FuelLevelLength <= pTank.LowFuelAlarm)//eğer tank gruplanmış tanklardan birisi ise, sadece aktif tankın seviyesi kontrol edilir
                {
                    //todo signalr
                    //DomainEventPublisher.Raise<StopTabancaOfPumpSaleEvent>(new StopTabancaOfPumpSaleEvent() { EventTime = DateTime.Now, FuelHeightMm = tankOlcumu.FuelLevel_MM, pompaSatis = pPumpSale });
                }
                else if (string.IsNullOrEmpty(pTank.TankGroupNo) && pTank.LowFuelAlarm != null && tankOlcumu.FuelLevelLength <= pTank.LowFuelAlarm)//tank gruplanmış bir tank değilse, normal seviyesi kontrol edilir
                {
                    //todo signalr
                    //DomainEventPublisher.Raise<StopTabancaOfPumpSaleEvent>(new StopTabancaOfPumpSaleEvent() { EventTime = DateTime.Now, FuelHeightMm = tankOlcumu.FuelLevel_MM, pompaSatis = pPumpSale });
                }
            }

            #endregion ölçüm bir satış sırasında yapılmışsa ve yakıt seviyesi tank tanımında belirtilen seviyenin altına düşmüşse, yakıt verme işlemi kesilir

            #region periyodik olmayan ve satış sırasında yapılmayan ölçümler DB'e yazılır,periyodik ölçümlerse işlenmek üzere Session'a eklenir

            if (tankOlcumu.OlcumSebebi != periodicTankMeasurementReason && tankOlcumu.OlcumSebebi != duringPumpSalesTankMeasurementReason)
            {
                PersistTankStatus(pTankOlcumu: tankOlcumu);
            }
            else if (tankOlcumu.OlcumSebebi == periodicTankMeasurementReason)
            {
                AddTankStatusToPeriyodikTankOlcumler(pTankOlcum: tankOlcumu);
            }

            #endregion periyodik olmayan ve satış sırasında yapılmayan ölçümler DB'e yazılır,periyodik ölçümlerse işlenmek üzere Session'a eklenir
        }

        public void PersistTankStatus(TankStatus pTankOlcumu)
        {
            try
            {
                _tankStatusRepository.InsertOrUpdate(pTankOlcumu);

                if (!_memoryCache.TryGetValue(MemoryCacheKeys.EnumClasses_LookupTypes_MeasurementReasons_Periodic, out LookupTable periodicTankMeasurementReason))
                    periodicTankMeasurementReason = _lookupTableRepository.GetByTypeName(EnumClasses.LookupTypes.TankMeasurementReasons, nameof(EnumClasses.TankMeasurementReasons.Periodic));

                if (pTankOlcumu.OlcumSebebi == periodicTankMeasurementReason)//#sümer#bunu burda yapmak doğru değil sanki
                    SetLastPersistedPeriyodikTankOlcum(pTankOlcumu);
            }
            catch (Exception)
            {
            }
        }

        public void SetLastPersistedPeriyodikTankOlcum(TankStatus tankOlcum)
        {
            foreach (var tank in _channelData.Tanks)
            {
                if (tankOlcum.Tank.Id == tank.Id)
                {
                    tank.LastPersistedPeriyodikTankOlcum = tankOlcum;
                    tank.NotPersistStartTime = null;
                }
            }
        }

        public void AddTankStatusToPeriyodikTankOlcumler(TankStatus pTankOlcum)
        {
            Log.Logger.ForContext("LogKey", LogKeys.AddTankStatusToPeriyodikTankOlcumler).Information("Tank=" + pTankOlcum.Tank.Code);
            //log.Warn("AddTankStatusToPeriyodikTankOlcumler" + pTankOlcum.Tank.TankId);

            foreach (var sessionTank in _channelData.Tanks)
            {
                if (pTankOlcum.Tank.Code == sessionTank.Code)
                {
                    sessionTank.EnSonOlculenTankYakitSeviyesi = pTankOlcum.FuelLevelLength;

                    Log.Logger.ForContext("LogKey", LogKeys.EnSonOlculenTankYakitSeviyesi).Information("Tank=" + pTankOlcum.Tank.Code + " Message=" + sessionTank.EnSonOlculenTankYakitSeviyesi);
                    //log.Warn("Tank.TankId=" + pTankOlcum.Tank.TankId + "EnSonOlculenTankYakitSeviyesi=" + sessionTank.EnSonOlculenTankYakitSeviyesi);

                    #region Periyodik tank ölçümlerini tutmak için kullanılan yapıya eklenir

                    sessionTank.SessionTankOlcumler.Ekle(pTankOlcum);

                    #endregion Periyodik tank ölçümlerini tutmak için kullanılan yapıya eklenir

                    #region periyodik ölçümün DB'ye yazılıp yazılmayacağına karar verilir

                    TankStatus lastTankStatus = LastPersistedPeriyodikTankOlcum(pTank: sessionTank);

                    if (lastTankStatus != null)
                    {
                        decimal yukseklikFark = 0;

                        yukseklikFark = Math.Abs(lastTankStatus.FuelLevelLength - pTankOlcum.FuelLevelLength);

                        decimal ikiOlcumArasindakiYukseklikFarki = pTankOlcum.Tank.HeightLimitForTwoTankStatus();

                        #region eğer ölçümle database'e kayıt edilen en son ölçüm arasındaki fark tank tanımında belirtilen  mm'den büyükse ölçüm database'e kaydedilir,yoksa kaydedilmez

                        if (yukseklikFark >= ikiOlcumArasindakiYukseklikFarki)
                        {
                            #region tank ölçüm sonucu database'e kaydedilir

                            PersistTankStatus(pTankOlcumu: pTankOlcum);

                            #endregion tank ölçüm sonucu database'e kaydedilir
                        }
                        else
                        {
                            #region Tank seviyesi değişmediği için database'e yazılmaz

                            ProcessNotPersistedTankStatus(pTankOlcumu: pTankOlcum);

                            #endregion Tank seviyesi değişmediği için database'e yazılmaz
                        }

                        #endregion eğer ölçümle database'e kayıt edilen en son ölçüm arasındaki fark tank tanımında belirtilen  mm'den büyükse ölçüm database'e kaydedilir,yoksa kaydedilmez
                    }
                    else
                    {
                        #region tank ölçüm sonucu database'e kaydedilir

                        PersistTankStatus(pTankOlcumu: pTankOlcum);

                        #endregion tank ölçüm sonucu database'e kaydedilir
                    }

                    #endregion periyodik ölçümün DB'ye yazılıp yazılmayacağına karar verilir

                    ProcessPeriyodikTankOlcumler(sessionTank);
                }
            }
        }

        public TankStatus LastPersistedPeriyodikTankOlcum(Tank pTank)
        {
            foreach (var tank in _channelData.Tanks)
            {
                if (pTank.Id == tank.Id)
                {
                    return (tank.LastPersistedPeriyodikTankOlcum);
                }
            }
            return null;
        }

        public void ProcessNotPersistedTankStatus(TankStatus pTankOlcumu)
        {
            foreach (var tank in _channelData.Tanks)
            {
                if (pTankOlcumu.Tank.Id == tank.Id)
                {
                    Log.Logger.ForContext("LogKey", LogKeys.TankSeviyesiKacOlcumdurDegismiyor).Information("Tank=" + pTankOlcumu.Tank.Code + " Message=" + tank.TankSeviyesiKacOlcumdurDegismiyor);
                    //log.Warn("AutoFillingNotRisingCounter for TankId:" + pTankOlcumu.Tank.TankId + " is =" + tank.TankSeviyesiKacOlcumdurDegismiyor);

                    Log.Logger.ForContext("LogKey", LogKeys.TankStatusNotPersisted).Information("Tank=" + pTankOlcumu.Tank.Code + " FuelHeightMm=" + pTankOlcumu.FuelLevelLength);
                    //log.Warn("Tank Status Bir önceki ölçüme çok yakın olduğu için DB'e yazılmadı. TankId=" + pTankOlcumu.Tank.TankId + "Ölçüm Değeri=" + pTankOlcumu.FuelLevel_MM);

                    #region tank seviyesi değişmese bile, tank tanımında belirtilen belirli bir periyodda ölçüm sonucunu database'e yazmamız gerekir

                    if (tank.NotPersistStartTime == null)
                    {
                        tank.NotPersistStartTime = DateTime.Now;
                    }
                    else
                    {
                        TimeSpan gecenZaman = (TimeSpan)(DateTime.Now - tank.NotPersistStartTime);
                        int minutes = gecenZaman.Minutes;

                        #region eğer geçen zaman, tank tanımında belirtilen dakikadan fazlaysa kayıt kaydedilecek ve dakika sıfırlanacak

                        if (minutes >= tank.NotPersistIntervalMinute)
                        {
                            PersistTankStatus(pTankOlcumu: pTankOlcumu);
                        }

                        #endregion eğer geçen zaman, tank tanımında belirtilen dakikadan fazlaysa kayıt kaydedilecek ve dakika sıfırlanacak
                    }

                    #endregion tank seviyesi değişmese bile, tank tanımında belirtilen belirli bir periyodda ölçüm sonucunu database'e yazmamız gerekir
                }
            }
        }

        public void ProcessPeriyodikTankOlcumler(Tank pTank)
        {
            foreach (var sessionTank in _channelData.Tanks)
            {
                if (pTank.Id == sessionTank.Id)
                {
                    List<TankStatus> olcumler = sessionTank.SessionTankOlcumler.ReturnValues();
                    int enFazlaTekrarEdenOlcumMmInteger = 0, TankSeviyesiKacOlcumdurDegismiyor = 0, TankSeviyesiKacOlcumdurArtiyor = 0, TankSeviyesiKacOlcumdurAzaliyor = 0;

                    #region session bünyesindeki ölçümlerin tümü kullanılarak değişimler belirlenir

                    FindValuesForOlcumler(sessionTank, null, ref enFazlaTekrarEdenOlcumMmInteger, ref TankSeviyesiKacOlcumdurDegismiyor, ref TankSeviyesiKacOlcumdurArtiyor, ref TankSeviyesiKacOlcumdurAzaliyor);

                    #endregion session bünyesindeki ölçümlerin tümü kullanılarak değişimler belirlenir

                    sessionTank.TankSeviyesiKacOlcumdurArtiyor = TankSeviyesiKacOlcumdurArtiyor;
                    sessionTank.TankSeviyesiKacOlcumdurDegismiyor = TankSeviyesiKacOlcumdurDegismiyor;
                    sessionTank.TankSeviyesiKacOlcumdurAzaliyor = TankSeviyesiKacOlcumdurAzaliyor;

                    #region bilgiler log'a yazılır

                    foreach (TankStatus tankOlcumForLog in sessionTank.SessionTankOlcumler.ReturnValues())
                    {
                        Log.Logger.ForContext("LogKey", LogKeys.PeriodicTankMeasurement)
                            .Information("TankId=" + sessionTank.Id
                            + "OlcumDate=" + tankOlcumForLog.StatusInfoDate
                            + "FuelMM=" + tankOlcumForLog.FuelLevelLength
                            + "AutoFillingNotRisingConstant=" + sessionTank.AutoFillingNotRisingConstant
                            + "TankSeviyesiKacOlcumdurDegismiyor=" + tankOlcumForLog.TankSeviyesiKacOlcumdurDegismiyor
                            + "TankSeviyesiKacOlcumdurAzaliyor=" + tankOlcumForLog.TankSeviyesiKacOlcumdurAzaliyor
                            + "TankSeviyesiKacOlcumdurArtıyor=" + tankOlcumForLog.TankSeviyesiKacOlcumdurArtiyor
                            + "AutoFillingStarted=" + sessionTank.AutoFillingStarted
                            + "EnSonOlculenTankYakitSeviyesi=" + sessionTank.EnSonOlculenTankYakitSeviyesi
                            + " EnFazlaTekrarEdenOlcum=" + tankOlcumForLog.EnFazlaTekrarEdenOlcum
                            + " Fuel Level Difference=" + tankOlcumForLog.FuelLevelMMIntegerDifferenceWithPreviousTankStatus
                            + " FuelLevelMMInteger=" + tankOlcumForLog.FuelLevelMMInteger);
                    }

                    #endregion bilgiler log'a yazılır

                    if (_channelData.IsContinuingPumpSales())
                    {
                        Log.Logger.ForContext("LogKey", LogKeys.ContinuingPumpSales).Warning("Tank=" + sessionTank.Code + " Message=" + LogKeys.ContinuingPumpSales);
                        //log.Warn("Devam Eden Satış Var");
                    }

                    int sondakiEnFazlaTekrarEdenOlcumSayisindanBuyuklerSayisi = KacAdetVar(sessionTank, enFazlaTekrarEdenOlcumMmInteger, sessionTank.AutoFillingNotRisingConstant, 2);
                    int sondakiEnFazlaTekrarEdenOlcumSayisi = KacAdetVar(sessionTank, enFazlaTekrarEdenOlcumMmInteger, sessionTank.AutoFillingNotRisingConstant, 1);

                    #region eğer tank seviye artışı belirli bir sayının üzerinde ise, otomatik dolum başlatacağız

                    if (!sessionTank.AutoFillingStarted
                        && olcumler.Count > 50
                        && sessionTank.AutoFillingNotRisingConstant > 0
                        && sessionTank.IsDetectAutoFilling
                        && TankSeviyesiKacOlcumdurArtiyor >= sessionTank.AutoFillingNotRisingConstant
                        && TankSeviyesiKacOlcumdurArtiyor > TankSeviyesiKacOlcumdurDegismiyor
                        && sondakiEnFazlaTekrarEdenOlcumSayisindanBuyuklerSayisi >= (int)(sessionTank.AutoFillingNotRisingConstant / 2)
                        && sondakiEnFazlaTekrarEdenOlcumSayisi == 0
                        )
                    {
                        TankStatus dolumBaslangicSeviyesiOlcumu = null;

                        #region session'daki tank ölçümlerinden stabil olan seviyeye ait en düşük mm'li ölçümün bulunması gerçekleştirilir

                        for (int i = 0; i < olcumler.Count; i++)
                        {
                            if (olcumler[i].FuelLevelMMInteger == enFazlaTekrarEdenOlcumMmInteger &&
                                (dolumBaslangicSeviyesiOlcumu == null ||
                                (dolumBaslangicSeviyesiOlcumu != null && dolumBaslangicSeviyesiOlcumu.FuelLevelLength > olcumler[i].FuelLevelLength)))
                                dolumBaslangicSeviyesiOlcumu = olcumler[i];
                        }

                        #endregion session'daki tank ölçümlerinden stabil olan seviyeye ait en düşük mm'li ölçümün bulunması gerçekleştirilir

                        if (dolumBaslangicSeviyesiOlcumu != null)
                        {
                            //todo signalr
                            //DomainEventPublisher.Raise<TankAutoFillingStartedEvent>(new TankAutoFillingStartedEvent() { EventTime = dolumBaslangicSeviyesiOlcumu.StatusInfoDate, TankOlcum = dolumBaslangicSeviyesiOlcumu });

                            Log.Logger.ForContext("LogKey", LogKeys.TankAutoFillingStarted).Information("Tank=" + sessionTank.Code +
                                "enFazlaTekrarEdenOlcumMmInteger=" + enFazlaTekrarEdenOlcumMmInteger +
                                "sondakiEnFazlaTekrarEdenOlcumSayisindanBuyuklerSayisi=" + sondakiEnFazlaTekrarEdenOlcumSayisindanBuyuklerSayisi +
                                "DolumBaslangicSeviyesiOlcumuDate=" + dolumBaslangicSeviyesiOlcumu.StatusInfoDate +
                                " with Mm=" + dolumBaslangicSeviyesiOlcumu.FuelLevelLength);
                            //taskService.SaveTarpetTaskLogs("TankAndPumpOps", "enFazlaTekrarEdenOlcumMmInteger=" + enFazlaTekrarEdenOlcumMmInteger + "sondakiEnFazlaTekrarEdenOlcumSayisindanBuyuklerSayisi=" + sondakiEnFazlaTekrarEdenOlcumSayisindanBuyuklerSayisi + "", (int)TarpetLogCodes.AutoFillingStarted);
                            //log.Warn("TankAutoFillingStartedEvent Raised at DolumBaslangicSeviyesiOlcumuDate=" + dolumBaslangicSeviyesiOlcumu.StatusInfoDate + " with Mm=" + dolumBaslangicSeviyesiOlcumu.FuelLevel_MM);
                        }
                    }

                    #endregion eğer tank seviye artışı belirli bir sayının üzerinde ise, otomatik dolum başlatacağız
                }
            }
        }

        public void FindValuesForOlcumler(Tank tank, DateTime? buTarihtenSonraki, ref int enFazlaTekrarEdenOlcum, ref int TankSeviyesiKacOlcumdurDegismiyor, ref int TankSeviyesiKacOlcumdurArtiyor, ref int TankSeviyesiKacOlcumdurAzaliyor)
        {
            foreach (var sessionTank in _channelData.Tanks)
            {
                if (tank.Id == sessionTank.Id)
                {
                    List<TankStatus> tumOlcumler = sessionTank.SessionTankOlcumler.ReturnValues();

                    if (tumOlcumler == null || tumOlcumler.Count == 0)
                    {
                        Log.Logger.ForContext("LogKey", LogKeys.AllTankMeasurementsNull).Warning("Tank=" + sessionTank.Code + " Message=" + LogKeys.AllTankMeasurementsNull);
                        //log.Warn("FindValuesForOlcumler exited since tumOlcumler is null");
                        return;
                    }

                    List<TankStatus> olcumler = new List<TankStatus>();

                    #region eğer tarih belirtilmişse, belirtilen tarihten sonraki ölçümler kullanılır, yoksa tümü kullanılır

                    if (buTarihtenSonraki == null)
                    {
                        olcumler = tumOlcumler;
                    }
                    else
                    {
                        for (int i = 0; i < tumOlcumler.Count; i++)
                        {
                            if (tumOlcumler[i].StatusInfoDate >= buTarihtenSonraki)
                            {
                                olcumler.Add(tumOlcumler[i]);
                            }
                        }
                    }

                    #endregion eğer tarih belirtilmişse, belirtilen tarihten sonraki ölçümler kullanılır, yoksa tümü kullanılır

                    int enFazlaTekrarEdenCounter = 0;

                    enFazlaTekrarEdenOlcum = 0;

                    #region list'deki tank ölçümlerinin integer degeri bulunur

                    for (int i = 0; i < olcumler.Count; i++)
                    {
                        if (olcumler[i].FuelLevelMMInteger == 0)
                        {
                            olcumler[i].FuelLevelMMInteger = (int)Math.Round(olcumler[i].FuelLevelLength, 0, MidpointRounding.AwayFromZero);
                        }
                    }

                    #endregion list'deki tank ölçümlerinin integer degeri bulunur

                    #region ölçüm ile bir önceki ölçüm arasındaki fark hesaplanır

                    for (int i = olcumler.Count - 1; i > 0; i--)
                    {
                        olcumler[i].FuelLevelMMIntegerDifferenceWithPreviousTankStatus = olcumler[i].FuelLevelMMInteger - olcumler[i - 1].FuelLevelMMInteger;
                    }

                    #endregion ölçüm ile bir önceki ölçüm arasındaki fark hesaplanır

                    #region ölçümler içindeki en fazla tekrar etmiş olan seviyenin(stabil seviye) tesbit edilmesine çalışılır

                    List<OlcumCounter> olcumSayilariForMaxOccurence = new List<OlcumCounter>();

                    #region ölçümler içinde hangi ölçümün kaç kez tekrarlandığı belirlenir

                    bool olcumBulunduInteger = false;

                    for (int oncekiOlcumlerIndis = 0; oncekiOlcumlerIndis < olcumler.Count; oncekiOlcumlerIndis++)
                    {
                        if (olcumler[oncekiOlcumlerIndis].FuelLevelMMIntegerDifferenceWithPreviousTankStatus == 0)
                        {
                            olcumBulunduInteger = false;

                            for (int countIndis = 0; countIndis < olcumSayilariForMaxOccurence.Count; countIndis++)
                            {
                                if (olcumler[oncekiOlcumlerIndis].FuelLevelMMInteger == olcumSayilariForMaxOccurence[countIndis].olcumIntegerMm)
                                {
                                    olcumSayilariForMaxOccurence[countIndis].counter++;
                                    olcumBulunduInteger = true;
                                }
                            }

                            if (!olcumBulunduInteger)
                            {
                                OlcumCounter yeniOlcum = new OlcumCounter();

                                yeniOlcum.olcumIntegerMm = olcumler[oncekiOlcumlerIndis].FuelLevelMMInteger;
                                yeniOlcum.counter = 1;

                                olcumSayilariForMaxOccurence.Add(yeniOlcum);
                            }
                        }
                    }

                    #endregion ölçümler içinde hangi ölçümün kaç kez tekrarlandığı belirlenir

                    #region en fazla tekrar eden ölçümün, stabil ölçüm olduğu düşünülerek kontrollerde kullanmak üzere bu değer bulunur

                    for (int countIndis = 0; countIndis < olcumSayilariForMaxOccurence.Count; countIndis++)
                    {
                        if (olcumSayilariForMaxOccurence[countIndis].counter > enFazlaTekrarEdenCounter)
                        {
                            enFazlaTekrarEdenCounter = olcumSayilariForMaxOccurence[countIndis].counter;
                            enFazlaTekrarEdenOlcum = olcumSayilariForMaxOccurence[countIndis].olcumIntegerMm;
                        }
                    }

                    #endregion en fazla tekrar eden ölçümün, stabil ölçüm olduğu düşünülerek kontrollerde kullanmak üzere bu değer bulunur

                    Log.Logger.ForContext("LogKey", LogKeys.MostRepeatedTankMeasurement).Information("Tank=" + sessionTank.Code + " Message=" + "Most Repeated Tank Measurement:" + enFazlaTekrarEdenOlcum + " Counter:" + enFazlaTekrarEdenCounter);
                    //log.Warn("En Fazla Tekrar Eden Ölçüm=" + enFazlaTekrarEdenOlcum + " Sayısı=" + enFazlaTekrarEdenCounter);

                    #endregion ölçümler içindeki en fazla tekrar etmiş olan seviyenin(stabil seviye) tesbit edilmesine çalışılır

                    #region yükseklik azalış, artışları belirlenir

                    TankSeviyesiKacOlcumdurDegismiyor = 0;
                    TankSeviyesiKacOlcumdurArtiyor = 0;
                    TankSeviyesiKacOlcumdurAzaliyor = 0;

                    foreach (TankStatus tankOlcum in olcumler)
                    {
                        if ((tankOlcum.FuelLevelMMInteger - enFazlaTekrarEdenOlcum) == 0)
                        {
                            TankSeviyesiKacOlcumdurDegismiyor++;
                        }
                        else if ((tankOlcum.FuelLevelMMInteger - enFazlaTekrarEdenOlcum) >= 1)
                        {
                            TankSeviyesiKacOlcumdurArtiyor++;
                        }
                        else
                        {
                            TankSeviyesiKacOlcumdurAzaliyor++;
                        }

                        tankOlcum.TankSeviyesiKacOlcumdurArtiyor = TankSeviyesiKacOlcumdurArtiyor;
                        tankOlcum.TankSeviyesiKacOlcumdurDegismiyor = TankSeviyesiKacOlcumdurDegismiyor;
                        tankOlcum.TankSeviyesiKacOlcumdurAzaliyor = TankSeviyesiKacOlcumdurAzaliyor;
                        tankOlcum.EnFazlaTekrarEdenOlcum = enFazlaTekrarEdenOlcum;
                    }

                    #endregion yükseklik azalış, artışları belirlenir

                    return;
                }
            }
        }

        //operasyon = 1  : =
        //operasyon = 2  : >
        public int KacAdetVar(Tank tank, int olcumDeger, int sondanKacTane, int operasyon)
        {
            int counter = 0;

            foreach (var sessionTank in _channelData.Tanks)
            {
                if (tank.Id == sessionTank.Id)
                {
                    List<TankStatus> olcumler = sessionTank.SessionTankOlcumler.ReturnValues();

                    if (olcumler == null)
                    {
                        Log.Logger.ForContext("LogKey", LogKeys.AllTankMeasurementsNull).Warning("Tank=" + sessionTank.Code + " Message=" + LogKeys.AllTankMeasurementsNull);
                        //log.Warn("KacAdetVar exited since olcumler is null");
                        return counter;
                    }

                    int olcumSayisi = olcumler.Count;

                    if (olcumSayisi < sondanKacTane)
                    {
                        Log.Logger.ForContext("LogKey", LogKeys.TankSeviyesiKacOlcumdurDegismiyor).Warning("Tank=" + sessionTank.Code + " Message=" + "KacAdetVar exited since olcumSayisi < sondanKacTane" + "olcumSayisi=" + olcumSayisi + "sondanKacTane=" + sondanKacTane);
                        //log.Warn("KacAdetVar exited since olcumSayisi < sondanKacTane" + "olcumSayisi=" + olcumSayisi + "sondanKacTane=" + sondanKacTane);
                        return counter;
                    }

                    for (int i = olcumSayisi; i > (olcumSayisi - sondanKacTane); i--)
                    {
                        if ((operasyon == 1 && olcumler[i - 1].FuelLevelMMInteger == olcumDeger) || (operasyon == 2 && olcumler[i - 1].FuelLevelMMInteger > olcumDeger))
                        {
                            counter++;
                        }
                    }

                    return (counter);
                }
            }

            return counter;
        }

        #endregion Methods

        //public void SatisTamamlandi(PumpSales pPompaSatis)
        //{
        //    foreach (var dolumNoktasi in currentSession.FillingPoints)
        //    {
        //        if (dolumNoktasi.StationPumpNo == pPompaSatis.Nozzle.FillingPoint.StationPumpNo)
        //        {
        //            if (pPompaSatis != null)
        //                dolumNoktasi.Tutar = (int)pPompaSatis.Amount;

        //            if (pPompaSatis != null)
        //                dolumNoktasi.Litre = (int)pPompaSatis.Quantity;

        //            dolumNoktasi.AnlikVerilenLitre = 0;
        //            dolumNoktasi.LowFuelLevelCounter = 0;

        //            DomainEventPublisher.Raise<UpdatePumpScreenEvent>(new UpdatePumpScreenEvent() { EventTime = DateTime.Now, DolumNoktasi = dolumNoktasi, SalesCompleted = true });
        //        }
        //    }

        //    if (pPompaSatis != null)
        //        currentSession.SatisTamamlandi(pPompaSatis);

        //}

        //public TankStatus ListeyeEnSonEklenenTankOlcumu(Tank pTank)
        //{
        //    foreach (var session_tank in currentSession.Tanks)
        //    {
        //        if (pTank.TankId == session_tank.TankId)
        //        {
        //            List<TankStatus> olcumler = session_tank.SessionTankOlcumler.ReturnValues();

        //            if (olcumler.Count != 0)
        //                return (olcumler[olcumler.Count - 1]);
        //            else
        //                return null;
        //        }
        //    }

        //    return (null);

        //}

        //public void PrintSalesCash(string stationName, string unitPrice, string fillingPointId, string fleetName, string receiptNo, string quantity, string plate, string pumpId, string pumpTrnxTime, string pumpTrnxDate, string amount, string productName, string kilometer, EnumClasses.PumpSaleConfirmationDetailType? confirmationDetailType, string nozzleNo, string restLimiteAmount, string driverName, string personelName)
        //{
        //    DomainEventPublisher.Raise<PrintSalesCashEvent>(new PrintSalesCashEvent()
        //    {
        //        StationName = stationName,
        //        UnitPrice = unitPrice,
        //        FillingPointId = fillingPointId,
        //        FleetName = fleetName,
        //        ReceiptNo = receiptNo,
        //        Quantity = quantity,
        //        Plate = plate,
        //        PumpId = pumpId,
        //        PumpTrnxTime = pumpTrnxTime,
        //        PumpTrnxDate = pumpTrnxDate,
        //        Amount = amount,
        //        ProductName = productName,
        //        Kilometer = kilometer,
        //        ConfirmationDetailType = confirmationDetailType,
        //        NozzleNo = nozzleNo,
        //        RestLimiteAmount = restLimiteAmount,
        //        DriverName = driverName,
        //        PersonelName = personelName
        //    });
        //}
    }
}