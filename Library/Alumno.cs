using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Alumno
    {
        private static string[,] alumnos;
        private static string[,] notas;
        private static int contadorAlumnos = 0;

        public static void ListarAlumnos()
        {
            Console.WriteLine($"\n| {"Nro DNI",-10} | {"Nombres",-15} | {"Apellidos",-15} | {"Correo",-15} | {"Celular",-10} |");
            Console.WriteLine(new string('-', 81));
            Console.WriteLine($"@@@");
            Console.WriteLine(new string('-', 81));
        }

        public static void RegistrarAlumno(string nroDni, string nombres, string apellidos, string correo, string celular)
        {
            alumnos[contadorAlumnos, 1] = nroDni; // Nro DNI
            alumnos[contadorAlumnos, 2] = nombres; // Nombres
            alumnos[contadorAlumnos, 3] = apellidos; // Apellidos
            alumnos[contadorAlumnos, 4] = correo; // Correo
            alumnos[contadorAlumnos, 5] = celular; // Celular

            contadorAlumnos++;
            Console.WriteLine($"Alumno {nombres} {apellidos} fue registrado.");
        }

        public static void ModificarAlumno(string nroDni)
        {
            // modificar un alumno por su Nro DNI

            Console.WriteLine($"Alumno con Nro DNI {nroDni} fue modificado.");
        }

        public static void EliminarAlumno(string nroDni)
        {
            // eliminar un alumno por su Nro DNI
            //Console.WriteLine($"Se elimino el alumno {nombres} {apellidos}.");
        }

        public static void RegistrarNotas()
        //public static void RegistrarNotas(string nroDni)
        {
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                MostrarMenuNotas();

                int opcion;
                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    Console.WriteLine("Opción no válida. Por favor, intente de nuevo.");
                    Console.ReadKey();
                    Console.Clear();
                    continue;
                }

                switch (opcion)
                {
                    case 5:
                        salir = true;
                        Console.WriteLine("Cerrando registro de notas...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Por favor, intente de nuevo.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }

        }

        public static void ReporteOrdenado()
        {
            // generar un reporte ordenado de los alumnos
        }

        public static void ReporteNNotas(int n)
        {
            // generar un reporte de los N alumnos con mayor nota
        }

        private static void MostrarMenuNotas()
        {
            Console.WriteLine("---- REGISTRO DE NOTAS ----");
            Console.WriteLine("1. Registrar T1");
            Console.WriteLine("2. Registrar Parcial");
            Console.WriteLine("3. Registrar T2");
            Console.WriteLine("4. Registrar Final");
            Console.WriteLine("5. Salir");
            Console.Write("\nSeleccione una opción: ");
        }
    }
}
