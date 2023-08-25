using Board.Contracts.FavoriteAd;
using UpBoard.Contracts.FavoriteAd;

namespace UpBoard.Application.AppData.Contexts.FavoriteAd.Repositories
{
    public interface IFavoriteAdRepository
    {
        Task<InfoFavoriteAdResponse> FindById(Guid id, CancellationToken cancellation);

        Task<IQueryable<InfoFavoriteAdResponse>> GetAll();

        Task<Guid> AddAsync(CreateFavoriteAdRequest request, CancellationToken cancellation);

        Task DeleteAsync(DeleteFavoriteAdRequest request, CancellationToken cancellation);
    }
}
