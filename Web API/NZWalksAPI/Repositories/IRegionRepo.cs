using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public interface IRegionRepo
    {
        Task<List<Region>> GetAllAsync();
        Task<Region?> GetByIdAsync(Guid id);
        Task<Region?> DeleteRegionAsync(Guid id);
        Task<Region> CreateRegionAsync(Region region);
        Task<Region?> UpdateRegionAsync(Guid id, Region region);
    }
}
