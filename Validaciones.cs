using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
//using System.Collections.Generic;
//using System.Text;

namespace TrabajoPractico1
{
    public class Validaciones
    {
        public String SoloLetras(string array)
        {
            while(!Regex.IsMatch(array, @"^[a-zA-Z]+$"))
            {
                Console.Write("Error, ingrese solo letras por favor:");
                array = Console.ReadLine();
            }
            return array;
        }
        public int ConvertirNumero(string numero)
        {
            int num = VerificoNumero(numero);
            while(num<0)
            {
                Console.WriteLine("El numero valor ingresado no corresponde intentelo nuevamente");
                string nuevoNumero = Console.ReadLine();
                num = VerificoNumero(nuevoNumero);
            }
            return num;
        }
        public int VerificoNumero(string numero)
        {
            int num = -1;
            try
            {
                num = int.Parse(numero);
            }
            catch
            {

            }
            return num;
        }
    }
}
