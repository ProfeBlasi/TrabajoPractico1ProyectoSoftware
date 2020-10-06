using System;
//using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.Text;

namespace TrabajoPractico1
{
    public class Cliente
    {
        private int clienteId;
        private int dni;
        private string nombre;
        private string apellido;
        private string email;
        [Key][Required]
        public int ClienteId { get => clienteId; set => clienteId = value; }
        [Column(TypeName = "varchar(10)")]
        public int DNI { get => dni; set => dni = value; }
        [Column(TypeName = "varchar(45)")]
        public string Nombre { get => nombre; set => nombre = value; }
        [Column(TypeName = "varchar(45)")]
        public string Apellido { get => apellido; set => apellido = value; }
        [Column(TypeName = "varchar(45)")]
        public string Email { get => email; set => email = value; }
    }
}