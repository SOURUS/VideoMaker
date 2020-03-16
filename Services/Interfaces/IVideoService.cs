namespace Services.Interfaces
{
    using Core.ObjectValue;
    using Services.Model;
    using System.Threading.Tasks;

    public interface IVideoService
    {
        public Task<Result<string>> CreateVideo(VideoCreation videoCreation);
    }
}
