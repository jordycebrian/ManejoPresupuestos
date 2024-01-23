using Dapper;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuesto.Servicios
{
    public class RepositorioUsuarios:IRepositorioUsuarios
    {

        private readonly string connectionString;

        public RepositorioUsuarios(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> CrearUsuario(Usuario usuario)
        {
            using var connection = new SqlConnection(connectionString);
            var usuarioid = await connection.QuerySingleAsync<int>(@"INSERT INTO Usuarios (Email, EmailNormalizado,
                                                              PasswordHash) VALUES (@Email, @EmailNormalizado,@PasswordHash);
                                                              SELECT SCOPE_IDENTITY()", usuario);


            await connection.ExecuteAsync("CrearDatosUsuarios", new { usuarioid },
                commandType: System.Data.CommandType.StoredProcedure);

            return usuarioid;
        }


        public async Task<Usuario> BuscarUsuarioPorEmail(string emailNormalizado)
        {
            using var connection = new SqlConnection(connectionString);
            var usuario = await connection.QuerySingleOrDefaultAsync<Usuario>("" +
                "SELECT * FROM Usuarios Where EmailNormalizado = @emailNormalizado",
                new { emailNormalizado });

            return usuario;
        }
    }
}
