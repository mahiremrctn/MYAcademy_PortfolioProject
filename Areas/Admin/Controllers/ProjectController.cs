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
            TempData["SuccessMessage"] = "Proje başarıyla eklendi.";
            return RedirectToAction("ProjectList");
        }
        [HttpGet]
        public IActionResult UpdateProject(int id)
        {
            var project = _context.Projects.Find(id);
            if (project == null)
            {
                return NotFound();
            }
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName");
            return View(project);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateProject(Project project)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName");
                return View(project);
            }
            var existingProject = _context.Projects.Find(project.ProjectId);
            if (existingProject == null)
            {
                return NotFound();
            }
            existingProject.ProjectTitle = project.ProjectTitle;
            existingProject.Description = project.Description;
            existingProject.Technologies = project.Technologies;
            existingProject.ImageUrl = project.ImageUrl;
            existingProject.CategoryId = project.CategoryId;
            existingProject.IsActive = project.IsActive;
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Proje başarıyla güncellendi.";
            return RedirectToAction("ProjectList");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProject(int id) {
            var project = _context.Projects.Find(id);
            if (project == null)
            {
                return NotFound();
            }
            _context.Projects.Remove(project);
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Proje başarıyla silindi.";
            return RedirectToAction("ProjectList");
        }
    }
    }
