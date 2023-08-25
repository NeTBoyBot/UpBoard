using UpBoard.Domain;
using Board.Infrastucture.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpBoard.Infrastucture.Repository;
using UpBoard.Application.AppData.Contexts.FavoriteAd.Repositories;
using AutoMapper;
using Board.Contracts.FavoriteAd;
using UpBoard.Contracts.FavoriteAd;
using System.Text.Json.Serialization.Metadata;

namespace Doska.DataAccess.Repositories
{
    ///<inheritdoc cref="IFavoriteAdRepository"/>
    public class FavoriteAdRepository : IFavoriteAdRepository
    {
        private readonly IRepository<FavoriteAd> _baseRepository;
        private readonly IMapper _mapper;

        public FavoriteAdRepository(IRepository<FavoriteAd> baseRepository,IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        ///<inheritdoc/>
        public Task<Guid> AddAsync(CreateFavoriteAdRequest request, CancellationToken cancellation)
        {
            var model = _mapper.Map<FavoriteAd>(request);

            _baseRepository.AddAsync(model,cancellation);

            return Task.Run(() => model.Id);
        }

        ///<inheritdoc/>
        public Task DeleteAsync(DeleteFavoriteAdRequest request, CancellationToken cancellation)
        {
            var model = _mapper.Map<FavoriteAd>(request);
            return _baseRepository.DeleteAsync(model,cancellation);
        }

        ///<inheritdoc/>
        public async Task<InfoFavoriteAdResponse> FindById(Guid id, CancellationToken cancellation)
        {
            var model = await _baseRepository.GetByIdAsync(id, cancellation);
            return _mapper.Map<InfoFavoriteAdResponse>(model);
        }

        ///<inheritdoc/>
        public async Task<IQueryable<InfoFavoriteAdResponse>> GetAll()
        {
            return (await _baseRepository.GetAll()).Select(a=>new InfoFavoriteAdResponse
            {
                AdvertisementId = a.AdvertisementId,
                Id = a.Id,
                UserId = a.UserId,
                
            }).AsQueryable();
        }
    }
}
