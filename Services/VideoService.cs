namespace Services
{
    using Core.ObjectValue;
    using Services.Interfaces;
    using Services.Model;
    using System.Threading.Tasks;

    public class VideoService : IVideoService
    {
        public VideoService()
        { 
        }

        public Task<Result<string>> CreateVideo(VideoCreation videoCreation)
        {
            throw new System.NotImplementedException();
        }
    }
}
