namespace DesafioEntregable.Modells
{
    public class ClaseUsuario
    {
        public int getId()
        {
            int id = this._idUClass;

            return id;

        }

        public void setId(int DBId)
        {
            this._idUClass = DBId;

        }

        public string getNombre()
        {
            string nombre = this._nombreUClass;

            return nombre;
        }

        public void setNombre(string DBNombre)
        {
            this._nombreUClass = DBNombre;

        }

        public string getApellido()
        {
            string apellido = this._apellidoUClass;

            return apellido;
           
        }

        public void setApellido(string DBApellido)
        {
            this._apellidoUClass = DBApellido;

        }

        public string getNombreUsuario()
        {
            string nombreUsuario = this._nombreUsuarioUClass;

            return nombreUsuario;
        }

        public void setNombreUsuario(string DBNombreUsuario)
        {
            this._nombreUsuarioUClass = DBNombreUsuario;

        }

        public string getContraseña()
        {
            string contraseña = this._contraseñaUClass;


            return contraseña;
        }

        public void setContraseña(string DBContraseña)
        {
            this._contraseñaUClass = DBContraseña;

        }

        public string getMail()
        {
            string mail = this._mailUClass;

            return mail;
        }

        public void setMail(string DBMail)
        {

            this._mailUClass = DBMail;
        }


        private int _idUClass;

        private string _nombreUClass;

        private string _apellidoUClass;

        private string _nombreUsuarioUClass;

        private string _contraseñaUClass;

        private string _mailUClass;

    }

   
}
