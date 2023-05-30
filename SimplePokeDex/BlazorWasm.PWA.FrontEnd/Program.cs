using BlazorWasm.Compartilhado.Entidades;
using BlazorWasm.FrontEnd.Helpers;
using BlazorWasm.FrontEnd.Repositorio;
using BlazorWasm.PWA.FrontEnd;
using BlazorWasm.PWA.FrontEnd.Repositorio;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
ConfigureServices(builder.Services);

await builder.Build().RunAsync();



//Registro dos Servicos Que serao utilizados via injecao de dependencia nos componentes/paginas RAZOR
 static void ConfigureServices(IServiceCollection services)
{
    services.AddScoped<IHttpService, HttpService>();

    //ATivar Repositorio Verdadeiro (no SGBD)
    services.AddTransient<IRepository<Categoria>, CategoriaRepository>();
    services.AddTransient<IRepository<Produto>, ProdutoRepository>();
    services.AddTransient <IRepository<Pokemon>, PokemonRepository>();

    //ATIVAR Repositorio em Memoria (Fake)
    //services.AddSingleton<IRepository<Categoria>, RepositoryInMemoryCategoria>();
    //services.AddSingleton<IRepository<Produto>, RepositoryInMemoryProduto>();

}