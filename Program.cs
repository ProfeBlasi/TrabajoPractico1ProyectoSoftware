using AccesData.Context;
using AccesData.Queries;
using AccesData.Repositories;
using Aplication.Service;
using Domain.Commands;
using Domain.Interfaces.Queries;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
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
            Menu menu = new Menu();
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
            //_services.AddTransient<IAlquileresRepository, AlquileresRepository>();
            //_services.AddTransient<IAlquileresService, AlquileresService>();
            //alquilerService = _services.BuildServiceProvider().GetRequiredService<IAlquileresService>();
            //_services.AddTransient<IClienteRepository, ClienteRepository>();
            //_services.AddTransient<IClienteService, ClienteService>();
            //clienteService = _services.BuildServiceProvider().GetRequiredService<IClienteService>();
        }
    }
}