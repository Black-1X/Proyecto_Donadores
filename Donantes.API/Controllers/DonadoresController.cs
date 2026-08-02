using Donantes.API.Dtos.Common;
using Donantes.API.Dtos.Donadores;
using Donantes.API.Services.Donadores;
using Microsoft.AspNetCore.Mvc;

namespace Donantes.API.Controllers
{
    [ApiController]
    [Route("api/donadores")]
    public class DonadoresController : ControllerBase
    {
        private readonly IDonadorService _donadorService;

        // Inyectamos el servicio de donadores
        public DonadoresController(IDonadorService donadorService)
        {
            _donadorService = donadorService;
        }

        // Obtener todos los donadores
        // Obtener y buscar donadores con paginación
[HttpGet]
public async Task<
    ActionResult<ResponseDto<PageDto<List<ResponseDonadorDto>>>>
> GetDonadores(
    [FromQuery] string searchTerm = "",
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10
)
{
    var response = await _donadorService.GetPageAsync(
        searchTerm,
        page,
        pageSize
    );

    return StatusCode(
        response.StatusCode,
        response
    );
}

        // Obtener un donador por su Id
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<ResponseDonadorDto>>>
            GetDonador(string id)
        {
            var response = await _donadorService.GetByIdAsync(id);

            return StatusCode(
                response.StatusCode,
                response
            );
        }

        // Crear un nuevo donador
        [HttpPost]
        public async Task<ActionResult<ResponseDto<ResponseDonadorDto>>>
            CrearDonador([FromBody] CreateDonadorDto dto)
        {
            var response = await _donadorService.CreateAsync(dto);

            return StatusCode(
                response.StatusCode,
                response
            );
        }

        // Actualizar un donador
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<ResponseDonadorDto>>>
            ActualizarDonador(
                string id,
                [FromBody] UpdateDonadorDto dto
            )
        {
            var response = await _donadorService.UpdateAsync(id, dto);

            return StatusCode(
                response.StatusCode,
                response
            );
        }

        // Eliminar un donador
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<bool>>>
            EliminarDonador(string id)
        {
            var response = await _donadorService.DeleteAsync(id);

            return StatusCode(
                response.StatusCode,
                response
            );
        }
    }
}