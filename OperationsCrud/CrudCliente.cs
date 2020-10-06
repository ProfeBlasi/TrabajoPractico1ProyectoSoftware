using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Linq;
namespace TrabajoPractico1
{
    public class CrudCliente
    {
        Contexto contexto = Contexto.getInstance();
        static CrudCliente instance = null;
        public static CrudCliente getInstance()
        {
            if (instance == null)
            {
                instance = new CrudCliente();
            }
            return instance;
        }
        public void registrarCliente()
        {
            try
            {
                Console.WriteLine("Ingrese el nombre del cliente ");
                string nombre = Validaciones.SoloLetras(Console.ReadLine());
                Console.WriteLine("Ingrese apellido del cliente ");
                string apellido = Validaciones.SoloLetras(Console.ReadLine());
                Console.WriteLine("Ingrese el dni del cliente");
                string dniString = Validaciones.SoloLetras(Console.ReadLine());
                Console.WriteLine("Ingrese el mail del correcto");
                string email = Validaciones.SoloLetras(Console.ReadLine());
                int dni = Validaciones.ConvertirNumero(dniString);
                if (contexto.Cliente.Any(x => x.DNI == dni))
                    Console.WriteLine("Ya existe un cliente registrado con ese dni");
                else
                {
                    Cliente cliente = new Cliente();
                    {
                        cliente.Nombre = nombre;
                        cliente.Apellido = apellido;
                        cliente.DNI = dni;
                        cliente.Email = email;
                    };
                    contexto.Cliente.Add(cliente);
                    contexto.SaveChanges();
                    Console.WriteLine("El cliente se registro exitosamente, presione una tecla para continuar");
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine("Se produjo un error inesperado " + e.Message);
            }
        }
        public bool ExisteCliente(int dni)
        {
            return contexto.Cliente.Any(x => x.DNI == dni);
        }
        public string getNombre(int id)
        {
            var query = (from x in contexto.Cliente where x.ClienteId == id select x).FirstOrDefault();
            return query.Nombre + " " + query.Apellido;
        }
        public int getId(int dni)
        {
            var query = (from x in contexto.Cliente where x.DNI == dni select x).FirstOrDefault();
            return query.ClienteId;
        }
    }
}