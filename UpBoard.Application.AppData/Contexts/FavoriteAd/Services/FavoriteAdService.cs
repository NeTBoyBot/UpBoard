using AutoMapper;
using Board.Contracts.FavoriteAd;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpBoard.Application.AppData.Contexts.FavoriteAd.Repositories;
using UpBoard.Application.AppData.Contexts.User.Services;
using UpBoard.Contracts.FavoriteAd;

namespace UpBoard.Application.AppData.Contexts.FavoriteAd.Services
{
    public class FavoriteAdService : IFavoriteAdService
    {
        public readonly IFavoriteAdRepository _favoriteadRepository;
        public readonly IUserService _userService;
        public readonly ILogger<FavoriteAdService> _logger;

        public FavoriteAdService(IFavoriteAdRepository favoriteadRepository, IUserService userService, ILogger<FavoriteAdService> logger)
        {
            _favoriteadRepository = favoriteadRepository;
            _userService = userService;
            _logger = logger;
        }

        public async Task<Guid> CreateFavoriteAdAsync(CreateFavoriteAdRequest createAd, CancellationToken cancellation)
        {

            _logger.LogInformation($"Добавление объявления под id {createAd.AdvertisementId} в избранные пользователя под id {createAd.UserId}");

            var adId = await _favoriteadRepository.AddAsync(createAd, cancellation);

            return adId;
        }

        public async Task DeleteAsync(DeleteFavoriteAdRequest request, CancellationToken cancellation)
        {
            _logger.LogInformation($"Удаление объявления под id {request.Id}");

            await _favoriteadRepository.DeleteAsync(request, cancellation);
        }

        public async Task<IReadOnlyCollection<InfoFavoriteAdResponse>> GetAll()
        {
            _logger.LogInformation($"Получение всех избранных объявлений");

            return await (await _favoriteadRepository.GetAll())
                .OrderBy(a => a.Id).ToListAsync();
        }

        public async Task<IReadOnlyCollection<InfoFavoriteAdResponse>> GetAllUserFavorites(Guid userId, CancellationToken token)
        {
            _logger.LogInformation($"Получение всех избранных объявлений пользователя под id {userId}");

            return await (await _favoriteadRepository.GetAll()).Where(a => a.UserId == userId)
                .ToListAsync();
        }

        public async Task<InfoFavoriteAdResponse> GetByIdAsync(Guid id, CancellationToken cancellation)
        {
            _logger.LogInformation($"Получение избранного объявления под id {id}");

            var existingad = await _favoriteadRepository.FindById(id, cancellation);
            return existingad;
        }
    }
}
