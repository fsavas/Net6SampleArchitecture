using System.ComponentModel;

namespace PumpService.Core.Defaults
{
    public static class EnumClasses
    {
        public enum FileExtensionTypes
        {
            [Description("xlsx")]
            xlsx = 1
        }

        public enum FileMediaTypes
        {
            [Description("application/octet-stream")]
            OctetStream = 1
        }

        public enum AuditTypes
        {
            Insert = 1,
            Update = 2,
            Delete = 3
        }

        public enum ProbeTypes
        {
            Asis = 1,
            Teosis = 2,
            Mepsan = 3
        }

        public enum TankMeasurementReasons
        {
            Periodic = 1,
            DuringPumpSales = 7
            //SatisSonrasi = 2,
            //DolumSonrasi = 3,
            //DolumOncesi = 4,
            //SatisOncesi = 5,
            //KalibrasyonIcin = 6,
            //SatisSirasinda = 7
        }

        public enum DcrStates
        {
            IdleState = 0,
            CallState = 1,
            BusyState = 2,
            PausedState = 3,
            UnpaidState = 4,
            ErrorState = 5,
            AuthorizedState = 6,
            WaitState = 7,
            PaidState = 8
        }

        public enum MepsanStates
        {
            PumpNotProgrammedState = 0,
            ResetState = 1,
            AuthorizedState = 2,
            FillingState = 4,
            FillingCompletedState = 5,
            MaxAmountVolumeReachedState = 6,
            SwitchedOffState = 7,
            PausedState = 11,
            CallState = 8,
            NeedTankStatusReadState = 9,
            NeedAuthorizationState = 10,
            IdleState = 11
        }

        public enum DeviceTypeTypes
        {
            Serial = 1,
            FillingPoint = 2
        }

        public enum DeviceTypeGroups
        {
            Communication = 1,
            Other = 2
        }

        public enum DeviceParameterTypes
        {
            Int = 1,
            String = 2
        }

        public enum DeviceParameterNames
        {
            Com = 1,
            BaudRate = 2,
            DataBits = 3,
            StopBits = 4,
            Parity = 5,
            ReadTimeout = 6,
            WriteTimeout = 7,
            AbuAddress = 8,
            CpuId = 9,
            NozzleId = 10
        }

        //public enum PersonnelPositionTypes
        //{
        //    [Description(MemoryCacheKeys.EnumClasses_PersonnelPositionTypes_Pumper)]
        //    Pumper = 1,

        //    [Description(MemoryCacheKeys.EnumClasses_PersonnelPositionTypes_Manager)]
        //    Manager = 2,

        //    [Description(MemoryCacheKeys.EnumClasses_PersonnelPositionTypes_Driver)]
        //    Driver = 3
        //}

        //public enum PortTypes
        //{
        //    [Description("Clcbox")]
        //    Clcbox = 1,

        //    [Description("ProbeX")]
        //    ProbeX = 2,

        //    [Description("Adams")]
        //    Adams = 3,

        //    [Description("Akord AcNette")]
        //    AkordAcNette = 4
        //}

        public enum LookupTypes
        {
            AuditTypes = 1,

            PersonnelPositionTypes = 2,

            PortTypes = 3,

            ProbeTypes = 4,

            TankMeasurementReasons = 5,

            DeviceParameterTypes = 6,

            DeviceTypeTypes = 7,

            DeviceTypeGroups = 8,

            DcrStates = 9,

            MepsanStates = 10,

            DeviceParameterNames = 11,

            Models = 12
        }

        public enum LiquidType
        {
            Fuel = 1,

            Water = 2
        }
    }
}