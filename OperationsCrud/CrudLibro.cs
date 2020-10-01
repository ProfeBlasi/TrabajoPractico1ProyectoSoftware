using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Linq;
using Microsoft.EntityFrameworkCore.Internal;
using System.Security.Cryptography.X509Certificates;

namespace TrabajoPractico1
{
    public class CrudLibro
    {
        static CrudLibro instance = null;
        Contexto contexto = Contexto.getInstance();
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
            return contexto.Libros.Any(x => x.ISBN == isbn);
        }
        public bool ExisteStock(string isbn)
        {
            return contexto.Libros.Any(x => x.ISBN == isbn && x.Stock > 0);
        }
        public void DescuentoStock(string isbn)
        {
            var query = (from x in contexto.Libros where x.ISBN == isbn select x).FirstOrDefault();
            query.Stock = query.Stock - 1;
            contexto.SaveChanges();
        }
        public void AumentoStock(string isbn)
        {
            var query = (from x in contexto.Libros where x.ISBN == isbn select x).FirstOrDefault();
            query.Stock = query.Stock + 1;
            contexto.SaveChanges();
        }
        public void ListaDeLibrosConStock()
        {
            List<Libros> lista = (from x in contexto.Libros where x.Stock > 0 select x).ToList();
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
        }
        public string getTitulo(string isbn)
        {
            var query = (from x in contexto.Libros where x.ISBN == isbn select x).FirstOrDefault();
            return query.Titulo;
        }
    }
}