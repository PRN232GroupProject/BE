using BusinessObjects.DAO.Interfaces;
using BusinessObjects.Entities;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implements
{
    public class ResourceRepository : IResourceRepository
    {
        private readonly IResourceDAO _resourceDao;

        public ResourceRepository(IResourceDAO resourceDao)
        {
            _resourceDao = resourceDao;
        }

        public async Task<bool> CheckExistingResourceAsync(string resourceTitle)
        {
            return await _resourceDao.CheckExistingResourceAsync(resourceTitle);
        }

        public async Task<Resource?> CreateResourceAsync(Resource resource)
        {
            return await _resourceDao.CreateResourceAsync(resource);
        }

        public async Task<bool> DeleteResourceAsync(int resourceId)
        {
            return await _resourceDao.DeleteResourceAsync(resourceId);
        }

        public async Task<List<Resource>> GetAllResourcesAsync()
        {
            return await _resourceDao.GetAllResourcesAsync();
        }

        public async Task<Resource?> GetResourceByIdAsync(int resourceId)
        {
            return await _resourceDao.GetResourceByIdAsync(resourceId);
        }

        public async Task<bool> UpdateResourceAsync(Resource resource)
        {
            return await _resourceDao.UpdateResourceAsync(resource);
        }
    }
}
