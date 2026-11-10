using System.ComponentModel.DataAnnotations;

namespace AutoSzerviz.Models
{
    public class Appointment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Az időpont megadása kötelező!")]
        [Display(Name = "Időpont")]
        public DateTime AppointmentDate { get; set; }

        [Display(Name = "Státusz")]
        public string Status { get; set; } = "Foglalva";

        public int VehicleId { get; set; }
        public Vehicle? Vehicle { get; set; }

        public int ServiceId { get; set; }
        public Service? Service { get; set; }

        public string UserId { get; set; } = string.Empty;
    }
}