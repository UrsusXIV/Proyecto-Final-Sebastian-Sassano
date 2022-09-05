using DesafioEntregable.Controllers.DTOS;
using DesafioEntregable.Repository;
using Microsoft.AspNetCore.Mvc;
namespace DesafioEntregable.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ProductoController : ControllerBase
    {
        [HttpGet(Name = "TraerProductos")]

        public List<GetProducto> consultaProductos([FromBody]GetProducto productoConsulta)
        {
            return ProductoHandler.traerProductos();

        }

        [HttpPost(Name = "CrearProducto")]

        public bool crearProduto([FromBody] PostProducto producto)
        {
            return ProductoHandler.insertarProducto(producto);

        }


        [HttpPut(Name = "ModificarProducto")]
        public bool modProducto([FromBody] PutProducto producto)
        {
            return ProductoHandler.modificarProducto(producto);

        }

        [HttpDelete(Name = "BorrarProducto")]

        public bool deleteProducto([FromBody] DeleteProducto producto)
        {

            return ProductoHandler.borrarProducto(producto);
        }

    }
}
