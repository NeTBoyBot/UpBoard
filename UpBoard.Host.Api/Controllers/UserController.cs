using Board.Contracts.Category;
using Board.Contracts.User;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UpBoard.Application.AppData.Contexts.User.Services;
using UpBoard.Contracts.Category;
using UpBoard.Contracts.User;

namespace UpBoard.Host.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с пользователями
    /// </summary>
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Получение всех пользователей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoUserResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAll();

            return Ok(result);
        }

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="request">Данные для создания пользователя</param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(IReadOnlyCollection<Guid>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserRequest request, CancellationToken cancellation)
        {
            var result = await _userService.CreateUserAsync(request, cancellation);

            return Created("",result);
        }

        /// <summary>
        /// Обновление категории
        /// </summary>
        /// <param name="request">Данные для изменения пользователя</param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoUserResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateCategory([FromQuery] EditUserRequest request, CancellationToken cancellation)
        {

            var result = await _userService.EditUserAsync(request, cancellation);

            return Ok(result);
        }

        /// <summary>
        /// Удаление категории
        /// </summary>
        /// <param name="request">Данные для удаления пользователя</param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteAd([FromQuery] DeleteUserRequest request, CancellationToken cancellation)
        {

            await _userService.DeleteAsync(request, cancellation);
            return Ok();
        }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        /// <param name="request"></param>
        /// <param name="Canctoken"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Login(LoginUserRequest request, CancellationToken Canctoken)
        {
            var token = await _userService.Login(request, Canctoken);
            return Created("", token);
        }
        /// <summary>
        /// Получение авторизованного пользователя
        /// </summary>
        /// <param name="token">Токен отмены</param>
        /// <returns>Информация об авторизованном пользователе</returns>
        [HttpGet("current")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoUserResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCurentUser(CancellationToken token)
        {
            var result = await _userService.GetCurrentUser(token);

            return Ok(result);
        }
    }
}
