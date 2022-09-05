using DesafioEntregable.Controllers.DTOS;
using DesafioEntregable.Repository;
using Microsoft.AspNetCore.Mvc;
namespace DesafioEntregable.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ValidacionController : ControllerBase
    {


        [HttpGet(Name = "ValidarUsuario")]

        public List <GetUsuario> validationUser([FromBody]GetUsuario validacion)
        {

            return UsuarioHandler.funcionLog(validacion);

        }
        

        

    }
}
