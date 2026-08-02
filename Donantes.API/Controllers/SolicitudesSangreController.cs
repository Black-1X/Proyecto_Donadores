using Donantes.API.Dtos.Common;
using Donantes.API.Dtos.Donadores;
using Donantes.API.Dtos.SolicitudesSangre;
using Donantes.API.Services.SolicitudesSangre;
using Microsoft.AspNetCore.Mvc;

namespace Donantes.API.Controllers
{
    [ApiController]
    [Route("api/solicitudes-sangre")]
    public class SolicitudesSangreController : ControllerBase
    {
        private readonly ISolicitudSangreService _solicitudSangreService;

        public SolicitudesSangreController(
            ISolicitudSangreService solicitudSangreService
        )
        {
            _solicitudSangreService = solicitudSangreService;
        }

        // GET: api/solicitudes-sangre
        // Obtener solicitudes con paginación
// Obtener y buscar solicitudes con paginación
        [HttpGet]
        public async Task<ActionResult<ResponseDto<PageDto<List<ResponseSolicitudSangreDto>>>>> GetAll(
            [FromQuery] string searchTerm = "",
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10
        )
        {
            var response =
                await _solicitudSangreService.GetPageAsync(
                    searchTerm,
                    page,
                    pageSize
                );

            return StatusCode(
                response.StatusCode,
                response
            );
        }

        // GET: api/solicitudes-sangre/{id}
        [HttpGet("{id}")]
        public async Task<
            ActionResult<ResponseDto<ResponseSolicitudSangreDto>>
        > GetById(string id)
        {
            var response = await _solicitudSangreService.GetByIdAsync(id);

            return StatusCode(
                response.StatusCode,
                response
            );
        }

        // POST: api/solicitudes-sangre
        [HttpPost]
        public async Task<
            ActionResult<ResponseDto<CreateSolicitudResponseDto>>
        > Create([FromBody] CreateSolicitudSangreDto dto)
        {
            var response =
                await _solicitudSangreService.CreateAsync(dto);

            return StatusCode(
                response.StatusCode,
                response
            );
        }

        // PUT: api/solicitudes-sangre/{id}
        [HttpPut("{id}")]
        public async Task<
            ActionResult<ResponseDto<ResponseSolicitudSangreDto>>
        > Update(
            string id,
            [FromBody] UpdateSolicitudSangreDto dto
        )
        {
            var response =
                await _solicitudSangreService.UpdateAsync(id, dto);

            return StatusCode(
                response.StatusCode,
                response
            );
        }

        // DELETE: api/solicitudes-sangre/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<bool>>> Delete(string id)
        {
            var response = await _solicitudSangreService.DeleteAsync(id);

            return StatusCode(
                response.StatusCode,
                response
            );
        }


        [HttpGet("{id}/donadores-disponibles")]
        public async Task<
            ActionResult<ResponseDto<List<ResponseDonadorDto>>>
        > GetDonadoresDisponibles(string id)
        {
            var response =
                await _solicitudSangreService
                    .GetDonantesDisponiblesAsync(id);

            return StatusCode(
                response.StatusCode,
                response
            );
        }


       [HttpPut("asignar-donador")]
        public async Task<
            ActionResult<ResponseDto<ResponseSolicitudSangreDto>>
        > AsignarDonador(
            [FromBody] AsignarDonadorDto dto
        )
        {
            var response =
                await _solicitudSangreService
                    .AsignarDonadorAsync(
                        dto.SolicitudId,
                        dto.DonadorId
                    );

            return StatusCode(
                response.StatusCode,
                response
            );
        }

    }
}