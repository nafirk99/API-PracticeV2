using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
       Task<Walk> CreateAsync(Walk walk);
       // These two will be by default null, isAscending is Default to Ascending
       Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true); 
       Task<Walk?> GetByIdAsync(Guid id);
       Task<Walk?> UpdateAsync(Guid id, Walk walk);
       Task<Walk?> DeleteAsync(Guid id);
    }
}
