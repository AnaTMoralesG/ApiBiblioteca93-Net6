using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using WebApi93.Services.IServices;
using WebApi93.Services.Services;

namespace WebApi93.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AutoresController : ControllerBase
    {
        private readonly IAutorServices _autorServices;

        public AutoresController(IAutorServices autorServices)
        {

            _autorServices = autorServices;
        }

        [HttpGet]
        public async Task<IActionResult> GetAutores()
        {
            return Ok(await _autorServices.GetAutores());
        }

        [HttpPost]
        public async Task<IActionResult> CrearAutor([FromBody] Autor autor)
        {
            try
            {
                return Ok(await _autorServices.Crear(autor));

            }catch (Exception ex) 
            {
                throw new Exception("Sucedio un error " +  ex.Message);
            }
        }




    }
}
