using IntegrandoApisConAdo.Models;
using System.Data.SqlClient;
using System.Data;

namespace IntegrandoApisConAdo.Repository
{
    public class ProductoVendidoRepository : ConectBD
    {
        public void AddProductoVendido(List<VentaEfectuada> ventaEfectuadas)
        {

            string cmdText = "INSERT INTO ProductoVendido VALUES " +
                "(@Stock, @IdProducto, @IdVenta);";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
                    {
                        sqlConnection.Open();

                        foreach (var item in ventaEfectuadas)
                        {
                            sqlCommand.Parameters.Clear();
                            sqlCommand.Parameters.Add(new SqlParameter("@Stock", SqlDbType.VarChar, 255)).Value = item.StockProducto;
                            sqlCommand.Parameters.Add(new SqlParameter("@IdProducto", SqlDbType.Money)).Value = item.IdProducto;
                            sqlCommand.Parameters.Add(new SqlParameter("@IdVenta", SqlDbType.Money)).Value = item.IdVenta;

                            sqlCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public bool DeleteProductoVendido(int id)
        {
            string cmdText = "DELETE FROM ProductoVendido WHERE idProducto = @id";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
                    {
                        sqlConnection.Open();

                        sqlCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.BigInt)).Value = id;

                        sqlCommand.ExecuteNonQuery();
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                return false; 
            }
        }

    }
}
