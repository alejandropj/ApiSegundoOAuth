using ApiSegundoOAuth.Data;
using ApiSegundoOAuth.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ApiSegundoOAuth.Repositories
{
    public class RepositoryCubos
    {
        private CubosContext context;
        public RepositoryCubos(CubosContext context)
        {
            this.context = context;
        }
        public async Task<List<Cubo>> GetCubosAsync()
        {
            return await this.context.Cubos.ToListAsync();
        }        
        public async Task<List<Cubo>> FindCubosByMarcaAsync(string marca)
        {
            return 
                await this.context.Cubos.Where(x=>x.marca == marca).ToListAsync();
        }

        public async Task RegisterUser(int id, string nombre, string email, string pass, string imagen)
        {
            UsuarioCubo user = new UsuarioCubo();
            user.id_usuario = id;
            user.nombre = nombre;
            user.email = email;
            user.pass = pass;
            user.imagen = imagen;
            this.context.UsuariosCubos.Add(user);
            await this.context.SaveChangesAsync();
        }

        public async Task<UsuarioCubo> LogInUser(string email, string pass)
        {
            return await this.context.UsuariosCubos.Where
                (x => x.email == email && x.pass == pass).FirstOrDefaultAsync();
        }

        public async Task<UsuarioCubo> FindUser(int id)
        {
            return await this.context.UsuariosCubos
                .Where(x => x.id_usuario == id).FirstOrDefaultAsync();
        }

/*        public async Task PedidoUser(CompraCubos compra)
        {
            this.context.CompraCubos.Add(compra);
            await this.context.SaveChangesAsync();
        }     */   
        public async Task<List<CompraCubos>> GetPedidoUser(int id)
        {
            return await this.context.CompraCubos
                .Where(x => x.id_usuario == id).ToListAsync();
        }

    }
}
