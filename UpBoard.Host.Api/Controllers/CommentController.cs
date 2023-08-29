using Board.Contracts.Comment;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UpBoard.AppServices.Services.Comment;
using UpBoard.Contracts.Comment;

namespace Doska.API.Controllers
{
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
        [HttpGet("/allComments")]
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
        [HttpGet("/CommentsForUser")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCommentResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCommentsForUser(Guid userId, CancellationToken cancellation)
        {
            var result = await _commentService.GetAllCommentsForUser(userId,cancellation);

            return Ok(result);
        }

        /// <summary>
        /// Создание комментария
        /// </summary>
        /// <param name="request">Данные для создания комментария</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns></returns>
        [HttpPost("/createComment")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCommentResponse>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateComment([FromQuery]CreateCommentRequest request, CancellationToken cancellation)
        {

            var result = await _commentService.CreateCommentAsync(request,cancellation);

            return Created("", result);
        }

        /// <summary>
        /// Удаление комментария
        /// </summary>
        /// <param name="request">Данные для удаления комментария</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns></returns>
        [HttpDelete("/deleteComment")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteComment([FromQuery]DeleteCommentRequest request, CancellationToken cancellation)
        {
            await _commentService.DeleteAsync(request,cancellation);
            return Ok();
        }
    }
}
