using System.ComponentModel.DataAnnotations;
namespace MYAcademy_PortfolioProject.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage ="Kategori Adı Boş Bırakılamaz.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Kategori adı 2 ile 50 karakter arasında olmalıdır.")]
        public string CategoryName { get; set; }
        public List<Project>? Projects { get; set; }
    }
}
