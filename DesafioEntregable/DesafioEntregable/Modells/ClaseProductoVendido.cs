namespace DesafioEntregable.Modells
{
    public class ClaseProductoVendido : ClaseProducto
    {
        public int getId()
        {
            int id = _idPVClass;

            return id;

        }

        public void setId(int DBId)
        {
            _idPVClass = DBId;

        }

        public int getIdUsuario()
        {
            int idUsuario = _idUsuarioPVClass;

            return idUsuario;

        }

        public void setIdUsuario(int DBIdUsuario)
        {
            _idUsuarioPVClass = DBIdUsuario;

        }

        public int getIdVenta()
        {
            int idVenta = _idVentaPVClass;

            return idVenta;

        }
        public void setIdVenta(int DBIdVenta)
        {
            _idVentaPVClass = DBIdVenta;
        }


        public int getStock()
        {
            int stock = _stockPVClass;

            return stock;
        }

        public void setStock(int DBStock)
        {
            _stockPVClass = DBStock;

        }


        private int _idPVClass;

        private int _idUsuarioPVClass;

        private int _idVentaPVClass;

        private int _stockPVClass;

    }
}
