using Microsoft.AspNetCore.Mvc;

namespace MYAcademy_PortfolioProject.NewFolder.ViewComponents.DefaultViewComponents
{
    public class DefaultStyleComponentPartial : ViewComponent
    {
            public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
