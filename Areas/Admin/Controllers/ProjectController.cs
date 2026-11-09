using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYAcademy_PortfolioProject.Context;

namespace MYAcademy_PortfolioProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProjectController : Controller
    {
        private readonly PortfolioContext _context;
        public ProjectController(PortfolioContext context)
        {
            _context = context;
        }
        public IActionResult ProjectList()
        {
            var projects = _context.Projects.Include(p => p.Category).ToList();
            return View(projects);
        }
    }
}
