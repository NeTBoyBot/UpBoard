using UpBoard.Domain;
using Microsoft.EntityFrameworkCore;
using UpBoard.Infrastucture.Repository;
using UpBoard.Application.AppData.Contexts.User.Repositories;
using Board.Contracts.User;
using AutoMapper;
using UpBoard.Contracts.User;

namespace Doska.DataAccess.Repositories
{
    ///<inheritdoc cref="IUserRepository"/>
    public class UserRepository : IUserRepository
    {
        private readonly IRepository<User> _baseRepository;
        private readonly IMapper _mapper;

        public UserRepository(IRepository<User> baseRepository,IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        ///<inheritdoc/>
        public Task<Guid> AddAsync(RegisterUserRequest model,CancellationToken cancellation)
        {
            var user = _mapper.Map<User>(model);

            _baseRepository.AddAsync(user, cancellation);

            return Task.Run(() => user.Id);
        }

        ///<inheritdoc/>
        public Task DeleteAsync(DeleteUserRequest request, CancellationToken cancellation)
        {
            var user = _mapper.Map<User>(request);
            return _baseRepository.DeleteAsync(user, cancellation);
        }

        ///<inheritdoc/>
        public Task EditUserAsync(EditUserRequest edit, CancellationToken cancellation)
        {
            var user = _mapper.Map<User>(edit);
            return _baseRepository.UpdateAsync(user,cancellation);
        }

        ///<inheritdoc/>
        public async Task<InfoUserResponse> FindById(Guid id, CancellationToken cancellation)
        {
            var user = (await _baseRepository.GetAll()).Where(i => i.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<InfoUserResponse>(user);
        }

        ///<inheritdoc/>
        public async Task<IQueryable<InfoUserResponse>> GetAll()
        {
            return (await _baseRepository.GetAll()).Select(u=>_mapper.Map< InfoUserResponse>(u));
        }
    }
}
