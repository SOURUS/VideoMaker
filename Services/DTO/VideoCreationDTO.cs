namespace Services.DTO
{
    using Core.Domain;
    using System.Collections.Generic;

    public class VideoCreationDTO
    {
        public Audio AudioFile { get; set; }
        public List<Image> Images { get; set; } 
        public string Path { get; set; }
    }
}
