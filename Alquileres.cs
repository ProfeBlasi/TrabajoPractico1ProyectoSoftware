using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Windows.Markup;

namespace TrabajoPractico1
{
    public class Alquileres
    {
        private int id;
        private Cliente clienteId;
        private Libros isbn;
        private EstadoDeAlquileres estado;
        private DateTime? fechaAlquiler = null;
        private DateTime? fechaReserva = null;
        private DateTime? fechaDevolucion = null;
        //Se utiliza convencion de tipo 1 para la relacion de uno a muchos
        [Required]
        public int Cliente { get; set; }
        [ForeignKey("Cliente")]
        public Cliente cliente { get => clienteId; set => clienteId = value; }
        [Required]
        [Column(TypeName = "varchar(45)")]
        public string ISBN { get; set; }
        [ForeignKey("ISBN")]
        public Libros Libro { get => isbn; set => isbn = value; }
        [Required]
        public int Estado { get; set; }
        [ForeignKey("Estado")]
        public EstadoDeAlquileres EstadoId { get => estado; set => estado = value; }
        public int ID { get => id; set => id = value; }
        [Column(TypeName ="Date")]
        public DateTime? FechaAlquiler { get => fechaAlquiler; set => fechaAlquiler = value; }
        [Column(TypeName = "Date")]
        public DateTime? FechaReserva { get => fechaReserva; set => fechaReserva = value; }
        [Column(TypeName = "Date")]
        public DateTime? FechaDevolucion { get => fechaDevolucion; set => fechaDevolucion = value; }
    }
}
