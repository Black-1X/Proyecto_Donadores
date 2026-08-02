namespace Donantes.API.Entities
{
    public class SolicitudSangre : BaseEntity
    {
        public string BloodType { get; set; } = string.Empty;

        public string Hospital { get; set; } = string.Empty;

        public string Contact { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        // Toda solicitud nueva comienza activa
        public bool Status { get; set; } = true;

        // Será null mientras no tenga un donador asignado
        public string? DonadorId { get; set; }
    }
}