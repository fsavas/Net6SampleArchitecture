using static PumpService.Core.Defaults.EnumClasses;

namespace PumpService.Core.Helpers
{
    public static class FileHelper
    {
        #region Methods

        public static string GetFilePath(string fileName, FileExtensionTypes fileExtensionTypes)
        {
            var path = @"./Files/";//var path = @".\Files\";//todo get from db
            MakeContentDirectory(path);

            return path += fileName + "." + fileExtensionTypes; ;
        }

        public static void MakeContentDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        #endregion Methods
    }
}