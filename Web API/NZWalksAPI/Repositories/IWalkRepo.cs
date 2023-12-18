using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public interface IWalkRepo
    {
        Task<List<Walk>> GetAllAsync(string? filterOn = null,string? filterQuery = null,
                                        string? sortBy=null, bool isAscending = true, int pageNumber=1,int pageSize=1);
        Task<Walk?> GetByIdAsync(Guid id);
        Task<Walk> CreateWalkAsync(Walk w);
        Task<Walk?> UpdateWalkAsync(Guid id, Walk w);
        Task<Walk?> DeleteWalkAsync(Guid id);
    }
}
