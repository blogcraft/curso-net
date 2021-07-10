using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_api.Data.AppDb.Context;
using web_api.Data.AppDb.Model;

namespace web_api.Controllers.Db
{
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [Route("api/db/[controller]")]
    [ApiController]
    [Authorize]
    public class ClientesController : ControllerBase
    {
        private readonly AppdbContext _appdbContext;
        public ClientesController(AppdbContext appdbContext)
        {
            _appdbContext = appdbContext;
        }

        // GET: api/Clientes
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<ActionResult> ObtenerClientes([FromQuery] int limite)
        {
            if (limite <= 0)
                throw new ArgumentException("limite invalido", nameof(limite));

            List<Cliente> clientes = await _appdbContext.Cliente.ToListAsync();

            if (!clientes.Any())
                return NotFound();
            return Ok(clientes.Take((int)limite));
        }

        // GET: api/Clientes/1
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult> ObtenerCliente(int id)
        {
            Cliente cliente = await _appdbContext.Cliente.FindAsync(id);

            if (cliente == null)
                return NotFound();
            return Ok(cliente);
        }

        // POST: api/Clientes
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public async Task<ActionResult> CrearCliente([FromBody] Cliente cliente)
        {
            // validar y guardar en BD
            bool ocurrioAlgoMalo = false;

            if (ocurrioAlgoMalo)
                return BadRequest();

            _appdbContext.Cliente.Add(cliente);
            await _appdbContext.SaveChangesAsync();

            return CreatedAtAction("ObtenerCliente", new { id = cliente.ClienteId }, cliente);
        }

        // PUT: api/Clientes/1
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Administrador")]
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarCliente(int id, Cliente cliente)
        {
            if (id != cliente.ClienteId)
            {
                return BadRequest();
            }

            _appdbContext.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _appdbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ClienteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Clientes/1
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Administrador")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> BorrarCliente(int id)
        {
            Cliente cliente = await _appdbContext.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _appdbContext.Cliente.Remove(cliente);
            await _appdbContext.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> ClienteExists(int id)
        {
            return await _appdbContext.Cliente.AnyAsync(e => e.ClienteId == id);
        }
    }
}
