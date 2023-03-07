using PumpService.Core;
using PumpService.Core.Defaults;
using PumpService.Core.Domain.Tanks;
using PumpService.Core.Repository.Tanks;
using PumpService.Services.ExportImport;
using Serilog;

namespace PumpService.Services.Tanks
{
    public partial class TankService : BaseService, ITankService
    {
        #region Fields

        private readonly ITankRepository _tankRepository;
        private readonly IExportManager<TankGrid, Tank> _exportManager;

        #endregion Fields

        #region Constructor

        public TankService(IUnitOfWork unitOfWork, ITankRepository tankRepository, IExportManager<TankGrid, Tank> exportManager)
            : base(unitOfWork)
        {
            _tankRepository = tankRepository;
            _exportManager = exportManager;
        }

        #endregion Constructor

        #region Base Methods

        public virtual void DeleteTank(long tankId)
        {
            var tank = GetTankById(tankId);

            if (tank == null)
                throw new ArgumentNullException(nameof(tank));

            _tankRepository.Delete(tank);
            _unitOfWork.SaveChanges();
        }

        public virtual List<Tank> GetAllTanks()
        {
            List<Tank> LoadTanksFunc()
            {
                var query = from s in _tankRepository.Table orderby s.Id select s;
                return query.ToList();
            }

            return LoadTanksFunc();
        }

        public virtual Tank GetTankById(long tankId)
        {
            if (tankId == 0)
                return null;

            Tank LoadTankFunc()
            {
                return _tankRepository.GetById(tankId);
            }

            return LoadTankFunc();
        }

        public virtual void InsertTank(Tank tank)
        {
            if (tank == null)
                throw new ArgumentNullException(nameof(tank));

            _tankRepository.Insert(tank);
            _unitOfWork.SaveChanges();
        }

        public virtual void UpdateTank(Tank tank)
        {
            if (tank == null)
                throw new ArgumentNullException(nameof(tank));

            Log.Logger.ForContext("User", "Fatih").Information("Update tank service starting");
            _tankRepository.Update(tank);
            _unitOfWork.SaveChanges();
            Log.Logger.ForContext("User", "Fatih").Information("Update tank service ending");
        }

        #endregion Base Methods

        #region Methods

        public IPagedList<Tank> SearchTanks(TankSearch tankSearch)
        {
            return _tankRepository.SearchTanks(tankSearch);
        }

        public string ExportTanks(TankSearch tankSearch)
        {
            var list = (List<Tank>)_tankRepository.SearchAllTanks(tankSearch);

            return _exportManager.ExportToExcel(list);
        }

        public double FindProcessedHeightOfTankStatus(Tank tank, double pmeasuredHeight, EnumClasses.LiquidType pLiquidtype)
        {
            double hesaplananHeight = pmeasuredHeight;

            #region yakıt veya su ile ilgili offset değeri varsa işlenir, gelen ölçüm sonucuna yakıt/su için belirtilen offset değeri eklenir (offset değeri negatif olarak belirtilmişse çıkartma yapılır)

            if (pLiquidtype == EnumClasses.LiquidType.Fuel && tank.FuelOffset != null)
            {
                hesaplananHeight = pmeasuredHeight + Convert.ToDouble(tank.FuelOffset);
            }
            else if (pLiquidtype == EnumClasses.LiquidType.Water && tank.WaterOffset != null)
            {
                hesaplananHeight = pmeasuredHeight + Convert.ToDouble(tank.WaterOffset);
            }

            #endregion yakıt veya su ile ilgili offset değeri varsa işlenir, gelen ölçüm sonucuna yakıt/su için belirtilen offset değeri eklenir (offset değeri negatif olarak belirtilmişse çıkartma yapılır)

            #region offset değerleri sonrasında yada gelen parametrenin eksi olması ihtimaline karşı kontrol yapılır

            if (hesaplananHeight < 0)
            {
                return 0;
            }

            #endregion offset değerleri sonrasında yada gelen parametrenin eksi olması ihtimaline karşı kontrol yapılır

            return hesaplananHeight;
        }

        //mm olarak gönderilen yakıt veya su ölçüm sonucunun lt olarak karşılığını döndürür
        // liquidtype: 1=yakıt 2=su
        public double FindVolumesOfLiquid(Tank tank, double pmeasuredHeight, EnumClasses.LiquidType pLiquidtype)
        {
            //#region ölçülen değerler probe ile ilgili en alt seviye bilgileri kullanılarak kontrol edilir

            //#region eğer su seviyesi, tankta hiç su yokken probe'un gösterdiği yükseklikten küçük-eşitse, su seviyesi 0 döndürülür
            //if (pLiquidtype == EnumClasses.LiquidType.Water && tank.SuYokkenOkunanYukseklik != null && tank.SuYokkenOkunanYukseklik > 0)
            //{
            //    if (pmeasuredHeight <= (double)tank.SuYokkenOkunanYukseklik)
            //    {
            //        return 0;
            //    }
            //}
            //#endregion

            //#region eğer yakıt seviyesi, tankta hiç su ve yakıt yokken probe'un gösterdiği yükseklikten küçük-eşitse, yakıt seviyesi 0 döndürülür
            //if (pLiquidtype == EnumClasses.LiquidType.Fuel && tank.YakitVeSuYokkenOkunanYukseklik != null && tank.YakitVeSuYokkenOkunanYukseklik > 0)
            //{
            //    if (pmeasuredHeight <= (double)tank.YakitVeSuYokkenOkunanYukseklik)
            //    {
            //        return 0;
            //    }
            //}
            //#endregion

            ////#sümer#todo# tankta su varsa, ancak hiç yakıt yoksa bu durumu handle etmiyoruz burada,su olduğu için su seviyesi yükselir, dolayısıyla yakıt seviyesi yükselir,ancak aslında yakıt yoktur
            //#endregion

            //double measurementLtOtm = 0, measurementLtMan = 0, measurementLtGeo = 0;

            //#region yakıt suyun üstünde olduğu için su miktarı yakıt miktarından çıkartılarak yakıt miktarı bulunur,bunun yapılmaması gerektiğini söyledi İrfan murat, onun için iptal ettik
            ////fuelHeightLt = fuelHeightLt - waterHeightLt;
            //#endregion

            //#region tank tanımında yer alan kalibrasyon sıralaması göz önüne alınarak hacim döndürülür

            //measurementLtOtm = FindAutomaticCalibrationVolume(tank, pmeasuredHeight);
            //measurementLtGeo = FindGeometricCalibrationVolume(tank, pmeasuredHeight);
            //measurementLtMan = FindManuelCalibrationVolume(tank, pmeasuredHeight);

            //log.Warn("CalibrationOrder=" + tank.CalibrationOrder + "Height=" + pmeasuredHeight + "measurementLtOtm=" + measurementLtOtm + "measurementLtGeo=" + measurementLtGeo + "measurementLtMan=" + measurementLtMan + "LiquidType=" + pLiquidtype);

            //for (int i = 0; i < 3; i++)
            //{
            //    if (tank.CalibrationOrder[i] == 'A' && measurementLtOtm != 0)
            //        return measurementLtOtm;
            //    else if (tank.CalibrationOrder[i] == 'M' && measurementLtMan != 0)
            //        return measurementLtMan;
            //    else if (tank.CalibrationOrder[i] == 'G' && measurementLtGeo != 0)
            //        return measurementLtGeo;
            //}

            return 0;
        }

        #endregion Methods
    }
}