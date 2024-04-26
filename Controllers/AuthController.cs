using ApiSegundoOAuth.Helpers;
using ApiSegundoOAuth.Models;
using ApiSegundoOAuth.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Numerics;
using System.Security.Claims;

namespace ApiSegundoOAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private RepositoryCubos repo;
        private HelperActionServicesOAuth helper;

        public AuthController(RepositoryCubos repo, HelperActionServicesOAuth helper)
        {
            this.repo = repo;
            this.helper = helper;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> Login(LoginModel model)
        {
            UsuarioCubo user = await this.repo.LogInUser
                (model.Email, model.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            else
            {
                SigningCredentials credentials =
                    new SigningCredentials(
                        this.helper.GetKeyToken(),
                        SecurityAlgorithms.HmacSha256);
                string jsonUser =
                    JsonConvert.SerializeObject(user);
                Claim[] info = new[]
                {
                    new Claim("UserData", jsonUser)
                };

                JwtSecurityToken token = new JwtSecurityToken(
                    claims: info,
                    issuer: this.helper.Issuer,
                    audience: this.helper.Audience,
                    signingCredentials: credentials,
                    expires: DateTime.UtcNow.AddMinutes(30),
                    notBefore: DateTime.UtcNow
                    );
                return Ok(
                    new
                    {
                        response =
                        new JwtSecurityTokenHandler()
                        .WriteToken(token)
                    });
            }
        }
        [Authorize]
        [HttpGet("[action]")]
        public async Task<ActionResult<UsuarioCubo>> PerfilUser()
        {
            Claim claim = HttpContext.User
                .FindFirst(x => x.Type == "UserData");
            string jsonUser = claim.Value;
            UsuarioCubo user =
                JsonConvert.DeserializeObject<UsuarioCubo>(jsonUser);
            return user;
        }

        [Authorize]
        [HttpGet("[action]")]
        public async Task<ActionResult<List<CompraCubos>>> Compras()
        {
            Claim claim = HttpContext.User
                .FindFirst(x => x.Type == "UserData");
            string jsonUser = claim.Value;
            UsuarioCubo user =
                JsonConvert.DeserializeObject<UsuarioCubo>(jsonUser);
            List<CompraCubos> compras = await this.repo.GetPedidoUser(user.id_usuario);
            return compras;
        }
    }
}
