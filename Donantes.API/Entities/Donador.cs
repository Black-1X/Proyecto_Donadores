namespace Donantes.API.Entities
{
    public class Donador : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string BloodType { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public bool Available { get; set; }
    }
}