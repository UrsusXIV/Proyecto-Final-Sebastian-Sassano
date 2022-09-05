using DesafioEntregable.Controllers.DTOS;
using DesafioEntregable.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DesafioEntregable.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class NombreProgramaController : ControllerBase
    {


        [HttpGet(Name = "TraerNombrePrograma")]

        public string traerNombre([FromBody] GetNombrePrograma nombrePrograma)
        {

            return nombrePrograma.Nombre;

        }

        
    }


}
