using BusinessObjects.DTO.Chapter;
using BusinessObjects.DTO.Resource;
using BusinessObjects.Entities;
using BusinessObjects.Mapper;
using Repository.Implements;
using Repository.Interfaces;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implements
{
    public class ResourceService : IResourceService
    {
        private readonly IResourceRepository _resourceRepository;
        private readonly IMapperlyMapper _mapper;

        public ResourceService(IResourceRepository resourceRepository, IMapperlyMapper mapper)
        {
            _resourceRepository = resourceRepository;
            _mapper = mapper;
        }

        public async Task<ResourceResponse> CreateResourceAsync(CreateResourceRequest request)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(request.ResourceTitle))
                {
                    throw new ArgumentException("Resource title is required.");
                }

                // Check if resource already exists
                var exists = await _resourceRepository.CheckExistingResourceAsync(request.ResourceTitle);
                if (exists)
                {
                    throw new InvalidOperationException($"Resource with title '{request.ResourceTitle}' already exists.");
                }

                // Map to entity
                var resource = _mapper.CreateResourceRequestToResource(request);
                Console.WriteLine($"Mapped resource - Title: {resource.Title}, Description: {resource.Description}");

                // Create resource
                var createdResource = await _resourceRepository.CreateResourceAsync(resource);
                if (createdResource == null)
                {
                    throw new InvalidOperationException("Failed to create resource. Resource may already exist.");
                }
                Console.WriteLine($"Resource created successfully with ID: {createdResource.Id}");

                // Map to response DTO
                var response = _mapper.ResourceToResourceResponse(createdResource);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating resource: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<bool> DeleteResourceAsync(int resourceId)
        {
            try
            {
                Console.WriteLine($"Deleting resource ID: {resourceId}");

                var resource = await _resourceRepository.GetResourceByIdAsync(resourceId);
                if (resource == null)
                {
                    throw new KeyNotFoundException($"Resource with ID {resourceId} not found.");
                }

                var result = await _resourceRepository.DeleteResourceAsync(resourceId);

                Console.WriteLine($"Delete result: {result}");

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DeleteResourceAsync: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<List<ResourceResponse>> GetAllResourcesAsync()
        {
            try
            {
                var resources = await _resourceRepository.GetAllResourcesAsync();
                return _mapper.ResourcesToResourceResponses(resources);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAllResourcesAsync: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<ResourceResponse?> GetResourceByIdAsync(int resourceId)
        {
            try
            {
                var resource = await _resourceRepository.GetResourceByIdAsync(resourceId);

                if (resource == null)
                {
                    return null;
                }

                return _mapper.ResourceToResourceResponse(resource);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetResourceByIdAsync: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<ResourceResponse> UpdateResourceAsync(UpdateResourceRequest request)
        {
            try
            {
                Console.WriteLine($"Updating resource ID: {request.ResourceId}, Title: {request.ResourceTitle}");

                // Validate input
                if (string.IsNullOrWhiteSpace(request.ResourceTitle))
                {
                    throw new ArgumentException("Resource title is required.");
                }

                // Get existing resource
                var existingResource = await _resourceRepository.GetResourceByIdAsync(request.ResourceId);
                if (existingResource == null)
                {
                    throw new KeyNotFoundException($"Resource with ID {request.ResourceId} not found.");
                }

                Console.WriteLine($"Found existing resource: {existingResource.Title}");

                // Check if another resource with same title exists
                var exists = await _resourceRepository.CheckExistingResourceAsync(request.ResourceTitle);

                if (exists)
                {
                    throw new InvalidOperationException($"Another resource with title '{request.ResourceTitle}' already exists.");
                }

                // Update properties manually to avoid tracking issues
                existingResource.Title = request.ResourceTitle;
                existingResource.Description = request.ResourceDescription;

                Console.WriteLine($"Updated properties - Title: {existingResource.Title}");

                // Save changes
                var isUpdated = await _resourceRepository.UpdateResourceAsync(existingResource);

                if (!isUpdated)
                {
                    throw new InvalidOperationException("Failed to update resource.");
                }

                Console.WriteLine("Resource updated successfully");

                // Return updated resource
                var response = _mapper.ResourceToResourceResponse(existingResource);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateResourceAsync: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw;
            }
        }
    }
}
