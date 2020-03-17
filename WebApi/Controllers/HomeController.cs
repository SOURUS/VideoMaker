namespace WebApi.Controllers
{
    using Core.Domain;
    using Core.Enum;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Services.DTO;
    using Services.Interfaces;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using WebApi.Helper;

    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public HomeController(IFileManagerService _fileManagerService,
            IVideoService _videoService,
            IWebHostEnvironment _environment)
        {
            fileManagerService = _fileManagerService;
            videoService = _videoService;
            environment = _environment;
        }

        private readonly IWebHostEnvironment environment;
        private readonly IFileManagerService fileManagerService;
        private readonly IVideoService videoService;

        //TODO: Controller is zu fat, but working with incoming files should be at API layout, it could be nice write a handler
        [HttpPost]
        public async Task<IActionResult> Index(List<IFormFile> images, IFormFile audioFile)
        {
            //TODO: ofc here must be something FluentValidation.
            if (images.Count <= 0 || audioFile == null)
            {
                return StatusCode(500, "Please provide at least 1 image and only one audio file");
            }

            var folderName = fileManagerService.CreateFolder().Data;
            List<Image> savedImages = new List<Image>();
            Audio savedAudio = null;

            foreach (var formFile in images)
            {
                if (formFile.Length > 0)
                {
                    var typeOfFile = ExtentionDeterminizer.Determinize(formFile.FileName);
                    
                    using (var ms = new MemoryStream())
                    {
                        formFile.CopyTo(ms);
                        var fileBytes = ms.ToArray();

                        var imageFile = fileManagerService.SaveFile(fileBytes, Path.GetExtension(formFile.FileName), FileTypeEnum.picture, folderName);
                        savedImages.Add(imageFile.Data as Image);
                    }
                }
            }

            using (var ms = new MemoryStream())
            {
                audioFile.CopyTo(ms);
                var fileBytes = ms.ToArray();

                savedAudio = fileManagerService.SaveFile(fileBytes, Path.GetExtension(audioFile.FileName), FileTypeEnum.audio, folderName).Data as Audio;
            }

            var filePath = videoService.CreateVideo(new VideoCreationDTO { AudioFile = savedAudio, Images = savedImages, Path = folderName });

            if (filePath.Success)
            {
                return Ok(filePath.Data);
            }

            return StatusCode(500, "Happened something bad");
        }
    }
}
