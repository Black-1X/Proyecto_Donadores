using Donantes.API.Dtos.Donadores;
using Donantes.API.Entities;

namespace Donantes.API.Mappers
{
    public static class DonadorMapper
    {
        public static Donador ToEntity(CreateDonadorDto dto)
        {
            return new Donador
            {
                Name = dto.Nombre,
                Dni = dto.Dni,
                BloodType = dto.TipoSangre,
                Phone = dto.Telefono,
                City = dto.Ciudad,

                // Siempre comienza disponible
                Available = true
            };
        }

        public static ResponseDonadorDto ToDto(Donador entity)
        {
            return new ResponseDonadorDto
            {
                Id = entity.Id,
                Nombre = entity.Name,
                Dni = entity.Dni,
                TipoSangre = entity.BloodType,
                Telefono = entity.Phone,
                Ciudad = entity.City,
                Disponible = entity.Available,
                FechaCreacion = entity.CreatedDate.ToString("dd-MM-yy"),
                FechaModificacion = entity.ModifiedDate?.ToString("dd-MM-yy")
            };
        }

        public static void UpdateEntity(UpdateDonadorDto dto, Donador entity)
        {
            entity.Name = dto.Nombre;
            entity.Dni = dto.Dni;
            entity.BloodType = dto.TipoSangre;
            entity.Phone = dto.Telefono;
            entity.City = dto.Ciudad;
            entity.Available = dto.Disponible;
            entity.ModifiedDate = DateTime.UtcNow;
        }
    }
}
