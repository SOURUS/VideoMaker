namespace Services.DTO
{
    using Core.Enum;

    public class FileSaverDTO
    {
        public byte[] Data { get; set; }
        public FileTypeEnum FileType { get; set; }
        public string FolderName { get; set; }
    }
}
