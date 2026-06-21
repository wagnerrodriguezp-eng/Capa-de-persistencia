using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SGA.Dominio.Base;
using SGA.Dominio.Entidades.Acceso;
using SGA.Modelo.Modelos;
using SGA.Persistencia.Base;
using SGA.Persistencia.Context;
using SGA.Persistencia.Interfaces.Acceso;

namespace SGA.Persistencia.Repositories.Acceso
{
    public class RepositorioRegistroUso : RepositorioBase<RegistroUso>, IRepositorioRegistroUso
    {
        private readonly SGAContexto _contexto;
        private readonly ILogger<RepositorioRegistroUso> _logger;
        private readonly IConfiguration _configuracion;

        public RepositorioRegistroUso(SGAContexto contexto,
                                    ILogger<RepositorioRegistroUso> logger,
                                    IConfiguration configuracion) : base(contexto)
        {
            _contexto = contexto;
            _logger = logger;
            _configuracion = configuracion;
        }

        public async Task<ResultadoOperacion> ObtenerUsosPorUsuario(int usuarioId)
        {
            ResultadoOperacion resultado = new ResultadoOperacion();
            try
            {
                resultado.Datos = await _contexto.RegistrosUso
                                                 .Where(r => r.UsuarioId == usuarioId && r.Eliminado == false)
                                                 .OrderByDescending(r => r.FechaHora)
                                                 .Select(r => new ModeloRegistroUsoDetalle
                                                 {
                                                     RegistroUsoId = r.Id,
                                                     UsuarioId = r.UsuarioId,
                                                     ViajeId = r.ViajeId,
                                                     FechaHora = r.FechaHora,
                                                     Resultado = r.Resultado.ToString()
                                                 })
                                                 .ToListAsync();
            }
            catch (Exception ex)
            {
                resultado.Exito = false;
                resultado.Mensaje = _configuracion["ErrorRepositorioRegistroUso:ObtenerPorUsuario"];
                _logger.LogError(resultado.Mensaje, ex.ToString());
            }
            return resultado;
        }
    }
}
