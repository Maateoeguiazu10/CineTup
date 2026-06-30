using System.ComponentModel.DataAnnotations;

namespace CineTup.Application.Requests
{
    public class ShowTimeRequest
    {
        [Range(1, 9999, ErrorMessage = "La película es obligatoria.")]
        public int MovieId { get; set; }

        [Required(ErrorMessage = "La fecha y hora son obligatorias.")]
        public DateTime? StartTime { get; set; }

        [Range(0.01, 99999.99, ErrorMessage = "El precio debe ser mayor a 0.")]
        public decimal TicketPrice { get; set; }
    }
}
