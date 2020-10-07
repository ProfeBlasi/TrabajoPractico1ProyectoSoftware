using AccesData.Commands;
using AccesData.Context;
using AccesData.Queries;
using Aplication.Service;
using Domain.Commands;
using Domain.DTO;
using Domain.Entities;
using Domain.Interfaces.Queries;
using Domain.Interfaces.Service;
using System;
using System.Collections.Generic;

namespace TrabajoPractico1.Menu
{
    public class _Menu
    {
        Vistas vista = Vistas.getInstance();
        public void MenuEstrutura()
        {
            string opcion = "";
            do
            {
                Console.Clear();
                vista.MenuPrincipal();
                opcion = Console.ReadLine();
                switch (opcion)
                {
                    case "1":
                        Console.Clear();
                        MenuRegistrarCliente();
                        break;
                    case "2":
                        Console.Clear();
                        MenuRegistrarLosAlquileres();
                        break;
                    case "3":
                        Console.Clear();
                        MenuReservaConDetallesDelLibro();
                        break;
                    case "4":
                        Console.Clear();
                        MenuListarLibrosConStock();
                        break;
                    case "5":
                        Console.Clear();
                        vista.FinDeMenu();
                        break;
                    default:
                        Console.Clear();
                        vista.OpcionInvalida();
                        Console.ReadKey(true);
                        Console.Clear();
                        break;
                }
            } while (opcion != "5");
        }
        public void MenuRegistrarCliente()
        {
            vista.RegistroDeClientes();
            Console.WriteLine("Ingrese el nombre del cliente ");
            string nombre = Validaciones.SoloLetras(Console.ReadLine());
            Console.WriteLine("Ingrese apellido del cliente ");
            string apellido = Validaciones.SoloLetras(Console.ReadLine());
            Console.WriteLine("Ingrese el dni del cliente");
            string dni = Validaciones.SoloNumeros(Console.ReadLine());
            Console.WriteLine("Ingrese el mail del cliente");
            string email = Validaciones.ComprobarFormatoEmail(Console.ReadLine());
            string mensaje = Program.clienteService.RegistrarCliente(dni,nombre,apellido,email);
            Console.WriteLine(mensaje);
            vista.FinDeVista();
        }
        public void MenuRegistrarLosAlquileres()
        {
            vista.RegistroReservasAlquileresCancelaciones();
            Console.WriteLine("Ingrese el dni del cliente");
            string dni = Validaciones.SoloNumeros(Console.ReadLine());
            Console.WriteLine("Ingrese el isbn del libro");
            string isbn = Validaciones.SoloNumeros(Console.ReadLine());
            Console.WriteLine("Ingrese la opcion deseada");
            Console.WriteLine("1 Si va a registrar un alquiler ** " + "2 Si va a registrar una reserva ** " + "3 Si se va a cancelar un alquiler o una reserva");
            string estado = Validaciones.SoloNumeros(Console.ReadLine());
            bool estadoCorrecto = true;
            do
            {
                switch (estado)
                {
                    case "1":
                        break;
                    case "2":
                        break;
                    case "3":
                        break;
                    default:
                        Console.WriteLine("Se ingreso una opcion incorrecta, vuelva a intentarlo");
                        break;
                }
                if (estado == "1" || estado == "2" || estado == "3")
                    estadoCorrecto = false;
            } while (estadoCorrecto);
            int estadoId = Validaciones.ConvertirNumero(estado);
            string mensaje = Program.alquilerService.RegistrarProceso(dni, isbn, estadoId);
            Console.WriteLine(mensaje);
            vista.FinDeVista();
        }
        public void MenuReservaConDetallesDelLibro()
        {
            vista.ReservaDeLibros();
            List<DetalleReserva> lista = Program.alquilerService.GetReservas();
            if (lista.Count != 0)
            {
                foreach (DetalleReserva x in lista)
                {
                    Console.WriteLine("Titulo:                  " + x.Titulo);
                    Console.WriteLine("Fecha de reserva:        " + x.FechaReserva?.ToString("dd/MM/yyyy"));
                    Console.WriteLine("Reservado por            " + x.ApellidoNombre);
                    Console.WriteLine("Isbn:                    " + x.ISBN);
                    Console.WriteLine("Editorial:               " + x.Editorial);
                    Console.WriteLine("Escrito por:             " + x.Autor);
                    Console.WriteLine("ISBN:                    " + x.ISBN);
                    Console.WriteLine("Edicion:                 " + x.Edicion);
                }
            }
            else
            {
                Console.WriteLine("No se ha registrado niguna reserva");
            }
            vista.FinDeVista();
        }
        public void MenuListarLibrosConStock()
        {
            vista.LibrosEnStock();
            List<Libros> lista = Program.libroService.GetLibros();
            if (lista == null)
                Console.WriteLine("No tenemos registrados libros con stock en este momento");
            else
                foreach (Libros x in lista)
                {
                    Console.WriteLine("Titulo:                  " + x.Titulo);
                    Console.WriteLine("Editorial:               " + x.Editorial);
                    Console.WriteLine("Escrito por:             " + x.Autor);
                    Console.WriteLine("ISBN:                    " + x.ISBN);
                    Console.WriteLine("Edicion:                 " + x.Edicion);
                    if (x.Stock == 1)
                        Console.WriteLine("Contamos con             " + x.Stock + " unidad");
                    else
                        Console.WriteLine("Contamos con             " + x.Stock + " unidades");
                    Console.WriteLine();
                }
            vista.FinDeVista();
        }
    }
}