using BlazorWasm.Compartilhado.Entidades;
using BlazorWasm.FrontEnd.Helpers;
using BlazorWasm.FrontEnd.Repositorio;

namespace BlazorWasm.PWA.FrontEnd.Repositorio
{
    public class PokemonRepository : IRepository<Pokemon>
    {
        private readonly IHttpService httpService;
        private string url = "api/pokemon";

        public PokemonRepository(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<List<Pokemon>> Get()
        {
            var response = await httpService.Get<List<Pokemon>>(url);

            if (!response.Sucesso)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }
        public async Task<Pokemon> Get(int Id)
        {
            var response = await httpService.Get<Pokemon>($"{url}/{Id}");
            if (!response.Sucesso)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }


        public async Task Add(Pokemon item)
        {

            var response = await httpService.Post(url, item);
            if (response.Sucesso == false)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }
        public async Task<int> AddAndGetId(Pokemon item)
        {
            var response = await httpService.Post<Pokemon, int>(url, item);
            if (response.Sucesso == false)
            {
                throw new ApplicationException(await response.GetBody());
            }
            return response.Response;
        }


        public async Task Update(Pokemon item)
        {
            var response = await httpService.Put(url, item);
            if (!response.Sucesso)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }

        public async Task Delete(int Id)
        {
            var response = await httpService.Delete($"{url}/{Id}");
            if (!response.Sucesso)
            {
                throw new ApplicationException(await response.GetBody());
            }
        }


    }
}
