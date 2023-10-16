using Board.Api.Tests;
using Board.Contracts.Category;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UpBoard.Domain;
using Board.Contracts.Comment;

namespace UpBoard.Api.Tests.Tests
{
    public class CommentTests:IClassFixture<BoardWebApplicationFactory>
    {
        private readonly BoardWebApplicationFactory _webApplicationFactory;

        public CommentTests(BoardWebApplicationFactory webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
        }

        
        [Fact]
        public async Task Comment_Add_Success()
        {
            // Arrange
            var httpClient = _webApplicationFactory.CreateClient();

            var model = new CreateCommentRequest
            {
                SenderId = DataSeedHelper.TestCommentId,
                Text = "test_message",
                UserId = Guid.NewGuid()
            };


            // Act
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("/comment", content);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var id = await response.Content.ReadFromJsonAsync<Guid>();

            await using var dbContext = _webApplicationFactory.CreateDbContext();
            var advert = dbContext.Find<Comment>(id);

            Assert.NotNull(advert);

            Assert.Equal(model.Text, advert!.Text);
            Assert.Equal(model.SenderId, advert.SenderId);
        }
    }
}
