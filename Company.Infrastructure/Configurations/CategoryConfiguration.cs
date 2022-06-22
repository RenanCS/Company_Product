using Company.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Infrastructure.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder
             .ToTable("tblCategoriaProduto")
            .HasKey(category => category.CategoryId);

            builder.Property(p => p.Name).HasMaxLength(250).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(250).IsRequired();
            builder.HasMany(c => c.Products).WithOne(c => c.Category).IsRequired().OnDelete(DeleteBehavior.Cascade);


            builder.HasData(
                new Category
                {
                    CategoryId = 1,
                    Name = "Eletrônico",
                    Description = "Eletrodomésticos",
                    Active = true
                },
                new Category
                {
                    CategoryId = 2,
                    Name = "Informática",
                    Description = "Produtos para Informática",
                    Active = true
                },
                new Category
                {
                    CategoryId = 3,
                    Name = "Celulares",
                    Description = "Aparelhos e acessórios",
                    Active = true
                },
                new Category
                {
                    CategoryId = 4,
                    Name = "Moda",
                    Description = "Artigos para vestuário em geral",
                    Active = true
                },
                new Category
                {
                    CategoryId = 5,
                    Name = "Livros",
                    Description = "Livros",
                    Active = true
                });
        }
    }
}
