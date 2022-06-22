using Company.Application.InputModel;
using FluentValidation;

namespace Company.Application.Validators
{
    public class ProductInputModelValidator : AbstractValidator<ProductInputModel>
    {
        public ProductInputModelValidator()
        {
            RuleFor(cp => cp.Name)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(250)
                .WithMessage("Nome do produto dever ser válido");

            RuleFor(cp => cp.Description)
                .NotEmpty()
                .NotNull()
                .MinimumLength(3)
                .MaximumLength(250)
                .WithMessage("Descrição do produto dever ser válido");

            RuleFor(cp => cp.CategoryName)
                .NotEmpty()
                .WithMessage("Categoria do produto deve ser válido");
        }
    }
}
