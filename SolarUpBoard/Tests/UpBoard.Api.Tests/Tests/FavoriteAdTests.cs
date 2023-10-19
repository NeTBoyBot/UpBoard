﻿using Board.Api.Tests;
using Board.Contracts.Comment;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UpBoard.Domain;
using Board.Contracts.FavoriteAd;

namespace UpBoard.Api.Tests.Tests
{
    public class FavoriteAdTests : IClassFixture<BoardWebApplicationFactory>
    {
        private readonly BoardWebApplicationFactory _webApplicationFactory;

        public FavoriteAdTests(BoardWebApplicationFactory webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
        }

        [Fact]
        public async Task FavoriteAd_Add_Success()
        {
            // Arrange
            var httpClient = _webApplicationFactory.CreateClient();

            var model = new CreateFavoriteAdRequest
            {
                AdvertisementId = DataSeedHelper.TestAdvertId,
                UserId = DataSeedHelper.TestUserId
            };


            // Act
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("/favoritead", content);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var id = await response.Content.ReadFromJsonAsync<Guid>();

            await using var dbContext = _webApplicationFactory.CreateDbContext();
            var advert = dbContext.Find<FavoriteAd>(id);

            Assert.NotNull(advert);

            Assert.Equal(model.AdvertisementId, advert!.AdvertisementId);
            Assert.Equal(model.UserId, advert.UserId);
        }
    }
}