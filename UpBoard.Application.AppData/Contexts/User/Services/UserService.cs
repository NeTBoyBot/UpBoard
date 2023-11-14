using AutoMapper;
using Board.Contracts.File;
using Board.Contracts.User;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using UpBoard.Application.AppData.Contexts.User.Repositories;
using UpBoard.Contracts.User;

namespace UpBoard.Application.AppData.Contexts.User.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UserService> _logger;
        private readonly IHttpContextAccessor _httpContext;
        private IConfiguration _configuration;

        public UserService(IUserRepository userRepository,
            IMapper mapper,
            ILogger<UserService> logger,
            IHttpContextAccessor httpContext,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
            _httpContext = httpContext;
            _configuration = configuration;
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
        public async Task<IQueryable<InfoUserResponse>> GetAll()
        {
            var result = await _userRepository.GetAll();

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

        public async Task<string> Login(LoginUserRequest request, CancellationToken cancellationToken)
        {
            InfoUserResponse existingUser = await _userRepository.Login(request,cancellationToken);

            if (existingUser == null)
                throw new InvalidOperationException($"Пользователя с почтой '{request.Email}' не существует");


            IList<Claim> claimPack = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, existingUser.Id.ToString()),
                new Claim(ClaimTypes.Role, existingUser.UserName),
                new Claim(ClaimTypes.Email, existingUser.Email)
            };

            string? secretKey = _configuration["Jwt:Key"];

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            SigningCredentials signCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claimPack,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: signCredentials
                );


            string result = new JwtSecurityTokenHandler().WriteToken(token);
            return result;
        }
        public async Task<InfoUserResponse> GetCurrentUser(CancellationToken cancellation)
        {
            //_logger.LogInformation($"Получение авторизованного пользователя");

            var claims = _httpContext.HttpContext.User.Claims;
            var claimId = claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(claimId))
            {
                return null;
            }

            var id = Guid.Parse(claimId);
            var user = await _userRepository.FindById(id, cancellation);

            if (user == null)
            {
                throw new Exception($"Не найден пользователь с идентификатором '{id}'.");
            }

            //TODO
            //var result = new InfoUserResponse
            //{
            //    Id = user.Id,
            //    UserName = user.UserName,
            //    Registrationdate = user.Registrationdate,
            //    Email = user.Email,
            //    PhoneNumber = user.PhoneNumber,
            //    IsVerified = user.IsVerified
            //};

            return user;
        }

        public async Task<string> VerifyUserAsync(Guid id, int VerificationCode, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Подтверждение аккаунта под id {id}");

            var user = await _userRepository.FindById(id, cancellationToken);

            if (user == null)
                throw new Exception("Данного пользователя не существует!");

            if (user.IsVerified)
                throw new Exception("Аккаунт пользователя уже подтверждён");

            if (user.VerificationCode.Equals(VerificationCode))
            {
                user.IsVerified = true;
                await _userRepository.VerifyUserAsync(id, cancellationToken);
            }

            return "Аккаунт был успешно подтверждён";
        }
    }
}
