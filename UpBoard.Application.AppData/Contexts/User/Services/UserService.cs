using AutoMapper;
using Board.Contracts.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UpBoard.Application.AppData.Contexts.User.Repositories;
using UpBoard.Contracts.User;

namespace UpBoard.Application.AppData.Contexts.User.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository,
            IMapper mapper,
            ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
        }

        ///<inheritdoc/>
        public async Task<Guid> CreateUserAsync(RegisterUserRequest registerUser, CancellationToken cancellation)
        {
            _logger.LogInformation($"Создание пользователя");
            
            var userId = await _userRepository.AddAsync(registerUser, cancellation);

            return userId;
        }

        ///<inheritdoc/>
        public async Task DeleteAsync(DeleteUserRequest request, CancellationToken cancellation)
        {
            _logger.LogInformation($"Удаление пользователя под id {request.Id}");

            await _userRepository.DeleteAsync(request, cancellation);
        }

        ///<inheritdoc/>
        public async Task<InfoUserResponse> EditUserAsync(EditUserRequest request, CancellationToken cancellation)
        {
            _logger.LogInformation($"Изменение пользователя под id {request.Id}");

            await _userRepository.EditUserAsync(_mapper.Map<EditUserRequest>(request), cancellation);

            return _mapper.Map<InfoUserResponse>(request);
        }

        ///<inheritdoc/>
        public async Task<IReadOnlyCollection<InfoUserResponse>> GetAll()
        {
            var result = await (await _userRepository.GetAll())
                .OrderBy(a => a.Id).ToListAsync();

            _logger.LogInformation($"Получение всех пользователей");

            return result;
        }

        ///<inheritdoc/>
        public async Task<InfoUserResponse> GetByIdAsync(Guid id, CancellationToken cancellation)
        {
            _logger.LogInformation($"Получение пользователя под id {id}");

            var existingUser = await _userRepository.FindById(id, cancellation);
            return _mapper.Map<InfoUserResponse>(existingUser);
        }
    }
}
