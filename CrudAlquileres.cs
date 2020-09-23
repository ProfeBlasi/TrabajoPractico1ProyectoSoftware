using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace TrabajoPractico1
{
    public class CrudAlquileres
    {
        static CrudAlquileres instance = null;
        CrudCliente crudCliente = CrudCliente.getInstance();
        CrudLibro crudLibro = CrudLibro.getInstance();
        public static CrudAlquileres getInstance()
        {
            if (instance == null)
            {
                instance = new CrudAlquileres();
            }
            return instance;
        }
        public void registrarLosAlquileres()
        {
            Console.WriteLine("Ingrese el dni del cliente");
            string dniString = Console.ReadLine();
            Console.WriteLine("Ingrese el isbn del libro");
            string isbn = Console.ReadLine();
            int dni = int.Parse(dniString);
            if (VerificoCliente(dni))
                if (VerificoLibro(isbn))
                {
                    //Si el cliente existe y el libro esta registrado en la db le consultamos cual
                    //es el proceso que desea realizar
                    int estadoId = EstadoId();
                    switch (estadoId)
                    {
                        case 1:
                            //En esta funcion se puede alquilar con reserva previa o sin reserva
                            TipoDeAlquiler(dni, isbn, estadoId);
                            break;
                        case 2:
                            if (VerificoStock(isbn))
                            {
                                //Si hay stock se reserva el libro al cliente seleccionado
                                Reservar(dni, isbn, estadoId);
                            }
                            break;
                        case 3:
                            //Con esta opcion se puede concelar una reserva o un alquiler
                            Cancelar(dni, isbn);
                            break;
                    }
                }
        }
        //ESte metodo devuelve si o si la opcion 1, 2 0 3 referidas a un alquiler, una reserva o una cancelacion
        private int EstadoId()
        {
            Console.WriteLine("Ingrese la opcion deseada");
            Console.WriteLine("1 ** Si va a registrar un alquiler");
            Console.WriteLine("2 ** Si va a registrar una reserva");
            Console.WriteLine("3 ** Si se va a cancelar un alquiler o una reserva");
            string estado = Console.ReadLine();
            bool ver = false;
            if (estado == "1" || estado == "2" || estado == "3")
            {
                ver = true;
            }
            while (ver == false)
            {
                Console.WriteLine("Se detecto que ingreso un valor de estado incorrecto, intentelo nuevamente");
                Console.WriteLine("1 ** Si va a registrar un alquiler");
                Console.WriteLine("2 ** Si va a registrar una reserva");
                Console.WriteLine("3 ** Si se va a cancelar un alquiler o una reserva");
                estado = Console.ReadLine();
                if (estado == "1" || estado == "2" || estado == "3")
                {
                    ver = true;
                }
            }
            int estadoId = int.Parse(estado);
            return estadoId;
        }
        //Filtramos si es un alquiler con reserva previa o no
        private void TipoDeAlquiler(int dni, string isbn, int estadoId)
        {
            Console.WriteLine("¿El alquiler es con reserva anticipada?, presione la opcion correcta");
            Console.WriteLine("1- SI");
            Console.WriteLine("2- NO");
            string tipoDeAlquiler = Console.ReadLine();
            bool ver = false;
            if (tipoDeAlquiler == "1" || tipoDeAlquiler == "2")
            {
                ver = true;
            }
            while (ver == false)
            {
                Console.WriteLine("Se detecto que ingreso un valor de estado incorrecto, intentelo nuevamente");
                Console.WriteLine("¿El alquiler es con reserva anticipada?, presione la opcion correcta");
                Console.WriteLine("1- SI");
                Console.WriteLine("2- NO");
                tipoDeAlquiler = Console.ReadLine();
                if (tipoDeAlquiler == "1" || tipoDeAlquiler == "2")
                {
                    ver = true;
                }
            }
            if (tipoDeAlquiler == "1")
            {
                alquilerConAnticipacion(dni, isbn, estadoId);
            }
            if (tipoDeAlquiler == "2")
            {
                if (VerificoStock(isbn))
                {
                    alquilerSinAntipacion(dni, isbn, estadoId);
                }
            }
        }
        //Similar al metodo existe cliente pero deja una leyenda al final
        private bool VerificoCliente(int dni)
        {
            bool existe = crudCliente.ExisteCliente(dni);
            if (existe == true)
            {
                return true;
            }
            else
            {
                Console.WriteLine("El cliente no se encuentra registrado en el sistema");
                return false;
            }
        }
        //Similar al metodo existe libro pero deja una leyenda al final
        private bool VerificoLibro(string isbn)
        {
            bool existe = crudLibro.ExisteISBN(isbn);
            if (existe == true)
            {
                return true;
            }
            else
            {
                Console.WriteLine("El ISBN del libro no se encuentra registrado en el sistema");
                return false;
            }
        }
        private bool VerificoStock(string isbn)
        {
            bool existe = crudLibro.ExisteStock(isbn);
            if (existe == true)
            {
                return true;
            }
            else
            {
                Console.WriteLine("En este momento no contamos con stock para ese libro");
                return false;
            }
        }
        private void alquilerSinAntipacion(int dni, string isbn, int estadoId)
        {
            using (Contexto contexto = new Contexto())
            {
                Alquileres alqui = new Alquileres();
                {
                    alqui.Cliente = crudCliente.getClienteId(dni);
                    alqui.ISBN = isbn;
                    crudLibro.DescuentoStock(isbn);
                    alqui.Estado = estadoId;
                    alqui.FechaAlquiler = DateTime.Today;
                };
                contexto.Alquileres.Add(alqui);
                contexto.SaveChanges();
                Console.WriteLine("El proceso se registro exitosamente, presione una tecla para continuar");
            }
        }
        private void alquilerConAnticipacion(int dni, string isbn, int estadoId)
        {
            using (Contexto contexto = new Contexto())
            {
                int clienteId = crudCliente.getClienteId(dni);
                List<Alquileres> lista = (from x in contexto.Alquileres where x.Cliente == clienteId && x.ISBN == isbn && x.Estado == 2 select x).ToList();
                if (lista.Count != 0)
                {
                    Alquileres reserva = lista[0];
                    reserva.FechaReserva = null;
                    reserva.FechaAlquiler = DateTime.Today;
                    reserva.Estado = 1;
                    contexto.Alquileres.Update(reserva);
                    contexto.SaveChanges();
                    Console.WriteLine("El proceso se registro exitosamente, presione una tecla para continuar");
                }
                else
                {
                    Console.WriteLine("El cliente no tiene ninguna reserva registrada");
                }
            }
        }
        private void Reservar(int dni, string isbn, int estadoId)
        {
            using (Contexto contexto = new Contexto())
            {
                Alquileres alqui = new Alquileres();
                {
                    alqui.Cliente = crudCliente.getClienteId(dni);
                    alqui.ISBN = isbn;
                    crudLibro.DescuentoStock(isbn);
                    alqui.Estado = estadoId;
                    alqui.FechaReserva = DateTime.Today;
                };
                contexto.Alquileres.Add(alqui);
                contexto.SaveChanges();
                Console.WriteLine("El proceso se registro exitosamente, presione una tecla para continuar");
            }
        }
        private void Cancelar(int dni, string isbn)
        {
            using (Contexto contexto = new Contexto())
            {
                int clienteId = crudCliente.getClienteId(dni);
                List<Alquileres> lista = (from x in contexto.Alquileres where x.Cliente == clienteId && x.ISBN == isbn select x).ToList();
                if (lista.Count != 0)
                {
                    Alquileres reserva = lista[0];
                    crudLibro.AumentoStock(isbn);
                    contexto.Alquileres.Remove(reserva);
                    contexto.SaveChanges();
                    Console.WriteLine("El proceso se registro exitosamente, presione una tecla para continuar");
                }
                else
                {
                    Console.WriteLine("El cliente o el libro no tienen una reserva asociada");
                }
            }
        }
    }
}
