using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_PortfolioProject.Context;

namespace MYAcademy_PortfolioProject.NewFolder.ViewComponents.DefaultViewComponents
{
    public class _DefaultProjectComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;
        public _DefaultProjectComponentPartial(PortfolioContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var values = _context.Projects
                .Include(p => p.Category)
                .Where(p => p.IsActive)
                .OrderByDescending(p => p.CreatedDate)
                .ToList();
            ViewBag.Categories = _context.Categories
                .Where(c=> _context.Projects.Any(p => p.CategoryId == c.CategoryId && p.IsActive))
                .OrderBy(c => c.CategoryName)
                .ToList();
            return View(values);
        }
    }
}
