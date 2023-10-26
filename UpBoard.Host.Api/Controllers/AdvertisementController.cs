﻿using Board.Contracts.Ad;
using Board.Contracts.Category;
using Doska.AppServices.Services.Ad;
using Doska.AppServices.Services.Categories;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UpBoard.Contracts.Ad;
using UpBoard.Contracts.Category;

namespace UpBoard.Host.Api.Controllers
{
    [Route("post")]
    [ApiController]
    public class AdvertisementController : ControllerBase
    {
        private readonly IAdvertisementService _advertisementService;

        public AdvertisementController(IAdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
        }

        /// <summary>
        /// Получение всех объявлений для страницы
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoAdResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllForPage(int pageSize, int pageIndex)
        {
            var result = await _advertisementService.GetAllForPage(pageSize,pageIndex);

            return Ok(result);
        }


        /// <summary>
        /// Получение объявления по Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoAdResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAdById(Guid id, CancellationToken cancellation)
        {
            var result = await _advertisementService.GetByIdAsync(id, cancellation);

            return Ok(result);
        }

        /// <summary>
        /// Создание объявления
        /// </summary>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoAdResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateAd([FromBody] CreateAdvertisementRequest request, CancellationToken cancellation)
        {
            var result = await _advertisementService.CreateAdAsync(request, cancellation);

            return Created("", result);
        }

        /// <summary>
        /// Обновление категории
        /// </summary>
        /// <param name="request">Данные для создания объявления</param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoCategoryResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateCategory([FromQuery] UpdateAdRequest request, CancellationToken cancellation)
        {

            var result = await _advertisementService.EditAdAsync(request, cancellation);

            return Ok(result);
        }

        /// <summary>
        /// Удаление объявления
        /// </summary>
        /// <param name="request">Данные для удаления объявления</param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteAd([FromQuery] DeleteAdRequest request, CancellationToken cancellation)
        {
            await _advertisementService.DeleteAsync(request, cancellation);
            return Ok();
        }
    }
}
