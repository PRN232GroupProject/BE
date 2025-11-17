using BusinessObjects.DTO.Test;
using BusinessObjects.Metadata;
using ChemistryProjectPrep.API.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace ChemistryProjectPrep.API.Controllers
{
    [Route("api/testsessions")]
    [ApiController]
    public class TestSessionController : ControllerBase
    {
        private readonly ITestSessionService _service;
        public TestSessionController(ITestSessionService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTestSessionRequest request)
        {
            var session = await _service.CreateTestSessionAsync(request);

            var response = ApiResponseBuilder.BuildResponse(
                200,
                "Test session created successfully",
                session
            );

            return Ok(response);
        }

        [HttpGet("{sessionId}")]
        [Authorize(Roles ="Admin,Staff,Student")]
        public async Task<IActionResult> GetById(int sessionId)
        {
            var session = await _service.GetSessionByIdAsync(sessionId);
            if (session == null)
            {
                var errorResponse = ApiResponseBuilder.BuildResponse<StudentTestSessionResponse>(
                    404, "Session not found", null!
                );
                return NotFound(errorResponse);
            }

            var response = ApiResponseBuilder.BuildResponse(
                200,
                "Success",
                session
            );

            return Ok(response);
        }

        [HttpGet("user/{userId}/test/{testId}")]
        [Authorize(Roles = "Admin,Staff,Student")]
        public async Task<IActionResult> GetByUserAndTest(int userId, int testId)
        {
            var sessions = await _service.GetSessionsByUserAsync(userId, testId);

            var response = ApiResponseBuilder.BuildResponse(
                200,
                "Success",
                sessions
            );

            return Ok(response);
        }
    }
}
