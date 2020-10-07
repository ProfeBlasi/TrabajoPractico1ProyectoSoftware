using AccesData.Commands;
using AccesData.Context;
using AccesData.Queries;
using Aplication.Service;
using Domain.Commands;
using Domain.Interfaces.Queries;
using Domain.Interfaces.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using TrabajoPractico1.Menu;

namespace TrabajoPractico1
{
    public class Program
    {
        public static IServiceCollection _services;
        public static IAlquileresService alquilerService;
        public static ILibroService libroService;
        public static IClienteService clienteService;
        static void Main(string[] args)
        {
            Inyection();
            _Menu menu = new _Menu();
            menu.MenuEstrutura();
        }
        private static void Inyection()
        {
            _services = new ServiceCollection();
            _services.AddDbContext<Contexto>();
            _services.AddTransient<IGenericRepository, GenericsRepository>();
            _services.AddTransient<ILibroService, LibroService>();
            _services.AddTransient<ILibroQuery, LibroQuery>();
            libroService = _services.BuildServiceProvider().GetRequiredService<ILibroService>();
            _services.AddTransient<IClienteService, ClienteService>();
            _services.AddTransient<IClienteQuery, ClienteQuery>();
            clienteService = _services.BuildServiceProvider().GetRequiredService<IClienteService>();
            _services.AddTransient<IAlquileresService, AlquileresService>();
            _services.AddTransient<IAlquileresQuery, AlquileresQuery>();
            alquilerService = _services.BuildServiceProvider().GetRequiredService<IAlquileresService>();
        }
    }
}