namespace Services
{
    using Core.Domain;
    using Core.Enum;
    using Core.ObjectValue;
    using Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    public class FileManagerService : IFileManagerService
    {
        public FileManagerService(string basePath)
        {
            BaseSourcePath = basePath;
        }

        readonly string BaseSourcePath;

        public Task<bool> CheckFile(string path)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> CheckFolder(string path)
        {
            throw new System.NotImplementedException();
        }

        public Task<Result<bool>> CreateFolder(string name)
        {
            throw new System.NotImplementedException();
        }

        public Result<BaseFileObject> SaveFile(byte[] data, string fileExtension, FileTypeEnum fileType)
        {
            BinaryWriter Writer = null;
            BaseFileObject result = null;
            List<Error> errors = new List<Error>();

            string fileName = $"{Guid.NewGuid()}{fileExtension}";
            var fullPath = $@"{BaseSourcePath}{fileName}";

            try
            {
                Writer = new BinaryWriter(File.OpenWrite(fullPath));

                Writer.Write(data);
                Writer.Flush();
                Writer.Close();

                switch (fileType)
                {
                    case FileTypeEnum.audio:
                        {
                            result = new Audio { Name = fileName, Path = fullPath, Extension = fileExtension };
                            break;
                        }
                    case FileTypeEnum.picture:
                        {
                            result = new Image { Name = fileName, Path = fullPath, Extension = fileExtension };
                            break;
                        }
                    default:
                        {
                            errors.Add(new Error { Message = "Unknown type of file" });
                            break;
                        }
                }
                
            }
            catch
            {
                return new Result<BaseFileObject> { Success = false, Errors = errors };
            }

            return new Result<BaseFileObject> { Data = result };
        }
    }
}