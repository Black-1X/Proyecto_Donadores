using System.ComponentModel.DataAnnotations;

namespace Donantes.API.Dtos.Donadores
{
    public class UpdateDonadorDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; } = string.Empty;
        
        [Required]
        [StringLength(13, MinimumLength = 13)]
        public string Dni { get; set; } = string.Empty;

        [Required(ErrorMessage = "El tipo de sangre es obligatorio.")]
        [RegularExpression( @"^(A|B|AB|O)[+-]$",ErrorMessage = "Tipo de sangre inválido. Valores permitidos: A+, A-, B+, B-, AB+, AB-, O+, O-.")]
        public string TipoSangre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        public string Telefono { get; set; } = string.Empty;

        [Required(ErrorMessage = "La ciudad es obligatoria.")]
        public string Ciudad { get; set; } = string.Empty;

        public bool Disponible { get; set; }
    }
}