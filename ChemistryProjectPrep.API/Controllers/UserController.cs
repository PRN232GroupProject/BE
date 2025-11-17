using BusinessObjects.DTO.User;
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

        [HttpGet(ApiEndpointConstants.User.GetAllUsersEndpoint)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsers();
                var response = ApiResponseBuilder.BuildResponse(
                statusCode: 200,
                message: "Users retrieved successfully",
                data: users
            );
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet(ApiEndpointConstants.User.GetUserEndpoint)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserById([FromRoute] int userId)
        {
            try
            {
                var user = await _userService.GetUserById(userId);
                if (user == null)
                {
                    var notFoundResponse = ApiResponseBuilder.BuildResponse<object>(
                        statusCode: 404,
                        message: "User not found",
                        data: null
                    );
                    return NotFound(notFoundResponse);
                }
                var response = ApiResponseBuilder.BuildResponse(
                    statusCode: 200,
                    message: "User retrieved successfully",
                    data: user
                );
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost(ApiEndpointConstants.User.CreateUserEndpoint)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateUser([FromBody] UserRequestDTO userRequest)
        {
            try
            {
                var result = await _userService.CreateUser(userRequest);
                var response = ApiResponseBuilder.BuildResponse(
                    statusCode: 201,
                    message: "User created successfully",
                    data: result
                );
                return Created(string.Empty, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut(ApiEndpointConstants.User.UpdateUserEndpoint)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUser([FromRoute] int userId, [FromBody] UserRequestDTO userRequest)
        {
            try
            {
                var result = await _userService.UpdateUser(userId, userRequest);
                var response = ApiResponseBuilder.BuildResponse(
                    statusCode: 200,
                    message: "User updated successfully",
                    data: result
                );
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete(ApiEndpointConstants.User.DeleteUserEndpoint)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser([FromRoute] int userId)
        {
            try
            {
                var result = await _userService.DeleteUser(userId);
                var response = ApiResponseBuilder.BuildResponse(
                    statusCode: 200,
                    message: "User deleted successfully",
                    data: result
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
