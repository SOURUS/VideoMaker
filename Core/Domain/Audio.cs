namespace Core.Domain
{
    public class Audio: BaseFileObject
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Extension { get; set; }
    }
}
