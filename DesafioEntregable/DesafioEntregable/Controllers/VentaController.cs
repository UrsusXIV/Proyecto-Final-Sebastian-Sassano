using DesafioEntregable.Controllers.DTOS;
using DesafioEntregable.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DesafioEntregable.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class VentaController : ControllerBase
    {


        [HttpPost(Name = "CrearVenta")]
        public bool crearVenta([FromBody] PostVenta venta)
        {
            return VentaHandler.insertarVenta(venta);

        }

        [HttpDelete(Name = "BorrarVenta")]
        public bool borrarVenta([FromBody] DeleteVenta ventaEliminada)
        {

            return VentaHandler.eliminarVenta(ventaEliminada);

        }

        [HttpGet(Name = "TraerVenta")]
        
        public List<GetVenta> traerVentas([FromBody] GetVenta ventas)
        {

            return VentaHandler.traerListaVentas();

        }
    }


}
