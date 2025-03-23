using FluentValidation;
using MeSoftCase.Infrastructure.Helpers;

namespace MeSoftCase.WebUI.Models.AppUserModels
{
    public record GenerateReferralUrlModel(string Email);

    public class GenerateReferralUrlValidator : AbstractValidator<GenerateReferralUrlModel>
    {
        public GenerateReferralUrlValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Bu alan zorunludur")
                .EmailAddress().WithMessage("Geçerli bir email adresi giriniz")
                .Must(x => !x.IsMalicious()).WithMessage("Kötü niyetli, zararlı içerik tespit edilmiştir.");
        }
    }
}
