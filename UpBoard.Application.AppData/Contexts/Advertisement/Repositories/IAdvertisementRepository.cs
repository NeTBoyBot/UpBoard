using Board.Contracts.Ad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpBoard.Contracts.Ad;

namespace UpBoard.Application.AppData.Contexts.Advertisement.Repositories
{
    public interface IAdvertisementRepository
    {
        Task<InfoAdResponse> FindById(Guid id, CancellationToken cancellation);

        Task<IQueryable<InfoAdResponse>> GetAll();

        Task<Guid> AddAsync(CreateAdvertisementRequest model, CancellationToken cancellation);

        Task DeleteAsync(DeleteAdRequest request, CancellationToken cancellation);

        Task EditAdAsync(UpdateAdRequest request, CancellationToken cancellation);
    }
}
