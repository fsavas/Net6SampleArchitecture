using PumpService.Core.Defaults;
using PumpService.Services.Channel.Utility;
using Serilog;
using System.Globalization;

namespace PumpService.Services.Channel.Tanks.Messages
{
    public abstract class MepsanDDAMessage
    {
        //private readonly IProductRepository _productRepository;

        //public MepsanDDAMessage()
        //{
        //    var _productRepository = (IProductRepository)ServiceContainer.Provider.GetService(typeof(IProductRepository));
        //}

        #region Methods

        public ushort CalculateCRC(byte[] data)
        {
            ushort sum = 0;
            bool startCalculate = false;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == 0x2) startCalculate = true; // STX foudn start calculation. STX is included n teh calculation
                if (startCalculate)
                {
                    sum = (ushort)(sum + data[i]);
                }
                if (data[i] == 0x3) break;  // ETX found . quit
            }

            ushort checksum = (ushort)(~sum + 1);
            return checksum;
        }

        public int GetCrcFromMessage(byte[] message)
        {
            IEnumerable<byte> crcBytes = message.Slice(message.Length - 5, 5);

            string str = "";
            foreach (byte b in crcBytes)
            {
                str = str + (char)b;
            }

            return Convert.ToInt32(str);
        }

        private byte[] DataBytes(byte[] data)
        {
            int startPos = -1, endPos = -1;
            byte[] retVal = new byte[1];

            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == 0x2) startPos = i;

                if (data[i] == 0x3) endPos = i;
            }
            IEnumerable<byte> bb = data.Skip(startPos + 1).Take(endPos - startPos - 1).Select(x => x);

            retVal = bb.ToArray<byte>();

            return retVal;
        }

        public IEnumerable<T[]> GetPackets<T>(IList<T> buffer, T delimiter)
        {
            // gets delimiters' indexes
            var delimiterIdxs = Enumerable.Range(0, buffer.Count())
                                          .Where(i => buffer[i].Equals(delimiter))
                                          .ToArray();

            // creates a list of delimiters' indexes pair (startIdx,endIdx)
            var dlmtrIndexesPairs = delimiterIdxs.Take(delimiterIdxs.Count() - 1)
                                                 .SelectMany(
                                                             (startIdx, idx) =>
                                                             delimiterIdxs.Skip(idx + 1)
                                                                          .Select(endIdx => new { startIdx, endIdx })
                                                            );
            // creates array of packets
            var packets = dlmtrIndexesPairs.Select(p => buffer.Skip(p.startIdx)
                                                              .Take(p.endIdx - p.startIdx + 1)
                                                              .ToArray())
                                           .ToArray();

            return packets;
        }

        public double GetProductLevel(byte[] data)
        {
            byte[] bytes = data;

            byte[] productBytes = { bytes[3], bytes[4], bytes[5], bytes[6], bytes[7], bytes[8], bytes[9] };

            string str = "";

            foreach (byte b in productBytes)
            {
                str = str + (char)b;
            }

            double value = 0;

            try
            {
                value = Double.Parse(str.Trim(), CultureInfo.GetCultureInfo("tr-TR")) / 100;
            }
            catch (Exception e)
            {
                value = -1;
                Log.Logger.ForContext("LogKey", LogKeys.WrongTankMeasurementFuelLevelError).Error(" Message=" + e.Message + " StackTrace=" + e.StackTrace + " FuelHeightMm=" + value.ToString());
                //taskService.SaveTarpetTaskLogs("MepsanProbe", "HATALI ÖLÇÜM " + "FuelHeightMm=" + value.ToString(), (int)TarpetLogCodes.ProbeFuelLevelReadError);
            }

            return value;
        }

        public double GetInterfaceLevel(byte[] data)
        {
            byte[] bytes = data;

            byte[] productBytes = { bytes[11], bytes[12], bytes[13], bytes[14], bytes[15], bytes[16], bytes[17] };

            string str = "";

            foreach (byte b in productBytes)
            {
                str = str + (char)b;
            }

            double value = 0;

            try
            {
                value = Double.Parse(str.Trim(), CultureInfo.GetCultureInfo("tr-TR")) / 100;
            }
            catch (Exception e)
            {
                value = -1;
                Log.Logger.ForContext("LogKey", LogKeys.WrongTankMeasurementWaterLevelError).Error(" Message=" + e.Message + " StackTrace=" + e.StackTrace + " WaterHeightMm=" + value.ToString());
                //taskService.SaveTarpetTaskLogs("MepsanProbe", "HATALI ÖLÇÜM " + "WaterHeightMm=" + value.ToString(), (int)TarpetLogCodes.ProbeWaterLevelReadError);
            }

            return value;
        }

        public double GetAverageTemperature(byte[] data)
        {
            byte[] bytes = data;

            byte[] productBytes = { bytes[19], bytes[20], bytes[21], bytes[22], bytes[23], bytes[24] };

            string str = "";

            foreach (byte b in productBytes)
            {
                str = str + (char)b;
            }

            double value = 0;

            try
            {
                value = Double.Parse(str.Trim(), CultureInfo.GetCultureInfo("tr-TR")) / 10;
            }
            catch (Exception e)
            {
                value = -99;
                Log.Logger.ForContext("LogKey", LogKeys.WrongTankMeasurementTemperatureError).Error(" Message=" + e.Message + " StackTrace=" + e.StackTrace + " Temperature=" + value.ToString());
                //taskService.SaveTarpetTaskLogs("MepsanProbe", "HATALI ÖLÇÜM " + "Temperature=" + value.ToString(), (int)TarpetLogCodes.ProbeTemperatureReadError);
            }

            return value;
        }

        #endregion Methods
    }
}