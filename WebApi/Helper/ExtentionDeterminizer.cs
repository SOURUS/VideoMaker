

namespace WebApi.Helper
{
    using Core.Enum;
    using System.Collections.Generic;
    using System.IO;

    public static class ExtentionDeterminizer
    {
        //TODO: good to have it on appsettings.json
        static List<string> PICTURE_EXTENSIONS = new List<string> { ".jpg", ".png" };
        static List<string> AUDIO_EXTENSIONS = new List<string> { ".mp3" };

        public static FileTypeEnum? Determinize(string fileName)
        {
            var extension = Path.GetExtension(fileName);
            if (PICTURE_EXTENSIONS.Contains(extension))
            {
                return FileTypeEnum.picture;
            }

            if (AUDIO_EXTENSIONS.Contains(extension))
            {
                return FileTypeEnum.audio;
            }

            return FileTypeEnum.unknown;
        }
    }
}
