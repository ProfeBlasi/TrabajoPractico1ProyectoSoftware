using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
namespace TrabajoPractico1
{
    public class CrudAlquileres
    {
        static CrudAlquileres instance = null;
        CrudCliente crudCliente = CrudCliente.getInstance();
        CrudLibro crudLibro = CrudLibro.getInstance();
        Contexto contexto = Contexto.getInstance();
        public static CrudAlquileres getInstance()
        {
            if (instance == null)
            {
                instance = new CrudAlquileres();
            }
            return instance;
        }
        public void ComienzoRegistro()
        {
            Console.WriteLine("Ingrese el dni del cliente");
            string dniString = Validaciones.SoloNumeros(Console.ReadLine());
            int dni = Validaciones.ConvertirNumero(dniString);
            if(crudCliente.ExisteCliente(dni))
            {
                Console.WriteLine("Ingrese el isbn del libro");
                string isbn = Validaciones.SoloNumeros(Console.ReadLine());
                if (crudLibro.ExisteISBN(isbn))
                {
                    int estadoId = EstadoId();
                    switch (estadoId)
                    {
                        case 1:
                            Alquiler(dni, isbn, estadoId);
                            break;
                        case 2:
                            Reservar(dni, isbn, estadoId);
                            break;
                        case 3:
                            Cancelar(dni, isbn, estadoId);
                            break;
                    }
                }
                else
                    Console.WriteLine("El libro no se encuentra registrado");
            }
            else
                Console.WriteLine("El cliente no se encuentra registrado");
        }
        private int EstadoId()
        {
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
            return estadoId;
        }
        private void Alquiler(int dni, string isbn, int estadoId)
        {
            if (crudLibro.ExisteStock(isbn))
            {
                Alquileres alquiler = GeneraObjeto(dni, isbn, estadoId, DateTime.Now, null, DateTime.Now.AddDays(7), true);
                contexto.Alquileres.Add(alquiler);
                contexto.SaveChanges();
                Console.WriteLine(crudCliente.getNombre(dni) + " alquilo " + crudLibro.getTitulo(isbn) + " hasta el dia " + DateTime.Now.AddDays(7).ToString("dd/MM/yyyy"));
            }
            else
            {
                Console.WriteLine("En este momento no contamos con stock para realizar el alquiler");
            }
        }
        private void Reservar(int dni, string isbn, int estadoId)
        {
            if (crudLibro.ExisteStock(isbn))
            {
                Alquileres reserva = GeneraObjeto(dni, isbn, estadoId, null, DateTime.Now, null, true);
                contexto.Alquileres.Add(reserva);
                contexto.SaveChanges();
                Console.WriteLine(crudCliente.getNombre(dni) + " reservo " + crudLibro.getTitulo(isbn) + " con fecha " + DateTime.Now.ToString("dd/MM/yyyy"));
            }
            else
            {
                Console.WriteLine("En este momento no contamos con stock para realizar una reserva");
            }
        }
        private void Cancelar(int dni, string isbn, int estadoId)
        {
            List<Alquileres> AlquilerReserva = ExisteAlquileroReserva(crudCliente.getId(dni), isbn);
            if (AlquilerReserva.Any())
            {
                Alquileres cancelacion = GeneraObjeto(dni, isbn, estadoId, null, null, null, false);
                Alquileres auxiliar = AlquilerReserva.Last();
                auxiliar = cancelacion;
                contexto.Alquileres.Update(auxiliar);
                contexto.SaveChanges();
                Console.WriteLine(crudCliente.getNombre(dni) + " cancelo " + crudLibro.getTitulo(isbn));
            }
            else
            {
                Console.WriteLine("El cliente no tiene ninguna reserva o alquiler registrado");
            }
        }
        private Alquileres GeneraObjeto(int dni, string isbn, int estadoId, DateTime? fechaAlquiler, DateTime? fechaReserva, DateTime? fechaDevolucion, bool descuento)
        {
            Alquileres alqui = new Alquileres();
            {
                alqui.Cliente = crudCliente.getId(dni);
                alqui.ISBN = isbn;
                if (descuento)
                    crudLibro.DescuentoStock(isbn);
                else
                    crudLibro.AumentoStock(isbn);
                alqui.Estado = estadoId;
                alqui.FechaAlquiler = fechaAlquiler;
                alqui.FechaReserva = fechaReserva;
                alqui.FechaDevolucion = fechaDevolucion;
            };
            return alqui;
        }
        private List<Alquileres> ExisteAlquileroReserva(int clienteId, string isbn)
        {
            List<Alquileres> lista = (from x in contexto.Alquileres where (x.Cliente == clienteId && x.ISBN == isbn) && (x.Estado == 2 || x.Estado == 1) select x).ToList();
            var list = lista.OrderBy(x => x.FechaDevolucion).ToList();
            return list;
        }
    }
}