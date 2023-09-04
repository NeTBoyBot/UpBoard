using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Board.Api.Tests;
using Board.Contracts.Ad;
using Newtonsoft.Json;
using UpBoard.Domain;
using Xunit;

namespace UpBoard.Api.Tests.Tests
{
    public class AdvertTests : IClassFixture<BoardWebApplicationFactory>
    {
        public AdvertTests(BoardWebApplicationFactory webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
        }

        private readonly BoardWebApplicationFactory _webApplicationFactory;

        [Fact]
        public async Task Advert_GetById_Success()
        {
            // Arrange
            var httpClient = _webApplicationFactory.CreateClient();
            var id = DataSeedHelper.TestAdvertId;

            // Act
            var response = await httpClient.GetAsync($"/AdById?id={id}");

            // Assert

            Assert.NotNull(response);

            var result = await response.Content.ReadFromJsonAsync<InfoAdResponse>();

            Assert.NotNull(result);

            Assert.Equal("test_advert_name", result!.Name);
            Assert.Equal("test_desc", result.Description);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }


        [Fact]
        public async Task Advert_GetById_Fail()
        {
            // Arrange
            var httpClient = _webApplicationFactory.CreateClient();
            var id = Guid.Empty;

            // Act
            var response = await httpClient.GetAsync($"/AdById?id={id}");

            // Assert

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);


        }

        [Fact]
        public async Task Advert_Add_Success()
        {
            // Arrange
            var httpClient = _webApplicationFactory.CreateClient();

            var model = new CreateAdvertisementRequest
            {
                Name = "test_name",
                Description = "test_description",
                CategoryId = DataSeedHelper.TestCategoryId,
                OwnerId = DataSeedHelper.TestUserId
            };


            // Act
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("/createAd", content);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var id = await response.Content.ReadFromJsonAsync<Guid>();

            await using var dbContext = _webApplicationFactory.CreateDbContext();
            var advert = dbContext.Find<Advertisement>(id);

            Assert.NotNull(advert);

            Assert.Equal(model.Name, advert!.Name);
            Assert.Equal(model.Description, advert.Description);
        }
    }
}