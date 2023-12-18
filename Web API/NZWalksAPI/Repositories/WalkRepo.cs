using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Data;
using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public class WalkRepo : IWalkRepo
    {
        private readonly NZWalksDbContext _dbContext;
        public WalkRepo(NZWalksDbContext dbContext)
        {
              _dbContext = dbContext;
        }

        public async Task<Walk> CreateWalkAsync(Walk w)
        {
            await _dbContext.Walks.AddAsync(w);
            await _dbContext.SaveChangesAsync();
            
            return w;   
        }

        public async Task<Walk?> DeleteWalkAsync(Guid id)
        {
            var walk = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (walk == null)
            {
                return null;
            }

            _dbContext.Walks.Remove(walk);
            await _dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
                                                    string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 1)
        {
            var walks = _dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();
            // Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase)) 
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            //Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
                if(sortBy.Equals("LengthInKm", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAscending ? walks.OrderBy(x => x.LengthInKm) : walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return await walks.Skip(skipResults).Take(pageSize).ToListAsync();

            //return await _dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
            ////return await _dbContext.Walks.Include(x => x.Difficulty).Include(x => x.Region).ToListAsync();
        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            //var walk = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            var walk = await _dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
            if(walk == null)
            {
                return null;
            }
            return walk;
        }

        public async Task<Walk?> UpdateWalkAsync(Guid id, Walk w)
        {
            var walk = _dbContext.Walks.FirstOrDefault(x => x.Id == id);
            if (walk == null)
            {
                return null;
            }
            walk.Name = w.Name;
            walk.Description = w.Description;
            walk.LengthInKm = w.LengthInKm;
            walk.WalkImageUrl = w.WalkImageUrl;
            walk.DifficultyId = w.DifficultyId;
            walk.RegionId = w.RegionId;
            await _dbContext.SaveChangesAsync();

            return walk;
        }
    }
}
