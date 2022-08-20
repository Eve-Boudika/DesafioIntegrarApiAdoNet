using IntegrandoApisConAdo.Models;
using System.Data;
using System.Data.SqlClient;

namespace IntegrandoApisConAdo.Repository
{
    public class UsuarioRepository : ConectBD
    {
        public int UpdateUser(Usuario usuario)
        {
            int rowsAffected = 0;

            string cmdText = "UPDATE Usuario SET " +
                "Nombre = @Nombre, " +
                "Apellido = @Apellido, " +
                "NombreUsuario = @NombreUsuario , " +
                "Contraseña = @Contraseña , " +
                "Mail = @Mail " +
                "WHERE Id=@id ;";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
                    {
                        sqlConnection.Open();

                        sqlCommand.Parameters.Add(new SqlParameter("@Nombre", SqlDbType.VarChar, 255)).Value = usuario.Nombre;
                        sqlCommand.Parameters.Add(new SqlParameter("@Apellido", SqlDbType.VarChar, 255)).Value = usuario.Apellido;
                        sqlCommand.Parameters.Add(new SqlParameter("@NombreUsuario", SqlDbType.VarChar, 255)).Value = usuario.NombreUsuario;
                        sqlCommand.Parameters.Add(new SqlParameter("@Contraseña", SqlDbType.VarChar, 255)).Value = usuario.Contraseña;
                        sqlCommand.Parameters.Add(new SqlParameter("@Mail", SqlDbType.VarChar, 255)).Value = usuario.Mail;
                        sqlCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.VarChar, 255)).Value = usuario.Id;

                        rowsAffected = sqlCommand.ExecuteNonQuery();
                    }

                }
                return rowsAffected;
            }
            catch (Exception ex)
            {
                return -1; 
            }
        }
    }
}
