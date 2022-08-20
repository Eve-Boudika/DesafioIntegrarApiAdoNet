using IntegrandoApisConAdo.Models;
using System.Data.SqlClient;
using System.Data;
using System.Xml;

namespace IntegrandoApisConAdo.Repository
{
    public class VentaRepository : ConectBD
    {
        public bool AddVenta(List<Producto> listaProductos, int idVendedor)
        {

            int idNuevaVenta = 0;

            string cmdText = "INSERT INTO Venta VALUES " +
                "(@Comentarios); SELECT CAST(scope_identity() AS int)";

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand sqlCommand = new SqlCommand(cmdText, sqlConnection))
                    {
                        sqlConnection.Open();

                        sqlCommand.Parameters.Add(new SqlParameter("@Comentarios", SqlDbType.VarChar, 255)).Value = "Nueva Venta";

                        idNuevaVenta = (int)sqlCommand.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            if (idNuevaVenta != 0)
            {
                List<VentaEfectuada> ventaEfectuadas = AnalizarProductos(listaProductos, idVendedor, idNuevaVenta);


                foreach (var item in ventaEfectuadas)
                {
                    if (item.StockEnAlmacen < item.StockProducto)
                    {
                        return false;
                    }
                }

                ProductoVendidoRepository productoVendidoRepository = new ProductoVendidoRepository();
                productoVendidoRepository.AddProductoVendido(ventaEfectuadas);

                ProductoRepository productoRepository = new ProductoRepository();
                foreach (var item in ventaEfectuadas)
                {
                    var NuevoStockAlmacen = item.StockEnAlmacen - item.StockProducto;
                    productoRepository.UpdateStockProducto(item.IdProducto, NuevoStockAlmacen);
                }
            }

            return true;
        }

        private List<VentaEfectuada> AnalizarProductos(List<Producto> listaProductos, int idVendedor, int idNuevaVenta)
        {

            List<VentaEfectuada> ventaEfectuadas = new List<VentaEfectuada>();

            bool ExistProduct = false;
            foreach (var itemProducto in listaProductos)
            {
                foreach (var itemVenta in ventaEfectuadas)
                {
                    if (itemVenta.IdProducto == itemProducto.Id)
                    {
                        itemVenta.StockProducto++;
                        ExistProduct = true;
                    }
                }
                if (ExistProduct is not true)
                {
                    VentaEfectuada productoRecienVendido = new VentaEfectuada();
                    productoRecienVendido.IdProducto = itemProducto.Id;
                    productoRecienVendido.IdVendedor = idVendedor;
                    productoRecienVendido.StockProducto = 1;
                    productoRecienVendido.IdVenta = idNuevaVenta;
                    productoRecienVendido.StockEnAlmacen = itemProducto.Stock;

                    ventaEfectuadas.Add(productoRecienVendido);
                }
                ExistProduct = false;
            }
            return ventaEfectuadas;
        }
    }

}
