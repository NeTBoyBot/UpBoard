using AutoMapper;
using Board.Contracts.Ad;
using Board.Infrastucture.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpBoard.Application.AppData.Contexts.Advertisement.Repositories;
using UpBoard.Contracts.Ad;
using UpBoard.Infrastucture.Repository;

namespace UpBoard.Infrastructure.DataAccess.Contexts.Advertisement
{
    public class AdvertisementRepository : IAdvertisementRepository
    {
        private readonly IRepository<Domain.Advertisement> _baseRepository;
        private readonly IMapper _mapper;

        public AdvertisementRepository(IRepository<Domain.Advertisement> baseRepository,IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public Task<Guid> AddAsync(CreateAdvertisementRequest model, CancellationToken cancellation)
        {
            var ad = _mapper.Map<Domain.Advertisement>(model);
            _baseRepository.AddAsync(ad, cancellation);

            return Task.Run(()=> ad.Id);
        }

        public Task DeleteAsync(DeleteAdRequest request, CancellationToken cancellation)
        {
            var ad = _mapper.Map<Domain.Advertisement>(request);

            return _baseRepository.DeleteAsync(ad, cancellation);
        }

        public Task EditAdAsync(UpdateAdRequest request, CancellationToken cancellation)
        {
            var ad = _mapper.Map<Domain.Advertisement>(request);

            return _baseRepository.UpdateAsync(ad, cancellation);
        }

        public async Task<InfoAdResponse> FindById(Guid id, CancellationToken cancellation)
        {
            var model = await _baseRepository.GetByIdAsync(id, cancellation);
            return _mapper.Map<InfoAdResponse>(model);
        }

        public async Task<IQueryable<InfoAdResponse>> GetAll()
        {
            return (await _baseRepository.GetAll()).Select(a=>new InfoAdResponse
            {
                CategoryId = a.CategoryId,
                CreationDate = a.CreationDate,
                Desc = a.Description,
                Id = a.Id,
                Name = a.Name,
                OwnerId = a.OwnerId,
                Price = a.Price
            });
        }
    }
}
