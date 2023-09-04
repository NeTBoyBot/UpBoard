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
using Microsoft.EntityFrameworkCore;

namespace Doska.DataAccess.Repositories
{
    ///<inheritdoc cref="ICategoryRepository"/>
    public class CategoryRepository : ICategoryRepository
    {
        public readonly IRepository<Category> _baseRepository;
        private readonly IMapper _mapper;

        public CategoryRepository(IRepository<Category> baseRepository,IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        ///<inheritdoc/>
        public Task<Guid> AddAsync(CreateCategoryRequest model, CancellationToken cancellation)
        {
            var created = _mapper.Map<Category>(model);
            _baseRepository.AddAsync(created, cancellation);
            return Task.Run(()=> created.Id);
        }

        ///<inheritdoc/>
        public Task DeleteAsync(DeleteCategoryRequest request, CancellationToken cancellation)
        {
            var deleted = _mapper.Map<Category>(request);
            return _baseRepository.DeleteAsync(deleted,cancellation);
        }

        ///<inheritdoc/>
        public Task EditAdAsync(UpdateCategoryRequest request, CancellationToken cancellation)
        {
            return _baseRepository.UpdateAsync(_mapper.Map<Category>(request),cancellation);
        }

        ///<inheritdoc/>
        public async Task<InfoCategoryResponse> FindById(Guid id, CancellationToken cancellation)
        {
            var model = await _baseRepository.GetByIdAsync(id, cancellation);
            return _mapper.Map<InfoCategoryResponse>(model);
        }

        ///<inheritdoc/>
        public async Task<IQueryable<InfoCategoryResponse>> GetAll()
        {
            return (await _baseRepository.GetAll())
                .Include(c=>c.SubCategories)
                .Select(c => _mapper.Map<InfoCategoryResponse>(c));
        }
    }
}
