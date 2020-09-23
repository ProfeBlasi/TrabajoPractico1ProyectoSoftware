using System;
using System.Collections.Generic;
using System.Text;

namespace TrabajoPractico1
{
    public class Menu
    {
        public void MenuEstrutura()
        {
            MenuInicio();
            int opcion=0;
            while(opcion != 5)
            { 
                try
                {
                    opcion = int.Parse(Console.ReadLine());
                    if (opcion > 5)
                    {
                        Console.Clear();
                        Console.WriteLine("Ha ingresado una opcion invalida");
                        MenuEstrutura();
                    }

                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("Ha ingresado una opcion invalida, porfavor vuelva a elegir la opcion");
                    MenuEstrutura();
                }
                switch (opcion)
                {
                    case 1:
                        Console.Clear();
                        MenuRegistrarCliente();
                        break;
                    case 2:
                        Console.Clear();
                        MenuRegistrarLosAlquileres();
                        break;
                    case 3:
                        Console.Clear();
                        MenuReservaConDetallesDelLibro();
                        break;
                    case 4:
                        Console.Clear();
                        MenuListarLibrosConStock();
                        break;
                }
                Console.Clear();
                Console.WriteLine();
                MenuInicio();
            }
            MenuFin();
        }
        public void MenuInicio()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("************************************************************************************");
            Console.WriteLine("                      Biblioteca Municipal de Carmen de Areco");
            Console.WriteLine("************************************************************************************");
            Console.WriteLine("************************************************************************************");
            Console.WriteLine("                           Presione la opcion deseada");
            Console.WriteLine("************************************************************************************");
            Console.WriteLine();
            Console.WriteLine("1 ** Registrar un cliente");
            Console.WriteLine("2 ** Registrar un alquiler, una reserva o una cancelacion");
            Console.WriteLine("3 ** Listar las reservas con los detalles de los libros");
            Console.WriteLine("4 ** Listar la informacion de los libros que tienen stock");
            Console.WriteLine("5 ** Salir");
        }
        public void MenuRegistrarCliente()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("************************************************************************************");
            Console.WriteLine("                             Registro de clientes");
            Console.WriteLine("************************************************************************************");
            Console.WriteLine();
            Console.WriteLine();
            CrudCliente crudCliente = CrudCliente.getInstance();
            crudCliente.registrarCliente();
            Console.WriteLine();
            Console.WriteLine("Pulse cualquier tecla para continuar");
            Console.ReadKey(true);
        }
        public void MenuRegistrarLosAlquileres()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("************************************************************************************");
            Console.WriteLine("           Registro de Reservas, Alquileres o cancelaciones");
            Console.WriteLine("************************************************************************************");
            Console.WriteLine();
            Console.WriteLine();
            CrudAlquileres crudAlquileres = CrudAlquileres.getInstance();
            crudAlquileres.registrarLosAlquileres();
            Console.WriteLine();
            Console.WriteLine("Pulse cualquier tecla para continuar");
            Console.ReadKey(true);
        }
        public void MenuReservaConDetallesDelLibro()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("************************************************************************************");
            Console.WriteLine("                             Reserva de libros");
            Console.WriteLine("************************************************************************************");
            Console.WriteLine();
            Console.WriteLine();
            CrudEstadoDeAlquileres crudEstadoDeAlquileres = CrudEstadoDeAlquileres.getInstance();
            crudEstadoDeAlquileres.mostrarReservaConDetalleDeLibro();
            Console.WriteLine();
            Console.WriteLine("Pulse cualquier tecla para continuar");
            Console.ReadKey(true);
        }
        public void MenuListarLibrosConStock()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("************************************************************************************");
            Console.WriteLine("                             Libros en stock");
            Console.WriteLine("************************************************************************************");
            Console.WriteLine();
            Console.WriteLine();
            CrudLibro crudLibro = CrudLibro.getInstance();
            crudLibro.mostrarListaDeLibrosConStock();
            Console.WriteLine();
            Console.WriteLine("Pulse cualquier tecla para continuar");
            Console.ReadKey(true);
        }
        public void MenuFin()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("************************************************************************************");
            Console.WriteLine("                      Gracias por utilizar nuestros servicios");
            Console.WriteLine("************************************************************************************");
            Console.WriteLine("************************************************************************************");
            Console.WriteLine("                         Saludos muy atte. Ezquiel Blasi");
            Console.WriteLine("************************************************************************************");
            Console.WriteLine();
        }
    }
}
