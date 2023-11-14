using AutoMapper;
using Board.Contracts.Comment;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpBoard.Domain;
using UpBoard.Application.AppData.Contexts.Comment.Repositories;
using UpBoard.Application.AppData.Contexts.User.Services;
using UpBoard.Contracts.Comment;

namespace UpBoard.AppServices.Services.Comment
{
    ///<inheritdoc cref="ICommentService"/>
    public class CommentService : ICommentService
    {
        private readonly IUserService _userService;
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CommentService> _logger;

        public CommentService(ICommentRepository commentRepository, IMapper mapper, ILogger<CommentService> logger,
            IUserService userService)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _logger = logger;
            _userService = userService;
        }

        ///<inheritdoc/>
        public async Task<Guid> CreateCommentAsync(CreateCommentRequest createComment,CancellationToken cancellation)
        {
            _logger.LogInformation($"Создание комментария");

            var id = (await _userService.GetCurrentUser(cancellation)).Id;

            var commentId = await _commentRepository.AddAsync(createComment,id,cancellation);

            return commentId;
        }

        ///<inheritdoc/>
        public async Task DeleteAsync(DeleteCommentRequest request, CancellationToken cancellation)
        {
            _logger.LogInformation($"Удаление комментария под id {request.Id}");
            
            await _commentRepository.DeleteAsync(request,cancellation);
        }

        ///<inheritdoc/>
        public async Task<IQueryable<InfoCommentResponse>> GetAll()
        {
            _logger.LogInformation($"Получение всех комментариев");

            return (await _commentRepository.GetAll());
        }

        ///<inheritdoc/>
        public async Task<IQueryable<InfoCommentResponse>> GetAllCommentsForUser(Guid userId, CancellationToken cancellation)
        {
            _logger.LogInformation($"Получение всех комментариев пользователя под id {userId}");

            return (await _commentRepository.GetAll()).Where(a => a.UserId == userId);
        }

        ///<inheritdoc/>
        public async Task<InfoCommentResponse> GetByIdAsync(Guid id, CancellationToken cancellation)
        {
            _logger.LogInformation($"Получение комментария под id {id}");

            var existingcomment = await _commentRepository.FindById(id,cancellation);
            return _mapper.Map<InfoCommentResponse>(existingcomment);
        }
    }
}
