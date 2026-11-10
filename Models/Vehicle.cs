using System.ComponentModel.DataAnnotations;

namespace AutoSzerviz.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A rendszám megadása kötelező!")]
        [StringLength(10)]
        [Display(Name = "Rendszám")]
        public string LicensePlate { get; set; } = string.Empty;

        [Required(ErrorMessage = "A jármű típusának megadása kötelező!")]
        [Display(Name = "Márka és Típus")]
        public string MakeAndModel { get; set; } = string.Empty;

        [Display(Name = "Évjárat")]
        public int Year { get; set; }

        public string UserId { get; set; } = string.Empty;
    }
}