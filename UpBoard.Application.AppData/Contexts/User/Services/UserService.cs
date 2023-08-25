using AutoMapper;
using Board.Contracts.Category;
using Board.Contracts.User;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UpBoard.Application.AppData.Contexts.Advertisement.Repositories;
using UpBoard.Application.AppData.Contexts.User.Repositories;
using UpBoard.Contracts.User;

namespace UpBoard.Application.AppData.Contexts.User.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        private readonly IMemoryCache _cache;

        private const string UserCachingKey = "User";
        private const string UserIdCachingKey = "UserId";

        public UserService(IUserRepository userRepository,
            IMapper mapper, IConfiguration conf,
            ILogger<UserService> logger, IMemoryCache cache)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = conf;
            _logger = logger;
            _cache = cache;
        }

        public async Task<Guid> CreateUserAsync(RegisterUserRequest registerUser, CancellationToken cancellation)
        {
            _logger.LogInformation($"Создание пользователя");
            
            var userId = await _userRepository.AddAsync(registerUser, cancellation);

            return userId;
        }

        public async Task DeleteAsync(DeleteUserRequest request, CancellationToken cancellation)
        {
            _logger.LogInformation($"Удаление пользователя под id {request.Id}");

            await _userRepository.DeleteAsync(request, cancellation);
        }

        public async Task<InfoUserResponse> EditUserAsync(EditUserRequest request, CancellationToken cancellation)
        {
            _logger.LogInformation($"Изменение пользователя под id {request.Id}");

            await _userRepository.EditUserAsync(_mapper.Map<EditUserRequest>(request), cancellation);

            return _mapper.Map<InfoUserResponse>(request);
        }

        public async Task<IReadOnlyCollection<InfoUserResponse>> GetAll()
        {
            var result = await (await _userRepository.GetAll())
                .OrderBy(a => a.Id).ToListAsync();

            _logger.LogInformation($"Получение всех пользователей");

            return result;
        }

        public async Task<InfoUserResponse> GetByIdAsync(Guid id, CancellationToken cancellation)
        {
            _logger.LogInformation($"Получение пользователя под id {id}");

            var existingUser = await _userRepository.FindById(id, cancellation);
            return _mapper.Map<InfoUserResponse>(existingUser);
        }
    }
}
