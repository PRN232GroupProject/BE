using BusinessObjects.Context;
using BusinessObjects.DAO.Base.Implements;
using BusinessObjects.DAO.Interfaces;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessObjects.DAO.Implements
{
    public class ResourceDAO : GenericRepository<Resource>, IResourceDAO
    {
        public ResourceDAO(ChemProjectDbContext context) : base(context)
        {
        }

        public async Task<Resource?> CreateResourceAsync(Resource resource)
        {
            try
            {
                var exists = await CheckExistingResourceAsync(resource.Title);
                if (exists)
                {
                    return null;
                }
                await _context.Resources.AddAsync(resource);
                await _context.SaveChangesAsync();
                return resource;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating resource: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<bool> DeleteResourceAsync(int resourceId)
        {
            try
            {
                var resource = await _context.Resources.FindAsync(resourceId);
                if (resource == null)
                {
                    return false;
                }
                _context.Resources.Remove(resource);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting resource: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<List<Resource>> GetAllResourcesAsync()
        {
            try
            {
                return await _context.Resources
                    .AsNoTracking()
                    .OrderBy(r => r.Title)
                    .ThenBy(r => r.LessonId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving all resources: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<Resource?> GetResourceByIdAsync(int resourceId)
        {
            try
            {
                return await _context.Resources
                    .AsNoTracking()
                    .FirstOrDefaultAsync(r => r.Id == resourceId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving resource by ID: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<bool> UpdateResourceAsync(Resource resource)
        {
            try
            {
                if (resource == null)
                {
                    return false;
                }

                var exists = await CheckExistingResourceAsync(resource.Title);
                if (exists)
                {
                    Console.WriteLine("Resource title already exists.");
                    return false;
                }

                var existingResource = _context.Resources.Local.FirstOrDefault(r => r.Id == resource.Id);
                if (existingResource != null)
                {
                    _context.Entry(existingResource).State = EntityState.Detached;
                }

                _context.Resources.Update(resource);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating resource: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<bool> CheckExistingResourceAsync(string resourceTitle)
        {
            try
            {
                var existingTitle = _context.Resources
                    .AsNoTracking()
                    .AnyAsync(r => r.Title.ToLower() == resourceTitle.ToLower());

                if (existingTitle != null)
                {
                    return await existingTitle;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking existing resource: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task<bool> MarkCompletedResourceAsnyc(int resourceId)
        {
            try
            {
                var resource = await GetResourceByIdAsync(resourceId);

                if (resource == null)
                {
                    return false;
                }
                    
                resource.IsCompleted = true;
                resource.CompletedAt = DateTime.UtcNow;

                await UpdateAsync(resource);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error marking resource: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }
    }
}
