using Bogus;
using Company.Application.InputModel;
using Company.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.UnitTests.Helper
{
    public class FactoryProduct
    {
        public static IEnumerable<Product> GetProductsFaker(int quantityCategory = 1)
        {
            var categoryStub = FactoryCategory.GetCategoriesFaker().FirstOrDefault();

            var list = new List<Product>();
            for (int i = 0; i < quantityCategory; i++)
            {

                var productFaker = new Faker<Product>()
                    .RuleFor(c => c.Id, f => f.Random.Int(1, 10))
                    .RuleFor(c => c.Description, f => f.Commerce.ProductDescription())
                    .RuleFor(c => c.Name, f => f.Commerce.ProductName())
                    .RuleFor(c => c.Active, f => f.PickRandomParam(new bool[] { true, true, false }))
                    .RuleFor(c => c.Perishable, f => f.PickRandomParam(new bool[] { true, true, false }))
                    .RuleFor(c => c.Category, f => categoryStub)
                    .RuleFor(c => c.CategoryId, f => categoryStub.CategoryId);



                list.Add(productFaker);
            }
            return list;
        }

        public static ProductInputModel GetProductsInputModelFaker(int idProductInputModel)
        {
            var categoryStub = FactoryCategory.GetCategoriesFaker().FirstOrDefault();


            var productFaker = new Faker<ProductInputModel>()
                .RuleFor(c => c.Id, f => idProductInputModel)
                .RuleFor(c => c.Description, f => f.Commerce.ProductDescription())
                .RuleFor(c => c.Name, f => f.Commerce.ProductName())
                .RuleFor(c => c.Active, f => f.PickRandomParam(new bool[] { true, true, false }))
                .RuleFor(c => c.Perishable, f => f.PickRandomParam(new bool[] { true, true, false }))
                .RuleFor(c => c.Category, f => categoryStub)
                .RuleFor(c => c.CategoryId, f => categoryStub.CategoryId);


            return productFaker;
        }
    }

}
