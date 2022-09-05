using DesafioEntregable.Controllers.DTOS;
using DesafioEntregable.Modells;
using System.Data.SqlClient;

namespace DesafioEntregable.Repository
{
    public class VentaHandler : DBHandler
    {
        // ●	Traer Ventas
        public static List<GetVenta> traerListaVentas()
        {
            List<GetVenta> listaVenta = new List<GetVenta>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var selectQuery = "SELECT Venta.*, ProductoVendido.Stock, ProductoVendido.IdProducto FROM VENTA INNER JOIN ProductoVendido ON VENTA.Id = ProductoVendido.IdVenta";

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(selectQuery, sqlConnection))
                {
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {
                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                var parVentas = new GetVenta();

                                parVentas._idCVenta = Convert.ToInt32(dataReader["Id"]);

                                parVentas._comentarioCVenta = dataReader["Comentarios"].ToString();

                                parVentas._stockPV = Convert.ToInt32(dataReader["Stock"]);

                                parVentas._idProductoV = Convert.ToInt32(dataReader["IdProducto"]);

                                listaVenta.Add(parVentas);


                            }
                        }
                    }
                }
                sqlConnection.Close();
            }
            return listaVenta;
        }

        // Obtiene el número de la última venta
        public static int traerUltimaVenta()
        {
            int idUltimaVenta = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var selectQuery = "SELECT MAX (Id) FROM Venta";

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(selectQuery, sqlConnection))
                {
                    idUltimaVenta = Convert.ToInt32(sqlCommand.ExecuteScalar());


                }

                sqlConnection.Close();
            }

            return idUltimaVenta;
        }

        //●	Cargar Venta
        public static bool insertarVenta(PostVenta venta)
        {
            int ultimaVenta = 0;

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                // Crea la venta
                var queryInsert = "INSERT INTO Venta (Comentarios) Values (@Comentarios)";

                bool insert = false;  // indica el resultado del alta de la venta

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    
                    sqlCommand.Parameters.Add(new SqlParameter("Comentarios", System.Data.SqlDbType.VarChar) { Value = venta._comentarioCVenta });
                    

                    int numberOfRows = sqlCommand.ExecuteNonQuery();

                    if (numberOfRows > 0)
                    {
                        // Traemos el numero de la última venta.

                       ultimaVenta = traerUltimaVenta();

                       insert = true;

                    }
                }

                // Carga los productos vendidos
                                
                for (int i = 0; i< venta._productosVendidos.Count; i++)
                {
                    List<GetProducto> getProductos = new List<GetProducto>();

                    PutProducto putProducto = new PutProducto ();

                    getProductos = ProductoHandler.traerUnicoProducto(venta._productosVendidos[i]);  // Busco el producto


                    if(getProductos.Count > 0) // Quiere decir que encontro un producto y se ejecuta el resto del metodo.
                    {
                        // Actualizo el stock
                        
                        putProducto._idProductClass = venta._productosVendidos[i];

                        putProducto._stockProductClass = (getProductos[0]._stockProductClass)-1; // Se asume que por cada producto vendido recibido en la venta, la cantidad es igual a uno (1)

                        ProductoHandler.modificarStockProducto(putProducto);

                        // Insertar el producto vendido en la tabla correspondiente

                        PostProductoV insertarProductosVendidos = new PostProductoV ();

                        insertarProductosVendidos._stockPV = 1; /* Se asume que el campo STOCK, en la tabla Productos Vendidos de la BBDD
                                                                 * corresponde a la cantidad de productos vendidos, como la cantidad no se solicita
                                                                 * en el get de la venta se asume que la misma es 1 por cada producto informado */
                        insertarProductosVendidos._idVentaPV = ultimaVenta;

                        insertarProductosVendidos._idProductoV = venta._productosVendidos[i];
                        
                        ProductoVendidoHandler.insertarProductoV(insertarProductosVendidos);
                       
                                          
                    }
                                                       

                }

                sqlConnection.Close();

                return insert;
            }
        }

        //●	Eliminar Venta
        public static bool eliminarVenta(DeleteVenta delete)
        {
            int tempIdVenta = delete._idCVenta;

            bool deleteBool = false;

            List <GetProductoV> productosVendidos = new List <GetProductoV>();

            productosVendidos = ProductoVendidoHandler.traerProductosVPorVenta(tempIdVenta); // Busco los productos vendidos para la venta a eliminar

            for( int i = 0; i < productosVendidos.Count; i++)
            {
                GetProducto traerProductos = new GetProducto();

                traerProductos._idProductClass = productosVendidos[i]._idProductoV;

                List<GetProducto> listaProductosStock = new List<GetProducto> ();

                listaProductosStock = ProductoHandler.traerUnicoProducto(productosVendidos[i]._idProductoV); // Recupero el producto para conocer el stock

                PutProducto actualizarProducto = new PutProducto();
                
                actualizarProducto._stockProductClass = (listaProductosStock[0]._stockProductClass) + 1;

                actualizarProducto._idProductClass = listaProductosStock[0]._idProductClass;

                ProductoHandler.modificarStockProducto(actualizarProducto); // Se actualizá el stock



            }
            // Se borran los productos vendidos de la venta.

            DeleteProductoV productoABorrar = new DeleteProductoV();

            productoABorrar._idVentaPV = tempIdVenta;

            ProductoVendidoHandler.borrarProductoVendido(productoABorrar);

            

            // Se borra la venta

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var deleteQuery = $"DELETE FROM Venta WHERE Id = {tempIdVenta}";

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(deleteQuery, sqlConnection))
                {

                    int numberOfRows = sqlCommand.ExecuteNonQuery();

                    if (numberOfRows > 0)
                    {

                        deleteBool = true;

                    }

                }
                sqlConnection.Close();
            }


                return deleteBool;
        }
    }
}
