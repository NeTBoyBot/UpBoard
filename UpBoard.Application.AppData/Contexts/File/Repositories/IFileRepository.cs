using Board.Contracts.File;
using Board.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpBoard.Contracts.File;

namespace UpBoard.Application.AppData.Contexts.File.Repositories
{
    public interface IFileRepository
    {
        /// <summary>
        /// Поиск файла по Id
        /// </summary>
        /// <param name="id">Id файла</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns></returns>
        Task<InfoFileResponse> FindById(Guid id, CancellationToken cancellation);

        /// <summary>
        /// Добавление файла
        /// </summary>
        /// <param name="model">ДТО файла</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns></returns>
        Task<Guid> AddAsync(CreateFileRequest model, CancellationToken cancellation);

        /// <summary>
        /// Удаление файла
        /// </summary>
        /// <param name="model">ДТО удаления файла</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns></returns>
        Task DeleteAsync(DeleteFileRequest model, CancellationToken cancellation);
    }
}
