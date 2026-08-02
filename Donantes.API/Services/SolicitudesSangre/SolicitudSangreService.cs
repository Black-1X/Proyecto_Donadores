using Donantes.API.Constanst;
using Donantes.API.Database;
using Donantes.API.Dtos.Common;
using Donantes.API.Dtos.Donadores;
using Donantes.API.Dtos.SolicitudesSangre;
using Donantes.API.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Donantes.API.Services.SolicitudesSangre
{
    public class SolicitudSangreService : ISolicitudSangreService
    {
        private readonly BloodDonationDbContext _context;
        private const int PAGE_SIZE = 10;
        private const int PAGE_SIZE_LIMIT = 100;

        public SolicitudSangreService(BloodDonationDbContext context)
        {
            _context = context;
        }

        public async Task<
                ResponseDto<PageDto<List<ResponseSolicitudSangreDto>>>
            > GetPageAsync(
                string searchTerm = "",
                int page = 1,
                int pageSize = 10
            )
            {
                page = Math.Abs(page);
                pageSize = Math.Abs(pageSize);

                page = page <= 0 ? 1 : page;

                pageSize = pageSize <= 0
                    ? PAGE_SIZE
                    : pageSize;

                pageSize = pageSize > PAGE_SIZE_LIMIT
                    ? PAGE_SIZE_LIMIT
                    : pageSize;

                int startIndex = (page - 1) * pageSize;

                IQueryable<Entities.SolicitudSangre> solicitudQuery =
                    _context.SolicitudesSangre.AsNoTracking();

                // Buscar por sangre, hospital, contacto, ciudad o estado
                if (!string.IsNullOrEmpty(searchTerm))
                {
                    searchTerm = searchTerm.Trim().ToLower();

                    if (searchTerm == "activa" || searchTerm == "true")
                    {
                        solicitudQuery = solicitudQuery
                            .Where(s => s.Status);
                    }
                    else if (
                        searchTerm == "atendida" ||
                        searchTerm == "false"
                    )
                    {
                        solicitudQuery = solicitudQuery
                            .Where(s => !s.Status);
                    }
                    else
                    {
                        solicitudQuery = solicitudQuery.Where(s =>
                            (
                                s.BloodType + " " +
                                s.Hospital + " " +
                                s.Contact + " " +
                                s.City
                            )
                            .ToLower()
                            .Contains(searchTerm)
                        );
                    }
                }

                int totalRows = await solicitudQuery.CountAsync();

                var solicitudesEntity = await solicitudQuery
                    .OrderByDescending(s => s.CreatedDate)
                    .Skip(startIndex)
                    .Take(pageSize)
                    .ToListAsync();

                var solicitudesDto = solicitudesEntity
                    .Select(SolicitudSangreMapper.ToDto)
                    .ToList();

                int totalPages = (int)Math.Ceiling(
                    totalRows / (double)pageSize
                );

                var pageData =
                    new PageDto<List<ResponseSolicitudSangreDto>>
                    {
                        CurrentPage = page,
                        PageSize = pageSize,
                        TotalItems = totalRows,
                        TotalPages = totalPages,
                        HasPreviousPage = page > 1,
                        HasNextPage = page < totalPages,
                        Items = solicitudesDto
                    };

                return new ResponseDto<
                    PageDto<List<ResponseSolicitudSangreDto>>
                >
                {
                    StatusCode =
                        HttpStatusCode.OK,

                    Status = true,

                    Message =
                        "Solicitudes obtenidas correctamente.",

                    Data = pageData
                };
            }

       public async Task<ResponseDto<CreateSolicitudResponseDto>> GetByIdAsync(string id)
{
    var solicitud = await _context.SolicitudesSangre
        .AsNoTracking()
        .FirstOrDefaultAsync(s => s.Id == id);

    if (solicitud is null)
    {
        return new ResponseDto<CreateSolicitudResponseDto>
        {
            StatusCode = HttpStatusCode.NotFound,
            Status = false,
            Message = "Solicitud no encontrada."
        };
    }

    var tipoSangre = solicitud.BloodType
        .Trim()
        .ToUpper();

    var ciudadSolicitud = solicitud.City
        .Trim()
        .ToUpper();

    var donantes = await _context.Donadores
        .AsNoTracking()
        .Where(d =>
            d.Available &&
            d.BloodType.ToUpper() == tipoSangre
        )
        .OrderByDescending(d =>
            d.City.ToUpper() == ciudadSolicitud
        )
        .ThenBy(d => d.Name)
        .ToListAsync();

    return new ResponseDto<CreateSolicitudResponseDto>
    {
        StatusCode = HttpStatusCode.OK,
        Status = true,
        Message = donantes.Count > 0
            ? "Solicitud encontrada. Se encontraron donantes disponibles."
            : "Solicitud encontrada, pero no hay donantes disponibles.",
        Data = new CreateSolicitudResponseDto
        {
            Solicitud = SolicitudSangreMapper.ToDto(solicitud),
            DonantesDisponibles = donantes
                .Select(DonadorMapper.ToDto)
                .ToList()
        }
    };
}

        public async Task<ResponseDto<CreateSolicitudResponseDto>> CreateAsync(CreateSolicitudSangreDto dto)
        {
            var solicitud = SolicitudSangreMapper.ToEntity(dto);
            _context.SolicitudesSangre.Add(solicitud);
            await _context.SaveChangesAsync();

            var tipoSangre = dto.TipoSangre.Trim().ToUpper();

            var donantes = await _context.Donadores
                .AsNoTracking()
                .Where(d => d.Available && d.BloodType.ToUpper() == tipoSangre)
                .OrderByDescending(d => d.City.ToUpper() == dto.Ciudad.Trim().ToUpper())
                .ThenBy(d => d.Name)
                .ToListAsync();

            return new ResponseDto<CreateSolicitudResponseDto>
            {
                StatusCode = 201,
                Status = true,
                Message = donantes.Count > 0
                    ? "Solicitud creada. Se encontraron donantes disponibles."
                    : "Solicitud creada, pero no se encontraron donantes disponibles.",
                Data = new CreateSolicitudResponseDto
                {
                    Solicitud = SolicitudSangreMapper.ToDto(solicitud),
                    DonantesDisponibles = donantes.Select(DonadorMapper.ToDto).ToList()
                }
            };
        }

        public async Task<ResponseDto<ResponseSolicitudSangreDto>> UpdateAsync(string id, UpdateSolicitudSangreDto dto)
        {
            var solicitud = await _context.SolicitudesSangre.FindAsync(id);

            if (solicitud is null)
            {
                return new ResponseDto<ResponseSolicitudSangreDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "Solicitud no encontrada."
                };
            }

            SolicitudSangreMapper.UpdateEntity(dto, solicitud);
            await _context.SaveChangesAsync();

            return new ResponseDto<ResponseSolicitudSangreDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "Solicitud actualizada correctamente.",
                Data = SolicitudSangreMapper.ToDto(solicitud)
            };
        }

        public async Task<ResponseDto<bool>> DeleteAsync(string id)
        {
            var solicitud = await _context.SolicitudesSangre.FindAsync(id);

            if (solicitud is null)
            {
                return new ResponseDto<bool>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Status = false,
                    Message = "Solicitud no encontrada.",
                    Data = false
                };
            }

            // Si la solicitud tiene un donador asignado, se libera
            if (!string.IsNullOrEmpty(solicitud.DonadorId))
            {
                var donador = await _context.Donadores
                    .FirstOrDefaultAsync(d => d.Id == solicitud.DonadorId);

                if (donador is not null)
                {
                    donador.Available = true;
                    donador.ModifiedDate = DateTime.UtcNow;
                }
            }

            _context.SolicitudesSangre.Remove(solicitud);

            await _context.SaveChangesAsync();

            return new ResponseDto<bool>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Solicitud eliminada correctamente.",
                Data = true
            };
        }


                public async Task<ResponseDto<List<ResponseDonadorDto>>>
                    GetDonantesDisponiblesAsync(string solicitudId)
                {
                    var solicitud = await _context.SolicitudesSangre
                        .AsNoTracking()
                        .FirstOrDefaultAsync(s => s.Id == solicitudId);

                    if (solicitud is null)
                    {
                        return new ResponseDto<List<ResponseDonadorDto>>
                        {
                            StatusCode =
                                HttpStatusCode.NotFound,

                            Status = false,
                            Message = "Solicitud no encontrada.",
                            Data = new List<ResponseDonadorDto>()
                        };
                    }

                    if (!solicitud.Status)
                    {
                        return new ResponseDto<List<ResponseDonadorDto>>
                        {
                            StatusCode =
                                HttpStatusCode.BadRequest,

                            Status = false,
                            Message = "La solicitud ya fue atendida.",
                            Data = new List<ResponseDonadorDto>()
                        };
                    }

                    var tipoSangre = solicitud.BloodType
                        .Trim()
                        .ToUpper();

                    var ciudad = solicitud.City
                        .Trim()
                        .ToUpper();

                    var donadores = await _context.Donadores
                        .AsNoTracking()
                        .Where(d =>
                            d.Available &&
                            d.BloodType.ToUpper() == tipoSangre
                        )
                        .OrderByDescending(d =>
                            d.City.ToUpper() == ciudad
                        )
                        .ThenBy(d => d.Name)
                        .ToListAsync();

                    return new ResponseDto<List<ResponseDonadorDto>>
                    {
                        StatusCode =
                            HttpStatusCode.OK,

                        Status = true,

                        Message = donadores.Count > 0
                            ? "Se encontraron donadores disponibles."
                            : "No hay donadores disponibles.",

                        Data = donadores
                            .Select(DonadorMapper.ToDto)
                            .ToList()
                    };
                }

            public async Task<ResponseDto<ResponseSolicitudSangreDto>>
                AsignarDonadorAsync(
                    string solicitudId,
                    string donadorId
                )
            {
                var solicitud = await _context.SolicitudesSangre
                    .FirstOrDefaultAsync(s => s.Id == solicitudId);

                if (solicitud is null)
                {
                    return new ResponseDto<ResponseSolicitudSangreDto>
                    {
                        StatusCode =
                          HttpStatusCode.NotFound,

                        Status = false,
                        Message = "Solicitud no encontrada."
                    };
                }

                if (!solicitud.Status)
                {
                    return new ResponseDto<ResponseSolicitudSangreDto>
                    {
                        StatusCode =
                            HttpStatusCode.BadRequest,

                        Status = false,
                        Message = "La solicitud ya tiene un donador asignado."
                    };
                }

                var donador = await _context.Donadores
                    .FirstOrDefaultAsync(d => d.Id == donadorId);

                if (donador is null)
                {
                    return new ResponseDto<ResponseSolicitudSangreDto>
                    {
                        StatusCode =
                            HttpStatusCode.NotFound,

                        Status = false,
                        Message = "Donador no encontrado."
                    };
                }

                if (!donador.Available)
                {
                    return new ResponseDto<ResponseSolicitudSangreDto>
                    {
                        StatusCode =
                           HttpStatusCode.BadRequest,

                        Status = false,
                        Message = "El donador no está disponible."
                    };
                }

                var tipoSolicitud = solicitud.BloodType
                    .Trim()
                    .ToUpper();

                var tipoDonador = donador.BloodType
                    .Trim()
                    .ToUpper();

                if (tipoSolicitud != tipoDonador)
                {
                    return new ResponseDto<ResponseSolicitudSangreDto>
                    {
                        StatusCode =
                           HttpStatusCode.BadRequest,

                        Status = false,
                        Message =
                            "El tipo de sangre del donador no coincide."
                    };
                }

                solicitud.DonadorId = donador.Id;
                solicitud.Status = false;
                solicitud.ModifiedDate = DateTime.UtcNow;

                donador.Available = false;
                donador.ModifiedDate = DateTime.UtcNow;

                await _context.SaveChangesAsync();

                return new ResponseDto<ResponseSolicitudSangreDto>
                {
                    StatusCode =
                  HttpStatusCode.OK,

                    Status = true,
                    Message = "Donador asignado correctamente.",
                    Data = SolicitudSangreMapper.ToDto(solicitud)
                };
            }
    }
}
