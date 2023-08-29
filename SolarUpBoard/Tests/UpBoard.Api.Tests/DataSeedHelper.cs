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
                Name = "test_advert_name",
                Description = "test_desc",
                CreationDate = DateTime.UtcNow,
                CategoryId = testCategory.Id
            };

           
            db.Add(advert);

            var testUser = new User
            {
                Email = "test_mail",
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

            db.SaveChanges();

            TestAdvertId = advert.Id;
        }
    }
}