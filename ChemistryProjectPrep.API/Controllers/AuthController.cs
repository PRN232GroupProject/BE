using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System.Security.Claims;
using BusinessObjects.Metadata;
using BusinessObjects.DTO;
using ChemistryProjectPrep.API.Constants;

namespace ChemistryProjectPrep.API.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost(ApiEndpointConstants.Auth.LoginEndpoint)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);

            var response = ApiResponseBuilder.BuildResponse(
                statusCode: result.StatusCode,
                message: result.Message,
                data: result.Data
            );

            return StatusCode(result.StatusCode, response);
        }

        [HttpPost(ApiEndpointConstants.Auth.RegisterEndpoint)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request);

            var response = ApiResponseBuilder.BuildResponse(
                statusCode: result.StatusCode,
                message: result.Message,
                data: result.Data
            );

            return StatusCode(result.StatusCode, response);
        }
    }
}