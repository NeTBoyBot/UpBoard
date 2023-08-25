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

        Task<InfoFileResponse> FindById(Guid id, CancellationToken cancellation);

        Task<Guid> AddAsync(CreateFileRequest model, CancellationToken cancellation);

        Task DeleteAsync(DeleteFileRequest model, CancellationToken cancellation);
    }
}
