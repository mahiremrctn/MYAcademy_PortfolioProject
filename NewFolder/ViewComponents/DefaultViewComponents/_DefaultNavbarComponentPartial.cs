using Microsoft.AspNetCore.Mvc;

namespace MYAcademy_PortfolioProject.NewFolder.ViewComponents.DefaultViewComponents
{
    public class _DefaultNavbarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
