using web_api.Data.AppDb.Context;

namespace web_api.Services
{
    public interface ITransaccionService
    {
        Task ComprarAsync(int productoId, int cuentaId, int cantidad);
    }

    public class TransaccionService : ITransaccionService
    {
        private readonly AppdbContext _appdbContext;
        private readonly ICalculoService _calculoService;
        public TransaccionService(AppdbContext appdbContext, ICalculoService calculoService)
        {
            _appdbContext = appdbContext;
            _calculoService = calculoService;
        }

        public async Task ComprarAsync(int productoId, int cuentaId, int cantidad)
        {
            Cartera productoExistente = await _appdbContext.Cartera
                .Where(c => c.ProductoId == productoId && c.CuentaId == cuentaId)
                .FirstOrDefaultAsync();

            if (productoExistente == null)
            {
                decimal montoTotal = await CalcularMontoAsync(productoId, cantidad);
                Cartera nuevaCompra = new Cartera()
                {
                    ProductoId = productoId,
                    CuentaId = cuentaId,
                    Cantidad = cantidad,
                    MontoTotal = montoTotal
                };
                _appdbContext.Cartera.Add(nuevaCompra);
                await _appdbContext.SaveChangesAsync();
            }
            else
            {
                await ActualizarCartera(productoExistente, cantidad, productoId);
            }
        }

        public async Task ActualizarCartera(Cartera item, int cantidad, int productoId)
        {
            int cantidadActualizada = item.Cantidad + cantidad;
            decimal montoTotal = await CalcularMontoAsync(productoId, cantidadActualizada);

            item.Cantidad = cantidadActualizada;
            item.MontoTotal = montoTotal;

            await _appdbContext.SaveChangesAsync();
        }

        public async Task<decimal> CalcularMontoAsync(int productoId, int cantidad)
        {
            decimal precio = await ObtenerPrecioAsync(productoId);
            return _calculoService.PxQ(precio, cantidad);
        }

        public async Task<decimal> ObtenerPrecioAsync(int productoId)
        {
            return await _appdbContext.Precio
                .Where(x => x.ProductoId == productoId)
                .Select(x => x.Valor)
                .FirstOrDefaultAsync();
        }
    }
}
