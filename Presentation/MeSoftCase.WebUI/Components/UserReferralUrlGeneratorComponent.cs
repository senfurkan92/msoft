using Microsoft.AspNetCore.Mvc;

namespace MeSoftCase.WebUI.Components
{
    public class UserReferralUrlGeneratorComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("~/Components/UserReferralUrlGeneratorComponent.cshtml");
        }
    }
}
