using AutoMapper;
using Board.Contracts.Category;
using Doska.AppServices.Services.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using UpBoard.Application.AppData.Contexts.Categories.Repositories;
using UpBoard.Application.AppData.Contexts.User.Services;
using UpBoard.Contracts.Category;
using UpBoard.Domain;
using static Newtonsoft.Json.JsonConvert;

namespace Doska.AppServices.Services.Ad
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryService> _logger;
        private readonly IMemoryCache _cache;

        private const string CategoriesCachingKey = "Categories";

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, ILogger<CategoryService> logger,
            IMemoryCache cache)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
            _logger = logger;
            _cache = cache;
        }

        public async Task<Guid> CreateCategoryAsync(CreateCategoryRequest request, CancellationToken cancellation)
        { 

            _logger.LogInformation($"Создание категории {SerializeObject(request)}");
           
            var categoryId = await _categoryRepository.AddAsync(request,cancellation);

            return categoryId;
        }

        public async Task DeleteAsync(DeleteCategoryRequest request,CancellationToken cancellation)
        {
            _logger.LogInformation($"Удаление категории под Id: {request.Id}");

            await _categoryRepository.DeleteAsync(request, cancellation);
        }

        public async Task<InfoCategoryResponse> EditCategoryAsync(UpdateCategoryRequest request, CancellationToken cancellation)
        {
            _logger.LogInformation($"Изменение имени категории под id {request.Id} на имя {request.CategoryName}");

            await _categoryRepository.EditAdAsync(request,cancellation);

            return _mapper.Map<InfoCategoryResponse>(request);
        }

        public async Task<IReadOnlyCollection<InfoCategoryResponse>> GetAll()
        {
            _logger.LogInformation($"Получение всех категорий");

            return await (await _categoryRepository.GetAll())
                .OrderBy(a => a.Id).ToListAsync();
        }

        public async Task<InfoCategoryResponse> GetByIdAsync(Guid id, CancellationToken cancellation)
        {
            _logger.LogInformation($"Поиск объявления под id: {id}");

            var existingCategory = await _categoryRepository.FindById(id,cancellation);
            return _mapper.Map<InfoCategoryResponse>(existingCategory);
        }

        public async Task<IReadOnlyCollection<InfoCategoryResponse>> GetAllChildCategories(Guid parentId)
        {
            _logger.LogInformation($"Получение всех категорий");

            return await (await _categoryRepository.GetAll())
                .Where(c => c.ParentId == parentId).OrderBy(a => a.Id).ToListAsync();
        }
    }
}
