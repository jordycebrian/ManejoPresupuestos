using ManejoPresupuesto.Models;

namespace ManejoPresupuesto.Servicios
{
    public interface IRepositorioTiposCuentas
    {
        Task Actualizar(TiposCuentas tiposCuentas);
        Task Borrar(int id);
        Task Crear(TiposCuentas tiposCuentas);
        Task<bool> Existe(string nombre, int usuarioId, int id = 0);
        Task<IEnumerable<TiposCuentas>> Obtener(int usuarioId);
        Task<TiposCuentas> ObtenerPorId(int id, int usuarioId);
        Task Ordenar(IEnumerable<TiposCuentas> tiposCuentasOrdenados);
    }
}
