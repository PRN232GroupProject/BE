using BusinessObjects.DTO.Resource;
using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IResourceService
    {
        Task<List<ResourceResponse>> GetAllResourcesAsync();
        Task<ResourceResponse?> GetResourceByIdAsync(int resourceId);
        Task<ResourceResponse> CreateResourceAsync(CreateResourceRequest request);
        Task<ResourceResponse> UpdateResourceAsync(UpdateResourceRequest request);
        Task<bool> DeleteResourceAsync(int resourceId);
    }
}
