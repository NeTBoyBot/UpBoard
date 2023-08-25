using UpBoard.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Board.Contracts.Category;
using UpBoard.Contracts.Category;

namespace UpBoard.Application.AppData.Contexts.Categories.Repositories
{
    public interface ICategoryRepository
    {
        /// <summary>
        /// Поиск категории по Id
        /// </summary>
        /// <param name="id">Id категории</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns></returns>
        Task<InfoCategoryResponse> FindById(Guid id, CancellationToken cancellation);

        /// <summary>
        /// Получение всех категорий
        /// </summary>
        /// <returns></returns>
        Task<IQueryable<InfoCategoryResponse>> GetAll();

        /// <summary>
        /// Добавление категории
        /// </summary>
        /// <param name="model">ДТО создания категории</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns></returns>
        Task<Guid> AddAsync(CreateCategoryRequest model, CancellationToken cancellation);

        /// <summary>
        /// Удаление категории
        /// </summary>
        /// <param name="reqest">ДТО удаления категории</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns></returns>
        Task DeleteAsync(DeleteCategoryRequest reqest, CancellationToken cancellation);

        /// <summary>
        /// Изменение категории
        /// </summary>
        /// <param name="request">ДТО изменения категории</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns></returns>
        Task EditAdAsync(UpdateCategoryRequest request, CancellationToken cancellation);
    }
}
