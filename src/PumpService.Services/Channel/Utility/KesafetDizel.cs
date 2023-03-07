namespace PumpService.Services.Channel.Utility
{
    public static class KesafetDizel
    {
        #region Methods

        public static double KesafetOraniBul(double pTemperature)
        {
            double result = 0;

            if (pTemperature < 0)
            {
                result = -pTemperature + 15;
            }
            else if (pTemperature == 0)
            {
                result = 15;
            }
            else if (pTemperature < 15)
            {
                result = 15 - pTemperature;
            }
            else if (pTemperature == 15)
            {
                result = 0;
            }
            else if (pTemperature > 15)
            {
                result = 15 - pTemperature;
            }

            return (1 + (result * 0.000842));
        }

        #endregion Methods
    }
}