using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGA.Dominio.Base;
using SGA.Dominio.Entidades.Acceso;
using SGA.Persistencia.Base;
using SGA.Persistencia.Context;
using SGA.Persistencia.Interfaces.Acceso;

namespace SGA.Persistencia.Repositories.Acceso
{
    public class RepositorioTarjetaRecargable : RepositorioBase<TarjetaRecargable>, IRepositorioTarjetaRecargable
    {
        private readonly SGAContexto _contexto;
        private readonly ILogger<RepositorioTarjetaRecargable> _logger;
        private readonly IConfiguration _configuracion;

        public RepositorioTarjetaRecargable(SGAContexto contexto,
                                    ILogger<RepositorioTarjetaRecargable> logger,
                                    IConfiguration configuracion) : base(contexto)
        {
            _contexto = contexto;
            _logger = logger;
            _configuracion = configuracion;
        }

        public async Task<ResultadoOperacion> ObtenerTarjetaPorNumero(string numeroTarjeta)
        {
            ResultadoOperacion resultado = new ResultadoOperacion();
            try
            {
                resultado.Datos = await _contexto.TarjetasRecargables
                                                 .FirstOrDefaultAsync(t => t.NumeroTarjeta == numeroTarjeta
                                                                        && t.Eliminado == false);
            }
            catch (Exception ex)
            {
                resultado.Exito = false;
                resultado.Mensaje = _configuracion["ErrorRepositorioTarjetaRecargable:ObtenerPorNumero"];
                _logger.LogError(resultado.Mensaje, ex.ToString());
            }
            return resultado;
        }
    }
}
