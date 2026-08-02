using Donantes.API.Database;
using Donantes.API.Dtos.Common;
using Donantes.API.Dtos.Donadores;
using Donantes.API.Mappers;
using Microsoft.EntityFrameworkCore;
using Donantes.API.Constanst;

namespace Donantes.API.Services.Donadores
{
    public class DonadorService : IDonadorService
    {
        private readonly BloodDonationDbContext _context;

        private const int PAGE_SIZE = 10;
        private const int PAGE_SIZE_LIMIT = 100;

        public DonadorService(BloodDonationDbContext context)
        {
            _context = context;
        }

       public async Task<ResponseDto<PageDto<List<ResponseDonadorDto>>>> GetPageAsync(
    string searchTerm = "",int page = 1,int pageSize = 10){
    page = Math.Abs(page);
    pageSize = Math.Abs(pageSize);

    // Si la página es cero, utiliza la página 1
    page = page <= 0 ? 1 : page;

    // Si el tamaño es cero, utiliza el tamaño predeterminado
    pageSize = pageSize <= 0 ? PAGE_SIZE : pageSize;

    // Evita solicitar más registros de los permitidos
    pageSize = pageSize > PAGE_SIZE_LIMIT
        ? PAGE_SIZE_LIMIT
        : pageSize;

    int startIndex = (page - 1) * pageSize;

    IQueryable<Entities.Donador> donadorQuery =
        _context.Donadores.AsNoTracking();

    // Buscar por nombre, sangre, teléfono o ciudad
    if (!string.IsNullOrEmpty(searchTerm))
    {
        searchTerm = searchTerm.Trim().ToLower();

        donadorQuery = donadorQuery.Where(d =>
            (
                d.Name + " " +
                d.Dni + " " +
                d.BloodType + " " +
                d.Phone + " " +
                d.City
            )
            .ToLower()
            .Contains(searchTerm)
        );
    }

    int totalRows = await donadorQuery.CountAsync();

    var donadoresEntity = await donadorQuery
        .OrderBy(d => d.Name)
        .Skip(startIndex)
        .Take(pageSize)
        .ToListAsync();

    var donadoresDto = donadoresEntity
        .Select(DonadorMapper.ToDto)
        .ToList();

    int totalPages = (int)Math.Ceiling(
        totalRows / (double)pageSize
    );

    var pageData = new PageDto<List<ResponseDonadorDto>>
    {
        CurrentPage = page,
        PageSize = pageSize,
        TotalItems = totalRows,
        TotalPages = totalPages,
        HasPreviousPage = page > 1,
        HasNextPage = page < totalPages,
        Items = donadoresDto
    };

    return new ResponseDto<PageDto<List<ResponseDonadorDto>>>
    {
        StatusCode = HttpStatusCode.OK,
        Status = true,
        Message = "Donadores obtenidos correctamente.",
        Data = pageData
         };
     }  
       public async Task<ResponseDto<ResponseDonadorDto>> GetByIdAsync(string id)
        {
            var donador = await _context.Donadores
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);

            if (donador is null)
            {
                return new ResponseDto<ResponseDonadorDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "Donador no encontrado."
                };
            }

            return new ResponseDto<ResponseDonadorDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "Donador encontrado.",
                Data = DonadorMapper.ToDto(donador)
            };
        }

        public async Task<ResponseDto<ResponseDonadorDto>> CreateAsync(CreateDonadorDto dto)
        {
            // Verificar si ya existe un donador con el mismo DNI
            var existeDni = await _context.Donadores
                .AnyAsync(d => d.Dni == dto.Dni);

            if (existeDni)
            {
                return new ResponseDto<ResponseDonadorDto>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    Status = false,
                    Message = "Ya existe un donador registrado con ese DNI."
                };
            }

            var entity = DonadorMapper.ToEntity(dto);

            _context.Donadores.Add(entity);

            await _context.SaveChangesAsync();

            return new ResponseDto<ResponseDonadorDto>
            {
                StatusCode = HttpStatusCode.Created,
                Status = true,
                Message = "Donador creado correctamente.",
                Data = DonadorMapper.ToDto(entity)
            };
        }

       public async Task<ResponseDto<ResponseDonadorDto>> UpdateAsync(
            string id,
            UpdateDonadorDto dto
        )
        {
            var entity = await _context.Donadores.FindAsync(id);

            if (entity is null)
            {
                return new ResponseDto<ResponseDonadorDto>
                {
                    StatusCode = HttpStatusCode.NotFound,
                    Status = false,
                    Message = "Donador no encontrado."
                };
            }

            var existeDni = await _context.Donadores
                .AnyAsync(d =>
                    d.Dni == dto.Dni &&
                    d.Id != id
                );

            if (existeDni)
            {
                return new ResponseDto<ResponseDonadorDto>
                {
                    StatusCode = HttpStatusCode.Conflict,
                    Status = false,
                    Message = "El DNI ya pertenece a otro donador."
                };
            }

            DonadorMapper.UpdateEntity(dto, entity);

            await _context.SaveChangesAsync();

            return new ResponseDto<ResponseDonadorDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = "Donador actualizado correctamente.",
                Data = DonadorMapper.ToDto(entity)
            };
        }

        public async Task<ResponseDto<bool>> DeleteAsync(string id)
{
    var entity = await _context.Donadores.FindAsync(id);

    if (entity is null)
    {
        return new ResponseDto<bool>
        {
            StatusCode = HttpStatusCode.NotFound,
            Status = false,
            Message = "Donador no encontrado.",
            Data = false
        };
    }

    // Verificar si el donador está asignado a una solicitud
    var tieneSolicitudAsignada = await _context.SolicitudesSangre
        .AnyAsync(s =>
            s.DonadorId == id &&
            !s.Status
        );

    if (tieneSolicitudAsignada)
    {
        return new ResponseDto<bool>
        {
            StatusCode = HttpStatusCode.Conflict,
            Status = false,
            Message = "No se puede eliminar el donador porque está asignado a una solicitud.",
            Data = false
        };
    }

    _context.Donadores.Remove(entity);

    await _context.SaveChangesAsync();

    return new ResponseDto<bool>
    {
        StatusCode = HttpStatusCode.OK,
        Status = true,
        Message = "Donador eliminado correctamente.",
        Data = true
    };
}
    }
}
