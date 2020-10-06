using System;
//using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Text;
namespace TrabajoPractico1
{
    public class EstadoDeAlquileres
    {
        private int estadoId;
        private string descripcion;

        [Required][Key]
        public int EstadoId { get => estadoId; set => estadoId = value; }
        [Column(TypeName = "varchar(45)")]
        public string Descripcion { get => descripcion; set => descripcion = value; }
    }
}