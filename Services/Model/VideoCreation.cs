namespace Services.Model
{
    using System.Collections.Generic;

    public class VideoCreation
    {
        public string AudioPath { get; set; }

        //TODO: in perfect world should be list of "Image" objects to have additional control about WaterMarks, Widht, Height usw...
        public List<string> ImagePaths { get; set; } 
    }
}
