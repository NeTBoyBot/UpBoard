using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpBoard.Domain;
using UpBoard.Infrastucture.Repository;
using UpBoard.Application.AppData.Contexts.Categories.Repositories;
using Board.Contracts.Category;
using AutoMapper;
using UpBoard.Contracts.Category;
using System.Net.Http.Headers;

namespace Doska.DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public readonly IRepository<Category> _baseRepository;
        private readonly IMapper _mapper;

        public CategoryRepository(IRepository<Category> baseRepository,IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public Task<Guid> AddAsync(CreateCategoryRequest model, CancellationToken cancellation)
        {
            var created = _mapper.Map<Category>(model);
            _baseRepository.AddAsync(created, cancellation);
            return Task.Run(()=> created.Id);
        }

        public Task DeleteAsync(DeleteCategoryRequest request, CancellationToken cancellation)
        {
            var deleted = _mapper.Map<Category>(request);
            return _baseRepository.DeleteAsync(deleted,cancellation);
        }

        public Task EditAdAsync(UpdateCategoryRequest request, CancellationToken cancellation)
        {
            return _baseRepository.UpdateAsync(_mapper.Map<Category>(request),cancellation);
        }

        public async Task<InfoCategoryResponse> FindById(Guid id, CancellationToken cancellation)
        {
            var model = await _baseRepository.GetByIdAsync(id, cancellation);
            return _mapper.Map<InfoCategoryResponse>(model);
        }

        public async Task<IQueryable<InfoCategoryResponse>> GetAll()
        {
            return (await _baseRepository.GetAll()).Select(c => new InfoCategoryResponse
            {
                Id = c.Id,
                CategoryName = c.CategoryName,
                ParentId = c.ParentId
            }).AsQueryable();
        }
    }
}
