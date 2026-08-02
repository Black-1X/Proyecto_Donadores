using Donantes.API.Dtos.SolicitudesSangre;
using Donantes.API.Entities;

namespace Donantes.API.Mappers
{
    public static class SolicitudSangreMapper
    {
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

                // Toda solicitud comienza activa
                Status = true,

                // Todavía no tiene donador
                DonadorId = null
            };
        }

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
                Estado = entity.Status,
                DonadorId = entity.DonadorId,
                CreatedDate = entity.CreatedDate.ToString("dd-MM-yy"),
                ModifiedDate = entity.ModifiedDate?.ToString("dd-MM-yy")
            };
        }

        public static void UpdateEntity(
            UpdateSolicitudSangreDto dto,
            SolicitudSangre entity
        )
        {
            entity.BloodType = dto.TipoSangre;
            entity.Hospital = dto.Hospital;
            entity.Contact = dto.Contacto;
            entity.City = dto.Ciudad;
            entity.ModifiedDate = DateTime.UtcNow;
        }
    }
}