using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoSzerviz.Models
{
    public class Service
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A szolgáltatás neve kötelező!")]
        [Display(Name = "Szolgáltatás neve")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Ár (Ft)")]
        public decimal Price { get; set; }

        [Display(Name = "Időtartam (perc)")]
        public int DurationInMinutes { get; set; }
    }
}