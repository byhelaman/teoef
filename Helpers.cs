using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEO_EF
{
    internal class Helpers
    {
        // Solicita una cadena al usuario y la valida usando la función "validar".
        // Si la entrada es inválida muestra un mensaje, pausa la consola y
        // puede ejecutar la acción "repintar" para redibujar la interfaz.
        public static string Solicitar(string prompt, Func<string, bool> validar, Action repintar = null)
        {
            string valor;
            do
            {
                Console.Write(prompt);
                valor = Console.ReadLine()?.Trim() ?? string.Empty; // Normaliza null a string.Empty
                if (!validar(valor))
                {
                    MostrarError("Entrada no válida. Intente nuevamente.");
                    Pausa();
                    repintar?.Invoke();
                }
            } while (!validar(valor));
            return valor;
        }

        // Version para números enteros. Utiliza int.TryParse para evitar excepciones
        // Devuelve el número tras validar su formato
        public static int Solicitar(string prompt, Func<int, bool> validar)
        {
            while (true)
            {
                Console.Write(prompt);

                if (int.TryParse(Console.ReadLine(), out int valor) && validar(valor))
                    return valor;
                
                MostrarError("Entrada no válida. Intente nuevamente.");
                Pausa();
            }
        }

        // Igual que la versión anterior pero para valores double
        public static double Solicitar(string prompt, Func<double, bool> validar)
        {
            while (true)
            {
                Console.Write(prompt);

                if (double.TryParse(Console.ReadLine(), out double valor) && validar(valor))
                    return valor;

                MostrarError("Entrada no válida. Intente nuevamente.");
                Pausa();
            }
        }

        // Muestra en consola la matriz "tabla" con formato de columnas.
        // Si "notas" es true imprime un encabezado y columnas distintas
        // para las notas de estudiantes
        public static void ImprimirTabla(string[,] tabla, bool notas = false)
        {
            if (tabla.GetLength(0) == 0)
            {
                Console.WriteLine("\nNo hay datos para mostrar."); return;
            }


            if (notas)
            {
                Console.WriteLine($"\n{"DNI",-10} | {"Nombres",-15} | {"Apellidos",-20} | {"T1",-5} | {"Parcial",-7} | {"T2",-5} | {"Final",-5} | {"Prom",-5}");
                Console.WriteLine(new string('-', 95));
                for (int i = 0; i < tabla.GetLength(0); i++)
                    Console.WriteLine($"{tabla[i, 0],-10} | {tabla[i, 1],-15} | {tabla[i, 2],-20} | {tabla[i, 5],-5} | {tabla[i, 6],-7} | {tabla[i, 7],-5} | {tabla[i, 8],-5} | {tabla[i, 9],-5}");
                return;
            }

            Console.WriteLine($"\n{"DNI",-10} | {"Nombres",-15} | {"Apellidos",-20} | {"Correo",-25} | {"Celular",-10}");
            Console.WriteLine(new string('-', 95));
            for (int i = 0; i < tabla.GetLength(0); i++)
                Console.WriteLine($"{tabla[i, 0],-10} | {tabla[i, 1],-15} | {tabla[i, 2],-20} | {tabla[i, 3],-25} | {tabla[i, 4],-10}");
        }

        // Imprime el mensaje en rojo para destacar errores
        public static void MostrarError(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n{msg}");
            Console.ResetColor();
        }

        // Espera a que el usuario presione una tecla y limpia la pantalla
        public static void Pausa()
        {
            Console.WriteLine("\nPresione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }

    }
}
