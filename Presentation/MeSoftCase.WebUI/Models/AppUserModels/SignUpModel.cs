using FluentValidation;
using MeSoftCase.Infrastructure.Helpers;

namespace MeSoftCase.WebUI.Models.AppUserModels
{
    public record SignUpModel(string Email, string Password, string? ReferralCode);

    public class SignUpModelValidator : AbstractValidator<SignUpModel>
    {
        public SignUpModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Bu alan zorunludur")
                .EmailAddress().WithMessage("Geçerli bir email adresi giriniz")
                .Must(x => !x.IsMalicious()).WithMessage("Kötü niyetli, zararlı içerik tespit edilmiştir.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Bu alan zorunludur")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,16}$")
                .WithMessage("Şifre en az 8 karakter, bir büyük harf, bir küçük harf ve bir rakam içermelidir.")
                .Must(x => !x.IsMalicious()).WithMessage("Kötü niyetli, zararlı içerik tespit edilmiştir.");
        }
    }
}
