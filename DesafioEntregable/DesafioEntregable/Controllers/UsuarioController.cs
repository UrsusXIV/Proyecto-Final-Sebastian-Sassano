using DesafioEntregable.Controllers.DTOS;
using DesafioEntregable.Repository;
using Microsoft.AspNetCore.Mvc;
namespace DesafioEntregable.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class UsuarioController : ControllerBase
    {
        [HttpGet(Name = "TraerNombre")]

        public List <GetUsuario> getUserName([FromBody]GetUsuario usuario)
        {
            return UsuarioHandler.traerUsuario(usuario);

        }


        [HttpPut(Name = "ModificarUsuario")]
        public bool updateUser([FromBody] PutUsuario usuario)
        {
            return UsuarioHandler.modificarUsuario(usuario);

        }

        [HttpPost(Name = "CrearUsuario")]

        public bool createUser([FromBody] PostUsuario register)
        {
            
            return UsuarioHandler.crearUsuario(register);

        }
        
        [HttpDelete(Name = "BorrarUsuario")]

        public bool deleteUser([FromBody]DeleteUsuario delete)
        {

            return UsuarioHandler.borrarUsuario(delete);

        }

    }
}
