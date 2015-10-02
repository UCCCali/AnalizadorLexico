    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EjercicioGramaticas01
{
    class Program
    {
        static void Main(string[] args)
        {
            Analizador_Lexico.Analex objAnalex = new Analizador_Lexico.Analex();

            objAnalex.AnalizadorLexico(@"C:\log\Ejemplo1.txt");

            Console.ReadLine();
        }
    }
}
