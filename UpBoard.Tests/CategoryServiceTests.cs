using AutoMapper;
using Doska.AppServices.Services.Ad;
using Moq;
using Shouldly;
using UpBoard.Application.AppData.Contexts.Categories.Repositories;
using Xunit.Abstractions;

namespace Board.Tests
{
    public class CategoryServiceTests : IClassFixture<CategoryListFixture>
    {
        public CategoryServiceTests(ITestOutputHelper output, CategoryListFixture fixture)
        {
            _output = output;
            _fixture = fixture;
            

            _output.WriteLine($"Test {nameof(CategoryServiceTests)} created");
        }

        private readonly ITestOutputHelper _output;
        private readonly CategoryListFixture _fixture;
        private readonly IMapper _mapper;

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        public async Task Category_GetById_Success1(int index)
        {
            // Arrange
            Guid id = CategoryListFixture.Ids[index];

            CancellationToken token = new CancellationToken(false);

            Mock<ICategoryRepository> categoryRepositoryMock = new Mock<ICategoryRepository>();
            Mock<IMapper> mapperMock = new Mock<IMapper>();
            Mock<Microsoft.Extensions.Logging.ILogger<CategoryService>> _logger = new Mock<Microsoft.Extensions.Logging.ILogger<CategoryService>>();

            var expected = _fixture.List.First(x => x.Id == id);

            categoryRepositoryMock.Setup(x => x.FindById(id, token)).ReturnsAsync(() => expected);

            CategoryService service = new CategoryService(categoryRepositoryMock.Object, mapperMock.Object, _logger.Object, null);

            // Act
            var result = await service.GetByIdAsync(id, token);

            // Assert
            result.ShouldNotBe(null);

            id.ShouldBe(result.Id);
            expected.CategoryName.ShouldBe(result.CategoryName);
            
            expected.ParentId.ShouldBe(result.ParentId);

            categoryRepositoryMock.Verify(x => x.FindById(id, token), Times.Once);
        }

        public static IEnumerable<object[]> GetByIdSuccessParams =>
            new List<object[]>
            {
                new object[] { 0, DateTime.Now },
                new object[] { 1, DateTime.Now }
            };

        [Theory]
        [MemberData(nameof(GetByIdSuccessParams))]
        public async Task Category_GetById_Success2(int index, DateTime dt)
        {
            _output.WriteLine($"Index: {index}, Time: {dt.Ticks}");

            // Arrange
            Guid id = CategoryListFixture.Ids[index];

            CancellationToken token = new CancellationToken(false);

            Mock<ICategoryRepository> categoryRepositoryMock = new Mock<ICategoryRepository>();
            Mock<IMapper> mapperMock = new Mock<IMapper>();
            Mock<Microsoft.Extensions.Logging.ILogger<CategoryService>> _logger = new Mock<Microsoft.Extensions.Logging.ILogger<CategoryService>>();

            var expected = _fixture.List.First(x => x.Id == id);

            categoryRepositoryMock.Setup(x => x.FindById(id, token)).ReturnsAsync(() => expected);

            CategoryService service = new CategoryService(categoryRepositoryMock.Object, mapperMock.Object, _logger.Object, null);

            // Act
            var result = await service.GetByIdAsync(id, token);

            // Assert
            result.ShouldNotBe(null);

            id.ShouldBe(result.Id);
            expected.CategoryName.ShouldBe(result.CategoryName);
            expected.ParentId.ShouldBe(result.ParentId);
            
            categoryRepositoryMock.Verify(x => x.FindById(id, token), Times.Once);
        }

        [Theory]
        [ClassData(typeof(CategoryIdTestData))]
        public async Task Category_GetById_Success3(Guid id)
        {
            _output.WriteLine($"Id: {id}");

            // Arrange

            CancellationToken token = new CancellationToken(false);

            Mock<ICategoryRepository> categoryRepositoryMock = new Mock<ICategoryRepository>();
            Mock<IMapper> mapperMock = new Mock<IMapper>();
            Mock<Microsoft.Extensions.Logging.ILogger<CategoryService>> _logger = new Mock<Microsoft.Extensions.Logging.ILogger<CategoryService>>();

            var expected = _fixture.List.First(x => x.Id == id);

            categoryRepositoryMock.Setup(x => x.FindById(id, token)).ReturnsAsync(() => expected);

            CategoryService service = new CategoryService(categoryRepositoryMock.Object, mapperMock.Object, _logger.Object, null);

            // Act
            var result = await service.GetByIdAsync(id, token);

            // Assert
            result.ShouldNotBe(null);

            id.ShouldBe(result.Id);
            expected.CategoryName.ShouldBe(result.CategoryName);
            expected.ParentId.ShouldBe(result.ParentId);

            categoryRepositoryMock.Verify(x => x.FindById(id, token), Times.Once);
        }

        [Fact]
        public async Task Category_GetById_Fail()
        {
            // Arrange
            var id = Guid.NewGuid();
            _output.WriteLine($"Id: {id}");

            CancellationToken token = new CancellationToken(false);

            Mock<ICategoryRepository> categoryRepositoryMock = new Mock<ICategoryRepository>();
            Mock<IMapper> mapperMock = new Mock<IMapper>();
            Mock<Microsoft.Extensions.Logging.ILogger<CategoryService>> _logger = new Mock<Microsoft.Extensions.Logging.ILogger<CategoryService>>();

            categoryRepositoryMock.Setup(x => x.FindById(id, token)).ReturnsAsync(() => null);

            CategoryService service = new CategoryService(categoryRepositoryMock.Object, mapperMock.Object, _logger.Object, null);

            // Act
            var result = await service.GetByIdAsync(id, token);

            // Assert
            result.ShouldBe(null);
            categoryRepositoryMock.Verify(x => x.FindById(id, token), Times.Once);
        }
    }
}