using System;
//using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Text;
//using System.Windows.Markup;
namespace TrabajoPractico1
{
    public class Libros
    {
        private string isbn;
        private string titulo;
        private string autor;
        private string editorial;
        private string edicion;
        private int stock;
        private string imagen;
        [Key][Required][Column(TypeName = "varchar(45)")]
        public string ISBN { get => isbn; set =>isbn =value; }
        [Column(TypeName = "varchar(45)")]
        public string Titulo { get => titulo; set => titulo = value; }
        [Column(TypeName = "varchar(45)")]
        public string Autor { get => autor; set => autor = value; }
        [Column(TypeName = "varchar(45)")]
        public string Editorial { get => editorial; set => editorial = value; }
        [Column(TypeName = "varchar(45)")]
        public string Edicion { get => edicion; set => edicion = value; }
        public int Stock { get => stock; set => stock = value; }
        [Column(TypeName = "varchar(45)")]
        public string Imagen { get => imagen; set => imagen = value; }
    }
}