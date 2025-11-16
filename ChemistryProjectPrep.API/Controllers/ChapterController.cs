using BusinessObjects.DTO.Chapter;
using BusinessObjects.Metadata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace ChemistryProjectPrep.API.Controllers
{
    [Route("api/chapters")]
    [ApiController]
    public class ChapterController : ControllerBase
    {
        private readonly IChapterService _chapterService;
        private readonly ILogger<ChapterController> _logger;

        public ChapterController(IChapterService chapterService, ILogger<ChapterController> logger)
        {
            _chapterService = chapterService;
            _logger = logger;
        }

        /// <summary>
        /// Get all chapters
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<List<ChapterResponse>>>> GetAllChapters()
        {
            try
            {
                var chapters = await _chapterService.GetAllChaptersAsync();

                var response = ApiResponseBuilder.BuildResponse(
                    200,
                    "Chapters retrieved successfully.",
                    chapters
                );

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all chapters");

                var response = ApiResponseBuilder.BuildResponse<List<ChapterResponse>>(
                    500,
                    $"Internal server error: {ex.Message}",
                    null
                );

                return StatusCode(500, response);
            }
        }

        /// <summary>
        /// Get chapter by ID
        /// </summary>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<ChapterResponse>>> GetChapterById(int id)
        {
            try
            {
                var chapter = await _chapterService.GetChapterByIdAsync(id);

                if (chapter == null)
                {
                    var notFoundResponse = ApiResponseBuilder.BuildResponse<ChapterResponse>(
                        404,
                        $"Chapter with ID {id} not found.",
                        null
                    );

                    return NotFound(notFoundResponse);
                }

                var response = ApiResponseBuilder.BuildResponse(
                    200,
                    "Chapter retrieved successfully.",
                    chapter
                );

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting chapter {id}");

                var response = ApiResponseBuilder.BuildResponse<ChapterResponse>(
                    500,
                    $"Internal server error: {ex.Message}",
                    null
                );

                return StatusCode(500, response);
            }
        }

        /// <summary>
        /// Create new chapter (Staff or Admin only)
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "Staff,Admin")]
        public async Task<ActionResult<ApiResponse<ChapterResponse>>> CreateChapter([FromBody] CreateChapterRequest request)
        {
            try
            {
                _logger.LogInformation($"Creating chapter: {request.ChapterName}");

                var chapter = await _chapterService.CreateChapterAsync(request);

                var response = ApiResponseBuilder.BuildResponse(
                    201,
                    "Chapter created successfully.",
                    chapter
                );

                return CreatedAtAction(nameof(GetChapterById), new { id = chapter.ChapterId }, response);
            }
            catch (ArgumentException ex)
            {
                var response = ApiResponseBuilder.BuildResponse<ChapterResponse>(
                    400,
                    ex.Message,
                    null
                );

                return BadRequest(response);
            }
            catch (InvalidOperationException ex)
            {
                var response = ApiResponseBuilder.BuildResponse<ChapterResponse>(
                    409,
                    ex.Message,
                    null
                );

                return Conflict(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating chapter");

                var response = ApiResponseBuilder.BuildResponse<ChapterResponse>(
                    500,
                    $"Internal server error: {ex.Message}",
                    null
                );

                return StatusCode(500, response);
            }
        }

        /// <summary>
        /// Update chapter (Staff or Admin only)
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "Staff,Admin")]
        public async Task<ActionResult<ApiResponse<ChapterResponse>>> UpdateChapter(int id, [FromBody] UpdateChapterRequest request)
        {
            try
            {
                if (id != request.Id)
                {
                    var response = ApiResponseBuilder.BuildResponse<ChapterResponse>(
                        400,
                        "ID mismatch.",
                        null
                    );

                    return BadRequest(response);
                }

                _logger.LogInformation($"Updating chapter ID: {id}");

                var chapter = await _chapterService.UpdateChapterAsync(request);

                var successResponse = ApiResponseBuilder.BuildResponse(
                    200,
                    "Chapter updated successfully.",
                    chapter
                );

                return Ok(successResponse);
            }
            catch (ArgumentException ex)
            {
                var response = ApiResponseBuilder.BuildResponse<ChapterResponse>(
                    400,
                    ex.Message,
                    null
                );

                return BadRequest(response);
            }
            catch (KeyNotFoundException ex)
            {
                var response = ApiResponseBuilder.BuildResponse<ChapterResponse>(
                    404,
                    ex.Message,
                    null
                );

                return NotFound(response);
            }
            catch (InvalidOperationException ex)
            {
                var response = ApiResponseBuilder.BuildResponse<ChapterResponse>(
                    409,
                    ex.Message,
                    null
                );

                return Conflict(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating chapter {id}");

                var response = ApiResponseBuilder.BuildResponse<ChapterResponse>(
                    500,
                    $"Internal server error: {ex.Message}",
                    null
                );

                return StatusCode(500, response);
            }
        }

        /// <summary>
        /// Delete chapter (Admin only)
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Staff,Admin")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteChapter(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting chapter ID: {id}");

                var result = await _chapterService.DeleteChapterAsync(id);

                var response = ApiResponseBuilder.BuildResponse(
                    200,
                    "Chapter deleted successfully.",
                    new { deletedId = id }
                );

                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                var response = ApiResponseBuilder.BuildResponse<object>(
                    404,
                    ex.Message,
                    null
                );

                return NotFound(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting chapter {id}");

                var response = ApiResponseBuilder.BuildResponse<object>(
                    500,
                    $"Internal server error: {ex.Message}",
                    null
                );

                return StatusCode(500, response);
            }
        }
    }
}
