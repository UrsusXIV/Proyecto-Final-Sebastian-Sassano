namespace DesafioEntregable.Modells
{
    public class ClaseProducto
    {
        public int getId()
        {
            int id = this._idProductClass;

            return id;
        }

        public void setId(int DBid)
        {
            this._idProductClass = DBid;

        }

        public string getDescripciones()
        {

            string descripciones = this._descripcionesProductClass;

            return descripciones;

        }

        public void setDescripciones(string DBDescripciones)
        {

            this._descripcionesProductClass = DBDescripciones;

        }

        public double getCosto()
        {
            double costo = this._costoProductClass;

            return costo;

        }

        public void setCosto(double DBCosto)
        {

            this._costoProductClass = DBCosto;

        }

        public double getPrecioVenta()
        {
            double precioVenta = this._precioVentaProductClass;

            return precioVenta;

        }

        public void setPrecioVenta(double DBPrecioVenta)
        {
            this._precioVentaProductClass = DBPrecioVenta;

        }

        public int getStock()
        {
            int stock = this._stockProductClass;

            return stock;
        }

        public void setStock(int DBStock)
        {
            this._stockProductClass = DBStock;

        }

        public int getIdUsuario()
        {
            int idUsuario = this._idUsuarioProductClass;

            return idUsuario;
        }

        public void setIdUsuario(int DBIdUsuario)
        {
            this._idUsuarioProductClass = DBIdUsuario;

        }

        private int _idProductClass;

        private string _descripcionesProductClass;

        private double _costoProductClass;

        private double _precioVentaProductClass;

        private int _stockProductClass;

        private int _idUsuarioProductClass;

    }


}
