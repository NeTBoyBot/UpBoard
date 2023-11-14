using UpBoard.Domain;
using Microsoft.EntityFrameworkCore;
using UpBoard.Infrastucture.Repository;
using UpBoard.Application.AppData.Contexts.User.Repositories;
using Board.Contracts.User;
using AutoMapper;
using UpBoard.Contracts.User;
using System.Linq.Expressions;

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
        public async Task<Guid> AddAsync(RegisterUserRequest model,CancellationToken cancellation)
        {
            var current = (await _baseRepository.GetAllFiltered(u =>u.Email == model.Email || u.PhoneNumber == model.PhoneNumber)).FirstOrDefault();

            if (current != null)
                throw new Exception("User with this data is registered");

            var user = _mapper.Map<User>(model);
            user.Registrationdate = DateTime.UtcNow;
            user.VerificationCode = new Random().Next(0, 10000);

            _baseRepository.AddAsync(user, cancellation);

            return user.Id;
        }

        ///<inheritdoc/>
        public Task DeleteAsync(DeleteUserRequest request, CancellationToken cancellation)
        {
            var user = _mapper.Map<User>(request);
            return _baseRepository.DeleteAsync(user, cancellation);
        }

        ///<inheritdoc/>
        public async Task EditUserAsync(EditUserRequest edit, CancellationToken cancellation)
        {
            var user = await _baseRepository.GetByIdAsync(edit.Id,cancellation);

            user.PhoneNumber = edit.PhoneNumber;
            user.Username = edit.Username;
            user.Password = edit.Password;
            user.Email = edit.Email;

            await _baseRepository.UpdateAsync(user,cancellation);
        }

        public async Task VerifyUserAsync(Guid id,CancellationToken cancellation)
        {
            var user = await _baseRepository.GetByIdAsync(id, cancellation);

            user.UserState = UpBoard.Domain.UserStates.UserStates.Verified;

            await _baseRepository.UpdateAsync(user, cancellation);

        }

        ///<inheritdoc/>
        public async Task<InfoUserResponse> FindById(Guid id, CancellationToken cancellation)
        {
            var user = await (await _baseRepository.GetAll()).Where(i => i.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<InfoUserResponse>(user);
        }

        ///<inheritdoc/>
        public async Task<IQueryable<InfoUserResponse>> GetAll()
        {
            return (await _baseRepository.GetAll()).Select(u=>_mapper.Map< InfoUserResponse>(u));
        }

        ///<inheritdoc/>
        public async Task<InfoUserResponse> Login(LoginUserRequest request, CancellationToken cancellation)
        {
            var users = await _baseRepository.GetAllFiltered(u => u.Email == request.Email && u.Password == request.Password);

            var user = users.FirstOrDefault();

            if (user == null)
                throw new Exception("User not found");

            return _mapper.Map<InfoUserResponse>(user);
        }
    }
}
