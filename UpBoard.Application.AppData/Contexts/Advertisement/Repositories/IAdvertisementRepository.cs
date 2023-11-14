using Board.Contracts.Ad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpBoard.Contracts.Ad;

namespace UpBoard.Application.AppData.Contexts.Advertisement.Repositories
{
    public interface IAdvertisementRepository
    {
        /// <summary>
        /// Поиск объявления по Id
        /// </summary>
        /// <param name="id">Id объявления</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns></returns>
        Task<InfoAdResponse> FindById(Guid id, CancellationToken cancellation);

        /// <summary>
        /// Получение всех объявлений
        /// </summary>
        /// <returns></returns>
        Task<IQueryable<InfoAdResponse>> GetAll();

        /// <summary>
        /// Создание объявления
        /// </summary>
        /// <param name="model">ДТО создания объявления</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns></returns>
        Task<Guid> AddAsync(CreateAdvertisementRequest model, Guid ownerId, CancellationToken cancellation);

        /// <summary>
        /// Удаление объявления
        /// </summary>
        /// <param name="request">ДТО удаления объявления</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns></returns>
        Task DeleteAsync(DeleteAdRequest request, CancellationToken cancellation);

        /// <summary>
        /// Изменение объявления
        /// </summary>
        /// <param name="request">ДТО изменения объявления</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns></returns>
        Task EditAdAsync(UpdateAdRequest request, CancellationToken cancellation);
        
    }
}
