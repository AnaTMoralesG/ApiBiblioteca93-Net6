using Domain.Entities;
using WebApi93.Context;
using WebApi93.Services.Services;

namespace WebApi93.Services.IServices
{
    public interface IAutorServices
    {
        public Task<Response<List<Autor>>> GetAutores();

        public Task<Response<Autor>> Crear(Autor i);


    }
}
