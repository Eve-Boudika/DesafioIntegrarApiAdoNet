using IntegrandoApisConAdo.Models;
using System.Data.SqlClient;
using IntegrandoApisConAdo.Repository;
using System.Data;

namespace IntegrandoApisConAdo.Repository
{
    public class ProductoRepository : ConectBD
    {
        public int AddProducto(Producto producto)
        {
            int rowsAffected = 0;

            string cmdText = "INSERT INTO Producto VALUES " +
                "(@Descripciones, @Costo, @PrecioVenta, @Stock, @IdUsuario);";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
                    {
                        sqlConnection.Open();

                        sqlCommand.Parameters.Add(new SqlParameter("@Descripciones", SqlDbType.VarChar, 255)).Value = producto.Descripciones;
                        sqlCommand.Parameters.Add(new SqlParameter("@Costo", SqlDbType.Money)).Value = producto.Costo;
                        sqlCommand.Parameters.Add(new SqlParameter("@PrecioVenta", SqlDbType.Money)).Value = producto.PrecioVenta;
                        sqlCommand.Parameters.Add(new SqlParameter("@Stock", SqlDbType.Int)).Value = producto.Stock;
                        sqlCommand.Parameters.Add(new SqlParameter("@IdUsuario", SqlDbType.BigInt)).Value = producto.IdUsuario;

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

        public int UpdateProducto(Producto producto)
        {
            int rowsAffected = 0;

            string cmdText = "UPDATE Producto SET " +
                "Descripciones = @Descripciones, " +
                "Costo = @Costo, " +
                "PrecioVenta = @PrecioVenta , " +
                "Stock = @Stock , " +
                "IdUsuario = @IdUsuario " +
                "WHERE Id=@id ;";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
                    {
                        sqlConnection.Open();

                        sqlCommand.Parameters.Add(new SqlParameter("@Descripciones", SqlDbType.VarChar, 255)).Value = producto.Descripciones;
                        sqlCommand.Parameters.Add(new SqlParameter("@Costo", SqlDbType.Money)).Value = producto.Costo;
                        sqlCommand.Parameters.Add(new SqlParameter("@PrecioVenta", SqlDbType.Money)).Value = producto.PrecioVenta;
                        sqlCommand.Parameters.Add(new SqlParameter("@Stock", SqlDbType.Int)).Value = producto.Stock;
                        sqlCommand.Parameters.Add(new SqlParameter("@IdUsuario", SqlDbType.BigInt)).Value = producto.IdUsuario;

                        sqlCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.BigInt)).Value = producto.Id;

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
        public void UpdateStockProducto(long idProducto, int NuevoStockAlmacen)
        {
            string cmdText = "UPDATE Producto SET " +
                "Stock = @Stock " +
                "WHERE Id=@id;";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
                    {
                        sqlConnection.Open();
                        sqlCommand.Parameters.Add(new SqlParameter("@Stock", SqlDbType.Int)).Value = NuevoStockAlmacen;

                        sqlCommand.Parameters.Add(new SqlParameter("@id", SqlDbType.BigInt)).Value = idProducto;

                        sqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public bool DeleteProducto(int id)
        {
            ProductoVendidoRepository productoVendidoRepository = new ProductoVendidoRepository();
            if (productoVendidoRepository.DeleteProductoVendido(id))
            {
                string cmdText = "DELETE FROM Producto WHERE id = @id";

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
                    return false; // en caso de error 
                }
            }
            return false;


            
        }

    }
}
