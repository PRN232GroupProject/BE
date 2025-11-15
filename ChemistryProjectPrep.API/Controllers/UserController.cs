using BusinessObjects.Metadata;
using ChemistryProjectPrep.API.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace ChemistryProjectPrep.API.Controllers
{
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet(ApiEndpointConstants.User.GetCurrentUserEndpoint)]
        public async Task<IActionResult> GetUserByToken()
        {
            try
            {
                var userData = await _userService.GetCurrentUser();

                var response = ApiResponseBuilder.BuildResponse(
                statusCode: 200,
                message: "User information retrieved successfully",
                data: userData
            );

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
