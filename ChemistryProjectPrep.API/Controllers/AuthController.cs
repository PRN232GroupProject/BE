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
            try
            {
                var response = await _authService.LoginAsync(request);
                if (response == null)
                {
                    return Unauthorized(ApiResponseBuilder.BuildResponse<LoginResponse>(
                        StatusCodes.Status401Unauthorized,
                        "Invalid email or password.",
                        null
                    ));
                }

                return Ok(ApiResponseBuilder.BuildResponse(
                    StatusCodes.Status200OK,
                    "Login successful.",
                    response
                ));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ApiResponseBuilder.BuildResponse<LoginResponse>(
                        StatusCodes.Status500InternalServerError,
                        "An unexpected error occurred while processing the request.",
                        null
                    ));
            }
        }

        [HttpPost(ApiEndpointConstants.Auth.RegisterEndpoint)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch (NotImplementedException)
            {
                var errorResponse = ApiResponseBuilder.BuildResponse<object>(
                    statusCode: StatusCodes.Status501NotImplemented,
                    message: "Register endpoint is not yet implemented",
                    data: null
                );

                return StatusCode(StatusCodes.Status501NotImplemented, errorResponse);
            }
            catch (Exception ex)
            {
                var errorResponse = ApiResponseBuilder.BuildResponse<object>(
                    statusCode: StatusCodes.Status500InternalServerError,
                    message: $"An error occurred: {ex.Message}",
                    data: null
                );

                return StatusCode(StatusCodes.Status500InternalServerError, errorResponse);
            }
        }
    }
}