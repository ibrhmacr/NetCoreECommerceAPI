using Domain.Entities;
using FluentValidation;

namespace Application.Validators.ProductValidator;

public class CreateProductValidator : AbstractValidator<Product>
{
    public CreateProductValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Urun adini bos gecmeyiniz")
            .MaximumLength(150)
            .MinimumLength(2)
            .WithMessage("Urun adini 5 ile 150 karakter arasinda giriniz");

        RuleFor(p => p.UnitsInStock)
            .NotEmpty()
            .NotNull()
            .WithMessage("Stok bilgisini bos gecmeyiniz")
            .Must(s => s >= 0)
            .WithMessage("Stok bilgisi 0 dan kucuk olamaz");
        
        RuleFor(p => p.Price)
            .NotEmpty()
            .NotNull()
            .WithMessage("Fiyat bilgisini bos gecmeyiniz")
            .Must(s => s >= 0)
            .WithMessage("Fiyat bilgisi 0 dan kucuk olamaz");

    }
}