using BusinessObjects.DTO.Lesson;
using BusinessObjects.Entities;
using BusinessObjects.Metadata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace ChemistryProjectPrep.API.Controllers
{
    [Route("api/lessons")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly ILessonService _lessonService;

        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var lessons = await _lessonService.GetAllAsync();
            if (lessons == null)
                return NotFound(ApiResponseBuilder.BuildResponse<object>(404, "Lesson list not found", null));
            return Ok(ApiResponseBuilder.BuildResponse(200, "Success", lessons));
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            var lesson = await _lessonService.GetLessonByIdAsync(id);

            if (lesson == null)
                return NotFound(ApiResponseBuilder.BuildResponse<object>(404, "Lesson not found", null));

            return Ok(ApiResponseBuilder.BuildResponse(200, "Success", lesson));
        }

        [HttpGet("chapter/{chapterId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByChapter(int chapterId)
        {
            var lessons = await _lessonService.GetLessonsByChapterAsync(chapterId);

            return Ok(ApiResponseBuilder.BuildResponse(200, "Success", lessons));
        }

        [HttpPost]
        [Authorize(Roles ="Admin,Staff")]
        public async Task<IActionResult> Create(CreateLessonRequest request)
        {
            var response = await _lessonService.CreateLessonAsync(request);
            return Ok(ApiResponseBuilder.BuildResponse(200, "Lesson created successfully", response));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> Update([FromRoute] int id, UpdateLessonRequest request)
        {
            var response = await _lessonService.UpdateLessonAsync(id ,request);
            return Ok(ApiResponseBuilder.BuildResponse(200, "Lesson updated", response));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> Delete(int id)
        {
            await _lessonService.DeleteLessonAsync(id);
            return Ok(ApiResponseBuilder.BuildResponse<object>(200, "Lesson deleted", null));
        }
    }
}
