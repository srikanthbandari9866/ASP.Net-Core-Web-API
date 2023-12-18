using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    
    public class RegionRepo : IRegionRepo
    {
        private readonly NZWalksDbContext _dbcontect;
        public RegionRepo(NZWalksDbContext dbContext)
        {
            _dbcontect = dbContext;
        }

        public async Task<Region> CreateRegionAsync(Region region)
        {
            await _dbcontect.Regions.AddAsync(region);
            await _dbcontect.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteRegionAsync(Guid id)
        {
            var exits = await _dbcontect.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (exits == null)
            {
                return null;
            }

            _dbcontect.Regions.Remove(exits);
            await _dbcontect.SaveChangesAsync();
            return exits;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await _dbcontect.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
            return await _dbcontect.Regions.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Region?> UpdateRegionAsync(Guid id, Region region)
        {
           var exits = await _dbcontect.Regions.FirstOrDefaultAsync(x => x.Id == id); 

            if (exits == null)
            {
                return null;
            }

            exits.Code = region.Code;
            exits.Name = region.Name;
            exits.RegionImageUrl = region.RegionImageUrl;

            await _dbcontect.SaveChangesAsync();
            return exits;
        }
    }
}
