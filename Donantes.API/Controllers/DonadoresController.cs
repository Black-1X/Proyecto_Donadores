
using Donantes.API.Database;
using Donantes.API.Dtos.Common;
using Donantes.API.Dtos.Donadores;
using Donantes.API.Entities;
using Donantes.API.Services.Donadores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Donantes.API.Controllers
{
   [ApiController]
    [Route("api/donadores")]
    public class DonadoresController : ControllerBase
    {
        private readonly BloodDonationDbContext _context;

        public DonadoresController(BloodDonationDbContext context)
        {
            _context = context;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Donador>>> GetDonadores()
        {
            return await _context.Donadores.ToListAsync();
           
        }

       
        [HttpGet("{id}")]
        public async Task<ActionResult<Donador>> GetDonador(string id)
        {
            var donador = await _context.Donadores.FindAsync(id);

            if (donador == null)
                return NotFound("Donador no encontrado.");

            return donador;
        }

       
        [HttpPost]
        public async Task<ActionResult<Donador>> CrearDonador(Donador donador)
        {
            _context.Donadores.Add(donador);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetDonador),
                new { id = donador.Id },
                donador
            );
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarDonador(string id, Donador donador)
        {
            if (id != donador.Id)
                return BadRequest("El Id no coincide.");

            _context.Entry(donador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Donadores.Any(d => d.Id == id))
                    return NotFound("Donador no encontrado.");

                throw;
            }

            return NoContent();
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarDonador(string id)
        {
            var donador = await _context.Donadores.FindAsync(id);

            if (donador == null)
                return NotFound("Donador no encontrado.");

            _context.Donadores.Remove(donador);
            await _context.SaveChangesAsync();

            return NoContent();

      
     
    }
}
}