using Board.Contracts.Category;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Board.Tests
{
    public class CategoryListFixture
    {
        public CategoryListFixture()
        {
            List = Ids.Select(x => new InfoCategoryResponse
            {
                Id = x,
                CategoryName = $"test category name {x.ToString().Substring(1, 4)}",
                ParentId = x
            }).ToList();
        }

        public static Guid[] Ids { get; } = { Guid.NewGuid(), Guid.NewGuid(), Guid.Parse("09258252-083B-439A-931E-828E7F1B4F17") };

        public List<InfoCategoryResponse> List { get; }
    }
}