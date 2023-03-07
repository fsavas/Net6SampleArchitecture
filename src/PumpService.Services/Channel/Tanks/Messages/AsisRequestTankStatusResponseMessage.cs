using System.Globalization;

namespace PumpService.Services.Channel.Tanks.Messages
{
    public class AsisRequestTankStatusResponseMessage : IMessage
    {
        #region Fields

        private byte _slaveAddress;
        private float? fuelRawHeight;
        private float? waterRawHeight;
        private float fuelAvgTemperature;
        private float? mFuelRawTemperature;

        public byte TSR { get; set; }
        public byte FSR { get; set; }
        public String FSRBinary { get; set; }
        public byte AlarmFlag { get; set; }
        public float Voltage { get; set; }

        #endregion Fields

        #region Methods

        private float TemperatureHextoFloat(string HexValue)
        {
            return ((((float)Convert.ToInt32(HexValue, 0x10)) / 10f) - 100f);
        }

        public void Initialize(byte[] frame)
        {
            if (frame == null)
                return;

            fuelRawHeight = waterRawHeight = 0f;
            byte index = 6;
            this.TSR = frame[index];
            this.FSR = frame[5];
            this.FSRBinary = Convert.ToString(this.FSR, 2).PadLeft(6, '0');
            string s = string.Format("{0:x2}{1:x2}{2:x2}", frame[9], frame[10], frame[11]);
            string str2 = string.Format("{0:x2}{1:x2}{2:x2}", frame[12], frame[13], frame[14]);
            string hexValue = string.Format("{0:x2}{1:x2}", frame[15], frame[0x10]);

            fuelRawHeight = ((float)int.Parse(s, NumberStyles.HexNumber)) / 1000f;
            waterRawHeight = ((float)int.Parse(str2, NumberStyles.HexNumber)) / 1000f;

            if (this.TSR != 15)
            {
                this.fuelAvgTemperature = this.TemperatureHextoFloat(hexValue);
                this.FuelRawTemperature = (this.FuelRawTemperature == 0f) ? new float?(this.fuelAvgTemperature) : this.FuelRawTemperature;
            }
            else
            {
                this.FuelRawTemperature = 0f;
            }

            this.AlarmFlag = frame[7];
            this.Voltage = ((float)frame[8]) / 10f;
        }

        #endregion Methods

        #region Properties

        public byte SlaveAddress
        {
            get
            {
                return _slaveAddress;
            }
            set
            {
                _slaveAddress = value;
            }
        }

        public float? FuelRawHeight
        {
            get { return fuelRawHeight; }
            set { fuelRawHeight = value; }
        }

        public float? WaterRawHeight
        {
            get { return waterRawHeight; }
            set { waterRawHeight = value; }
        }

        public virtual float? FuelRawTemperature
        {
            get
            {
                float? mFuelRawTemperature = this.mFuelRawTemperature;
                return new float?(mFuelRawTemperature.HasValue ? mFuelRawTemperature.GetValueOrDefault() : 0f);
            }
            set
            {
                if (value.HasValue && ((this.mFuelRawTemperature != (this.mFuelRawTemperature = value))))
                {
                    float? mFuelRawTemperature = value; // this.mFuelRawTemperature;
                    //float num = this.OffsetOptionsSets.FuelTemperatureOffset.Value;
                    //this.mFuelTemperature = mFuelRawTemperature.HasValue ? new float?(mFuelRawTemperature.GetValueOrDefault() - num) : null;
                    //this.FuelTemperatureChanged(this);
                }
            }
        }

        #endregion Properties

        #region NotImplemented

        public byte[] MessageFrame
        {
            get { throw new NotImplementedException(); }
        }

        public byte[] ProtocolDataUnit
        {
            get { throw new NotImplementedException(); }
        }

        public ushort TransactionId
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public byte ControlNo
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion NotImplemented
    }
}