using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Linq;
using System.Threading.Tasks.Dataflow;

namespace TrabajoPractico1
{
    public class CrudEstadoDeAlquileres
    {
        Contexto contexto = Contexto.getInstance();
        CrudCliente crudCliente = CrudCliente.getInstance();
        CrudLibro crudLibro = CrudLibro.getInstance();
        static CrudEstadoDeAlquileres instance = null;
        public static CrudEstadoDeAlquileres getInstance()
        {
            if (instance == null)
            {
                instance = new CrudEstadoDeAlquileres();
            }
            return instance;
        }
        public void ListaDeReservas()
        {
            List<Alquileres> lista = (from x in contexto.Alquileres where x.Estado == 2 select x).ToList();
            if(lista.Count!=0)
            {
                foreach (Alquileres x in lista)
                {
                    string nombre = crudCliente.getNombre(x.Cliente);
                    string titulo = crudLibro.getTitulo(x.ISBN);
                    DateTime fecha = Convert.ToDateTime(x.FechaReserva);
                    Console.WriteLine(nombre + " reservo " + titulo + " para la fecha " + fecha.ToString("dd-MM-yyyy"));
                }
            }
            else
            {
                Console.WriteLine("No se ha registrado niguna reserva");
            }
        }
    }
}