using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TrabajoPractico1.Menu
{
    public class Validaciones
    {
        public static String SoloLetras(string array)
        {
            while (!Regex.IsMatch(array, @"^[a-zA-Z]+$"))
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
        public static String ComprobarFormatoEmail(string EmailAComprobar)
        {
            string formato = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            while (!Regex.IsMatch(EmailAComprobar, formato))
            {
                Console.Write("Error, ingrese un formato de mail correcto:");
                EmailAComprobar = Console.ReadLine();
            }
            return EmailAComprobar;
        }
        public static int ConvertirNumero(string numero)
        {
            int num = 0;
            try
            {
                num = int.Parse(numero);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return num;
        }
    }
}
