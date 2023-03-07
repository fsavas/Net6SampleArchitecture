using System.Collections;

namespace PumpService.Services.Channel.Utility
{
    public class DcrUtilities
    {
        //construct a byte from 2 bytes with MSD and LSD
        public static byte ConstructByteFromTwoBytes(byte pmsd, byte plsd)
        {
            var msd = new BitArray(new byte[] { pmsd });
            var lsd = new BitArray(new byte[] { plsd });
            var combinedBits = new BitArray(8);

            int index = 0;
            //plsd parametresinin ilk dört biti, LSD(sağdaki 4 bit) olarak atanır
            for (int i = 0; i < 4; i++)
            {
                combinedBits[index++] = lsd[i];
            }
            //pmsd bilgisinin ilk dört biti, MSD(soldaki 4 bit) olarak atanır
            for (int i = 0; i < 4; i++)
            {
                combinedBits[index++] = msd[i];
            }

            byte[] new_bytes = new byte[1];
            combinedBits.CopyTo(new_bytes, 0);

            byte newByte = new_bytes[0];

            return (newByte);
        }

        //gönderilen string'deki virgul'ü atar virgul'den önceki ve sonraki bölümü 3 karakter olacak şekilde 0 ile pad eder ve 6 byte'lık bir string döner
        public static String removeVirgulAndPad(string birimFiyat)
        {
            int virgulPozisyon = 0;

            for (int i = 0; i < birimFiyat.Length; i++)
            {
                if (birimFiyat[i] == ',' || birimFiyat[i] == '.')
                    virgulPozisyon = i;
            }

            String virguldenSonrasi = birimFiyat.Substring(virgulPozisyon + 1, birimFiyat.Length - (virgulPozisyon + 1));
            if (virguldenSonrasi.Length < 3) virguldenSonrasi = virguldenSonrasi.PadRight(3, '0');//virgülden sonrası için sağa 0 ekler

            String virguldenOncesi = birimFiyat.Substring(0, virgulPozisyon);
            if (virguldenOncesi.Length < 3) virguldenOncesi = virguldenOncesi.PadLeft(3, '0');//virgülden öncesi için sola 0 ekler

            String yenisi = virguldenOncesi + virguldenSonrasi;

            return (yenisi);
        }

        public static String removeVirgul(string birimFiyat)
        {
            int virgulPozisyon = 0;

            for (int i = 0; i < birimFiyat.Length; i++)
            {
                if (birimFiyat[i] == ',' || birimFiyat[i] == '.')
                    virgulPozisyon = i;
            }

            String virguldenSonrasi = birimFiyat.Substring(virgulPozisyon + 1, birimFiyat.Length - (virgulPozisyon + 1));

            String virguldenOncesi = birimFiyat.Substring(0, virgulPozisyon);

            String yenisi = virguldenOncesi + virguldenSonrasi;

            return (yenisi);
        }

        //gönderilen string'i  0 ile pad eder ve gönderilen length kadar bir string döner
        public static String PadLeft(string deger, int length)
        {
            if (deger.Length < length)
                deger = deger.PadLeft(length, '0');

            return (deger);
        }

        public static System.Tuple<byte, byte> GetCpuIdAndNozzleId(byte message)
        {
            var combinedBits = new BitArray(new byte[] { message });
            var nozzleBits = new BitArray(4);
            var pumpBits = new BitArray(4);

            for (int i = 0; i < 4; i++)
            {
                nozzleBits[i] = combinedBits[i];
            }

            int index = 4;

            for (int i = 0; i < 4; i++)
            {
                pumpBits[i] = combinedBits[index++];
            }

            byte[] pump_bytes = new byte[1];
            pumpBits.CopyTo(pump_bytes, 0);
            byte cpuId = pump_bytes[0];

            byte[] nozzle_bytes = new byte[1];
            nozzleBits.CopyTo(nozzle_bytes, 0);
            byte nozzleId = nozzle_bytes[0];

            return new System.Tuple<byte, byte>(cpuId, nozzleId);
        }

        public static bool HasDigitsOnly(string str)
        {
            return str.All(c => c >= '0' && c <= '9');
        }
    }
}