

namespace Donantes.API.Dtos.SolicitudesSangre
{
    public class ResponseSolicitudSangreDto
    {
         public string Id { get; set; } = string.Empty;

        public string? TipoSangre { get; set; } = string.Empty;

        public string? Hospital { get; set; } = string.Empty;

        public string? Contacto { get; set; } = string.Empty;

        public string? Ciudad { get; set; } = string.Empty;

        public string? Estado { get; set; } = string.Empty;
    }
}