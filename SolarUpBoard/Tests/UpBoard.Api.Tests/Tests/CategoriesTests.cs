using Board.Api.Tests;
using Board.Contracts.Ad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Board.Contracts.Category;
using Newtonsoft.Json;
using UpBoard.Domain;

namespace UpBoard.Api.Tests.Tests
{
    public class CategoriesTests : IClassFixture<BoardWebApplicationFactory>
    {
        private readonly BoardWebApplicationFactory _webApplicationFactory;

        public CategoriesTests(BoardWebApplicationFactory webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
        }

        [Fact]
        public async Task Category_GetById_Success()
        {
            // Arrange
            var httpClient = _webApplicationFactory.CreateClient();
            var id = DataSeedHelper.TestCategoryId;

            // Act
            var response = await httpClient.GetAsync($"/CategoryById?id={id}");

            // Assert

            Assert.NotNull(response);

            var result = await response.Content.ReadFromJsonAsync<InfoCategoryResponse>();

            Assert.NotNull(result);

            Assert.Equal("test_cat_1", result!.CategoryName);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
        [Fact]
        public async Task Category_Add_Success()
        {
            // Arrange
            var httpClient = _webApplicationFactory.CreateClient();

            var model = new CreateCategoryRequest
            {
               CategoryName = "Test_Category",
               ParentId = null
            };


            // Act
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("/createCategory", content);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var id = await response.Content.ReadFromJsonAsync<Guid>();

            await using var dbContext = _webApplicationFactory.CreateDbContext();
            var advert = dbContext.Find<Category>(id);

            Assert.NotNull(advert);

            Assert.Equal(model.CategoryName, advert!.CategoryName);
            Assert.Equal(model.ParentId, advert.ParentId);
        }
    }
}
