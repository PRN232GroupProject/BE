using BusinessObjects.DTO.Resource;
using BusinessObjects.Metadata;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace ChemistryProjectPrep.API.Controllers
{
    [Route("api/resources")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private readonly ILogger<ResourceController> _logger;
        private readonly IResourceService _resourceService;

        public ResourceController(ILogger<ResourceController> logger, IResourceService resourceService)
        {
            _logger = logger;
            _resourceService = resourceService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<List<ResourceResponse>>>> GetAllResources()
        {
            try
            {
                var resources = await _resourceService.GetAllResourcesAsync();
                var response = ApiResponseBuilder.BuildResponse(
                    200,
                    "Resources retrieved successfully.",
                    resources
                );

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all resources");
                var response = ApiResponseBuilder.BuildResponse<List<ResourceResponse>>(
                    500,
                    $"Internal server error: {ex.Message}",
                    null
                );
                return StatusCode(500, response);
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ApiResponse<ResourceResponse?>>> GetResourceById(int id)
        {
            try
            {
                var resource = await _resourceService.GetResourceByIdAsync(id);
                if (resource == null)
                {
                    var notFoundResponse = ApiResponseBuilder.BuildResponse<ResourceResponse?>(
                        404,
                        "Resource not found.",
                        null
                    );
                    return NotFound(notFoundResponse);
                }
                var response = ApiResponseBuilder.BuildResponse(
                    200,
                    "Resource retrieved successfully.",
                    resource
                );
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting resource with ID {id}");
                var response = ApiResponseBuilder.BuildResponse<ResourceResponse?>(
                    500,
                    $"Internal server error: {ex.Message}",
                    null
                );

                return StatusCode(500, response);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Staff,Admin")]
        public async Task<ActionResult<ApiResponse<ResourceResponse>>> CreateResource([FromBody] CreateResourceRequest request)
        {
            try
            {
                _logger.LogInformation($"Creating new resource: {request.ResourceTitle}");

                var resource = await _resourceService.CreateResourceAsync(request);
                var response = ApiResponseBuilder.BuildResponse(
                    201,
                    "Resource created successfully.",
                    resource
                );

                return CreatedAtAction(nameof(GetResourceById), new { id = resource.ResourceId }, response);
            }

            catch (ArgumentException argEx)
            {
                _logger.LogWarning(argEx, "Validation error creating resource");
                var response = ApiResponseBuilder.BuildResponse<ResourceResponse>(
                    400,
                    $"Bad request: {argEx.Message}",
                    null
                );
                return BadRequest(response);
            }

            catch (InvalidOperationException invOpEx)
            {
                _logger.LogWarning(invOpEx, "Conflict error creating resource");
                var response = ApiResponseBuilder.BuildResponse<ResourceResponse>(
                    409,
                    $"Conflict: {invOpEx.Message}",
                    null
                );
                return Conflict(response);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating resource");
                var response = ApiResponseBuilder.BuildResponse<ResourceResponse>(
                    500,
                    $"Internal server error: {ex.Message}",
                    null
                );

                return StatusCode(500, response);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Staff,Admin")]
        public async Task<ActionResult<ApiResponse<UpdateResourceRequest>>> UpdateResource(int id, [FromBody] UpdateResourceRequest request)
        {
            try
            {
                if (id != request.ResourceId)
                {
                    var failureResponse = ApiResponseBuilder.BuildResponse<UpdateResourceRequest>(
                        400,
                        "Resource ID in URL does not match ID in request body.",
                        null
                    );

                    return BadRequest(failureResponse);
                }

                _logger.LogInformation($"Updating resource with ID: {id}");

                var updated = await _resourceService.UpdateResourceAsync(request);
                var successResponse = ApiResponseBuilder.BuildResponse(
                    200,
                    "Resource updated successfully.",
                    updated
                );

                return Ok(successResponse);
            }

            catch (ArgumentException argEx)
            {
                _logger.LogWarning(argEx, "Validation error updating resource");
                var response = ApiResponseBuilder.BuildResponse<bool>(
                    400,
                    $"Bad request: {argEx.Message}",
                    false
                );
                return BadRequest(response);
            }

            catch (KeyNotFoundException notFoundEx)
            {
                _logger.LogWarning(notFoundEx, "Resource not found for update");
                var response = ApiResponseBuilder.BuildResponse<bool>(
                    404,
                    $"Not found: {notFoundEx.Message}",
                    false
                );
                return NotFound(response);
            }

            catch (InvalidOperationException invOpEx)
            {
                _logger.LogWarning(invOpEx, "Conflict error updating resource");
                var response = ApiResponseBuilder.BuildResponse<bool>(
                    409,
                    $"Conflict: {invOpEx.Message}",
                    false
                );
                return Conflict(response);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating resource with ID {id}");
                var response = ApiResponseBuilder.BuildResponse<bool>(
                    500,
                    $"Internal server error: {ex.Message}",
                    false
                );
                return StatusCode(500, response);
            }
        }

        [HttpPut("mark/{id}")]
        [Authorize(Roles = "Staff,Admin,Student")]
        public async Task<ActionResult<ApiResponse<bool>>> MarkCompletedResource(int id)
        {
            try
            {

                _logger.LogInformation($"Marking resource with ID: {id}");

                var updated = await _resourceService.MarkCompletedResourceAsync(id);
                var successResponse = ApiResponseBuilder.BuildResponse(
                    200,
                    "Resource updated successfully.",
                    updated
                );

                return Ok(successResponse);
            }

            catch (ArgumentException argEx)
            {
                _logger.LogWarning(argEx, "Validation error updating resource");
                var response = ApiResponseBuilder.BuildResponse<bool>(
                    400,
                    $"Bad request: {argEx.Message}",
                    false
                );
                return BadRequest(response);
            }

            catch (KeyNotFoundException notFoundEx)
            {
                _logger.LogWarning(notFoundEx, "Resource not found for update");
                var response = ApiResponseBuilder.BuildResponse<bool>(
                    404,
                    $"Not found: {notFoundEx.Message}",
                    false
                );
                return NotFound(response);
            }

            catch (InvalidOperationException invOpEx)
            {
                _logger.LogWarning(invOpEx, "Conflict error updating resource");
                var response = ApiResponseBuilder.BuildResponse<bool>(
                    409,
                    $"Conflict: {invOpEx.Message}",
                    false
                );
                return Conflict(response);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating resource with ID {id}");
                var response = ApiResponseBuilder.BuildResponse<bool>(
                    500,
                    $"Internal server error: {ex.Message}",
                    false
                );
                return StatusCode(500, response);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Staff,Admin")]
        public async Task<ActionResult<ApiResponse<object>>> DeleteResource(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting resource with ID: {id}");

                var result = await _resourceService.DeleteResourceAsync(id);

                var response = ApiResponseBuilder.BuildResponse(
                    200,
                    "Resource deleted successfully.",
                    new { deletedId = id }
                );

                return Ok(response);
            }

            catch (KeyNotFoundException notFoundEx)
            {
                _logger.LogWarning(notFoundEx, "Resource not found for deletion");
                var response = ApiResponseBuilder.BuildResponse<object>(
                    404,
                    $"Not found: {notFoundEx.Message}",
                    false
                );

                return NotFound(response);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting resource with ID {id}");
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