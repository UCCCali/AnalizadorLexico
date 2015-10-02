using System;
using System.Collections.Generic;
using System.IO;

namespace EjercicioGramaticas01.Analizador_Lexico
{
    public class Analex
    {

        int numLinea = 1;
        String valcomplex;
        String[] reservadas = { "if", "while", "do", "for", "class", "else", "true", "double", "final", "String", "int"};
        List<String> lstPalabrasReservadas = new List<string>();

        public void AnalizadorLexico(String nombreArchivo)
        {
            char car;
            int val;
            StreamReader file = new StreamReader(nombreArchivo);

            foreach (var item in reservadas)
            {
                lstPalabrasReservadas.Add(item);
            }

            while (true)
            {
                val = file.Read();

                if (val == -1)
                {
                    break;
                }

                valcomplex = "";
                car = (char)val;

                //Eliminación de espacios en blanco
                while (car == ' ' || car == '\n' || car == '\r' || car == '\t')
                {
                    if (car == '\n')
                    {
                        numLinea++;
                    }
                    val = file.Read();
                    car = (char)val;
                }

                //Reconocimiento de numeros
                if (Char.IsNumber(car))
                {
                    valcomplex = valcomplex + car;

                    while (true)
                    {
                        val = file.Peek();
                        car = (char)val;
                        if (Char.IsNumber(car)) { valcomplex = valcomplex + car; file.Read(); }
                        else { break; }
                    }

                    if (car == '.')
                    {
                        valcomplex = valcomplex + car;
                        file.Read();

                        while (true)
                        {
                            val = file.Peek();
                            car = (char)val;
                            if (Char.IsNumber(car)) { valcomplex = valcomplex + car; file.Read(); }
                            else { break; }
                        }
                    }

                    Mostrar("NUMERO");                    
                    continue;

                }


                //Reconocimiento de identificadores y palabras reservadas
                if (Char.IsLetter(car))
                {
                    valcomplex = valcomplex + car;                    

                    while (true)
                    {
                        val = file.Peek();
                        car = (char)val;
                        if (Char.IsNumber(car) || Char.IsLetter(car) || car == '$' || car == '_') { valcomplex = valcomplex + car; file.Read(); }
                        else { break; }
                    }

                    if (lstPalabrasReservadas.Contains(valcomplex))
                    {
                        Mostrar("RESERVADA");
                    }
                    else
                    {
                        Mostrar("ID");
                    }
                    
                    continue;
                }


                //Reconocimiento de cadenas
                if (car == '"')
                {
                    while (true)
                    {
                        val = file.Read();
                        car = (char)val;
                        if (car != '"') { valcomplex = valcomplex + car;}
                        else { break; }
                    }

                    Mostrar("CADENA");
                    
                    continue;
                }


                //Reconocimiento de comentarios
                if (car == '/')
                {
                    valcomplex = valcomplex + car;                    
                    val = file.Peek();
                    car = (char)val;

                    if (car == '/')
                    {
                       valcomplex = valcomplex + car;
                       file.Read();

                       while (true)
                       {
                            val = file.Peek();
                            car = (char)val;
                            if (car != '\n' && car != '\r') { valcomplex = valcomplex + car; file.Read(); }
                            else { break; }
                        }

                        Mostrar("COMENTARIO");
                    }
                    else if (car == '*')
                    {
                        valcomplex = valcomplex + car;
                        file.Read();

                        while (true)
                        {
                            val = file.Read();
                            car = (char)val;
                            valcomplex = valcomplex + car;

                            if (car == '*')
                            {
                                val = file.Read();
                                car = (char)val;
                                valcomplex = valcomplex + car;
                                if (car == '/') break;
                            }
                        }

                        Mostrar("COMENTARIO");
                        continue;
                    }                  
                }
               
                // Reconocimiento de operadores
                valcomplex = valcomplex + car;
                switch (car)
                {
                    case '\r': break;
                    case '+':
                    case '.':
                    case '-':
                    case '*':
                    case '/':
                    case ';': Mostrar("OPERADOR"); break;
                    default: Mostrar("DESCONOCIDO"); break;
                }                 
                 
            }
        }

        private void Mostrar(String msg)
        {
            Console.WriteLine(numLinea + ": " + msg + "(" + valcomplex + ")");
        }
    }
}
