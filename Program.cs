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

                string nroDni = "";
                int index;
                switch (opcion)
                {
                    case 1:
                        Alumno.ListarAlumnos();
                        Console.WriteLine("\nPresione cualquier tecla para continuar...");
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 3:
                        Console.WriteLine("Ingrese el Nro DNI del alumno a eliminar: ");

                        Alumno.ModificarAlumno(nroDni);
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 4:
                        Console.WriteLine("Ingrese el Nro DNI del alumno a eliminar: ");
                        //nroDni = Console.ReadLine();

                        Alumno.EliminarAlumno(nroDni);
                        Console.ReadKey();
                        Console.Clear();
                        break;
                    case 5:
                        do
                        {
                            Console.WriteLine("\nIngrese el Nro DNI del alumno para registrar notas: ");
                            nroDni = Console.ReadLine();
                            index = Alumno.ObtenerIndiceAlumno(nroDni);

                            if (index == -1)
                            {
                                Console.WriteLine("Alumno no encontrado.");
                            }

                        } while (index == -1);


                        Alumno.RegistrarNotas(nroDni);
                        Console.ReadKey();
                        Console.Clear();

                        break;
                    case 6:
                        Alumno.ReporteOrdenado();
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
