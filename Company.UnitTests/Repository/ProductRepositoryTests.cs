using Company.Core.Entities;
using Company.Core.Repositories;
using Company.Infrastructure.Persistence;
using Company.Infrastructure.Persistence.Repositories;
using Company.UnitTests.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Company.UnitTests.Repository
{
    public class ProductRepositoryTests
    {
        private IProductRepository _productRepository;


        #region MAKESUT

        private void MakeSut(DbContextOptions<CompanyDbContext> optionsStub)
        {
            using (var context = new CompanyDbContext(optionsStub))
            {
                context.Database.OpenConnection();
                context.Database.EnsureCreated();
            }
        }
        #endregion


        [Fact]
        public async Task GetAllAsync_ShoulReturnListProducts_WhenProductExists()
        {
            var optionsStub = FactoryDbContext.CreateConnection();
            var expectCountCategory = 5;
           
            MakeSut(optionsStub);


            using (var context = new CompanyDbContext(optionsStub))
            {
                var categoryDb = context.Categories.First();
                var productsStub = FactoryProduct.GetProductsFaker(expectCountCategory, categoryDb);
                context.Products.AddRange(productsStub);
                context.SaveChanges();
            }


            using (var context = new CompanyDbContext(optionsStub))
            {
                _productRepository = new ProductRepository(context);

                var result = await _productRepository.GetAllAsync();

                Assert.Equal(expectCountCategory, result.Count());
            }
        }

        [Fact]
        public async Task GetByIdAsync_ShoulReturnProduct_WhenIdProductExists()
        {
            var optionsStub = FactoryDbContext.CreateConnection();
            Product productStub = null;

            MakeSut(optionsStub);

            using (var context = new CompanyDbContext(optionsStub))
            {
                var categoryDb = context.Categories.First();
                productStub = FactoryProduct.GetProductsFaker(1, categoryDb).First();
                context.Products.Add(productStub);
                context.SaveChanges();
            }

            using (var context = new CompanyDbContext(optionsStub))
            {
                _productRepository = new ProductRepository(context);

                var result = await _productRepository.GetByIdAsync(productStub.Id);

                Assert.Equal(productStub.Id, result.Id);
            }

        }

        [Fact]
        public async Task CreateAsync_ShoulReturnProduct_WhenAddNewProduct()
        {
            var optionsStub = FactoryDbContext.CreateConnection();
            Product productStub = null;

            MakeSut(optionsStub);

            using (var context = new CompanyDbContext(optionsStub))
            {
                var categoryDb = context.Categories.First();
                productStub = FactoryProduct.GetProductsFaker(1, categoryDb).First();

                _productRepository = new ProductRepository(context);
                await _productRepository.CreateAsync(productStub);
            }

            using (var context = new CompanyDbContext(optionsStub))
            {
                _productRepository = new ProductRepository(context);

                var result = await _productRepository.GetByIdAsync(productStub.Id);

                Assert.Equal(productStub.Id, result.Id);
            }
        }

        [Fact]
        public async Task UpdateAsync_ShoulReturnProductDiff_WhenUpdatedProduct()
        {
            var optionsStub = FactoryDbContext.CreateConnection();
            Product initialProductMock = null;
            Product updatedProductMock = null;

            MakeSut(optionsStub);

            using (var context = new CompanyDbContext(optionsStub))
            {
                var categoryDb = context.Categories.First();
                initialProductMock = FactoryProduct.GetProductsFaker(1, categoryDb).First();

                _productRepository = new ProductRepository(context);
                await _productRepository.CreateAsync(initialProductMock);
            }

            using (var context = new CompanyDbContext(optionsStub))
            {
                _productRepository = new ProductRepository(context);
                updatedProductMock = await _productRepository.GetByIdAsync(initialProductMock.Id);
                updatedProductMock.Description = "Teste Update";
            }

            using (var context = new CompanyDbContext(optionsStub))
            {
                _productRepository = new ProductRepository(context);
               await _productRepository.UpdateAsync(updatedProductMock);                 
            }

            using (var context = new CompanyDbContext(optionsStub))
            {
                _productRepository = new ProductRepository(context);
                var result = await _productRepository.GetByIdAsync(initialProductMock.Id);
                Assert.NotEqual(initialProductMock.Description, result.Description);
            }
        }

        [Fact]
        public async Task DeleteAsyncc_ShoulReturnNull_WhenRemoveProduct()
        {
            var optionsStub = FactoryDbContext.CreateConnection();
            Product initialProductMock = null;

            MakeSut(optionsStub);

            using (var context = new CompanyDbContext(optionsStub))
            {
                var categoryDb = context.Categories.First();
                initialProductMock = FactoryProduct.GetProductsFaker(1, categoryDb).First();

                _productRepository = new ProductRepository(context);
                await _productRepository.CreateAsync(initialProductMock);
            }                      

            using (var context = new CompanyDbContext(optionsStub))
            {
                _productRepository = new ProductRepository(context);
                await _productRepository.DeleteAsync(initialProductMock.Id);
            }

            using (var context = new CompanyDbContext(optionsStub))
            {
                _productRepository = new ProductRepository(context);
                var result = await _productRepository.GetByIdAsync(initialProductMock.Id);
                Assert.Null( result);
            }
        }
    }
}
