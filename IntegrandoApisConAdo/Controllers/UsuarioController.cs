using IntegrandoApisConAdo.Models;
using IntegrandoApisConAdo.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IntegrandoApisConAdo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private UsuarioRepository _usuarioRepository;
        public UsuarioController()
        {
            _usuarioRepository = new UsuarioRepository();
        }

    
        [HttpPut]
        public int Put([FromBody]Usuario usuario)
        {
            return _usuarioRepository.UpdateUser(usuario);
        }

    }
}
