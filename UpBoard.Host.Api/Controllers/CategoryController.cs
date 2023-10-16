using Board.Contracts.Category;
using Doska.AppServices.Services.Ad;
using Doska.AppServices.Services.Categories;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UpBoard.Contracts.Category;

namespace Doska.API.Controllers
{
    [Route("category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Получение всех категорий
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _categoryService.GetAll();

            return Ok(result);
        }

        /// <summary>
        /// Получение всех категорий
        /// </summary>
        /// <param name="id">Идентификатор категории</param>
        /// <returns></returns>
        [HttpGet("subcategories/{id}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllChildCategories(Guid id)
        {
            var result = await _categoryService.GetAllChildCategories(id);

            return Ok(result);
        }

        /// <summary>
        /// Получение категории по Id
        /// </summary>
        /// <param name="id">Идентификатор категории</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCategoryById(Guid id, CancellationToken cancellation)
        {
            var result = await _categoryService.GetByIdAsync(id, cancellation);

            return Ok(result);
        }

        /// <summary>
        /// Создание категории
        /// </summary>
        /// <param name="request">Данные для создания категории</param>
        /// <param name="cancellation">Токен отмены</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest request, CancellationToken cancellation)
        {

            var result = await _categoryService.CreateCategoryAsync(request, cancellation);

            return Created("", result);
        }

        /// <summary>
        /// Обновление категории
        /// </summary>
        /// <param name="request">Данные для изменения категории</param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryRequest request, CancellationToken cancellation)
        {

            var result = await _categoryService.EditCategoryAsync(request, cancellation);

            return Ok(result);
        }

        /// <summary>
        /// Удаление категории
        /// </summary>
        /// <param name="request">Данные для удаления категории</param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteAd(DeleteCategoryRequest request, CancellationToken cancellation)
        {

            await _categoryService.DeleteAsync(request, cancellation);
            return Ok();
        }
    }
}
