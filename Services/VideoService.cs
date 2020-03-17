namespace Services
{
    using Core.ObjectValue;
    using Services.Interfaces;
    using Services.DTO;
    using System.Diagnostics;

    public class VideoService : IVideoService
    {
        public VideoService(string basePath, string _ffmpegPath)
        {
            baseOutputPath = basePath;
            ffmpegPath = _ffmpegPath;
        }

        readonly string baseOutputPath;
        readonly string ffmpegPath;

        public Result<string> CreateVideo(VideoCreationDTO videoCreation)
        {
            // Notice: for every image I set up the same duration (4 seconds).
            // But if it's important to play whole the track and show the image, then we can use ffprobe
            // for example this command will give us track duration -> ffprobe -i %filepath% -show_entries format=duration -v quiet -of csv="p=0"
            var imagesPath = $"{videoCreation.Path}%d.jpg";
            var resultPath = $"{baseOutputPath}Result.mp4";

            using (Process p = new Process())
            {   
                p.StartInfo.FileName = ffmpegPath;
                p.StartInfo.Arguments = $"-y -framerate 1/4 -start_number 1 -i \"{imagesPath}\" -i \"{videoCreation.AudioFile.Path}\"  -c:v libx264 -r 25 -pix_fmt yuv420p -c:a aac -strict experimental -shortest -max_muxing_queue_size 9999 \"{resultPath}\"";

                p.Start();
                p.WaitForExit();
            }

            return new Result<string> { Data = resultPath };
        }
    }
}
