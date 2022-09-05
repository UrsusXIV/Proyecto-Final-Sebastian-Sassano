using DesafioEntregable.Controllers.DTOS;
using DesafioEntregable.Modells;
using System.Data.SqlClient;

namespace DesafioEntregable.Repository
{
    public class ProductoVendidoHandler : DBHandler
    {
       // ●	Traer Productos Vendidos
        public static List<GetProductoV> traerProductosVPorUsuario(GetProductoV idUsuario)
        {

            List<GetProductoV> listaProductosXusuario = new List<GetProductoV>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var selectQuery = $"SELECT * FROM ProductoVendido INNER JOIN Producto ON ProductoVendido.IdProducto = Producto.Id WHERE Producto.IdUsuario = {idUsuario._idUsuario}";

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(selectQuery, sqlConnection))
                {

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {

                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                var parProductoV = new GetProductoV();

                                parProductoV._idProductoV = Convert.ToInt32(dataReader["IdProducto"]);

                                parProductoV._stockPV = Convert.ToInt32(dataReader["Stock"]);

                                parProductoV._idPV = Convert.ToInt32(dataReader["Id"]);

                                parProductoV._idVentaPV = Convert.ToInt32(dataReader["IdVenta"]);

                                listaProductosXusuario.Add(parProductoV);

                            }
                        }

                    }

                }
                sqlConnection.Close();
            }

            return listaProductosXusuario;
        }



        public static List<GetProductoV> traerProductosVPorVenta(int numeroVenta)
        {

            List<GetProductoV> listaProductos = new List<GetProductoV>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var selectQuery = $"SELECT * FROM ProductoVendido WHERE IdVenta = {numeroVenta}";

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(selectQuery, sqlConnection))
                {

                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {

                        if (dataReader.HasRows)
                        {
                            while (dataReader.Read())
                            {
                                var parProductoV = new GetProductoV();

                                parProductoV._idProductoV = Convert.ToInt32(dataReader["IdProducto"]);

                                parProductoV._stockPV = Convert.ToInt32(dataReader["Stock"]);

                                parProductoV._idPV = Convert.ToInt32(dataReader["Id"]);

                                parProductoV._idVentaPV = Convert.ToInt32(dataReader["IdVenta"]);

                                listaProductos.Add(parProductoV);

                            }
                        }

                    }

                }
                sqlConnection.Close();
            }

            return listaProductos;
        }

        public static bool borrarProductoVendido(DeleteProductoV producto)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var deleteQuery = "DELETE FROM ProductoVendido WHERE IdVenta = @IdVenta";

                bool update = false;

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(deleteQuery, sqlConnection))
                {

                    sqlCommand.Parameters.Add(new SqlParameter("IdVenta", System.Data.SqlDbType.BigInt) { Value = producto._idVentaPV });

                    int numberOfRows = sqlCommand.ExecuteNonQuery();

                    if (numberOfRows > 0)
                    {

                        update = true;

                    }
                }
                sqlConnection.Close();
                return update;
            }
        }


        public static bool insertarProductoV(PostProductoV PVendido)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var queryInsert = "INSERT INTO ProductoVendido (Stock, IdProducto, IdVenta) Values (@Stock, @IdProducto, @IdVenta)";

                bool insert = false;

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {                   
                    sqlCommand.Parameters.Add(new SqlParameter("Stock", System.Data.SqlDbType.Int) { Value = PVendido._stockPV});
                    sqlCommand.Parameters.Add(new SqlParameter("IdProducto", System.Data.SqlDbType.BigInt) { Value = PVendido._idProductoV });
                    sqlCommand.Parameters.Add(new SqlParameter("IdVenta", System.Data.SqlDbType.BigInt) { Value = PVendido._idVentaPV});


                    int numberOfRows = sqlCommand.ExecuteNonQuery();

                    if (numberOfRows > 0)
                    {

                        insert = true;

                    }
                }
                sqlConnection.Close();
                return insert;
            }
        }


    }
}
