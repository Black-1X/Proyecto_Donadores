

namespace Donantes.API.Entities
{
    public class SolicitudSangre : BaseEntity
    {

      public string? BloodType { get; set; }
      public string? Hospital { get; set; }
      public string? Contact { get; set; }
      public string? City { get; set; }
      public string? Status { get; set; }


    }
}