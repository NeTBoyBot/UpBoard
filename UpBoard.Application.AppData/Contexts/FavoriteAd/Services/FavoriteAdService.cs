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
    ///<inheritdoc cref="IFavoriteAdService"/>
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

        ///<inheritdoc/>
        public async Task<Guid> CreateFavoriteAdAsync(CreateFavoriteAdRequest createAd, CancellationToken cancellation)
        {

            var id = (await _userService.GetCurrentUser(cancellation)).Id;

            _logger.LogInformation($"Добавление объявления под id {createAd.AdvertisementId} в избранные пользователя под id {id}");

            var adId = await _favoriteadRepository.AddAsync(createAd,id, cancellation);

            return adId;
        }

        ///<inheritdoc/>
        public async Task DeleteAsync(DeleteFavoriteAdRequest request, CancellationToken cancellation)
        {
            _logger.LogInformation($"Удаление объявления под id {request.Id}");

            await _favoriteadRepository.DeleteAsync(request, cancellation);
        }

        ///<inheritdoc/>
        public async Task<IQueryable<InfoFavoriteAdResponse>> GetAll()
        {
            _logger.LogInformation($"Получение всех избранных объявлений");

            return (await _favoriteadRepository.GetAll());
        }

        ///<inheritdoc/>
        public async Task<IQueryable<InfoFavoriteAdResponse>> GetAllUserFavorites(CancellationToken token)
        {
            var userId = (await _userService.GetCurrentUser(token)).Id;

            _logger.LogInformation($"Получение всех избранных объявлений пользователя под id {userId}");

            return (await _favoriteadRepository.GetAll()).ToList().Where(a => a.UserId == userId).AsQueryable();
        }

        ///<inheritdoc/>
        public async Task<InfoFavoriteAdResponse> GetByIdAsync(Guid id, CancellationToken cancellation)
        {
            _logger.LogInformation($"Получение избранного объявления под id {id}");

            var existingad = await _favoriteadRepository.FindById(id, cancellation);
            return existingad;
        }
    }
}
