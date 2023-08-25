using UpBoard.Domain;
using UpBoard.Infrastucture.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpBoard.Application.AppData.Contexts.Comment.Repositories;
using AutoMapper;
using Board.Contracts.Comment;
using UpBoard.Contracts.Category;
using UpBoard.Contracts.Comment;
using System.Reflection.Metadata.Ecma335;

namespace Doska.DataAccess.Repositories
{
    ///<inheritdoc cref="ICommentRepository"/>
    public class CommentRepository : ICommentRepository
    {
        private readonly IRepository<Comment> _baseRepository;
        private readonly IMapper _mapper;

        public CommentRepository(IRepository<Comment> baseRepository,IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        ///<inheritdoc/>
        public Task<Guid> AddAsync(CreateCommentRequest model, CancellationToken cancellation)
        {
            var comment = _mapper.Map<Comment>(model);

            _baseRepository.AddAsync(comment,cancellation);

            return Task.Run(()=> comment.Id);
        }

        ///<inheritdoc/>
        public Task DeleteAsync(DeleteCommentRequest request, CancellationToken cancellation)
        {
            var comment = _mapper.Map<Comment>(request);

            return _baseRepository.DeleteAsync(comment,cancellation);
        }

        ///<inheritdoc/>
        public Task EditCommentAsync(UpdateCommentRequest request, CancellationToken cancellation)
        {
            var comment = _mapper.Map<Comment>(request);

            return _baseRepository.UpdateAsync(comment,cancellation);
        }

        ///<inheritdoc/>
        public async Task<InfoCommentResponse> FindById(Guid id, CancellationToken cancellation)
        {
            var comment = await _baseRepository.GetByIdAsync(id, cancellation);

            return _mapper.Map<InfoCommentResponse>(comment);
        }

        ///<inheritdoc/>
        public async Task<IQueryable<InfoCommentResponse>> GetAll()
        {
            return (await _baseRepository.GetAll()).Select(c=>new InfoCommentResponse
            {
                Id = c.Id,
                SenderId = c.SenderId,
                Text = c.Text,
                UserId = c.UserId
            }).AsQueryable();
        }
    }
}
