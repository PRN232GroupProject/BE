using BusinessObjects.DTO.Test;
using BusinessObjects.Metadata;
using ChemistryProjectPrep.API.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
﻿using BusinessObjects.DTO.TestSession;
using BusinessObjects.Metadata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Repository.Interfaces;

namespace ChemistryProjectPrep.API.Controllers
{
    [Route("api/sessions")]
    [ApiController]
    [Authorize]
    public class TestSessionController : ControllerBase
    {
        private readonly ILogger<TestSessionController> _logger;
        private readonly ITestSessionService _sessionService;

        public TestSessionController(ILogger<TestSessionController> logger, ITestSessionService sessionService, IStudentTestSessionRepository service)
        {
            _logger = logger;
            _sessionService = sessionService;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<TestSessionResponse>>>> GetAllTestSessions()
        {
            try
            {
                var sessions = await _sessionService.GetAllTestSessionsAsync();
                var response = ApiResponseBuilder.BuildResponse(
                    200,
                    "Test sessions retrieved successfully.",
                    sessions
                );

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all test sessions");
                var response = ApiResponseBuilder.BuildResponse<List<TestSessionResponse>>(
                    500,
                    $"Internal server error: {ex.Message}",
                    null
                );
                return StatusCode(500, response);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<TestSessionResponse>>> GetTestSessionById(int id)
        {
            try
            {
                var session = await _sessionService.GetTestSessionByIdAsync(id);
                if (session == null)
                {
                    var notFoundResponse = ApiResponseBuilder.BuildResponse<TestSessionResponse?>(
                        404,
                        "Test session not found.",
                        null
                    );
                    return NotFound(notFoundResponse);
                }
                var response = ApiResponseBuilder.BuildResponse(
                    200,
                    "Test session retrieved successfully.",
                    session
                );
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting test session with ID {id}");
                var response = ApiResponseBuilder.BuildResponse<TestSessionResponse?>(
                    500,
                    $"Internal server error: {ex.Message}",
                    null
                );
                return StatusCode(500, response);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<TestSessionResponse>>> CreateTestSession([FromBody] CreateTestSessionRequest request)
        {
            try
            {
                var createdSession = await _sessionService.CreateTestSessionAsync(request);
                var response = ApiResponseBuilder.BuildResponse(
                    201,
                    "Test session created successfully.",
                    createdSession
                );
                return CreatedAtAction(nameof(GetTestSessionById), new { id = createdSession.Id }, response);
            }

            catch (ArgumentException argEx)
            {
                _logger.LogWarning(argEx, "Validation error creating test session");
                var response = ApiResponseBuilder.BuildResponse<TestSessionResponse>(
                    400,
                    $"Bad request: {argEx.Message}",
                    null
                );
                return BadRequest(response);
            }

            catch (InvalidOperationException invOpEx)
            {
                _logger.LogWarning(invOpEx, "Conflict error creating test session");
                var response = ApiResponseBuilder.BuildResponse<TestSessionResponse>(
                    409,
                    $"Conflict: {invOpEx.Message}",
                    null
                );
                return Conflict(response);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating test session");
                var response = ApiResponseBuilder.BuildResponse<TestSessionResponse?>(
                    500,
                    $"Internal server error: {ex.Message}",
                    null
                );
                return StatusCode(500, response);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<TestSessionResponse>>> UpdateTestSession(int id, [FromBody] UpdateTestSessionRequest request)
        {
            try
            {
                if (id != request.Id)
                {
                    var failureResponse = ApiResponseBuilder.BuildResponse<UpdateTestSessionRequest>(
                        400,
                        "Session ID in URL does not match ID in request body.",
                        null
                    );

                    return BadRequest(failureResponse);
                }

                _logger.LogInformation($"Updating session with ID: {id}");

                var updated = await _sessionService.UpdateTestSessionAsync(request);
                var successResponse = ApiResponseBuilder.BuildResponse(
                    200,
                    "Session updated successfully.",
                    updated
                );

                return Ok(successResponse);
            }

            catch (ArgumentException argEx)
            {
                _logger.LogWarning(argEx, "Validation error updating session");
                var response = ApiResponseBuilder.BuildResponse<bool>(
                    400,
                    $"Bad request: {argEx.Message}",
                    false
                );
                return BadRequest(response);
            }

            catch (KeyNotFoundException notFoundEx)
            {
                _logger.LogWarning(notFoundEx, "Session not found for update");
                var response = ApiResponseBuilder.BuildResponse<bool>(
                    404,
                    $"Not found: {notFoundEx.Message}",
                    false
                );
                return NotFound(response);
            }

            catch (InvalidOperationException invOpEx)
            {
                _logger.LogWarning(invOpEx, "Conflict error updating session");
                var response = ApiResponseBuilder.BuildResponse<bool>(
                    409,
                    $"Conflict: {invOpEx.Message}",
                    false
                );
                return Conflict(response);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating session with ID {id}");
                var response = ApiResponseBuilder.BuildResponse<bool>(
                    500,
                    $"Internal server error: {ex.Message}",
                    false
                );
                return StatusCode(500, response);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteTestSession(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting session with ID: {id}");

                var result = await _sessionService.DeleteTestSessionAsync(id);

                var response = ApiResponseBuilder.BuildResponse(
                    200,
                    "Session deleted successfully.",
                    new { deletedId = id }
                );

                return Ok(response);
            }

            catch (KeyNotFoundException notFoundEx)
            {
                _logger.LogWarning(notFoundEx, "Session not found for deletion");
                var response = ApiResponseBuilder.BuildResponse<object>(
                    404,
                    $"Not found: {notFoundEx.Message}",
                    false
                );

                return NotFound(response);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting session with ID {id}");
                var response = ApiResponseBuilder.BuildResponse<object>(
                    500,
                    $"Internal server error: {ex.Message}",
                    false
                );

                return StatusCode(500, response);
            }
        }
        
        [HttpGet("user/{userId}/test/{testId}")]
        [Authorize(Roles = "Admin,Staff,Student")]
        public async Task<IActionResult> GetByUserAndTest(int userId, int testId)
        {
            var sessions = await _sessionService.GetSessionsByUserAsync(userId, testId);

            var response = ApiResponseBuilder.BuildResponse(
                200,
                "Success",
                sessions
            );

            return Ok(response);
        }
    }
}
