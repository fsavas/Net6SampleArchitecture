namespace PumpService.Core.Defaults
{
    public enum LogKeys
    {
        #region Channel Messages

        TankComPortNotFound = 1000,//"Tank ile ilgili Com Port Null görünüyor for Tank:" + _tank.Code
        TankComPortNotOpen = 1001,//"Tank ile ilgili Com Portu Kontrol Ediniz" + _tank.Code
        TankComPortConnected = 1002,//"Tank Açıldı" + _tank.Code
        TankMeasurementReasonNotFound = 1003,

        //"MepsanTank ile ilgili Exception Oluştu baslat() metodunda for Tank" + _tank.Code + "Stacktrace=" + e.StackTrace + "Message=" + e.Message
        IProbeMasterNotFound = 1004,

        ProcessTankStatusEnd = 1005,
        TankMeasurementReason = 1006,
        WrongTankMeasurementFuelLevel = 1007,
        WrongTankMeasurementTemperature = 1008,
        WrongTankMeasurementWaterLevel = 1009,
        WrongTankMeasurementFuelLevelError = 1010,
        WrongTankMeasurementTemperatureError = 1011,
        WrongTankMeasurementWaterLevelError = 1012,
        AddTankStatusToPeriyodikTankOlcumler = 1013,//todo adını değiştir
        EnSonOlculenTankYakitSeviyesi = 1014,//todo adını değiştir
        TankSeviyesiKacOlcumdurDegismiyor = 1015,
        TankStatusNotPersisted = 1016,
        PeriodicTankMeasurement = 1017,
        ContinuingPumpSales = 1018,
        TankAutoFillingStarted = 1019,
        AllTankMeasurementsNull = 1020,
        MostRepeatedTankMeasurement = 1021,
        ProbeSerialNumberNotFound = 1022,

        #endregion Channel Messages

        #region Information
                
        ComPortOpening = 100000,
        ComPortOpened = 100001,
        ComPortClosing = 100002,
        ComPortClosed = 100003,
        RequestPumpStatus = 100004,
        Authorize = 100005,
        RequestEndOfFilling = 100006,
        PaidConfirmed = 100007,
        UpdateUnitPrice = 100008,
        NozzleTotalizer = 100009,

        #endregion Information

        #region Warning

        DeviceTypeLookupTableNull = 200000,
        DeviceNotExists = 200001,
        SerialPortNotExists = 200002,
        PumpContainerInfoNull = 200003,
        PumpSerialDevicesNull = 200004,
        AbuAddressOrCpuIdOrNozzleNull = 200005,
        ComPortEmpty = 200006,
        UpdateUnitPriceDataEmpty = 200007,

        #endregion Warning

        #region Error

        StartPumpsException = 300000,
        ProcessPumpException = 300001,
        RequestPumpStatusException = 300002,
        RequestPumpStatusTimeout = 300003,
        AuthorizeException = 300004,
        AuthorizeTimeout = 300005,
        RequestFillingInfoException = 300006,
        RequestFillingInfoTimeout = 300007,
        PaidConfirmedException = 300008,
        PaidConfirmedTimeout = 300009,
        GetAmountException = 300010,
        GetVolumeException = 300011,
        GetUnitPriceException = 300012,
        ComPortCloseException = 300013,
        ComPortOpenException = 300014,
        DiscardInBufferException = 300015,
        DiscardOutBufferException = 300016,
        SerialPortReadBufferException = 300017,
        SerialPortWriteBufferException = 300018,
        UpdateUnitPriceException = 3000019,
        UpdateUnitPriceTimeout = 300020,
        NozzleTotalizerException = 3000021,
        NozzleTotalizerTimeout = 300022,

        #endregion Error

        ApplicationRestricted = 201, // web servisin ait olduğu uygulama belirli bir tarihe kadar kısıtlandığı zaman
        AutoFillingCompletedSuccessfully = 202,//otomatik dolumun başarıyla tamamlandığını belirtir
        AutoFillingCompletedSuccessfullyBySystem = 303,//otomatik dolumun sistem tarafından başarıyla tamamlandığını belirtir
        AutoFillingCompletedWithFailure = 290,//otomatik dolum kapatılma aşamasında problem çıktığını gösterir
        AutoFillingConvertedToHayali = 295,//otomatik dolumun hayali dolum olarak güncellendiğini gösterir
        AutoFillingConvertedToManuel = 289,//dolum tipinin otomatikten manuel'e çevrildiğini gösterir
        AutoFillingStarted = 203,//otomatik dolum başlatıldığını belirtir
        CalibrationTaskEnded = 204,//kalibrasyon ile ilgili task bittiği zaman
        CalibrationTaskStarted = 205,//kalibrasyon ile ilgili task başlatıldığı zaman
        DcrBugOccurred = 206,//DCR arabirim tamamlanmış bir satışı bug olarak tamamlanmamış olarak gönderdiği zaman
        DcrAmountError = 320,////Bazen pompalardan tutar hatalı geliyor
        DcrUnitPriceError = 321,//Bazen pompalardan birim fiyat hatalı geliyor
        EmptyResponseCode = 207,//web servisten boş response kod geldiği zaman
        Failure = 208,// web servise erişilemediği zaman
        FillingPointCommError = 283,  //FPConnectionLostEvent içinde FP iletişim hatası alındığında üretilir
        FillingPointAbuCommError = 285,  //FPConnectionLostEvent içinde FP iletişim hatası alındığında üretilir (ABU olanlar)
        FillingPointPricesCouldNotRead = 313,//Dolum noktasındaki fiyatlar okunamadığı zaman
        FillingPointPricesUpdated = 209,//Dolum noktasındaki fiyatlar güncellendiği zaman
        FillingPointPricesUpdateProblem = 308,//ürün fiyatları pompada güncellenemediği zaman
        FillingPointRequiredInfoMissing = 210,//FP ile ilgili gereken bilgiler tam olmadığı zaman
        FillingPointStarted = 211,//Dolum noktası başlatıldığı zaman
        HddCapacityDroppedBelowCriticalLevel = 212,//Hard disk kapasitesi %20'nin altına düştüğü zaman
        HddCapacityNormal = 213,//Hard disk kapasitesi normal değerler içinde olduğu zaman
        InfoDeleted = 214,//belirli objeler delete edildiği zaman; log'un taskname bölümünde silinen bilginin tipi yer alacaktır
        InfoUpdated = 215,//belirli objeler update edildiği zaman;log'un taskname bölümünde silinen bilginin tipi yer alacaktır
        IstasyonAktifDegil = 216,
        KapaliOlmayanPumpSalesKapatildi = 217,//Bağlantı koptuğu için statüsü kapalı hale gelmeyen bir pumpsales işlendiği zaman
        LogoutForApplicationUpgrade = 304,//uygulamanın upgrade edilebilmesi için logout gerçekleştirildiği zaman
        ManuelTankFillingCompleted = 291,//manuel dolum kapatıldığı zaman
        ManuelTankFillingCompletedBySystem = 302,//manuel dolum sistem tarafından kapatıldığı zaman
        ManuelTankFillingStarted = 292,//manuel dolum açıldığı zaman
        ManuelTankFillingConvertedToHayali = 297,//manuel dolumun hayali dolum olarak güncellendiğini gösterir
        MepsanAlarm = 316,//Mepsan pompalar alarm state'de olduğu zaman
        MepsanDataError = 322,//Mepsan pompalardan hatalı bilgi geldiği zaman
        NotAuthorizedBecauseOfLostTankConnection = 294,//tank ile bağlantı kurulamadığı için onay verilmediği zaman
        NotAuthorizedBecauseOfLowFuelLevel = 282,//Tank yakıt seviyesi yetersiz olduğu için onay verilmediği zaman
        NotAuthorizedBecauseOfOpenTankFilling = 293,//Tank ile ilgili açık bir dolum göründüğü için onay verilmediği zaman
        NotAuthorizedBecauseOfProductPriceChangeProblemOnFP = 309,//ürün fiyatı FillingPoint'de güncellenemediği için onay verilemediği zaman
        NozzleStoppedBecauseOfLowFuelLevel = 288,//tank seviyesi düştüğü için satış sırasında yakıt verme kesildiği zaman
        NozzleTotalizerOkunamadi = 218,//Tabanca ile ilgili totalizer okunamadığı zaman
        PoasConnectionLost = 219,//POAS'a bağlantı kurulamadığını gösterir
        PCSystemDateAssignmentError = 310,//İstasyonun saatini POAŞ'a göre ayarlarken sorun çıktığı zaman
        PCSystemDateErrorWithPoas = 220,//İstasyondaki PC'nin tarihi POAŞ'a göre farklı olduğu zaman; #sümer#todo#bu log Server'da kontrol edilmelidir, bu logu üreten istasyonlara bağlanıp sistem saatlerini düzenlemek lazım

        ProbeFuelLevelError = 324,
        ProbeFuelLevelReadError = 325,
        ProbeTemperatureError = 326,
        ProbeTemperatureReadError = 327,
        ProbeWaterLevelError = 328,
        ProbeWaterLevelReadError = 329,

        ProgramStarted = 305,
        ProgramEnded = 306,
        PumpSalesNotClosed = 221,//Pumpsales statüsü kapalı görünmüyorsa
        PumpSalesMarkedToResend = 300,//Pumpsales resend yapılmak üzere güncellenmişse
        PumpSalesTankAndActiveTankNotEqual = 301,//pumpsales ile ilgili belirlenen tank gruplanmış bir tank ise ve grupta aktif olduğu belirtilen tank'tan farklı ise; #sümer#todo# log'a bu bilgi düşerse istasyonla irtibat kurmak gerekir
        SalesCollectorSendUnitPricePeriodicallyNoDataToSend = 224,
        SalesSummaryBekleyenSatisOlduguIcinGonderilmedi = 307,
        SalesSummaryIncelenmelidirEventRaised = 286,
        SatisYapilanTankBelirlenemedi = 227,//Gruplanmış tanktan yakıt çeken tabanca satışlarında hangi tanktan yakıt çekildiği belirlenemediğinde
        StationApplicationVersionUpdated = 228,//istasyonda kullanılan program versiyonu update edildiği zaman
        StationOperationResumed = 229,//istasyon faaliyeti normale alınır
        StationOperationSuspended = 230,//istasyon faaliyeti askıya alınır
        StationCommonInquirySendStatusInfoTaskTkkFailure = 235,
        StationRequiredInfoMissing = 298,//istasyon tanımında gereken bilgilerin tam olmadığını gösterir
        StationSelfCheckInsertStationDataTaskTkkFailure = 238,
        StationSelfCheckReCheckStationDataFailed = 241,//self check işleminin tamamlanamadığını gösterir
        StationSelfCheckReCheckStationDataSuccessful = 242,//self check işleminin tamamlandığını gösterir
        TankAutomationSendTankDataTaskTkkFailure = 247,
        TankAutomationSendTankDataTaskTkkFailureSincePoasRecordExist = 315,
        TankCommError = 284,  //TankConnectionLostEvent içinde iletişim hatası alındığında üretilir
        TankFillingLevelsMustBeChecked = 299,//tank dolum ile ilgili seviye bilgilerinin kontrol edilmesi gerektiğini belirtir
        TankFuelMaxFillingLevelDropped = 248,
        TankFuelMaxFillingLevelExceeded = 249,
        TankFuelOrderLevelDropped = 250,
        TankFuelOrderLevelExceeded = 251,
        TankGeoCalibrationInfoCalculated = 252,//tank ile ilgili geometrik kalibrasyon hesaplandığı zaman
        TankGeoCalibrationInfoDeleted = 253,////tank ile ilgili geometrik kalibrasyon silindi
        TankGruptakiAktifTankOldu = 317,
        TankGruptakiPasifTankOldu = 318,
        TankHighWaterLevelDropped = 254,
        TankHighWaterLevelExceeded = 255,
        TankLowFuelLevelDropped = 256,
        TankLowFuelLevelExceeded = 257,
        TankManCalibrationInfoDeleted = 258,////tank ile ilgili manuel kalibrasyon silindi
        TankManCalibrationInfoLoaded = 259,//tank ile ilgili manuel kalibrasyon yüklendi
        TankManCalibrationFileContainDuplicateHeight = 260,//manuel kalibrasyon dosyasında aynı yükseklik birden fazla satırda yer alıyor
        TankManuelVeyaGeometrikKalibrasyonBilgisiYok = 261,//#sümer#todo#otomatik kalibrasyon'un tamamlanması için gereken bilgiler eksiktir,Server'da kontrol edilmelidir
        TankMaxTemperatureLevelDropped = 262,
        TankMaxTemperatureLevelExceeded = 263,
        TankMinTemperatureLevelDropped = 264,
        TankMinTemperatureLevelExceeded = 265,
        TankOtmCalibrationInfoDeleted = 266,//tank ile ilgili otomatik kalibrasyon silindi
        TankOtmKalibrasyonAktivasyonYuzdesiBelirtilmemis = 267,
        TankOverFuelLevelDropped = 268,
        TankOverFuelLevelExceeded = 269,
        TankRequiredInfoMissing = 270,//tank tanımında gereken bilgilerin tam olmadığını gösterir
        TankStarted = 271,//Tank başlatıldığı zaman

        TankStatusEmptyRegionError = 331,
        TankStatusErroneous = 296,//hatalı tank ölçümü
        TankStatusFuelLevelError = 311,
        TankStatusTemperatureError = 312,
        TankStatusWaterLevelError = 330,

        TankSummaryIncelenmelidirEventRaised = 287,
        TankWaterExistLevelDropped = 272,
        TankWaterExistLevelExceeded = 273,
        TarpetTaskDisabled = 274,//TarpetTask pasif hale getirildi
        TarpetTaskEnabled = 275,//TarpetTask aktif hale getirildi
        TarpetTaskNameIsInvalid = 276,//TarpetTask tablosundaki taskname geçerli bir class'ı göstermediği zaman
        TarpetTasksNotStartedBecauseOfNoInternetConnection = 319,
        TarpetTaskUpdated = 323,
        TaskMustRunBeforeSeven = 314,//task'ların saat 07:00'den önce çalıştırılması gerektiği belirtilir
        Timeout = 277, //web servis timeout dolayısıyla çalışmadığı zaman
        TotalizerAmountUyumsuzlugu = 278,//Totalizer uyumsuzluğu oluştuğu zaman;#sümer#todo#merkezde bunu kontrol etmeliyiz
        UnexpectedResponseCode = 279,//web servislerden beklenen response code'lardan biri gelmediği zaman kullanılır
        UserLogined = 280,//kullanıcı login olduğu zaman
        UserLoginFailure = 281,//girilen username/password yanlış olduğu zaman

        NotAuthorizedBecauseOfKitID = 332//KitID Okuma Başarısız

        //NotAuthorizedBecauseOfKitID = 332, ***ENSON***
    }
}