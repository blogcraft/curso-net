using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using web_api.Data.AppDb.Context;
using web_api.Data.AppDb.Model;

namespace web_api.Services
{
    public interface ICarteraService
    {
        Task<List<ConsCartRes>> ConsultaCarteraAsync(int? clienteId, string nombre, string apellidos, int? cuentaId, string numCuenta, string nomProducto);
    }

    public class CarteraService : ICarteraService
    {
        private readonly AppdbContext _appdbContext;
        public CarteraService(AppdbContext appdbContext)
        {
            _appdbContext = appdbContext;
        }

        public async Task<List<ConsCartRes>> ConsultaCarteraAsync(
            int? clienteId,
            string nombre,
            string apellidos,
            int? cuentaId,
            string numCuenta,
            string nomProducto
        )
        {
            IQueryable<ConsCartRes> query = from ca in _appdbContext.Cartera
                                            join cu in _appdbContext.Cuenta on ca.CuentaId equals cu.CuentaId
                                            join cl in _appdbContext.Cliente on cu.ClienteId equals cl.ClienteId
                                            join pr in _appdbContext.Producto on ca.ProductoId equals pr.ProductoId
                                            where cl.ClienteId == (clienteId ?? cl.ClienteId)
                                            && cl.Nombre == (nombre ?? cl.Nombre)
                                            && cl.Apellidos == (apellidos ?? cl.Apellidos)
                                            && cu.CuentaId == (cuentaId ?? cu.CuentaId)
                                            && cu.Numero == (numCuenta ?? cu.Numero)
                                            && pr.Nombre == (nomProducto ?? pr.Nombre)
                                            select new ConsCartRes
                                            {
                                                Nombre = cl.Nombre,
                                                Apellidos = cl.Apellidos,
                                                Cuenta = cu.Numero,
                                                Cantidad = ca.Cantidad,
                                                Producto = pr.Nombre
                                            };

            return await query.ToListAsync();
        }
    }

    public class ConsCartRes
    {
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Cuenta { get; set; }
        public int Cantidad { get; set; }
        public string Producto { get; set; }
    }
}
