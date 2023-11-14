using AutoMapper;
using Board.Contracts.Ad;
using Doska.AppServices.Services.Ad;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UpBoard.Application.AppData.Contexts.Advertisement.Repositories;
using UpBoard.Application.AppData.Contexts.User.Services;
using UpBoard.Contracts.Ad;
using static Newtonsoft.Json.JsonConvert;

namespace UpBoard.Application.AppData.Contexts.Advertisement.Services
{
    ///<inheritdoc cref="IAdvertisementService"/>
    public class AdvertisementService : IAdvertisementService
    {
        private readonly IAdvertisementRepository _adRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AdvertisementService> _logger;
        private readonly IUserService _userService;

        public AdvertisementService(IAdvertisementRepository adRepository, IMapper mapper,
             ILogger<AdvertisementService> logger, IUserService userService)
        {
            _adRepository = adRepository;
            _mapper = mapper;
            _logger = logger;
            _userService = userService;
        }

        ///<inheritdoc/>
        public async Task<Guid> CreateAdAsync(CreateAdvertisementRequest createAd, CancellationToken cancellation)
        {
            _logger.LogInformation($"Создание объявления {SerializeObject(createAd)}");

            var user = (await _userService.GetCurrentUser(cancellation));
            if (user == null)
                throw new Exception("User is not Authorized!");

            var userid = user.Id;


            var adId = await _adRepository.AddAsync(createAd,userid, cancellation);

            return adId;
        }

        ///<inheritdoc/>
        public async Task DeleteAsync(DeleteAdRequest request, CancellationToken cancellation)
        {
            _logger.LogInformation($"Удаление объявления под Id: {request.Id}");

            var user = (await _userService.GetCurrentUser(cancellation));
            if (user == null)
                throw new Exception("User is not Authorized!");

            var ad = await _adRepository.FindById(request.Id, cancellation);

            if (user.Id != ad.OwnerId)
                throw new Exception("You are not owner of this advertisement!");

            await _adRepository.DeleteAsync(request, cancellation);
        }

        ///<inheritdoc/>
        public async Task<InfoAdResponse> EditAdAsync(UpdateAdRequest request, CancellationToken cancellation)
        {
            _logger.LogInformation($"Изменение объявления под Id: {request.Id}, {SerializeObject(request)}");

            var user = (await _userService.GetCurrentUser(cancellation));
            if (user == null)
                throw new Exception("User is not Authorized!");

            var ad = await _adRepository.FindById(request.Id, cancellation);

            if (user.Id != ad.OwnerId)
                throw new Exception("You are not owner of this advertisement!");

            await _adRepository.EditAdAsync(request, cancellation);
            
            return _mapper.Map<InfoAdResponse>(request);
        }

        ///<inheritdoc/>
        public async Task<IQueryable<InfoAdResponse>> GetAdFiltered(string? name, Guid? subcategoryId)
        {
            _logger.LogInformation("Получение объявлений по фильтру");

            var query = await _adRepository.GetAll();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(q => q.Name == name);

            if (subcategoryId != null && subcategoryId != Guid.Empty)
                query = query.Where(q => q.CategoryId == subcategoryId);

            return query;
        }

        ///<inheritdoc/>
        public async Task<IQueryable<InfoAdResponse>> GetAllForPage(int pageSize, int pageIndex)
        {
            _logger.LogInformation("Получение всех объявлений");

            return (await _adRepository.GetAll())
                .Skip(pageSize*pageIndex).Take(pageSize);
        }

        ///<inheritdoc/>
        public async Task<InfoAdResponse> GetByIdAsync(Guid id, CancellationToken cancellation)
        {
            _logger.LogInformation($"Получение объявления под Id: {id}");

            var existingad = await _adRepository.FindById(id, cancellation);
          
            return existingad;
        }
        ///<inheritdoc/>
        public async Task<IQueryable<InfoAdResponse>> GetAllUserAds(Guid userId, CancellationToken token)
        {
            _logger.LogInformation($"Получение всех объявлений пользователя под Id: {userId}");

            return (await _adRepository.GetAll()).Where(a => a.OwnerId == userId);
        }
    }
}
