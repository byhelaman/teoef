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
        private static int contadorAlumnos = 0;


        public static int ObtenerIndiceAlumno(string nroDni)
        {
            if (contadorAlumnos == 0)
            {
                Console.WriteLine("\nNo hay alumnos registrados.");
                return -1; // No hay alumnos registrados
            }

            for (int i = 0; i < contadorAlumnos; i++)
            {
                if (alumnos[i, 0] == nroDni)
                {
                    return i;
                }
            }
            return -1; // No encontrado
        }

        public static void ListarAlumnos()
        {
            if(contadorAlumnos == 0)
            {
                Console.WriteLine("\nNo hay alumnos registrados.");
                return;
            }

            Console.WriteLine($"\n| {"Nro DNI",-10} | {"Nombres",-15} | {"Apellidos",-15} | {"Correo",-15} | {"Celular",-10} |");
            Console.WriteLine(new string('-', 81));
            Console.WriteLine($"@@@");
            Console.WriteLine(new string('-', 81));
        }

        public static void RegistrarAlumno(string nroDni, string nombres, string apellidos, string correo, string celular)
        {
            alumnos[contadorAlumnos, 0] = nroDni; // Nro DNI
            alumnos[contadorAlumnos, 1] = nombres; // Nombres
            alumnos[contadorAlumnos, 2] = apellidos; // Apellidos
            alumnos[contadorAlumnos, 3] = correo; // Correo
            alumnos[contadorAlumnos, 4] = celular; // Celular

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
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                MenuEliminarAlumno();

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
                    case 1:
                        // Confirmar eliminación
                        //Console.WriteLine($"Alumno con Nro DNI {nroDni} fue eliminado.");
                        salir = true;
                        break;
                    case 2:
                        salir = true;
                        Console.WriteLine("Cerrando menú de eliminación...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Por favor, intente de nuevo.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }

        public static void RegistrarNotas(string nroDni)
        {
            bool salir = false;

            int nota = 0;
            int index = -1; // se obtiene el índice del alumno por nroDni

            for (int i = 0; i < contadorAlumnos; i++)
            {
                if (alumnos[i, 0] == nroDni)
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
            {
                Console.WriteLine($"Alumno con Nro DNI {nroDni} no encontrado.");
                return;
            }

            while (!salir)
            {
                Console.Clear();
                MenuNotas();

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
                    case 1:
                        alumnos[index, 5] = nota.ToString(); // T1
                        break;
                    case 2:
                        alumnos[index, 6] = nota.ToString(); // Parcial
                        break;
                    case 3:
                        alumnos[index, 7] = nota.ToString(); // T2
                        break;
                    case 4:
                        alumnos[index, 8] = nota.ToString(); // Final
                        break;
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

        public static void ModificarAlumno()
        {
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                MenuModificarAlumno();

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
                        Console.WriteLine("Cerrando modificación de alumno...");
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
            bool salir = false;

            while (!salir)
            {
                Console.Clear();
                MenuTipoOrden();

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
                    case 6:
                        salir = true;
                        Console.WriteLine("Cerrando filtrado de alumno...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Por favor, intente de nuevo.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }
            }
        }

        public static void ReporteNNotas(int n)
        {
            // generar un reporte de los N alumnos con mayor nota
        }

        private static void MenuNotas()
        {
            Console.WriteLine("---- REGISTRO DE NOTAS ----");
            Console.WriteLine("1. T1");
            Console.WriteLine("2. Parcial");
            Console.WriteLine("3. T2");
            Console.WriteLine("4. Final");
            Console.WriteLine("5. Salir");
            Console.Write("\nSeleccione una opción: ");
        }

        private static void MenuModificarAlumno()
        {
            Console.WriteLine("--- MODIFICAR ALUMNO ---");
            Console.WriteLine("1. Nombres");
            Console.WriteLine("2. Apellidos");
            Console.WriteLine("3. Correo");
            Console.WriteLine("4. Celular");
            Console.WriteLine("5. Salir");
            Console.Write("\nSeleccione una opción: ");
        }

        private static void MenuEliminarAlumno()
        {
            Console.WriteLine("--- ELIMINAR ALUMNO ---");
            Console.WriteLine("1. Confirmar");
            Console.WriteLine("2. Cancelar");
            Console.Write("\nSeleccione una opción: ");
        }

        private static void MenuTipoOrden()
        {
            Console.WriteLine("--- TIPO DE ORDEN ---");
            Console.WriteLine("1. Por Nro DNI");
            Console.WriteLine("2. Por Nombres");
            Console.WriteLine("3. Por Apellidos");
            Console.WriteLine("4. Por Direccion");
            Console.WriteLine("5. Por Celular");
            Console.WriteLine("6. Salir");
            Console.Write("\nSeleccione una opción: ");
        }
    }
}
