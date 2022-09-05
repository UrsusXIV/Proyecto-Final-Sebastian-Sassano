namespace DesafioEntregable.Modells
{
    public class ClaseVenta
    {
        public int getId()
        {
            int id = _idCVenta;

            return id;

        }

        public void setId(int DBId)
        {
            _idCVenta = DBId;

        }

        private int _idCVenta;

    }
}
