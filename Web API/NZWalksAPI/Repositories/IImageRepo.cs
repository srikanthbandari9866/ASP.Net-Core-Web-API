using NZWalksAPI.Models.Domain;

namespace NZWalksAPI.Repositories
{
    public interface IImageRepo
    {
        Task<Image> ImageUpload(Image image);
    }
}
