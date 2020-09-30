using System;
using System.Collections.Generic;
//using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Linq;
namespace TrabajoPractico1
{
    public class CrudCliente
    {
        static CrudCliente instance = null;
        public static CrudCliente getInstance()
        {
            if (instance == null)
            {
                instance = new CrudCliente();
            }
            return instance;
        }
        public bool ExisteCliente(int dni)
        {
            using (Contexto contexto = new Contexto())
            {
                List<Cliente> lista = (from x in contexto.Cliente where x.DNI == dni select x).ToList();
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
        public int getClienteId(int dni)
        {
            using (Contexto contexto = new Contexto())
            {
                List<Cliente> lista = (from x in contexto.Cliente where x.DNI == dni select x).ToList();
                if (lista.Count != 0)
                {
                    return lista[0].ClienteId;
                }
                else
                {
                    return 0;
                }
            }
        }
        public string getNombreApellido(int Id)
        {
            using (Contexto contexto = new Contexto())
            {
                List<Cliente> lista = (from x in contexto.Cliente where x.ClienteId == Id select x).ToList();
                if (lista.Count != 0)
                {
                    string nombreApellido = lista[0].Nombre + " " + lista[0].Apellido;
                    return nombreApellido;
                }
                else
                {
                    return null;
                }
            }
        }
        public void registrarCliente()
        {
            try
            {
                Console.WriteLine("Ingrese el nombre del cliente ");
                string nombre = Console.ReadLine();
                Console.WriteLine("Ingrese apellido del cliente ");
                string apellido = Console.ReadLine();
                Console.WriteLine("Ingrese el dni del cliente");
                string dni = Console.ReadLine();
                Console.WriteLine("Ingrese el mail del correcto");
                string email = Console.ReadLine();
                int dniNumero = int.Parse(dni);
                if (ExisteCliente(dniNumero) == false)
                {
                    using (Contexto contexto = new Contexto())
                    {
                        Cliente cliente = new Cliente();
                        {
                            cliente.Nombre = nombre;
                            cliente.Apellido = apellido;
                            cliente.DNI = dniNumero;
                            cliente.Email = email;
                        };
                        contexto.Cliente.Add(cliente);
                        contexto.SaveChanges();
                        Console.WriteLine("El cliente se registro exitosamente, presione una tecla para continuar");
                    }
                }
                else
                {
                    Console.WriteLine("Ya existe un cliente registrado con ese dni");
                }
            }
            catch(FormatException e)
            {
                Console.WriteLine("Se produjo un error inesperado " + e.Message);
            }
        }
    }
}