using UpBoard.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Board.Contracts.Category;
using UpBoard.Contracts.Category;

namespace UpBoard.Application.AppData.Contexts.Categories.Repositories
{
    public interface ICategoryRepository
    {
        Task<InfoCategoryResponse> FindById(Guid id, CancellationToken cancellation);

        Task<IQueryable<InfoCategoryResponse>> GetAll();

        Task<Guid> AddAsync(CreateCategoryRequest model, CancellationToken cancellation);

        Task DeleteAsync(DeleteCategoryRequest reqest, CancellationToken cancellation);

        Task EditAdAsync(UpdateCategoryRequest request, CancellationToken cancellation);
    }
}
