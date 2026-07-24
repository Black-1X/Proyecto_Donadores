
namespace Donantes.API.Entities
{
    public class Donador
    {
        public string? Id {get; set;}

        public string? Name {get; set;}
         
         public string? Blood_type { get; set; }

         public string? Phone { get; set; }

         public string? City {get; set;}

         public bool  Available { get; set; }
    }
}