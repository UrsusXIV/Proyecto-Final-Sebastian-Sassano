using DesafioEntregable.Controllers.DTOS;
using DesafioEntregable.Modells;
using System.Data.SqlClient;

namespace DesafioEntregable.Repository
{
    public class UsuarioHandler : DBHandler
    {

        //●	Traer Usuario -> El metodo todos los datos a partir del Nombre de Usuario
        public static List<GetUsuario> traerUsuario(GetUsuario Usuario) 
        {
            List<GetUsuario> datosUsuario = new List<GetUsuario>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var selectQuery = $"SELECT * FROM Usuario WHERE NombreUsuario = '{Usuario._nombreUsuarioUClass}' ";

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(selectQuery, sqlConnection))
                {
                    using (SqlDataReader dataReader = sqlCommand.ExecuteReader())
                    {

                        if (dataReader.HasRows)
                        {

                            while (dataReader.Read())
                            {
                                var parGetUsuario = new GetUsuario();

                                parGetUsuario._nombreUsuarioUClass = dataReader["NombreUsuario"].ToString();

                                parGetUsuario._mailUClass = dataReader["Mail"].ToString();

                                parGetUsuario._nombreUClass = dataReader["Nombre"].ToString();

                                parGetUsuario._apellidoUClass = dataReader["Apellido"].ToString();

                                parGetUsuario._idUClass = Convert.ToInt32(dataReader["Id"]);


                                datosUsuario.Add(parGetUsuario);
                            }

                        }

                    }

                }
                sqlConnection.Close();

            }

