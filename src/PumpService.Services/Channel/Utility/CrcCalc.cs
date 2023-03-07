using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace PumpService.Services.Channel.Utility
{
    public class CrcCalc
    {
        #region Methods

        public static Byte[] CalcCRC(string RawData)
        {
            string str2 = "";
            try
            {
                int number = 0;
                StringType.MidStmtStr(ref RawData, 1, 1, "5");
                int num6 = (int)Math.Round((double)(((double)RawData.Length) / 2.0));
                for (int i = 1; i <= num6; i++)
                {
                    byte num5 = (byte)Math.Round(Conversion.Val("&H" + RawData.Substring((i - 1) * 2, 2)));
                    byte num = 1;
                    do
                    {
                        int num4 = (number % 2) + (num5 % 2);
                        number = (int)Math.Round(Conversion.Int((double)(((double)number) / 2.0)));
                        num5 = (byte)Math.Round(Conversion.Int((double)(((double)num5) / 2.0)));
                        if (num4 == 1)
                        {
                            number ^= 0xa001;
                        }
                        num = (byte)(num + 1);
                    }
                    while (num <= 8);
                }
                str2 = Conversion.Hex(number).PadLeft(4, '0');
                str2 = str2.Substring(2, 2) + str2.Substring(0, 2);
                byte[] b = HexStringToByte(str2);
                return b;
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                str2 = null; // eskiden Null idi
                ProjectData.ClearProjectError();
            }
            return null;
        }

        public static String Crc16calc(string RawData)
        {
            string str2 = "";
            try
            {
                int number = 0;
                StringType.MidStmtStr(ref RawData, 1, 1, "5");
                int num6 = (int)Math.Round((double)(((double)RawData.Length) / 2.0));
                for (int i = 1; i <= num6; i++)
                {
                    byte num5 = (byte)Math.Round(Conversion.Val("&H" + RawData.Substring((i - 1) * 2, 2)));
                    byte num = 1;
                    do
                    {
                        int num4 = (number % 2) + (num5 % 2);
                        number = (int)Math.Round(Conversion.Int((double)(((double)number) / 2.0)));
                        num5 = (byte)Math.Round(Conversion.Int((double)(((double)num5) / 2.0)));
                        if (num4 == 1)
                        {
                            number ^= 0xa001;
                        }
                        num = (byte)(num + 1);
                    }
                    while (num <= 8);
                }
                str2 = Conversion.Hex(number).PadLeft(4, '0');
                str2 = str2.Substring(2, 2) + str2.Substring(0, 2);
            }
            catch (Exception exception1)
            {
                ProjectData.SetProjectError(exception1);
                str2 = ""; // eskiden Null idi
                ProjectData.ClearProjectError();
            }
            return str2;
        }

        public static byte[] HexStringToByte(string StrData)
        {
            byte[] array = new byte[((int)Math.Round((double)(((double)(StrData.Length - 2)) / 2.0))) + 1];
            byte num2 = (byte)Information.UBound(array, 1);
            for (byte i = 0; i <= num2; i = (byte)(i + 1))
            {
                array[i] = (byte)Math.Round(Conversion.Val("&H" + StrData.Substring(i * 2, 2)));
            }
            return array;
        }

        public static void CRCfilter(string RawData, ref string pCRC, ref string pData)
        {
            if (RawData.EndsWith("03FA"))
            {
                if (RawData.Substring(RawData.Length - 6, 2) == "FA")
                {
                    RawData = RawData.Remove(RawData.Length - 8, 2);
                }
                if (RawData.Substring(RawData.Length - 8, 2) == "FA")
                {
                    RawData = RawData.Remove(RawData.Length - 10, 2);
                }
                pCRC = RawData.Substring(RawData.Length - 8, 4);
                pData = RawData.Substring(0, RawData.Length - 8);
            }
            else
            {
                pCRC = "";
                pData = "";
            }
        }

        //bu kod indexoutofrange exception veriyor
        public static string ByteToHexStr(byte[] ByteData, string separator = "")
        {
            string str2 = "";
            byte num2 = (byte)Information.UBound(ByteData, 1);
            for (byte i = 0; i <= num2; i = (byte)(i + 1))
            {
                str2 = str2 + separator + Conversion.Hex(ByteData[i]).PadLeft(2, '0');
            }
            return str2;
        }

        public static string ByteToHexStr2(byte[] ByteData, string separator = "")
        {
            string str2 = "";
            byte num2 = (byte)Information.UBound(ByteData, 1);
            for (byte i = 0; i <= num2; i = (byte)(i + 1))
            {
                str2 = str2 + separator + Conversion.Hex(ByteData[i]);
            }
            return str2;
        }

        #endregion Methods

        #region Commented

        /*
        public static string DLEcontrolADD(string RawCRCdata)
        {
            return Conversions.ToString(Operators.ConcatenateObject(Interaction.IIf(RawCRCdata.StartsWith("FA"), "10FA", RawCRCdata.Substring(0, 2)), Interaction.IIf(RawCRCdata.EndsWith("FA"), "10FA", RawCRCdata.Substring(2, 2))));
        }

        public static void ComputeCRC(byte[] message, ref byte[] CRC)
        {
            //Function expects a modbus message of any length as well as a 2 byte CRC array in which to
            //return the CRC values:

            ushort CRCFull = 0xFFFF;
            byte CRCHigh = 0xFF, CRCLow = 0xFF;
            char CRCLSB;

            for (int i = 0; i < (message.Length); i++)
            {
                CRCFull = (ushort)(CRCFull ^ message[i]);

                for (int j = 0; j < 8; j++)
                {
                    CRCLSB = (char)(CRCFull & 0x0001);
                    CRCFull = (ushort)((CRCFull >> 1) & 0x7FFF);

                    if (CRCLSB == 1)
                        CRCFull = (ushort)(CRCFull ^ 0xA001);
                }
            }
            CRC[1] = CRCHigh = (byte)((CRCFull >> 8) & 0xFF);
            CRC[0] = CRCLow = (byte)(CRCFull & 0xFF);
        }

        public static string CalculateCrc(string RawCRCdata, bool ASCIIcoded = false)
        {
            //  string str2 = RawCRCdata + DLEcontrolADD(Crc16calc(RawCRCdata)) + "03FA";
            string str2 = DLEcontrolADD(Crc16calc(RawCRCdata));
            if (!ASCIIcoded)
            {
                return str2;
            }
            string str3 = "";
            int num2 = (int)Math.Round((double)(((double)(str2.Length - 1)) / 2.0));
            for (int i = 0; i <= num2; i++)
            {
                str3 = str3 + Conversions.ToString(Strings.Chr((int)Math.Round(Conversion.Val("&H" + str2.Substring(i * 2, 2)))));
            }
            return str3;
        }

        public static string Crc16calc(byte[] RawData)
        {
            int number = 0;
            byte num5 = RawData[0];
            string sDest = Conversion.Hex(RawData[0]).ToString();
            StringType.MidStmtStr(ref sDest, 1, 1, "5");
            RawData[0] = (byte)Math.Round(Conversion.Val("&H" + sDest));
            int num7 = Information.UBound(RawData, 1);
            for (int i = 0; i <= num7; i++)
            {
                byte num6 = RawData[i];
                byte num = 1;
                do
                {
                    int num4 = (number % 2) + (num6 % 2);
                    number = (int)Math.Round(Conversion.Int((double)(((double)number) / 2.0)));
                    num6 = (byte)Math.Round(Conversion.Int((double)(((double)num6) / 2.0)));
                    if (num4 == 1)
                    {
                        number ^= 0xa001;
                    }
                    num = (byte)(num + 1);
                }
                while (num <= 8);
            }
            string str2 = Conversion.Hex(number).PadLeft(4, '0');
            str2 = str2.Substring(2, 2) + str2.Substring(0, 2);
            RawData[0] = num5;
            return str2;
        }
        */

        #endregion Commented
    }
}