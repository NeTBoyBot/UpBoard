using UpBoard.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Board.Contracts.User;
using UpBoard.Contracts.User;

namespace UpBoard.Application.AppData.Contexts.User.Repositories
{
    public interface IUserRepository
    {
        Task<InfoUserResponse> FindById(Guid id, CancellationToken cancellation);

        Task<IQueryable<InfoUserResponse>> GetAll();

        Task<Guid> AddAsync(RegisterUserRequest model, CancellationToken cancellation);

        Task DeleteAsync(DeleteUserRequest model, CancellationToken cancellation);

        Task EditUserAsync(EditUserRequest model, CancellationToken cancellation);

    }
}
