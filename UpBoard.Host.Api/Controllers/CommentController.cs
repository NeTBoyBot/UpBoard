using Board.Contracts.Comment;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UpBoard.AppServices.Services.Comment;
using UpBoard.Contracts.Comment;

namespace Doska.API.Controllers
{
    [Route("comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        /// <summary>
        /// Получение всех комментариев
        /// </summary>
        /// <param name="skip"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCommentResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _commentService.GetAll();

            return Ok(result);
        }

        /// <summary>
        /// Получение всех комментариев для пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns></returns>
        [HttpGet("comments/{id:guid}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCommentResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCommentsForUser(Guid userId, CancellationToken cancellation)
        {
            var result = await _commentService.GetAllCommentsForUser(userId, cancellation);

            return Ok(result);
        }

        /// <summary>
        /// Создание комментария
        /// </summary>
        /// <param name="request">Данные для создания комментария</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCommentResponse>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentRequest request, CancellationToken cancellation)
        {

            var result = await _commentService.CreateCommentAsync(request, cancellation);

            return Created("", result);
        }

        /// <summary>
        /// Удаление комментария
        /// </summary>
        /// <param name="request">Данные для удаления комментария</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteComment([FromQuery] DeleteCommentRequest request, CancellationToken cancellation)
        {
            await _commentService.DeleteAsync(request, cancellation);
            return Ok();
        }
    }
}
