namespace WebApi.Controllers
{
    using Core.Domain;
    using Core.Enum;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Services.Interfaces;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using WebApi.Helper;

    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public HomeController(IFileManagerService _fileManagerService)
        {
            fileManagerService = _fileManagerService;
        }

        private readonly IFileManagerService fileManagerService;

        [HttpPost]
        public async Task<IActionResult> Index(List<IFormFile> images, IFormFile audioFile)
        {
            //TODO: ofc here must be something FluentValidation.
            if (images.Count <= 0 || audioFile == null)
            {
                return StatusCode(500, "Please provide at least 1 image and only one audio file");
            }

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

                        var imageFile = fileManagerService.SaveFile(fileBytes, Path.GetExtension(formFile.FileName), FileTypeEnum.picture);
                        savedImages.Add(imageFile.Data as Image);
                    }
                }
            }

            using (var ms = new MemoryStream())
            {
                audioFile.CopyTo(ms);
                var fileBytes = ms.ToArray();

                savedAudio = fileManagerService.SaveFile(fileBytes, Path.GetExtension(audioFile.FileName), FileTypeEnum.audio).Data as Audio;
            }

            return Ok(new { count = images.Count });
        }
    }
}
