namespace Core.Domain
{
    public class Image: BaseFileObject
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
    }
}
