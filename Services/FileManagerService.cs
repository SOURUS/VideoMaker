namespace Services
{
    using Core.Domain;
    using Core.Enum;
    using Core.ObjectValue;
    using Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class FileManagerService : IFileManagerService
    {
        public FileManagerService(string basePath)
        {
            BaseSourcePath = basePath;
        }

        readonly string BaseSourcePath;

        public Result<string> CreateFolder()
        {
            var newFolderPath = string.Empty;

            while (true)
            {
                newFolderPath = BaseSourcePath + Guid.NewGuid().ToString() + '\\';
                
                if (!Directory.Exists(newFolderPath))
                {
                    Directory.CreateDirectory(newFolderPath);
                    return new Result<string> { Data = newFolderPath };
                }
            }
        }

        public Result<BaseFileObject> SaveFile(byte[] data,
            string fileExtension,
            FileTypeEnum fileType,
            string folderName)
        {
            BinaryWriter Writer = null;
            BaseFileObject result = null;
            List<Error> errors = new List<Error>();

            string fileName = GenerateFileName(fileType, fileExtension, folderName);
            var fullPath = $"{folderName + fileName}";

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

        // this function as side effect of lack of Glob.h in windows
        // be cuz of this lack, we can not grab non-sequence images -_-
        private string GenerateFileName(FileTypeEnum type, string extension, string folderName)
        {
            if (type == FileTypeEnum.audio)
            {
                return $"{Guid.NewGuid()}{extension}";
            }

            var fileCount = (from file in Directory.EnumerateFiles(folderName, $"*{extension}", SearchOption.TopDirectoryOnly)
                             select file).Count() + 1;

            return fileCount.ToString() + extension;
        }
    }
}