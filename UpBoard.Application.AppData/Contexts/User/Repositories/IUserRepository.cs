using UpBoard.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Board.Contracts.User;
using UpBoard.Contracts.User;
using System.Runtime.CompilerServices;

namespace UpBoard.Application.AppData.Contexts.User.Repositories
{
    public interface IUserRepository
    {
        /// <summary>
        /// Поиск пользователя по id
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns></returns>
        Task<InfoUserResponse> FindById(Guid id, CancellationToken cancellation);

        /// <summary>
        /// Получение всех пользователей
        /// </summary>
        /// <returns></returns>
        Task<IQueryable<InfoUserResponse>> GetAll();

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="model">ДТО пользователя</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns></returns>
        Task<Guid> AddAsync(RegisterUserRequest model, CancellationToken cancellation);

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="model">ДТО удаления пользователя</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns></returns>
        Task DeleteAsync(DeleteUserRequest model, CancellationToken cancellation);

        /// <summary>
        /// Изменение данных о пользователе
        /// </summary>
        /// <param name="model">ДТО изменения данных</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns></returns>
        Task EditUserAsync(EditUserRequest model, CancellationToken cancellation);

        /// <summary>
        /// Метод для получения пользователя по логину и паролю
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task<InfoUserResponse> Login(LoginUserRequest request, CancellationToken cancellation);

        /// <summary>
        /// Верификация пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        Task VerifyUserAsync(Guid id, CancellationToken cancellation);

    }
}
