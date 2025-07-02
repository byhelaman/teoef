using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Library;

namespace TEO_EF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool salir = false;

            while (!salir)
            {
                MostrarMenu();

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
                        Alumno.ListarAlumnos();
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 5:
                        Alumno.RegistrarNotas();
                        Console.ReadKey();
                        Console.Clear();

                        break;
                    case 8:
                        salir = true;
                        Console.WriteLine("Saliendo del programa...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Por favor, intente de nuevo.");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                }

            }
        }

        private static void MostrarMenu()
        {
            Console.WriteLine("----- MENU ALUMNOS -----");
            Console.WriteLine("1. Listar alumnos");
            Console.WriteLine("2. Registrar alumno");
            Console.WriteLine("3. Modificar alumno");
            Console.WriteLine("4. Eliminar alumno");
            Console.WriteLine("5. Registro de Notas");
            Console.WriteLine("6. Reporte Ordenado");
            Console.WriteLine("7. Reporte N notas (Los N alumnos que obtuvieron mayor nota)");
            Console.WriteLine("8. Salir");
            Console.Write("\nSeleccione una opción: ");
        }
    }
}
