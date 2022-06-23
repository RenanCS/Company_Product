﻿using Bogus;
using Company.Core.Entities;
using System.Collections.Generic;

namespace Company.UnitTests.Helper
{
    public class FactoryCategory
    {
        public static IEnumerable<Category> GetCategoriesFaker(int quantityCategory = 1)
        {
            var list = new List<Category>();
            for (int i = 0; i < quantityCategory; i++)
            {

                var category = new Faker<Category>()
                    .RuleFor(c => c.Description, f => f.Commerce.ProductDescription())
                    .RuleFor(c => c.Name, f => f.Commerce.ProductName())
                    .RuleFor(c => c.CategoryId, f => f.Random.Int(1, 10));
                list.Add(category);
            }
            return list;
        }
    }
}
