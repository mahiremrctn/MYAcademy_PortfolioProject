using System.ComponentModel.DataAnnotations;
namespace MYAcademy_PortfolioProject.Entities
{
    public class Project
    {
        public int ProjectId { get; set; }
        [Required(ErrorMessage = "Proje adı boş bırakılamaz.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Proje adı 3 ile 100 karakter arasında olmalıdır.")]
        public string ProjectTitle { get; set; }
        [StringLength(300)]
        public string? ImageUrl { get; set; }
        [Required(ErrorMessage = "Teknoloji bilgisi boş bırakılamaz.")]
        [StringLength(200, ErrorMessage = "Teknoloji bilgisi en fazla 200 karakter olabilir.")]
        public string Technologies { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; }
        [Required(ErrorMessage = "Açıklama boş bırakılamaz.")]
        [StringLength(1000, ErrorMessage = "Açıklama en fazla 1000 karakter olabilir.")]
        public string Description { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Lütfen bir kategori seçiniz.")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
