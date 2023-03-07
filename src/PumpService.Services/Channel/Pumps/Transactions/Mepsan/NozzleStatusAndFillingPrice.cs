using PumpService.Services.Channel.Utility;
using Serilog;
using System.Collections;

namespace TarPet.Comm.Pump.Transactions.Mepsan
{
    public class NozzleStatusAndFillingPrice
    {
        public decimal FillingPrice { get; set; }
        public int NozzleNumber { get; set; }
        public bool NozzleIn { get; set; } // true:NozzleIn false:NozzleOut

        public void SetNozzleNumberAndNozzleInOut(byte pNozio)
        {
            var nozioBits = new BitArray(new byte[] { pNozio });

            #region set nozzle in or out

            var nozzleInOrOut = pNozio & 16;//nozio[4] değerini bulmak için

            if (nozzleInOrOut == 16)//nozio[4] = 1 ise nozzle out, yoksa nozzle in demektir
            {
                NozzleIn = false;
            }
            else
                NozzleIn = true;

            #endregion set nozzle in or out

            #region set nozzle number

            NozzleNumber = pNozio & 15;//nozio[0,1,2,3] değerini bulmak için

            /*
            var nozzleBits = new BitArray(4);

            for (int i = 0; i < 4; i++)
            {
                nozzleBits[i] = nozioBits[i];
            }

            byte[] nozzle_bytes = new byte[1];
            nozzleBits.CopyTo(nozzle_bytes, 0);
            NozzleNumber = nozzle_bytes[0];
             * */

            #endregion set nozzle number
        }

        public void SetFillingPrice(byte[] pFillingPrice)
        {
            String fillingPrice = CrcCalc.ByteToHexStr(pFillingPrice);

            try
            {
                FillingPrice = Convert.ToDecimal(fillingPrice) / 1000;
            }
            catch (Exception e)
            {
                Log.Logger.Error("Filling Price is" + fillingPrice);
                Log.Logger.Error("Exception is" + "Message=" + e.Message + "StackTrace=" + e.StackTrace);
            }
        }
    }
}