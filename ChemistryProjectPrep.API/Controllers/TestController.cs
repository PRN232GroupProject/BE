using BusinessObjects.DTO.Test;
using BusinessObjects.Metadata;
using ChemistryProjectPrep.API.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System.Security.Claims;

namespace ChemistryProjectPrep.API.Controllers
{
    [Route(ApiEndpointConstants.TestEndpoint)]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;
        private readonly ILogger<TestController> _logger;

        public TestController(ITestService testService, ILogger<TestController> logger)
        {
            _testService = testService;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous] 
        public async Task<ActionResult<ApiResponse<List<TestResponseDto>>>> GetAllTests()
        {
            try
            {
                var tests = await _testService.GetAllTestsAsync();
                return Ok(ApiResponseBuilder.BuildResponse(
                    200, "Tests retrieved successfully.", tests
                ));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all tests");
                return StatusCode(500, ApiResponseBuilder.BuildResponse<List<TestResponseDto>>(
                    500, $"Internal server error: {ex.Message}", null
                ));
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous] 
        public async Task<ActionResult<ApiResponse<TestResponseDto>>> GetTestById(int id)
        {
            try
            {
                var test = await _testService.GetTestByIdAsync(id);
                if (test == null)
                {
                    return NotFound(ApiResponseBuilder.BuildResponse<TestResponseDto>(
                        404, $"Test with ID {id} not found.", null
                    ));
                }
                return Ok(ApiResponseBuilder.BuildResponse(
                    200, "Test retrieved successfully.", test
                ));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting test {id}");
                return StatusCode(500, ApiResponseBuilder.BuildResponse<TestResponseDto>(
                    500, $"Internal server error: {ex.Message}", null
                ));
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<ActionResult<ApiResponse<TestResponseDto>>> CreateTest([FromBody] CreateTestRequestDto request)
        {
            try
            {
                var newTest = await _testService.CreateTestAsync(request);

                return CreatedAtAction(nameof(GetTestById), new { id = newTest.Id },
                    ApiResponseBuilder.BuildResponse(
                        201, "Test created successfully.", newTest
                    )
                );
            }
            catch (UnauthorizedAccessException ex) 
            {
                return Unauthorized(ApiResponseBuilder.BuildResponse<TestResponseDto>(401, ex.Message, null));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating test");
                return StatusCode(500, ApiResponseBuilder.BuildResponse<TestResponseDto>(
                    500, $"Internal server error: {ex.Message}", null
                ));
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<ActionResult<ApiResponse<TestResponseDto>>> UpdateTest(int id, [FromBody] UpdateTestRequestDto request)
        {
            if (id != request.Id)
            {
                return BadRequest(ApiResponseBuilder.BuildResponse<TestResponseDto>(
                    400, "ID mismatch.", null
                ));
            }

            try
            {
                var updatedTest = await _testService.UpdateTestAsync(request);
                return Ok(ApiResponseBuilder.BuildResponse(
                    200, "Test updated successfully.", updatedTest
                ));
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ApiResponseBuilder.BuildResponse<TestResponseDto>(
                    404, ex.Message, null
                ));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating test {id}");
                return StatusCode(500, ApiResponseBuilder.BuildResponse<TestResponseDto>(
                    500, $"Internal server error: {ex.Message}", null
                ));
            }
        }
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteTest(int id)
        {
            try
            {
                await _testService.DeleteTestAsync(id);
                return Ok(ApiResponseBuilder.BuildResponse<object>(
                    200, "Test deleted successfully.", new { deletedId = id }
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
                _logger.LogError(ex, $"Error deleting test {id}");
                return StatusCode(500, ApiResponseBuilder.BuildResponse<object>(
                    500, $"Internal server error: {ex.Message}", null
                ));
            }
        }
    }
}
