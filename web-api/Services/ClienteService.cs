using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_api.Data.AppDb.Context;
using web_api.Data.AppDb.Model;

namespace web_api.Services
{
    public interface IClienteService
    {
        Task<List<Cliente>> ConsultaClienteAsync(int? clienteId, string nombre, string apellido);
    }

    public class ClienteService : IClienteService
    {
        private readonly AppdbContext _appdbContext;
        public ClienteService(AppdbContext appdbContext)
        {
            _appdbContext = appdbContext;
        }

        public async Task<List<Cliente>> ConsultaClienteAsync(int? clienteId, string nombre, string apellido)
        {
            IQueryable<Cliente> query = from c in _appdbContext.Cliente
                                        where c.ClienteId == (clienteId ?? c.ClienteId)
                                        && c.Nombre == (nombre ?? c.Nombre)
                                        && c.Apellidos == (apellido ?? c.Apellidos)
                                        select c;

            return await query.ToListAsync();
        }

    }
}
