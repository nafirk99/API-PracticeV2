using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IImgageRepository
    {
        Task<Image> Upload(Image image);
    }
}
