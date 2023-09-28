using Board.Contracts.File;
using Board.Contracts.User;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UpBoard.Application.AppData.Contexts.User.Repositories;
using UpBoard.Contracts.User;

namespace UpBoard.Application.AppData.Contexts.User.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Получить пользователя по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<InfoUserResponse> GetByIdAsync(Guid id, CancellationToken cancellation);

        /// <summary>
        /// Создание пользователя
        /// </summary>
        /// <param name="registerUser"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<Guid> CreateUserAsync(RegisterUserRequest registerUser, CancellationToken cancellation);

        /// <summary>
        /// Получение всех пользователей
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        Task<IQueryable<InfoUserResponse>> GetAll();

        /// <summary>
        /// Удаление пользователя по id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task DeleteAsync(DeleteUserRequest request, CancellationToken cancellation);

        /// <summary>
        /// Обновление информации о пользователе
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="editAd"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<InfoUserResponse> EditUserAsync(EditUserRequest request, CancellationToken cancellation);

        Task<InfoUserResponse> GetCurrentUser(CancellationToken cancellation);

        Task<string> Login(LoginUserRequest LoginUserRequest, CancellationToken cancellationToken);
        

    }
}
