namespace Donantes.API.Dtos.Donadores
{
    public class ResponseDonadorDto
    {
        public string Id { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Dni { get; set; } = string.Empty;
        public string TipoSangre { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Ciudad { get; set; } = string.Empty;
        public bool Disponible { get; set; }
        public string FechaCreacion { get; set; } = string.Empty;
        public string? FechaModificacion { get; set; }
    }
}
