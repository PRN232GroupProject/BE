using BusinessObjects.DAO.Base.Interfaces;
using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DAO.Interfaces
{
    public interface IResourceDAO : IGenericRepository<Resource>
    {
        Task<List<Resource>> GetAllResourcesAsync();
        Task<Resource?> GetResourceByIdAsync(int resourceId);
        Task<Resource?> CreateResourceAsync(Resource resource);
        Task<bool> UpdateResourceAsync(Resource resource);
        Task<bool> DeleteResourceAsync(int resourceId);
        Task<bool> CheckExistingResourceAsync(string resourceTitle);
        Task<bool> MarkCompletedResourceAsnyc(int resourceId);
    }
}
