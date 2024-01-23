using Dapper;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;
using System.Runtime.InteropServices.ObjectiveC;

namespace ManejoPresupuesto.Servicios
{
    public class RepositorioTiposCuentas:IRepositorioTiposCuentas
    {
        private readonly string connectionString;

        public RepositorioTiposCuentas(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(TiposCuentas tiposCuentas)
        {
            using var connection = new SqlConnection(connectionString);
            var id = await connection.QuerySingleAsync<int>("TiposCuentas_Insertar", 
                                                            new { usuarioId = tiposCuentas.UsuarioId,
                                                            nombre = tiposCuentas.Nombre},
                                                            commandType: System.Data.CommandType.StoredProcedure);

            tiposCuentas.Id = id;
        }

        public async Task<bool> Existe(string nombre, int usuarioId,int id = 0)
        {
            using var connection = new SqlConnection(connectionString);
            var existe = await connection.QueryFirstOrDefaultAsync<int>(
                                          @"SELECT 1 FROM TiposCuentas
                                          WHERE Nombre = @Nombre AND UsuarioId = @UsuarioId AND Id <> @id",
                                          new { nombre, usuarioId, id });
            return existe == 1;
        }


        public async Task<IEnumerable<TiposCuentas>> Obtener(int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryAsync<TiposCuentas>(@"SELECT Id, Nombre, Orden
                                                             FROM TiposCuentas
                                                             WHERE UsuarioId = @UsuarioId
                                                             ORDER BY Orden", new { usuarioId });


        }

        public async Task Actualizar(TiposCuentas tiposCuentas)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE TiposCuentas
                                            SET Nombre = @Nombre
                                            WHERE Id = @Id", tiposCuentas);
        }

        public async Task<TiposCuentas> ObtenerPorId(int id,int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<TiposCuentas>(@"SELECT Id, Nombre, Orden
                                                                      FROM TiposCuentas
                                                                      WHERE Id = @Id AND UsuarioId = @UsuarioId",
                                                                      new { id, usuarioId});
        }

        
        public async Task Borrar(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("DELETE TiposCuentas WHERE Id = @Id", new { id });
        }


        public async Task Ordenar(IEnumerable<TiposCuentas> tiposCuentasOrdenados)
        {
            var query = "UPDATE TiposCuentas SET Orden = @Orden WHERE Id = @Id";
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(query, tiposCuentasOrdenados);
        }
    }
}
