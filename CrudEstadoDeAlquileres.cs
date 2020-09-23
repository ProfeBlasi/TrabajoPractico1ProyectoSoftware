using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Linq;

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
        private List<Alquileres> listaDeAlquileresReservado()
        {
            using (Contexto contexto = new Contexto())
            {
                List<Alquileres> lista = (from x in contexto.Alquileres where x.EstadoId.EstadoId == 2 select x).ToList();
                return lista;
            }
        }
        public void mostrarReservaConDetalleDeLibro()
        {
            List<Alquileres> lista = new List<Alquileres>();
            CrudCliente crudCliente = CrudCliente.getInstance();
            CrudLibro crudLibro = CrudLibro.getInstance();
            lista = listaDeAlquileresReservado();
            if (lista.Count != 0)
            {
                foreach (Alquileres x in lista)
                {
                    int clientId = x.Cliente;
                    string nombre = crudCliente.getNombreApellido(clientId);
                    string ISBN = x.ISBN;
                    string titulo = crudLibro.getTituloLibro(ISBN);
                    Console.WriteLine(nombre + " tiene reservado el libro " + titulo);
                }
            }
            else
            {
                Console.WriteLine("No se ha registrado niguna reserva");
            }
        }
    }
}
