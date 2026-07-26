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

        public DonadoresController(IDonadorService donadorService)
        {
            _donadorService = donadorService;
        }

        // GET: api/donadores
        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<ResponseDonadorDto>>>> GetAll()
        {
            var response = await _donadorService.GetAllAsync();

            return StatusCode(
                response.StatusCode,
                response
            );
        }

        // GET: api/donadores/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<ResponseDonadorDto>>> GetById(
            string id
        )
        {
            var response = await _donadorService.GetByIdAsync(id);

            return StatusCode(
                response.StatusCode,
                response
            );
        }

        // POST: api/donadores
        [HttpPost]
        public async Task<ActionResult<ResponseDto<ResponseDonadorDto>>> Create(
            [FromBody] CreateDonadorDto dto
        )
        {
            var response = await _donadorService.CreateAsync(dto);

            return StatusCode(
                response.StatusCode,
                response
            );
        }

        // PUT: api/donadores/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<ResponseDonadorDto>>> Update(
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

        // DELETE: api/donadores/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<bool>>> Delete(string id)
        {
            var response = await _donadorService.DeleteAsync(id);

            return StatusCode(
                response.StatusCode,
                response
            );
        }
    }
}