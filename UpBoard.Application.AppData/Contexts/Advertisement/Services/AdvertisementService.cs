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
    public class AdvertisementService : IAdvertisementService
    {
        public readonly IAdvertisementRepository _adRepository;
        public readonly IMapper _mapper;
        public readonly ILogger<AdvertisementService> _logger;

        public AdvertisementService(IAdvertisementRepository adRepository, IMapper mapper,
             ILogger<AdvertisementService> logger)
        {
            _adRepository = adRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Guid> CreateAdAsync(CreateAdvertisementRequest createAd, CancellationToken cancellation)
        {
            _logger.LogInformation($"Создание объявления {SerializeObject(createAd)}");

            var adId = await _adRepository.AddAsync(createAd, cancellation);

            return adId;
        }

        public async Task DeleteAsync(DeleteAdRequest request, CancellationToken cancellation)
        {
            _logger.LogInformation($"Удаление объявления под Id: {request.Id}");

            await _adRepository.DeleteAsync(request, cancellation);
        }

        public async Task<InfoAdResponse> EditAdAsync(UpdateAdRequest request, CancellationToken cancellation)
        {
            _logger.LogInformation($"Изменение объявления под Id: {request.Id}, {SerializeObject(request)}");

            await _adRepository.EditAdAsync(request, cancellation);
            
            return _mapper.Map<InfoAdResponse>(request);
        }

        public async Task<IReadOnlyCollection<InfoAdResponse>> GetAdFiltered(string? name, Guid? subcategoryId)
        {
            _logger.LogInformation("Получение объявлений по фильтру");

            var query = await _adRepository.GetAll();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(q => q.Name == name);

            if (subcategoryId != null && subcategoryId != Guid.Empty)
                query = query.Where(q => q.CategoryId == subcategoryId);

            return await query.OrderBy(a => a.CreationDate).ToListAsync();
        }

        public async Task<IReadOnlyCollection<InfoAdResponse>> GetAllForPage(int pageSize, int pageIndex)
        {
            _logger.LogInformation("Получение всех объявлений");

            return await (await _adRepository.GetAll())
                .OrderBy(a => a.CreationDate).Skip(pageSize*pageIndex).Take(pageSize).ToListAsync();
        }


        public async Task<InfoAdResponse> GetByIdAsync(Guid id, CancellationToken cancellation)
        {
            _logger.LogInformation($"Получение объявления под Id: {id}");

            var existingad = await _adRepository.FindById(id, cancellation);
          
            return existingad;
        }

        public async Task<IReadOnlyCollection<InfoAdResponse>> GetAllUserAds(Guid userId, CancellationToken token)
        {
            _logger.LogInformation($"Получение всех объявлений пользователя под Id: {userId}");

            return await (await _adRepository.GetAll()).Where(a => a.OwnerId == userId)
                .ToListAsync();
        }
    }
}
