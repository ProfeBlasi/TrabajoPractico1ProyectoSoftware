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
            //Validaciones val = new Validaciones();
            //CrudLibro crudLibro = CrudLibro.getInstance();

            DateTime fecha = DateTime.Today;
            DateTime fecha2 = fecha.AddDays(7);

            Console.WriteLine(fecha.ToString("dd/MM/yyyy"));
            Console.WriteLine(fecha2.ToString("dd/MM/yyyy"));
            if (fecha2 < fecha)
                Console.WriteLine("Adelante");

            Console.WriteLine("Salio bien");
            Console.ReadKey(true);
        }
    }
}