
using Donantes.API.Dtos.Donadores;
using Donantes.API.Entities;

namespace Donantes.API.Mappers
{
    public class DonadorMapper 
    {
      
        public static Donador ToEntity(CreateDonadorDto dto)
        {
            return new Donador
            {
                Name = dto.Nombre,
                BloodType = dto.TipoSangre,
                Phone= dto.Telefono,
                City = dto.Ciudad,
                Available = dto.Disponible
            };
        }

  
        public static ResponseDonadorDto ToDto(Donador entity)
        {
            return new ResponseDonadorDto
            {
                Id = entity.Id,
                Nombre= entity.Name,
                TipoSangre= entity.BloodType,
                Telefono = entity.Phone,
                Ciudad = entity.City,
                Disponible = entity.Available
            };
        }


        public static void UpdateEntity(UpdateDonadorDto dto, Donador entity)
        {
            entity.Name = dto.Nombre;
            entity.BloodType = dto.TipoSangre;
            entity.Phone = dto.Telefono;
            entity.City = dto.Ciudad;
            entity.Available = dto.Disponible;
        }
    }
}