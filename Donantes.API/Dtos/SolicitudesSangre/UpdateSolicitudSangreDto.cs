using System.ComponentModel.DataAnnotations;

namespace Donantes.API.Dtos.SolicitudesSangre
{
    public class UpdateSolicitudSangreDto
    {
        [Required(ErrorMessage = "El tipo de sangre es obligatorio.")]
        [RegularExpression( @"^(A|B|AB|O)[+-]$",ErrorMessage = "Tipo de sangre inválido. Valores permitidos: A+, A-, B+, B-, AB+, AB-, O+, O-.")]
        public string TipoSangre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El hospital es obligatorio.")]
        public string Hospital { get; set; } = string.Empty;

        [Required(ErrorMessage = "El contacto es obligatorio.")]
        public string Contacto { get; set; } = string.Empty;

        [Required(ErrorMessage = "La ciudad es obligatoria.")]
        public string Ciudad { get; set; } = string.Empty;
    }
}