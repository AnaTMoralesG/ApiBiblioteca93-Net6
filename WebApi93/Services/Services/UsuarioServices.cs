using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using WebApi93.Context;
using WebApi93.Services.IServices;

namespace WebApi93.Services.Services
{
    public class UsuarioServices : IUsuarioServices
    {
        private readonly ApplicationDbContext _context;
        public string Mensaje;

        //Creación de un constructor
        public UsuarioServices(ApplicationDbContext context)
        {
            //_ significa que es privado
            _context = context;
        }

        //Lista de usuarios
        public async Task<Response<List<Usuario>>> ObtenerUsuarios () 
        {
            try
            {
                List<Usuario> response = await _context.Usuarios.Include(y=> y.Roles).ToListAsync();
                return new Response<List<Usuario>>(response);

            }
            catch (Exception ex) 
            {
                throw new Exception("Sucedio un error" + ex.Message);
            }

        }

        //Obtener Usuario
        public async Task<Response<Usuario>> ByID(int id)
        {
            try
            {
                Usuario usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.PkUsuario == id);
                //Usuario usuario = _context.Usuarios.Find(id);

                return new Response<Usuario>(usuario);


            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error" + ex.Message);
            }
        }

        //Crear Usuario
        public async Task<Response<Usuario>> Crear(UsuarioResponse request)
        {
            try
            {
                //instanciar un objeto
                //Forma uno 
                    //Usuario usuario = new Usuario();

                    //usuario.Nombre = request.Nombre;
                    //usuario.User = request.User;
                    //usuario.Password = request.Password;
                    //usuario.FkRol = request.FkRol;

                //Forma dos 
                Usuario usuario = new Usuario()
                {
                    Nombre = request.Nombre,
                    User = request.User,
                    Password = request.Password,
                    FkRol = request.FkRol
                };

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                return new Response<Usuario>(usuario, "Se agrego");


            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error" + ex.Message);
            }

        }

        //Eliminar usuario
        public async Task<Response<Usuario>> DeleteUser(int id)
        {
            try
            {
                Usuario usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.PkUsuario == id);
                //Usuario usuario = _context.Usuarios.Find(id);
                
                if (usuario == null)
                {
                    throw new Exception("No existe usuario");

                }

                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();

                return new Response<Usuario>(usuario);



            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error" + ex.Message);
            }
        }

        //Editar Usuario
        public async Task<Response<Usuario>> EditarUsuario(int id, UsuarioResponse request)
        {
            try
            {
                Usuario usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.PkUsuario == id);
               
                if (usuario == null)
                {
                    throw new Exception("No existe usuario");

                }
                usuario.Nombre = request.Nombre;
                usuario.User = request.User;
                usuario.Password = request.Password;
                usuario.FkRol = request.FkRol;

                await _context.SaveChangesAsync();

                return new Response<Usuario>(usuario, "Se editó");


            }
            catch (Exception ex)
            {
                throw new Exception("Sucedio un error" + ex.Message);
            }

        }

    }
}
