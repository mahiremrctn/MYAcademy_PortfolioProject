using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MYAcademy_PortfolioProject.Context;
using MYAcademy_PortfolioProject.Entities;
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
            var projects = _context.Projects.Include(p => p.Category)
                                .OrderByDescending(p => p.CreatedDate)
                                .ToList();
            return View(projects);
        }
        [HttpGet]
        public IActionResult CreateProject()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View(new Project());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateProject(Project project)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName");
                return View(project);
            }
            project.CreatedDate = DateTime.Now;
            _context.Projects.Add(project);
            _context.SaveChanges();
            return RedirectToAction("ProjectList");
        }
    }
}
