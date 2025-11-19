// ChemistryProjectPrep.API/Controllers/QuestionController.cs

using BusinessObjects.DTO.Question;
using BusinessObjects.Metadata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ChemistryProjectPrep.API.Constants;

namespace ChemistryProjectPrep.API.Controllers
{
    [Route("api/questions")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly ILogger<QuestionController> _logger;

        public QuestionController(IQuestionService questionService, ILogger<QuestionController> logger)
        {
            _questionService = questionService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<ActionResult<ApiResponse<List<QuestionResponseDto>>>> GetQuestions(
            [FromQuery] int? lessonId,
            [FromQuery] string? difficulty)
        {
            try
            {
                var questions = await _questionService.GetQuestionsAsync(lessonId, difficulty);

             
                return Ok(ApiResponseBuilder.BuildResponse(
                    200,
                    "Questions retrieved successfully.",
                    questions
                ));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting questions");
                return StatusCode(500, ApiResponseBuilder.BuildResponse<List<QuestionResponseDto>>(
                    500, $"Internal server error: {ex.Message}", null
                ));
            }
        }

      
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ApiResponse<QuestionResponseDto>>> GetQuestionById(int id)
        {
            try
            {
                var question = await _questionService.GetQuestionByIdAsync(id);
                if (question == null)
                {
                   
                    return NotFound(ApiResponseBuilder.BuildResponse<QuestionResponseDto>(
                        404, $"Question with ID {id} not found.", null
                    ));
                }
                return Ok(ApiResponseBuilder.BuildResponse(
                    200, "Question retrieved successfully.", question
                ));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting question {id}");
                return StatusCode(500, ApiResponseBuilder.BuildResponse<QuestionResponseDto>(
                    500, $"Internal server error: {ex.Message}", null
                ));
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<ActionResult<ApiResponse<QuestionResponseDto>>> CreateQuestion([FromBody] CreateQuestionRequestDto request)
        {
            try
            {
                var newQuestion = await _questionService.CreateQuestionAsync(request);

                return CreatedAtAction(nameof(GetQuestionById), new { id = newQuestion.Id },
                    ApiResponseBuilder.BuildResponse(
                        201, "Question created successfully.", newQuestion
                    )
                );
            }
            catch (UnauthorizedAccessException ex) 
            {
                return Unauthorized(ApiResponseBuilder.BuildResponse<QuestionResponseDto>(401, ex.Message, null));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ApiResponseBuilder.BuildResponse<QuestionResponseDto>(400, ex.Message, null));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating question");
                return StatusCode(500, ApiResponseBuilder.BuildResponse<QuestionResponseDto>(
                    500, $"Internal server error: {ex.Message}", null
                ));
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<ActionResult<ApiResponse<QuestionResponseDto>>> UpdateQuestion(int id, [FromBody] UpdateQuestionRequestDto request)
        {
            if (id != request.Id)
            {
           
                return BadRequest(ApiResponseBuilder.BuildResponse<QuestionResponseDto>(
                    400, "ID mismatch.", null
                ));
            }

            try
            {
                var updatedQuestion = await _questionService.UpdateQuestionAsync(request);

              
                return Ok(ApiResponseBuilder.BuildResponse(
                    200, "Question updated successfully.", updatedQuestion
                ));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ApiResponseBuilder.BuildResponse<QuestionResponseDto>(
                    404, ex.Message, null
                ));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating question {id}");
                return StatusCode(500, ApiResponseBuilder.BuildResponse<QuestionResponseDto>(
                    500, $"Internal server error: {ex.Message}", null
                ));
            }
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteQuestion(int id)
        {
            try
            {
                await _questionService.DeleteQuestionAsync(id);

             
                return Ok(ApiResponseBuilder.BuildResponse<object>(
                    200, "Question deleted successfully.", new { deletedId = id }
                ));
            }
            catch (KeyNotFoundException ex)
            {
          
                return NotFound(ApiResponseBuilder.BuildResponse<object>(
                    404, ex.Message, null
                ));
            }
            catch (InvalidOperationException ex)
            {
              
                return Conflict(ApiResponseBuilder.BuildResponse<object>(
                    409, ex.Message, null
                ));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting question {id}");
           
                return StatusCode(500, ApiResponseBuilder.BuildResponse<object>(
                    500, $"Internal server error: {ex.Message}", null
                ));
            }
        }

    }
}