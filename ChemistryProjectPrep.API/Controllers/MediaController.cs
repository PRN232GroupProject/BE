using BusinessObjects.Metadata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace ChemistryProjectPrep.API.Controllers
{
    [Route("api/media")]
    [ApiController]
    [Authorize(Roles = "Staff,Admin")]
    public class MediaController : ControllerBase
    {
        private readonly ICloudinaryService _cloudinaryService;

        public MediaController(ICloudinaryService cloudinaryService)
        {
            _cloudinaryService = cloudinaryService;
        }

        [HttpPost("image")]
        [RequestSizeLimit(10_000_000)] // 10 MB limit
        public async Task<IActionResult> UploadImage(IFormFile image)
        {
            var imageUrl = await _cloudinaryService.UploadImageAsync(image);
            return Ok(ApiResponseBuilder.BuildResponse(200, "Image uploaded successfully.", imageUrl));
        }

        [HttpPost("images")]
        [RequestSizeLimit(100_000_000)] // 100 MB limit
        public async Task<IActionResult> UploadMultipleImages(IFormFileCollection images)
        {
            var imageUrl = await _cloudinaryService.UploadMultipleImagesAsync(images);
            return Ok(ApiResponseBuilder.BuildResponse(200, "Images uploaded successfully.", imageUrl));
        }

        [HttpPost("video")]
        [RequestSizeLimit(500_000_000)] // 500 MB limit
        public async Task<IActionResult> UploadVideo(IFormFile video)
        {
            var videoUrl = await _cloudinaryService.UploadVideoAsync(video);
            return Ok(ApiResponseBuilder.BuildResponse(200, "Video uploaded successfully.", videoUrl));
        }

        [HttpPost("videos")]
        [RequestSizeLimit(2_000_000_000)] // 2 GB limit
        public async Task<IActionResult> UploadMultipleVideos(IFormFileCollection videos)
        {
            var videoUrl = await _cloudinaryService.UploadMultipleVideosAsync(videos);
            return Ok(ApiResponseBuilder.BuildResponse(200, "Videos uploaded successfully.", videoUrl));
        }

        [HttpPost("raw-file")]
        [RequestSizeLimit(10_000_000)] // 10 MB limit
        public async Task<IActionResult> UploadRawFile(IFormFile file)
        {
            var fileUrl = await _cloudinaryService.UploadRawFileAsync(file);
            return Ok(ApiResponseBuilder.BuildResponse(200, "File uploaded successfully.", fileUrl));
        }

        [HttpPost("raw-files")]
        [RequestSizeLimit(100_000_000)] // 100 MB limit
        public async Task<IActionResult> UploadMultipleRawFiles(IFormFileCollection files)
        {
            var fileUrl = await _cloudinaryService.UploadMultipleRawFilesAsync(files);
            return Ok(ApiResponseBuilder.BuildResponse(200, "Files uploaded successfully.", fileUrl));
        }

        [HttpDelete("resource")]
        public async Task<IActionResult> DeleteResource([FromQuery] string url)
        {
            await _cloudinaryService.DeleteAsync(url);
            return Ok(ApiResponseBuilder.BuildResponse<object>(200, "Resource deleted successfully.", null));
        }

        [HttpDelete("resources")]
        public async Task<IActionResult> DeleteMultipleResources([FromQuery] List<string> urls)
        {
            await _cloudinaryService.DeleteMultipleAsync(urls);
            return Ok(ApiResponseBuilder.BuildResponse<object>(200, "Resources deleted successfully.", null));
        }
    }
}
