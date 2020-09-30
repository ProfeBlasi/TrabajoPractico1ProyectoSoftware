using System;
using System.Collections.Generic;
//using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Linq;
//using Microsoft.VisualBasic;
//using System.Globalization;
namespace TrabajoPractico1
{
    public class CrudEstadoDeAlquileres
    {
        static CrudEstadoDeAlquileres instance = null;
        public static CrudEstadoDeAlquileres getInstance()
        {
            if (instance == null)
            {
                instance = new CrudEstadoDeAlquileres();
            }
            return instance;
        }
        public List<Alquileres> listaDeAlquileresReservado()
        {
            using (Contexto contexto = new Contexto())
            {
                List<Alquileres> lista = (from x in contexto.Alquileres where x.EstadoId.EstadoId == 2 select x).ToList();
                return lista;
            }
        }
        public List<Alquileres> listaDeAlquileresReservadoEspecifico(string isbn)
        {
            using (Contexto contexto = new Contexto())
            {
                List<Alquileres> lista = (from x in contexto.Alquileres where x.Libro.ISBN == isbn && x.EstadoId.EstadoId == 2 select x).ToList();
                return lista;
            }
        }
        public void mostrarReservaConDetalleDeLibro(List<Alquileres> lista)
        {
            CrudCliente crudCliente = CrudCliente.getInstance();
            CrudLibro crudLibro = CrudLibro.getInstance();
            if (lista.Count != 0)
            {
                foreach (Alquileres x in lista)
                {
                    int id = x.ID;
                    int clientId = x.Cliente;
                    string nombre = crudCliente.getNombreApellido(clientId);
                    string ISBN = x.ISBN;
                    string titulo = crudLibro.getTituloLibro(ISBN);
                    DateTime fecha = Convert.ToDateTime(x.FechaReserva);
                    Console.WriteLine("El sr/a " + nombre + " tiene reservado el libro " + titulo);
                    Console.WriteLine("ISBN " + ISBN + ",para la fecha " + fecha.ToString("dd-MM-yyyy"));
                }
            }
            else
            {
                Console.WriteLine("No se ha registrado niguna reserva");
            }
        }
    }
}