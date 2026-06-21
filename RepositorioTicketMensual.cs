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
    public class RepositorioTicketMensual : RepositorioBase<TicketMensual>, IRepositorioTicketMensual
    {
        private readonly SGAContexto _contexto;
        private readonly ILogger<RepositorioTicketMensual> _logger;
        private readonly IConfiguration _configuracion;

        public RepositorioTicketMensual(SGAContexto contexto,
                                    ILogger<RepositorioTicketMensual> logger,
                                    IConfiguration configuracion) : base(contexto)
        {
            _contexto = contexto;
            _logger = logger;
            _configuracion = configuracion;
        }

        public async Task<ResultadoOperacion> ObtenerTicketsVigentes(int usuarioId)
        {
            ResultadoOperacion resultado = new ResultadoOperacion();
            try
            {
                var hoy = DateTime.Now.Date;
                resultado.Datos = await _contexto.TicketsMensuales
                                                 .Where(t => t.UsuarioId == usuarioId
                                                          && t.Activa
                                                          && hoy >= t.FechaInicio
                                                          && hoy <= t.FechaVencimiento
                                                          && t.Eliminado == false)
                                                 .ToListAsync();
            }
            catch (Exception ex)
            {
                resultado.Exito = false;
                resultado.Mensaje = _configuracion["ErrorRepositorioTicketMensual:ObtenerVigentes"];
                _logger.LogError(resultado.Mensaje, ex.ToString());
            }
            return resultado;
        }
    }
}