            return datosUsuario;
        }

        //●	Inicio de sesión
        public static List<GetUsuario> funcionLog(GetUsuario parametro)
        {
            List<GetUsuario> validacionBasica = new List<GetUsuario>();

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var validacionQuery = $"SELECT * FROM Usuario WHERE NombreUsuario = '{parametro._nombreUsuarioUClass}'";

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(validacionQuery, sqlConnection))
                {
                    using (SqlDataReader dataReaderValidacion = sqlCommand.ExecuteReader())
                    {
                        if (dataReaderValidacion.HasRows)
                        {
                            while (dataReaderValidacion.Read())
                            {
                                var passwordBBDD = new GetUsuario();

                                passwordBBDD._contraseñaUClass = dataReaderValidacion["Contraseña"].ToString();



                                if (parametro._contraseñaUClass == passwordBBDD._contraseñaUClass)
                                {
                                    /* Si las contraseñas del Front y de la DDBB coinciden
                                    se actualiza la variable de salida en true
                                    y se alimentan todos los campos */
                                    parametro._validacionUClass = true;

                                    passwordBBDD._validacionUClass = parametro._validacionUClass;

                                    passwordBBDD._mailUClass = dataReaderValidacion["Mail"].ToString();

                                    passwordBBDD._nombreUClass = dataReaderValidacion["Nombre"].ToString();

                                    passwordBBDD._apellidoUClass = dataReaderValidacion["Apellido"].ToString();

                                    passwordBBDD._nombreUsuarioUClass = dataReaderValidacion["NombreUsuario"].ToString();

                                    passwordBBDD._idUClass = Convert.ToInt32(dataReaderValidacion["Id"]);


                                }

                                else /* Si las contraseñas no coinciden se actualiza la variable de salida a false 
                                      * y se inicializan todos los campos */
                                {
                                    parametro._validacionUClass = false;

                                    passwordBBDD._mailUClass = String.Empty;

                                    passwordBBDD._nombreUClass = String.Empty;

                                    passwordBBDD._apellidoUClass = String.Empty;

                                    passwordBBDD._nombreUsuarioUClass = String.Empty;

                                    passwordBBDD._contraseñaUClass = String.Empty;

                                    passwordBBDD._idUClass = 0;


                                }



                                validacionBasica.Add(passwordBBDD);
                            }
                        }
                    }
                }
                sqlConnection.Close();
            }
            return validacionBasica;
        }

        //●	Modificar usuario
        public static bool modificarUsuario(PutUsuario usuario)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))

            {
                // Como la premisa no lo define, decido actualizar por ID del usuario (se podría haber hecho alternativamente por nombre de usuario)
                var queryUpdate = "UPDATE Usuario SET Nombre = @Nombre, Apellido = @Apellido, NombreUsuario = @NombreUsuario, Contraseña = @Contraseña, Mail = @Mail WHERE Id = @Id";

                bool update = false;

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryUpdate, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("Nombre", System.Data.SqlDbType.VarChar) { Value = usuario._nombreUClass });
                    sqlCommand.Parameters.Add(new SqlParameter("Apellido", System.Data.SqlDbType.VarChar) { Value = usuario._apellidoUClass });
                    sqlCommand.Parameters.Add(new SqlParameter("NombreUsuario", System.Data.SqlDbType.VarChar) { Value = usuario._nombreUsuarioUClass });
                    sqlCommand.Parameters.Add(new SqlParameter("Contraseña", System.Data.SqlDbType.VarChar) { Value = usuario._contraseñaUClass });
                    sqlCommand.Parameters.Add(new SqlParameter("Mail", System.Data.SqlDbType.VarChar) { Value = usuario._mailUClass });
                    sqlCommand.Parameters.Add(new SqlParameter("Id", System.Data.SqlDbType.BigInt) { Value = usuario._idUClass });

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

        //●	Crear usuario
        public static bool crearUsuario(PostUsuario register)
        {
            // Se valida que ningung campo sea .Empty

            if(register._apellidoUClass == String.Empty || register._contraseñaUClass == String.Empty || register._nombreUsuarioUClass == String.Empty || register._nombreUClass == String.Empty || register._mailUClass == String.Empty)
            {

                return false;

            }

            // Se valida que el usuario no exista previamente

            GetUsuario validacionUsuario = new GetUsuario();

            List<GetUsuario> tempListaUsuarios = new List<GetUsuario>();

            validacionUsuario._nombreUsuarioUClass = register._nombreUsuarioUClass;

            tempListaUsuarios = traerUsuario(validacionUsuario);

            if(tempListaUsuarios.Count > 0)
            {
                return false;
            }

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var queryInsert = "INSERT INTO Usuario (Nombre,Apellido,NombreUsuario,Contraseña,Mail) Values(@Nombre,@Apellido,@NombreUsuario,@Contraseña,@Mail)";

                bool insert = false;

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(queryInsert, sqlConnection))
                {
                    sqlCommand.Parameters.Add(new SqlParameter("Nombre", System.Data.SqlDbType.VarChar) { Value = register._nombreUClass });
                    sqlCommand.Parameters.Add(new SqlParameter("Apellido", System.Data.SqlDbType.VarChar) { Value = register._apellidoUClass });
                    sqlCommand.Parameters.Add(new SqlParameter("NombreUsuario", System.Data.SqlDbType.VarChar) { Value = register._nombreUsuarioUClass });
                    sqlCommand.Parameters.Add(new SqlParameter("Contraseña", System.Data.SqlDbType.VarChar) { Value = register._contraseñaUClass });
                    sqlCommand.Parameters.Add(new SqlParameter("Mail", System.Data.SqlDbType.VarChar) { Value = register._mailUClass });

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

        //●	Eliminar Usuario
        public static bool borrarUsuario(DeleteUsuario deleteUsuario)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                var deleteQuery = "DELETE FROM Usuario WHERE Id = @Id";

                bool delete = false;

                sqlConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(deleteQuery, sqlConnection))
                {

                    sqlCommand.Parameters.Add(new SqlParameter("Id", System.Data.SqlDbType.BigInt) { Value = deleteUsuario._idUClass });

                    int numberOfRows = sqlCommand.ExecuteNonQuery();

                    if (numberOfRows > 0)
                    {

                        delete = true;

                    }
                }
                sqlConnection.Close();
                return delete;
            }
        }

    }
}

