using BusinessObjects.Metadata;
using ChemistryProjectPrep.API.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChemistryProjectPrep.API.Controllers
{
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        [HttpGet(ApiEndpointConstants.User.GetCurrentUserEndpoint)]
        public IActionResult Me()
        {
            var userData = new
            {
                UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                Email = User.FindFirst(ClaimTypes.Email)?.Value,
                Role = User.FindFirst(ClaimTypes.Role)?.Value
            };

            var response = ApiResponseBuilder.BuildResponse(
                statusCode: 200,
                message: "User information retrieved successfully",
                data: userData
            );

            return Ok(response);
        }
    }
}
