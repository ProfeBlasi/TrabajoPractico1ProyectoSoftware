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
        public void CominezoRegistro()
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
                Alquileres alquiler = proceso(dni, isbn, estadoId, DateTime.Now, null, DateTime.Now.AddDays(7), true);
                contexto.Alquileres.Add(alquiler);
                contexto.SaveChanges();
                Console.WriteLine(crudCliente.getNombre(dni) + " alquilo " + crudLibro.getTitulo(isbn) + " hasta el dia " + DateTime.Now.AddDays(7).ToString("dd/MM/yyyy"));
            }
            else
            {
                Console.WriteLine("En este momento no contamos con stock para realizar el alquiler");
            }
        }
        
        private Alquileres proceso(int dni, string isbn, int estadoId, DateTime? fechaAlquiler, DateTime? fechaReserva, DateTime? fechaDevolucion, bool descuento)
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
        private void alquilerConAnticipacion(int dni, string isbn, int estadoId)
        {
            using (Contexto contexto = new Contexto())
            {
                DateTime? fecha = null;
                int clienteId = crudCliente.getId(dni);
                List<Alquileres> lista = (from x in contexto.Alquileres where x.Cliente == clienteId && x.ISBN == isbn && x.Estado == 2 select x).ToList();
                if (lista.Count != 0)
                {
                    Alquileres reserva = lista[0];
                    reserva.FechaReserva = null;
                    reserva.FechaAlquiler = DateTime.Today;
                    reserva.FechaDevolucion = DateTime.Today.AddDays(7);
                    reserva.Estado = 1;
                    contexto.Alquileres.Update(reserva);
                    contexto.SaveChanges();
                    fecha = reserva.FechaDevolucion;
                    string nombre = crudCliente.getNombre(crudCliente.getId(dni));
                    string titulo = crudLibro.getTitulo(isbn);
                    DateTime fechaString = Convert.ToDateTime(fecha);
                    Console.WriteLine("El sr/a " + nombre + " alquilo " + titulo + " hasta " + fechaString.ToString("dd-MM-yyyy")); Console.WriteLine("El proceso se registro exitosamente, presione una tecla para continuar");
                }
                else
                {
                    Console.WriteLine("El cliente no tiene ninguna reserva registrada");
                }
            }
        }
        private void Reservar(int dni, string isbn, int estadoId)
        {
            DateTime? fechaVacia = null;
            if (crudLibro.ExisteStock(isbn))
            {
                DateTime fecha = Convert.ToDateTime(fechaVacia);
                fecha = DateTime.Today;
                ProcesoDeReserva(dni, isbn, estadoId, fecha);
            }
            else
            {
                using (Contexto contexto = new Contexto())
                {
                    List<Alquileres> lista = (from x in contexto.Alquileres where x.ISBN == isbn && x.Estado == 1 select x).ToList();
                    if (lista.Count != 0)
                    {
                        var list = lista.OrderBy(x => x.FechaDevolucion).ToList();
                        DateTime fecha = Convert.ToDateTime(list[0].FechaDevolucion);
                        Console.WriteLine("En este momento no contamos con stock, sin embargo puede reservar para la fecha");
                        Console.WriteLine(fecha.ToString("dd-MM-yyyy") + " que el libro sera devuelto de su alquiler");
                        Console.WriteLine("Desea registrar la reserva presione 1 para confirmar o cualquier tecla para salir");
                        string respuesta = Console.ReadLine();
                        if (respuesta == "1")
                        {
                            ProcesoDeReserva(dni, isbn, estadoId, fecha);
                            Console.WriteLine("Su reserva se registro exitosamente para la fecha " + fecha.ToString("dd-MM-yyyy"));
                        }
                    }
                    else
                    {
                        Console.WriteLine("En este momento no tenemos devoluciones pendientes para registrar una reserva");
                        Console.WriteLine("Esta es la lista de clientes que poseen una reserva");
                        Console.WriteLine();
                        //List<Alquileres> listaDeAlquileres = crudEstadoDeAlquileres.ListaDeReservas(isbn);
                        //crudEstadoDeAlquileres.mostrarReservaConDetalleDeLibro(listaDeAlquileres);
                        Console.WriteLine();
                        Console.WriteLine("Preione 1, si desea cancelar alguna reserva pendiente o cualquier tecla para continuar");
                        string respuesta = Console.ReadLine();
                        if (respuesta == "1")
                        {
                            try
                            {
                                Console.WriteLine("Ingrese el dni del cliente que va a cancelar su reserva");
                                string dniString = Console.ReadLine();
                                int dniInt = int.Parse(dniString);
                                List<Alquileres> listaCancelar = (from x in contexto.Alquileres where x.cliente.ClienteId == crudCliente.getId(dniInt) && x.Libro.ISBN == isbn && x.EstadoId.EstadoId == 2 select x).ToList();
                                if (listaCancelar.Count != 0)
                                {
                                    Cancelar(dniInt, isbn, estadoId);
                                    Reservar(dni, isbn, estadoId);
                                }
                                else
                                {
                                    Console.WriteLine("Se produjo un error, intentelo nuevamente");
                                }
                            }
                            catch (FormatException e)
                            {
                                Console.WriteLine("Se produjo un error inesperado " + e.Message);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No se cancelo ningun registro");
                        }
                    }

                }
            }

        }
        private void Cancelar(int dni, string isbn, int estado)
        {
            using (Contexto contexto = new Contexto())
            {
                try
                {
                    int clienteId = crudCliente.getId(dni);
                    List<Alquileres> lista = (from x in contexto.Alquileres where (x.Cliente == clienteId && x.ISBN == isbn) && (x.Estado == 2 || x.Estado == 1) select x).ToList();
                    var list = lista.OrderBy(x => x.FechaDevolucion).ToList();
                    int masAntiguo = list.Count;
                    if (list.Count != 0)
                    {
                        Alquileres reserva = lista[masAntiguo - 1];
                        reserva.FechaReserva = null;
                        reserva.FechaAlquiler = null;
                        reserva.FechaDevolucion = null;
                        reserva.Estado = 3;
                        contexto.Alquileres.Update(reserva);
                        contexto.SaveChanges();
                        crudLibro.AumentoStock(isbn);
                        Console.WriteLine("Se cancelo el registro mas antiguo del cliente seleccionado");
                    }
                    else
                    {
                        Console.WriteLine("El cliente o el libro no tienen una reserva asociada");
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine("Se produjo un error inesperado " + e.Message);
                }
            }
        }
        private void ProcesoDeReserva(int dni, string isbn, int estadoId, DateTime fecha)
        {
            using (Contexto contexto = new Contexto())
            {
                Alquileres alqui = new Alquileres();
                {
                    alqui.Cliente = crudCliente.getId(dni);
                    alqui.ISBN = isbn;
                    crudLibro.DescuentoStock(isbn);
                    alqui.Estado = estadoId;
                    alqui.FechaReserva = fecha;
                };
                contexto.Alquileres.Add(alqui);
                contexto.SaveChanges();
                string nombre = crudCliente.getNombre(crudCliente.getId(dni));
                //string titulo = crudLibro.getTituloLibro(isbn);
                //Console.WriteLine("El libro " + titulo + " fue reservado al sr/a " + nombre + ", y quitado del stock para que lo pueda alquilar");
                Console.WriteLine("De lo contrario el administrador puede cancelar la reserva considerando un tiempo limitado de espera");
            }
        }
    }
}