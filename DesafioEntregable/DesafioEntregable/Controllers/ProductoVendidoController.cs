using DesafioEntregable.Controllers.DTOS;
using DesafioEntregable.Repository;
using Microsoft.AspNetCore.Mvc;
namespace DesafioEntregable.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ProductoVendidoController : ControllerBase
    {
        [HttpGet(Name = "TraerProductosVendidos")]

        public List<GetProductoV> ProductosVendidosXUsuario([FromBody]GetProductoV productosVendidosXusuario)
        {
            return ProductoVendidoHandler.traerProductosVPorUsuario(productosVendidosXusuario);

        }

        

    }
}
