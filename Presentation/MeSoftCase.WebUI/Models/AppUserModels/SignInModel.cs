using FluentValidation;
using MeSoftCase.Infrastructure.Helpers;

namespace MeSoftCase.WebUI.Models.AppUserModels
{
    public record SignInModel(string Email, string Password);

    public class SignInModelValidator : AbstractValidator<SignInModel>
    {
        public SignInModelValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Bu alan zorunludur")
                .EmailAddress().WithMessage("Geçerli bir email adresi giriniz")
                .Must(x => !x.IsMalicious()).WithMessage("Kötü niyetli, zararlı içerik tespit edilmiştir.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Bu alan zorunludur")
                .Must(x => !x.IsMalicious()).WithMessage("Kötü niyetli, zararlı içerik tespit edilmiştir.");
        }
    }
}
