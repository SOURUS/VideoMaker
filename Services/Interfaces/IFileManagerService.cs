namespace Services.Interfaces
{
    using Core.Domain;
    using Core.Enum;
    using Core.ObjectValue;
    using System.Threading.Tasks;

    public interface IFileManagerService
    {
        public Task<Result<bool>> CreateFolder(string name);
        public Task<bool> CheckFolder(string path);
        public Task<bool> CheckFile(string path);
        public Result<BaseFileObject> SaveFile(byte[] image, string fileExtension, FileTypeEnum fileType);
    }
}
