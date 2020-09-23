﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrabajoPractico1
{
    public class Contexto : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Se debe camabiar el connection string para migrar la base de datos
            optionsBuilder.UseSqlServer(@"Server=DESKTOP-IKK8QNO;Database=Bibli;Trusted_Connection=True;");
        }
        private DbSet<Alquileres> alquileres;
        private DbSet<Cliente> cliente;
        private DbSet<EstadoDeAlquileres> estadoDeAlquileres;
        private DbSet<Libros> libros;
        public DbSet<Alquileres> Alquileres { get => alquileres; set => alquileres = value; }
        public DbSet<Cliente> Cliente { get => cliente; set => cliente = value; }
        public DbSet<EstadoDeAlquileres> EstadoDeAlquileres { get => estadoDeAlquileres; set => estadoDeAlquileres = value; }
        public DbSet<Libros> Libros { get => libros; set => libros = value; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Registro de Clientes
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasData(new Cliente
                {
                    ClienteId = 1234,
                    DNI = 1234,
                    Nombre = "Juan",
                    Apellido = "Perez",
                    Email = "jperez@gmail.com",
                });
                entity.HasData(new Cliente
                {
                    ClienteId = 2345,
                    DNI = 2345,
                    Nombre = "Jose",
                    Apellido = "Sosa",
                    Email = "jsosa@gmail.com",
                });
                entity.HasData(new Cliente
                {
                    ClienteId = 3456,
                    DNI = 3456,
                    Nombre = "Gabriel",
                    Apellido = "Ortiz",
                    Email = "gortiz@gmail.com",
                });
                entity.HasData(new Cliente
                {
                    ClienteId = 4567,
                    DNI = 4567,
                    Nombre = "Javier",
                    Apellido = "Fernandez",
                    Email = "jfernandez@gmail.com",
                });
            });

            //Registro de Estado de alquileres
            modelBuilder.Entity<EstadoDeAlquileres>(entity =>
            {
                entity.HasData(new EstadoDeAlquileres
                {
                    EstadoId = 1,
                    Descripcion = "Alquilado",
                });
                entity.HasData(new EstadoDeAlquileres
                {
                    EstadoId = 2,
                    Descripcion = "Reservado",
                });
                entity.HasData(new EstadoDeAlquileres
                {
                    EstadoId = 3,
                    Descripcion = "Cancelado",
                });
            });

            //Registro de Libros
            modelBuilder.Entity<Libros>(entity =>
            {
                entity.HasData(new Libros
                {
                    ISBN = "123",
                    Titulo = "20 celebres matematicos",
                    Autor = "Agnelli Fernando",
                    Editorial = "Kapeluz",
                    Edicion = "Limitada",
                    Stock = 5,
                    Imagen = "vacio"
                });
                entity.HasData(new Libros
                {
                    ISBN = "234",
                    Titulo = "Cien años de soledad",
                    Autor = "Gabriel Garcia Marquez",
                    Editorial = "Santillana",
                    Edicion = "Abierta",
                    Stock = 2,
                    Imagen = "vacio"
                });
                entity.HasData(new Libros
                {
                    ISBN = "345",
                    Titulo = "Fausto",
                    Autor = "Johann Wolfgang von Goethe",
                    Editorial = "Puerto de palos",
                    Edicion = "Cerrada",
                    Stock = 1,
                    Imagen = "vacio"
                });
                entity.HasData(new Libros
                {
                    ISBN = "456",
                    Titulo = "Don Quijote De la Mancha",
                    Autor = "Miguel de Cervantes",
                    Editorial = "Kapeluz",
                    Edicion = "Limitada",
                    Stock = 2,
                    Imagen = "vacio"
                });
                entity.HasData(new Libros
                {
                    ISBN = "567",
                    Titulo = "Edipo Rey",
                    Autor = "Sofocles",
                    Editorial = "Santillana",
                    Edicion = "Abierta",
                    Stock = 7,
                    Imagen = "vacio"
                });
                entity.HasData(new Libros
                {
                    ISBN = "678",
                    Titulo = "Las aventuras de Huckleberry Finn",
                    Autor = "Mark Twain",
                    Editorial = "Puerto de palos",
                    Edicion = "Cerrada",
                    Stock = 2,
                    Imagen = "vacio"
                });
            });
            //Registro de Alquileres
            modelBuilder.Entity<Alquileres>(entity =>
            {
                entity.HasData(new Alquileres
                {
                    ID = 1,
                    Cliente = 1234,
                    ISBN = "123",
                    Estado = 1,
                    FechaAlquiler = DateTime.Today,
                    FechaReserva=null,
                    FechaDevolucion=null,
                });
                entity.HasData(new Alquileres
                {
                    ID = 2,
                    Cliente = 2345,
                    ISBN = "234",
                    Estado = 2,
                    FechaAlquiler = null,
                    FechaReserva = DateTime.Today,
                    FechaDevolucion = null,
                });
                entity.HasData(new Alquileres
                {
                    ID = 3,
                    Cliente = 3456,
                    ISBN = "345",
                    Estado = 3,
                    FechaAlquiler = null,
                    FechaReserva = null,
                    FechaDevolucion = DateTime.Today,
                });

            });
        }
    }

}
