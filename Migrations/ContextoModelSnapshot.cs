﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TrabajoPractico1;

namespace TrabajoPractico1.Migrations
{
    [DbContext(typeof(Contexto))]
    partial class ContextoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TrabajoPractico1.Alquileres", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cliente")
                        .HasColumnType("int");

                    b.Property<int>("Estado")
                        .HasColumnType("int");

                    b.Property<DateTime?>("FechaAlquiler")
                        .HasColumnType("Date");

                    b.Property<DateTime?>("FechaDevolucion")
                        .HasColumnType("Date");

                    b.Property<DateTime?>("FechaReserva")
                        .HasColumnType("Date");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("varchar(45)");

                    b.HasKey("ID");

                    b.HasIndex("Cliente");

                    b.HasIndex("Estado");

                    b.HasIndex("ISBN");

                    b.ToTable("Alquileres");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Cliente = 1,
                            Estado = 1,
                            FechaAlquiler = new DateTime(2020, 9, 24, 0, 0, 0, 0, DateTimeKind.Local),
                            FechaDevolucion = new DateTime(2020, 10, 1, 0, 0, 0, 0, DateTimeKind.Local),
                            ISBN = "123"
                        },
                        new
                        {
                            ID = 2,
                            Cliente = 2,
                            Estado = 2,
                            FechaReserva = new DateTime(2020, 9, 24, 0, 0, 0, 0, DateTimeKind.Local),
                            ISBN = "234"
                        },
                        new
                        {
                            ID = 3,
                            Cliente = 3,
                            Estado = 3,
                            ISBN = "345"
                        });
                });

            modelBuilder.Entity("TrabajoPractico1.Cliente", b =>
                {
                    b.Property<int>("ClienteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Apellido")
                        .HasColumnType("varchar(45)");

                    b.Property<string>("DNI")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(45)");

                    b.Property<string>("Nombre")
                        .HasColumnType("varchar(45)");

                    b.HasKey("ClienteId");

                    b.ToTable("Cliente");

                    b.HasData(
                        new
                        {
                            ClienteId = 1,
                            Apellido = "Perez",
                            DNI = "1",
                            Email = "jperez@gmail.com",
                            Nombre = "Juan"
                        },
                        new
                        {
                            ClienteId = 2,
                            Apellido = "Sosa",
                            DNI = "2",
                            Email = "jsosa@gmail.com",
                            Nombre = "Jose"
                        },
                        new
                        {
                            ClienteId = 3,
                            Apellido = "Ortiz",
                            DNI = "3",
                            Email = "gortiz@gmail.com",
                            Nombre = "Gabriel"
                        },
                        new
                        {
                            ClienteId = 4,
                            Apellido = "Fernandez",
                            DNI = "4",
                            Email = "jfernandez@gmail.com",
                            Nombre = "Javier"
                        });
                });

            modelBuilder.Entity("TrabajoPractico1.EstadoDeAlquileres", b =>
                {
                    b.Property<int>("EstadoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion")
                        .HasColumnType("varchar(45)");

                    b.HasKey("EstadoId");

                    b.ToTable("EstadoDeAlquileres");

                    b.HasData(
                        new
                        {
                            EstadoId = 1,
                            Descripcion = "Alquilado"
                        },
                        new
                        {
                            EstadoId = 2,
                            Descripcion = "Reservado"
                        },
                        new
                        {
                            EstadoId = 3,
                            Descripcion = "Cancelado"
                        });
                });

            modelBuilder.Entity("TrabajoPractico1.Libros", b =>
                {
                    b.Property<string>("ISBN")
                        .HasColumnType("varchar(45)");

                    b.Property<string>("Autor")
                        .HasColumnType("varchar(45)");

                    b.Property<string>("Edicion")
                        .HasColumnType("varchar(45)");

                    b.Property<string>("Editorial")
                        .HasColumnType("varchar(45)");

                    b.Property<string>("Imagen")
                        .HasColumnType("varchar(45)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .HasColumnType("varchar(45)");

                    b.HasKey("ISBN");

                    b.ToTable("Libros");

                    b.HasData(
                        new
                        {
                            ISBN = "123",
                            Autor = "Agnelli Fernando",
                            Edicion = "Limitada",
                            Editorial = "Kapeluz",
                            Imagen = "vacio",
                            Stock = 5,
                            Titulo = "20 celebres matematicos"
                        },
                        new
                        {
                            ISBN = "234",
                            Autor = "Gabriel Garcia Marquez",
                            Edicion = "Abierta",
                            Editorial = "Santillana",
                            Imagen = "vacio",
                            Stock = 2,
                            Titulo = "Cien años de soledad"
                        },
                        new
                        {
                            ISBN = "345",
                            Autor = "Johann Wolfgang von Goethe",
                            Edicion = "Cerrada",
                            Editorial = "Puerto de palos",
                            Imagen = "vacio",
                            Stock = 1,
                            Titulo = "Fausto"
                        },
                        new
                        {
                            ISBN = "456",
                            Autor = "Miguel de Cervantes",
                            Edicion = "Limitada",
                            Editorial = "Kapeluz",
                            Imagen = "vacio",
                            Stock = 2,
                            Titulo = "Don Quijote De la Mancha"
                        },
                        new
                        {
                            ISBN = "567",
                            Autor = "Sofocles",
                            Edicion = "Abierta",
                            Editorial = "Santillana",
                            Imagen = "vacio",
                            Stock = 7,
                            Titulo = "Edipo Rey"
                        },
                        new
                        {
                            ISBN = "678",
                            Autor = "Mark Twain",
                            Edicion = "Cerrada",
                            Editorial = "Puerto de palos",
                            Imagen = "vacio",
                            Stock = 2,
                            Titulo = "Las aventuras de Huckleberry Finn"
                        });
                });

            modelBuilder.Entity("TrabajoPractico1.Alquileres", b =>
                {
                    b.HasOne("TrabajoPractico1.Cliente", "cliente")
                        .WithMany()
                        .HasForeignKey("Cliente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrabajoPractico1.EstadoDeAlquileres", "EstadoId")
                        .WithMany()
                        .HasForeignKey("Estado")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TrabajoPractico1.Libros", "Libro")
                        .WithMany()
                        .HasForeignKey("ISBN")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
