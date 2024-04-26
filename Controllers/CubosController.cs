using ApiSegundoOAuth.Models;
using ApiSegundoOAuth.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ApiSegundoOAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CubosController : ControllerBase
    {
        private RepositoryCubos repo;
        public CubosController(RepositoryCubos repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Cubo>>> GetCubos()
        {
            return await this.repo.GetCubosAsync();
        }
        [HttpGet("{marca}")]
        public async Task<ActionResult<List<Cubo>>> FindCuboMarca(string marca)
        {
            return await this.repo.FindCubosByMarcaAsync(marca);
        }

    }
}
