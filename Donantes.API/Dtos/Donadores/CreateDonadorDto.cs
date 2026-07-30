

namespace Donantes.API.Dtos.Donadores
{
    public class CreateDonadorDto
    {
         public string Nombre { get; set; } = "";

        public string TipoSangre { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;

        public string Ciudad { get; set; } = string.Empty;

        public bool Disponible { get; set; }
    }
}