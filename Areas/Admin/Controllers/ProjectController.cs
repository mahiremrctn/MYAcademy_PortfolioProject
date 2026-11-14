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
        private readonly IWebHostEnvironment _env;
        public ProjectController(PortfolioContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
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
        public IActionResult CreateProject(Project project, IFormFile? imageFile)
        {
            ValidateImage(imageFile);
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName");
                return View(project);
            }
            if (imageFile != null)
            {
                project.ImageUrl = SaveImage(imageFile);
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
        public IActionResult UpdateProject(Project project, IFormFile? imageFile)
        {
            ValidateImage(imageFile);
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
            string? oldImageUrl = null;
            if (imageFile != null)
            {
                oldImageUrl = existingProject.ImageUrl;
                existingProject.ImageUrl = SaveImage(imageFile);
            }
            existingProject.ProjectTitle = project.ProjectTitle;
            existingProject.Description = project.Description;
            existingProject.Technologies = project.Technologies;
            existingProject.CategoryId = project.CategoryId;
            existingProject.IsActive = project.IsActive;
            _context.SaveChanges();
            DeleteImage(oldImageUrl);
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
            DeleteImage(project.ImageUrl);
            TempData["SuccessMessage"] = "Proje başarıyla silindi.";
            return RedirectToAction("ProjectList");
        }
        private void ValidateImage(IFormFile? imageFile)
        {
            if (imageFile == null)
            {
                return;
            }
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            var extension = Path.GetExtension(imageFile.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
            {
                ModelState.AddModelError("imageFile", "Sadece .jpg, .jpeg, .png veya .webp dosyaları yüklenebilir.");
            }
            if (imageFile.Length > 2 * 1024 * 1024)
            {
                ModelState.AddModelError("imageFile", "Görsel en fazla 2 MB olabilir.");
            }
        }
        private string SaveImage(IFormFile imageFile)
        {
            var extension = Path.GetExtension(imageFile.FileName).ToLower();
            var fileName = Guid.NewGuid().ToString() + extension;
            var folderPath = Path.Combine(_env.WebRootPath, "images", "projects");
            Directory.CreateDirectory(folderPath);
            var filePath = Path.Combine(folderPath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                imageFile.CopyTo(stream);
            }
            return "/images/projects/" + fileName;
        }
        private void DeleteImage(string? imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                return;
            }
            var filePath = Path.Combine(_env.WebRootPath, imageUrl.TrimStart('/'));
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
    }
    }
