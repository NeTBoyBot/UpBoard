using System;
using UpBoard.Domain;
using UpBoard.Infrastructure.DataAccess;

namespace Board.Api.Tests
{
    public static class DataSeedHelper
    {
        public static Guid TestAdvertId { get; set; }
        public static Guid TestCategoryId { get; set; }
        public static Guid TestUserId { get; set; }
        public static Guid TestCommentId { get; set; }
        public static Guid TestFavoriteAdId { get; set; }

        public static void InitializeDbForTests(UpBoardDbContext db)
        {
            var testCategory = new Category
            {
                CategoryName = "test_cat_1"
            };
            db.Add(testCategory);

            TestCategoryId = testCategory.Id;

            var advert = new Advertisement
            {
                Id=Guid.NewGuid(),
                Name = "test_advert_name",
                Description = "test_desc",
                CreationDate = DateTime.UtcNow,
                CategoryId = testCategory.Id
            };
            TestAdvertId = advert.Id;

            db.Add(advert);

            var testUser = new User
            {
                Email = "user@example.com",
                Id = Guid.NewGuid(),
                Password = "test_password",
                PhoneNumber = "+79789988777",
                Rating = 5,
                Username = "test_username",
                UserState = UpBoard.Domain.UserStates.UserStates.Verified,
                Registrationdate = DateTime.UtcNow
            };
            TestUserId = testUser.Id;
            db.Add(testUser);

            var testComment = new Comment
            {
                Id = Guid.NewGuid(),
                Sender = testUser,
                SenderId = TestUserId,
                Text = "test_message",
                User = testUser,
                UserId = Guid.NewGuid()

            };
            TestCommentId = testComment.Id;
            db.Add(testComment);

            var testFavoriteAd = new FavoriteAd
            {
                Id = Guid.NewGuid(),
                Ad = advert,
                AdvertisementId = advert.Id,
                User = testUser,
                UserId = testUser.Id

            };
            TestFavoriteAdId = testComment.Id;
            db.Add(testFavoriteAd);

            db.SaveChanges();

            
        }
    }
}