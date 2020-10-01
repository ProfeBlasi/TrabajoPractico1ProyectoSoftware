//using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using System;
//using System.Net.Http.Headers;
namespace TrabajoPractico1
{
    public class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new Menu();
            menu.MenuEstrutura();
            Validaciones val = new Validaciones();
            CrudLibro crudLibro = CrudLibro.getInstance();
            Console.WriteLine("Salio bien");
            Console.ReadKey(true);
        }
    }
}