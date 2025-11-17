using BusinessObjects.DTO.Answer;
using BusinessObjects.Metadata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace ChemistryProjectPrep.API.Controllers
{
    [Route("api/answers")]
    [ApiController]
    [Authorize]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;
        private readonly ILogger<AnswerController> _logger;

        public AnswerController(IAnswerService answerService, ILogger<AnswerController> logger)
        {
            _answerService = answerService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<AnswerResponse>>>> GetAllAnswers()
        {
            try
            {
                var answers = await _answerService.GetAllAnswersAsync();
                var response = ApiResponseBuilder.BuildResponse(
                    200,
                    "Answers retrieved successfully.",
                    answers
                );

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all answers");
                var response = ApiResponseBuilder.BuildResponse<List<AnswerResponse>>(
                    500,
                    $"Internal server error: {ex.Message}",
                    null
                );
                return StatusCode(500, response);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<AnswerResponse>>> GetAnswerById(int answerId)
        {
            try
            {
                var answer = await _answerService.GetAnswerByIdAsync(answerId);
                if (answer == null)
                {
                    var notFoundResponse = ApiResponseBuilder.BuildResponse<AnswerResponse?>(
                        404,
                        "Answer not found.",
                        null
                    );
                    return NotFound(notFoundResponse);
                }
                var response = ApiResponseBuilder.BuildResponse(
                    200,
                    "Answer retrieved successfully.",
                    answer
                );
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting answer with ID {answerId}");
                var response = ApiResponseBuilder.BuildResponse<AnswerResponse?>(
                    500,
                    $"Internal server error: {ex.Message}",
                    null
                );

                return StatusCode(500, response);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<AnswerResponse>>> CreateAnswer([FromBody] CreateAnswerRequest request)
        {
            try
            {
                _logger.LogInformation($"Creating new answer for question ID: {request.QuestionId}");

                var answer = await _answerService.CreateAnswerAsync(request);
                var response = ApiResponseBuilder.BuildResponse(
                    201,
                    "Answer created successfully.",
                    answer
                );

                return CreatedAtAction(nameof(GetAnswerById), new { id = answer.Id }, response);
            }

            catch (ArgumentException argEx)
            {
                _logger.LogWarning(argEx, "Validation error creating answer");
                var response = ApiResponseBuilder.BuildResponse<AnswerResponse>(
                    400,
                    $"Bad request: {argEx.Message}",
                    null
                );
                return BadRequest(response);
            }

            catch (InvalidOperationException invOpEx)
            {
                _logger.LogWarning(invOpEx, "Conflict error creating answer");
                var response = ApiResponseBuilder.BuildResponse<AnswerResponse>(
                    409,
                    $"Conflict: {invOpEx.Message}",
                    null
                );
                return Conflict(response);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating answer");
                var response = ApiResponseBuilder.BuildResponse<AnswerResponse>(
                    500,
                    $"Internal server error: {ex.Message}",
                    null
                );

                return StatusCode(500, response);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<AnswerResponse>>> UpdateAnswer(int id, [FromBody] UpdateAnswerRequest request)
        {
            try
            {
                if (id != request.Id)
                {
                    var failureResponse = ApiResponseBuilder.BuildResponse<UpdateAnswerRequest>(
                        400,
                        "Answer ID in URL does not match ID in request body.",
                        null
                    );

                    return BadRequest(failureResponse);
                }

                _logger.LogInformation($"Updating answer with ID: {id}");

                var updated = await _answerService.UpdateAnswerAsync(request);
                var successResponse = ApiResponseBuilder.BuildResponse(
                    200,
                    "Answer updated successfully.",
                    updated
                );

                return Ok(successResponse);
            }

            catch (ArgumentException argEx)
            {
                _logger.LogWarning(argEx, "Validation error updating answer");
                var response = ApiResponseBuilder.BuildResponse<bool>(
                    400,
                    $"Bad request: {argEx.Message}",
                    false
                );
                return BadRequest(response);
            }

            catch (KeyNotFoundException notFoundEx)
            {
                _logger.LogWarning(notFoundEx, "Answer not found for update");
                var response = ApiResponseBuilder.BuildResponse<bool>(
                    404,
                    $"Not found: {notFoundEx.Message}",
                    false
                );
                return NotFound(response);
            }

            catch (InvalidOperationException invOpEx)
            {
                _logger.LogWarning(invOpEx, "Conflict error updating answer");
                var response = ApiResponseBuilder.BuildResponse<bool>(
                    409,
                    $"Conflict: {invOpEx.Message}",
                    false
                );
                return Conflict(response);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating answer with ID {id}");
                var response = ApiResponseBuilder.BuildResponse<bool>(
                    500,
                    $"Internal server error: {ex.Message}",
                    false
                );
                return StatusCode(500, response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteAnswer(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting answer with ID: {id}");

                var result = await _answerService.DeleteAnswerAsync(id);

                var response = ApiResponseBuilder.BuildResponse(
                    200,
                    "Answer deleted successfully.",
                    new { deletedId = id }
                );

                return Ok(response);
            }

            catch (KeyNotFoundException notFoundEx)
            {
                _logger.LogWarning(notFoundEx, "Answer not found for deletion");
                var response = ApiResponseBuilder.BuildResponse<object>(
                    404,
                    $"Not found: {notFoundEx.Message}",
                    false
                );

                return NotFound(response);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting answer with ID {id}");
                var response = ApiResponseBuilder.BuildResponse<object>(
                    500,
                    $"Internal server error: {ex.Message}",
                    false
                );

                return StatusCode(500, response);
            }
        }
    }
}