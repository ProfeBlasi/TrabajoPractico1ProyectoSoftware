using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
namespace TrabajoPractico1
{
    public class Validaciones
    {
        public static String SoloLetras(string array)
        {
            while(!Regex.IsMatch(array, @"^[a-zA-Z]+$"))
            {
                Console.Write("Error, ingrese solo letras por favor:");
                array = Console.ReadLine();
            }
            return array;
        }
        public static String SoloNumeros(string array)
        {
            while (!Regex.IsMatch(array, @"^[0-9]+$"))
            {
                Console.Write("Error, ingrese solo numeros por favor:");
                array = Console.ReadLine();
            }
            return array;
        }
        public static int ConvertirNumero(string numero)
        {
            int num =0;
            try
            {
                num = int.Parse(numero);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return num;
        } 
    }
}
