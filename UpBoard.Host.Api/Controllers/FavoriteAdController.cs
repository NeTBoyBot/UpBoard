﻿using Board.Contracts.Ad;
using Board.Contracts.FavoriteAd;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UpBoard.Application.AppData.Contexts.FavoriteAd.Services;
using UpBoard.Contracts.FavoriteAd;

namespace Doska.API.Controllers
{
    [ApiController]
    public class FavoriteAdController : ControllerBase
    {
        private readonly IFavoriteAdService _favoriteadService;

        public FavoriteAdController(IFavoriteAdService adService)
        {
            _favoriteadService = adService;
        }

        /// <summary>
        /// Получение всех избранных объявлений
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <returns></returns>
        [HttpGet("/FavoriteAds")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoFavoriteAdResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _favoriteadService.GetAll();

            return Ok(result);
        }

        /// <summary>
        /// Добавить объявление в избранное
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpPost("/createFavoriteAd")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoFavoriteAdResponse>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateAd([FromQuery]CreateFavoriteAdRequest request, CancellationToken cancellation)
        {
            var result = await _favoriteadService.CreateFavoriteAdAsync(request,cancellation);

            return Created("", result);
        }

        /// <summary>
        /// Удалить объявление из избранных
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        [HttpDelete("/deleteFavoriteAd")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteAd([FromQuery]DeleteFavoriteAdRequest request, CancellationToken cancellation)
        {
            await _favoriteadService.DeleteAsync(request,cancellation);
            return Ok();
        }

        /// <summary>
        /// Получить все избранные объявления авторизованного пользователя
        /// </summary>
        /// <param name="take"></param>
        /// <param name="skip"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("/allUserFavorites")]
        [ProducesResponseType(typeof(IReadOnlyCollection<InfoFavoriteAdResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllUserFavorites(Guid id, CancellationToken token)
        {
            var result = await _favoriteadService.GetAllUserFavorites(id,token);

            return Ok(result);
        }
    }
}
