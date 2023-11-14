using UpBoard.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Board.Contracts.Comment;
using UpBoard.Contracts.Comment;

namespace UpBoard.Application.AppData.Contexts.Comment.Repositories
{
    public interface ICommentRepository
    {
        /// <summary>
        /// Поиск комментария по Id
        /// </summary>
        /// <param name="id">Id комментария</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns></returns>
        Task<InfoCommentResponse> FindById(Guid id, CancellationToken cancellation);

        /// <summary>
        /// Получение всех комментариев
        /// </summary>
        /// <returns></returns>
        Task<IQueryable<InfoCommentResponse>> GetAll();

        /// <summary>
        /// Добавление комментария
        /// </summary>
        /// <param name="request">ДТО комментария</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns></returns>
        Task<Guid> AddAsync(CreateCommentRequest request,Guid senderId, CancellationToken cancellation);

        /// <summary>
        /// Удаление комментария
        /// </summary>
        /// <param name="request">ДТО удаления комментария</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns></returns>
        Task DeleteAsync(DeleteCommentRequest request, CancellationToken cancellation);
    }
}
