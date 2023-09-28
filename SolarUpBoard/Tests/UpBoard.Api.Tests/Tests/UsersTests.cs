using Board.Api.Tests;
using Board.Contracts.Ad;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UpBoard.Domain;
using Board.Contracts.User;

namespace UpBoard.Api.Tests.Tests
{
    public class UsersTests : IClassFixture<BoardWebApplicationFactory>
    {
        private readonly BoardWebApplicationFactory _webApplicationFactory;

        public UsersTests(BoardWebApplicationFactory webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
        }

        [Fact]
        public async Task Advert_GetById_Success()
        {
            // Arrange
            var httpClient = _webApplicationFactory.CreateClient();
            var id = DataSeedHelper.TestUserId;

            // Act
            var response = await httpClient.GetAsync($"/UserById?id={id}");

            // Assert

            Assert.NotNull(response);

            var result = await response.Content.ReadFromJsonAsync<InfoUserResponse>();

            Assert.NotNull(result);

            Assert.Equal("test_username", result!.UserName);
            Assert.Equal("test_mail", result.Email);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }


        [Fact]
        public async Task Advert_GetById_Fail()
        {
            // Arrange
            var httpClient = _webApplicationFactory.CreateClient();
            var id = Guid.Empty;

            // Act
            var response = await httpClient.GetAsync($"/UserById?id={id}");

            // Assert

            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);


        }

        [Fact]
        public async Task Advert_Add_Success()
        {
            // Arrange
            var httpClient = _webApplicationFactory.CreateClient();

            var model = new RegisterUserRequest
            {
                Email = "test_email@mail.ru",
                Password = "test_password",
                PhoneNumber = "+79998887766",
                Username = "ttest_username"
            };


            // Act
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("/Register", content);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var id = await response.Content.ReadFromJsonAsync<Guid>();

            await using var dbContext = _webApplicationFactory.CreateDbContext();
            var user = dbContext.Find<User>(id);

            Assert.NotNull(user);

            Assert.Equal(model.Username, user!.Username);
            Assert.Equal(model.Email, user.Email);
        }
    }
}
