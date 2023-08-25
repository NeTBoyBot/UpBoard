using Board.Contracts.FavoriteAd;
using UpBoard.Contracts.FavoriteAd;

namespace UpBoard.Application.AppData.Contexts.FavoriteAd.Repositories
{
    public interface IFavoriteAdRepository
    {
        /// <summary>
        /// Поиск избранного объявления по Id
        /// </summary>
        /// <param name="id">Id объявления</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns></returns>
        Task<InfoFavoriteAdResponse> FindById(Guid id, CancellationToken cancellation);

        /// <summary>
        /// Получение всех избранных объявлений
        /// </summary>
        /// <returns></returns>
        Task<IQueryable<InfoFavoriteAdResponse>> GetAll();

        /// <summary>
        /// Добавление избранного объявлеп я
        /// </summary>
        /// <param name="request">ДТО избранного объявления</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns></returns>
        Task<Guid> AddAsync(CreateFavoriteAdRequest request, CancellationToken cancellation);

        /// <summary>
        /// Удаление избранного объявления
        /// </summary>
        /// <param name="request">ДТО удаления объявления</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns></returns>
        Task DeleteAsync(DeleteFavoriteAdRequest request, CancellationToken cancellation);
    }
}
