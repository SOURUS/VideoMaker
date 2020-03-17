namespace Services.Interfaces
{
    using Core.ObjectValue;
    using Services.DTO;

    public interface IVideoService
    {
        public Result<string> CreateVideo(VideoCreationDTO videoCreation);
    }
}
