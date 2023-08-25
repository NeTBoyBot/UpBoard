using Board.Contracts.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpBoard.Contracts.File;

namespace UpBoard.Application.AppData.Contexts.File.Services
{
    /// <summary>
    /// Сервис для работы с файлами
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Получение файла по id
        /// </summary>
        /// <param name="id">Id файла</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns></returns>
        Task<InfoFileResponse> GetByIdAsync(Guid id, CancellationToken cancellation);

        /// <summary>
        /// Создание файла
        /// </summary>
        /// <param name="file">ДТО файла</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns></returns>
        Task<Guid> CreateFileAsync(CreateFileRequest file, CancellationToken cancellation);

        /// <summary>
        /// Удаление файла
        /// </summary>
        /// <param name="request">ДТО для удаления файла</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns></returns>
        Task DeleteAsync(DeleteFileRequest request, CancellationToken cancellation);
    }
}
