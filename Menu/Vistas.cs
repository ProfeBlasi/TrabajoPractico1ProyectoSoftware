using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrabajoPractico1.Menu
{
    public class Vistas
    {
        private static Vistas instance = null;
        public static Vistas getInstance()
        {
            if(instance==null)
            {
                instance = new Vistas();
            }
            return instance;
        }
        public void MenuPrincipal()
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
        public void OpcionInvalida()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Ha ingresado una opcion invalida, porfavor vuelva a elegir la opcion");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Presione una tecla para continuar");
        }
        public void RegistroDeClientes()
        {
            Console.WriteLine("************************************************************************************");
            Console.WriteLine("                             Registro de clientes");
            Console.WriteLine("************************************************************************************");
            Console.WriteLine();
        }
        public void RegistroReservasAlquileresCancelaciones()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("************************************************************************************");
            Console.WriteLine("           Registro de Reservas, Alquileres o cancelaciones");
            Console.WriteLine("************************************************************************************");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }
        public void ReservaDeLibros()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("************************************************************************************");
            Console.WriteLine("                             Reserva de libros");
            Console.WriteLine("************************************************************************************");
            Console.WriteLine();
            Console.WriteLine();
        }
        public void LibrosEnStock()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("************************************************************************************");
            Console.WriteLine("                             Libros en stock");
            Console.WriteLine("************************************************************************************");
            Console.WriteLine();
            Console.WriteLine();
        }
        public void FinDeVista()
        {
            Console.WriteLine();
            Console.WriteLine("Pulse cualquier tecla para continuar");
            Console.ReadKey(true);
        }
        public void FinDeMenu()
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
            Console.WriteLine("Pulse una tecla para finalizar");
            Console.ReadKey(true);
        }
    }
}
