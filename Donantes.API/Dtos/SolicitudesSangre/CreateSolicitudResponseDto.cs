using Donantes.API.Dtos.Donadores;

namespace Donantes.API.Dtos.SolicitudesSangre
{
    public class CreateSolicitudResponseDto
    {
        public ResponseSolicitudSangreDto Solicitud { get; set; } = new();
        public List<ResponseDonadorDto> DonantesDisponibles { get; set; } = new();
    }
}
