using System.ComponentModel.DataAnnotations;

namespace Donantes.API.Dtos.SolicitudesSangre
{
    public class AsignarDonadorDto
    {
        [Required(ErrorMessage = "El Id de la solicitud es obligatorio.")]
        public string SolicitudId { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Id del donador es obligatorio.")]
        public string DonadorId { get; set; } = string.Empty;
    }
}