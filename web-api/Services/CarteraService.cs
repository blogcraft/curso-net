using web_api.Data.AppDb.Context;

namespace web_api.Services
{
    public interface ICarteraService
    {
    }

    public class CarteraService : ICarteraService
    {
        private readonly AppdbContext _appdbContext;
        public CarteraService(AppdbContext appdbContext)
        {
            _appdbContext = appdbContext;
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
