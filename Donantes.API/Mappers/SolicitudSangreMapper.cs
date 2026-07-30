

using Donantes.API.Dtos.SolicitudesSangre;
using Donantes.API.Dtos;
using Donantes.API.Entities;

namespace Donantes.API.Mappers
{
  
    
    public static class SolicitudSangreMapper
    {
        // Convierte el DTO de creación en una entidad
        public static SolicitudSangre ToEntity(
            CreateSolicitudSangreDto dto
        )
        {
            return new SolicitudSangre
            {
                BloodType = dto.TipoSangre,
                Hospital = dto.Hospital,
                Contact = dto.Contacto,
                City = dto.Ciudad,
                Status = dto.Estado
            };
        }

        // Convierte la entidad en un DTO de respuesta
        public static ResponseSolicitudSangreDto ToDto(
            SolicitudSangre entity
        )
        {
            return new ResponseSolicitudSangreDto
            {
                Id = entity.Id,
                TipoSangre = entity.BloodType,
                Hospital = entity.Hospital,
                Contacto = entity.Contact,
                Ciudad = entity.City,
                Estado = entity.Status
            };
        }

        // Actualiza una entidad existente
        public static void UpdateEntity(
            UpdateSolicitudSangreDto dto,
            SolicitudSangre entity
        )
        {
            entity.BloodType = dto.TipoSangre;
            entity.Hospital = dto.Hospital;
            entity.Contact = dto.Contacto;
            entity.City = dto.Ciudad;
            entity.Status = dto.Estado;
            entity.ModifiedDate = DateTime.UtcNow;
    }
    
    }
    }
