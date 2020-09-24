using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using System.Runtime.Serialization;

namespace TrabajoPractico1
{
    public class CrudAlquileres
    {
        static CrudAlquileres instance = null;
        CrudCliente crudCliente = CrudCliente.getInstance();
        CrudLibro crudLibro = CrudLibro.getInstance();
        CrudEstadoDeAlquileres crudEstadoDeAlquileres = CrudEstadoDeAlquileres.getInstance();
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
            try
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
                                Reservar(dni, isbn, estadoId);
                                break;
                            case 3:
                                //Con esta opcion se puede concelar una reserva o un alquiler
                                Cancelar(dni, isbn);
                                break;
                        }
                    }
            }
            catch (FormatException e)
            {
                Console.WriteLine("Se produjo un error inesperado " + e.Message);
            }

        }
        //ESte metodo devuelve si o si la opcion 1, 2 0 3 referidas a un alquiler, una reserva o una cancelacion
        private int EstadoId()
        {
            try
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
            catch (FormatException e)
            {
                Console.WriteLine("Se produjo un error inesperado " + e.Message);
                //En este caso no va a entrar ninguna opcion del codigo
                return 0;
            }
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
                return false;
            }
        }
        private void alquilerSinAntipacion(int dni, string isbn, int estadoId)
        {
            using (Contexto contexto = new Contexto())
            {
                DateTime? fecha = null;
                Alquileres alqui = new Alquileres();
                {
                    alqui.Cliente = crudCliente.getClienteId(dni);
                    alqui.ISBN = isbn;
                    crudLibro.DescuentoStock(isbn);
                    alqui.Estado = estadoId;
                    alqui.FechaAlquiler = DateTime.Today;
                    alqui.FechaDevolucion = DateTime.Today.AddDays(7);
                    fecha = alqui.FechaDevolucion;
                };
                contexto.Alquileres.Add(alqui);
                contexto.SaveChanges();
                string nombre = crudCliente.getNombreApellido(crudCliente.getClienteId(dni));
                string titulo = crudLibro.getTituloLibro(isbn);
                DateTime fechaString = Convert.ToDateTime(fecha);
                Console.WriteLine("El sr/a " + nombre + " alquilo " + titulo + " hasta " + fechaString.ToString("dd-MM-yyyy"));
            }
        }
        private void alquilerConAnticipacion(int dni, string isbn, int estadoId)
        {
            using (Contexto contexto = new Contexto())
            {
                DateTime? fecha = null;
                int clienteId = crudCliente.getClienteId(dni);
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
                    string nombre = crudCliente.getNombreApellido(crudCliente.getClienteId(dni));
                    string titulo = crudLibro.getTituloLibro(isbn);
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
            //Si hay stock
            if (crudLibro.ExisteStock(isbn))
            {
                DateTime fecha = Convert.ToDateTime(fechaVacia);
                fecha = DateTime.Today;
                ProcesoDeReserva(dni, isbn, estadoId, fecha);
            }
            

            //Si no hay stock
            else
            {
                using (Contexto contexto = new Contexto())
                {
                    List<Alquileres> lista = (from x in contexto.Alquileres where x.ISBN == isbn && x.Estado == 1 select x).ToList();
                    if (lista.Count != 0)
                    {
                        //Lista ordenada en forma ascendente
                        var list = lista.OrderBy(x => x.FechaDevolucion).ToList();
                        DateTime fecha = Convert.ToDateTime(list[0].FechaDevolucion);
                        Console.WriteLine("En este momento no contamos con stock, sin embargo puede reservar para la fecha");
                        Console.WriteLine(fecha.ToString("dd-MM-yyyy") + " que el libro sera devuelto de su alquiler");
                        Console.WriteLine("Desea registrar la reserva presione 1 para confirmar o cualquier tecla para salir");
                        string respuesta = Console.ReadLine();
                        if(respuesta == "1")
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
                        List<Alquileres> listaDeAlquileres = crudEstadoDeAlquileres.listaDeAlquileresReservadoEspecifico(isbn);
                        crudEstadoDeAlquileres.mostrarReservaConDetalleDeLibro(listaDeAlquileres);
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
                                List<Alquileres> listaCancelar = (from x in contexto.Alquileres where x.cliente.ClienteId == crudCliente.getClienteId(dniInt) && x.Libro.ISBN == isbn && x.EstadoId.EstadoId == 2 select x).ToList();
                                if (listaCancelar.Count != 0)
                                {
                                    Cancelar(dniInt, isbn);
                                    Reservar(dni, isbn, estadoId);
                                }
                                else
                                {
                                    Console.WriteLine("Se produjo un error, intentelo nuevamente");
                                }
                            }
                            catch(FormatException e)
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
        //Podria sacar el id
        private void Cancelar(int dni, string isbn)
        {
            using (Contexto contexto = new Contexto())
            {
                try
                {
                    int clienteId = crudCliente.getClienteId(dni);
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
                catch(FormatException e)
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
                    alqui.Cliente = crudCliente.getClienteId(dni);
                    alqui.ISBN = isbn;
                    crudLibro.DescuentoStock(isbn);
                    alqui.Estado = estadoId;
                    alqui.FechaReserva = fecha;
                };
                contexto.Alquileres.Add(alqui);
                contexto.SaveChanges();
                string nombre = crudCliente.getNombreApellido(crudCliente.getClienteId(dni));
                string titulo = crudLibro.getTituloLibro(isbn);
                Console.WriteLine("El libro " + titulo + " fue reservado al sr/a " + nombre + ", y quitado del stock para que lo pueda alquilar");
                Console.WriteLine("De lo contrario el administrador puede cancelar la reserva considerando un tiempo limitado de espera");
            }
        }
    }
}
