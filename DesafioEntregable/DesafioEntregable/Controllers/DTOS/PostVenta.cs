namespace DesafioEntregable.Controllers.DTOS
{
    public class PostVenta
    {
        public int _idCVenta { get; set; }

        public string _comentarioCVenta { get; set; } // acepta null

        public List <int> _productosVendidos { get; set; }

        public int _idUsuario { get; set; }



    }


}

