using System.ComponentModel.DataAnnotations;

namespace CineTup.Application.Requests
{
    public class MovieRequest
    {
        [Required(ErrorMessage = "El título es obligatorio.")]
        [MinLength(1, ErrorMessage = "El título no puede estar vacío.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "El director es obligatorio.")]
        [MinLength(1, ErrorMessage = "El director no puede estar vacío.")]
        public string Director { get; set; } = string.Empty;

        [Required(ErrorMessage = "La categoría es obligatoria.")]
        [MinLength(1, ErrorMessage = "La categoría no puede estar vacía.")]
        public string Category { get; set; } = string.Empty;

        [Required(ErrorMessage = "El resumen es obligatorio.")]
        [MinLength(1, ErrorMessage = "El resumen no puede estar vacío.")]
        public string Summary { get; set; } = string.Empty;

        [Required(ErrorMessage = "La URL de la imagen es obligatoria.")]
        [MinLength(1, ErrorMessage = "La URL de la imagen no puede estar vacía.")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = "La URL del banner es obligatoria.")]
        [MinLength(1, ErrorMessage = "La URL del banner no puede estar vacía.")]
        public string BannerUrl { get; set; } = string.Empty;

        [Range(1, 9999, ErrorMessage = "La duración debe ser mayor a 0.")]
        public int Duration { get; set; }

        [Required(ErrorMessage = "El idioma es obligatorio.")]
        [MinLength(1, ErrorMessage = "El idioma no puede estar vacío.")]
        public string Language { get; set; } = string.Empty;

        public bool IsAvailable { get; set; }
    }
}
