namespace Services.Interfaces
{
    using Core.Domain;
    using Core.Enum;
    using Core.ObjectValue;

    public interface IFileManagerService
    {
        public Result<string> CreateFolder();
        public Result<BaseFileObject> SaveFile(byte[] image, string fileExtension, FileTypeEnum fileType, string pathToSave);
    }
}
