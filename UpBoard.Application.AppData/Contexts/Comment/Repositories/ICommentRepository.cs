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
        Task<InfoCommentResponse> FindById(Guid id, CancellationToken cancellation);

        Task<IQueryable<InfoCommentResponse>> GetAll();

        Task<Guid> AddAsync(CreateCommentRequest request, CancellationToken cancellation);

        Task DeleteAsync(DeleteCommentRequest request, CancellationToken cancellation);
    }
}
