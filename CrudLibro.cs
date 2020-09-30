using System;
using System.Collections.Generic;
//using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Linq;
namespace TrabajoPractico1
{
    public class CrudLibro
    {
        static CrudLibro instance = null;
        public static CrudLibro getInstance()
        {
            if (instance == null)
            {
                instance = new CrudLibro();
            }
            return instance;
        }
        public bool ExisteISBN(string isbn)
        {
            using (Contexto contexto = new Contexto())
            {
                List<Libros> lista = (from x in contexto.Libros where x.ISBN == isbn select x).ToList();
                if (lista.Count != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool ExisteStock(string isbn)
        {
            using (Contexto contexto = new Contexto())
            {
                List<Libros> lista = (from x in contexto.Libros where x.ISBN == isbn && x.Stock > 0 select x).ToList();
                if (lista.Count != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public void DescuentoStock(string isbn)
        {
            using (Contexto contexto = new Contexto())
            {
                List<Libros> lista = (from x in contexto.Libros where x.ISBN == isbn select x).ToList();
                int stock = lista[0].Stock;
                Libros libro = lista[0];
                {
                    libro.Stock = stock - 1;
                };
                contexto.Libros.Update(libro);
                contexto.SaveChanges();
            }
        }
        public void AumentoStock(string isbn)
        {
            using (Contexto contexto = new Contexto())
            {
                List<Libros> lista = (from x in contexto.Libros where x.ISBN == isbn select x).ToList();
                int stock = lista[0].Stock;
                Libros libro = lista[0];
                {
                    libro.Stock = stock + 1;
                };
                contexto.Libros.Update(libro);
                contexto.SaveChanges();
            }
        }
        public string getTituloLibro(string ISBN)
        {
            using (Contexto contexto = new Contexto())
            {
                List<Libros> lista = (from x in contexto.Libros where x.ISBN == ISBN select x).ToList();
                if (lista.Count != 0)
                {
                    string titulo = lista[0].Titulo;
                    return titulo;
                }
                else
                {
                    return null;
                }
            }
        }
        private List<Libros> listaDeLIbrosConStock()
        {
            using (Contexto contexto = new Contexto())
            {
                List<Libros> lista = (from x in contexto.Libros where x.Stock > 0 select x).ToList();
                return lista;
            }
        }
        public void mostrarListaDeLibrosConStock()
        {
            List<Libros> lista = new List<Libros>();
            lista = listaDeLIbrosConStock();
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
                {
                    Console.WriteLine("Contamos con             " + x.Stock + " unidades");
                }
                Console.WriteLine();
            }
        }
    }
}