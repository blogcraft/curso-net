using web_api.Data.AppDb.Context;

namespace web_api.Services
{
    public interface ITransaccionService
    {
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
    }
}
