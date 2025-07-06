using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Library;
using TEO_EF.Menu;

namespace TEO_EF
{
    internal class Program
    {

        static void Main(string[] args)
        {
            // Registros de ejemplo para facilitar pruebas
            Alumno.Agregar("23456789", "María José", "Ramírez Torres", "mramirez@gmail.com", "912345678");
            Alumno.Agregar("34567890", "Carlos Andrés", "Fernández Paredes", "cfernandez@gmail.com", "913456789");
            Alumno.Agregar("45678901", "Lucía", "Quiroz Salazar", "lquiroz@gmail.com", "914567890");
            Alumno.Agregar("56789012", "Pedro", "Vargas Llosa", "pvargas@gmail.com", "915678901");
            Alumno.Agregar("67890123", "Ana", "Huamaní Rojas", "ahuamani@gmail.com", "916789012");

            bool salir = false;
            while (!salir)
            {
                Console.Clear();
                MenuPrincipal.Mostrar();

                // Valida que la opción esté entre 1 y 8

                // Flujo Solicitar:
                //   1. Muestra el prompt (mensaje) y lee la línea del usuario.
                //   2. Aplica Trim() y normaliza a string.Empty si se recibe null
                //      para evitar excepciones por referencia nula.
                //   3. Evalúa la función "validar".
                //      - Si es false:     
                //          • Llama a MostrarError para destacar el problema. 
                //          • Pausa la ejecución hasta que el usuario presione una tecla.
                //          • Invoca repintar() si se proporcionó, de modo que la UI
                //            se refresque antes del siguiente intento.
                //   4. Repite los pasos 1‑3 en un bucle hasta obtener un valor válido.

                string opcion = Helpers.Solicitar("\nOpción: ", s => "12345678".Contains(s), MenuPrincipal.Mostrar);
                switch (opcion)
                {
                    case "1": Listar(); break;
                    case "2": Registrar(); break;
                    case "3": Modificar(); break;
                    case "4": Eliminar(); break;
                    case "5": RegistroNotas(); break;
                    case "6": ReporteOrdenado(); break;
                    case "7": ReporteTopN(); break;
                    case "8": salir = true; break;
                    default: Helpers.MostrarError("Entrada no válida. Intente nuevamente."); Helpers.Pausa(); break;
                }
            }

            Helpers.Pausa();
        }

        // Listar todos los alumnos en formato de tabla
        static void Listar()
        {
            var tabla = Alumno.ObtenerDatos();
            Helpers.ImprimirTabla(tabla);
            Helpers.Pausa();
        }

        // Solicitar datos por consola y registrar un nuevo alumno
        static void Registrar()
        {
            Console.WriteLine("\nIngrese los datos del alumno:");
            string dni = Helpers.Solicitar("DNI (8 dígitos): ", d => d.Length == 8 && d.All(char.IsDigit));
            string nombres = Helpers.Solicitar("Nombres: ", n => !string.IsNullOrWhiteSpace(n));
            string apellidos =  Helpers.Solicitar("Apellidos: ", a => !string.IsNullOrWhiteSpace(a));
            string correo = Helpers.Solicitar("Correo: ", c => c.Contains("@"));
            string celular = Helpers.Solicitar("Celular (9 dígitos): ", c => c.Length == 9 && c.All(char.IsDigit));

            if (Alumno.Agregar(dni, nombres, apellidos, correo, celular))
                Console.WriteLine($"\nEl alumno {nombres} {apellidos} fue registrado");
            else
                Helpers.MostrarError("No se pudo registrar (DNI duplicado o capacidad llena)");
            Helpers.Pausa();
        }

        // Permite modificar campos individuales de un alumno
        static void Modificar()
        {
            if (Alumno.ObtenerDatos().GetLength(0) == 0)
            {
                Helpers.MostrarError("No hay alumnos registrados."); Helpers.Pausa(); return;
            }

            string dni = SolicitarDni();

            bool salir = false;
            while (!salir)
            {
                MenuModificar.Mostrar();

                string op = Helpers.Solicitar("\nOpción: ", s => s.Length == 1 && "12345".Contains(s), MenuModificar.Mostrar);
                switch (op)
                {
                    case "1":
                        string nom = Helpers.Solicitar("Nuevos nombres: ", n => !string.IsNullOrWhiteSpace(n));
                        Alumno.ModificarCampo(dni, 1, nom);
                        Console.WriteLine("\nNombres actualizados."); Helpers.Pausa(); break;
                    case "2":
                        string ape = Helpers.Solicitar("Nuevos apellidos: ", v => !string.IsNullOrWhiteSpace(v));
                        Alumno.ModificarCampo(dni, 2, ape);
                        Console.WriteLine("\nApellidos actualizados."); Helpers.Pausa(); break;
                    case "3":
                        string cor = Helpers.Solicitar("Nuevo correo: ", c => c.Contains("@"));
                        Alumno.ModificarCampo(dni, 3, cor);
                        Console.WriteLine("\nCorreo actualizado."); Helpers.Pausa(); break;
                    case "4":
                        string cel = Helpers.Solicitar("Nuevo celular: ", c => c.Length == 9 && c.All(char.IsDigit));
                        Alumno.ModificarCampo(dni, 4, cel);
                        Console.WriteLine("\nCelular actualizado."); Helpers.Pausa(); break;
                    case "5": salir = true; break;
                    default: Helpers.MostrarError("Opción no válida. Intente nuevamente."); Helpers.Pausa(); break;
                }
            }
        }

        // Elimina un alumno después de confirmación
        static void Eliminar()
        {
            if (Alumno.ObtenerDatos().GetLength(0) == 0)
            {
                Helpers.MostrarError("No hay alumnos registrados."); Helpers.Pausa(); return;
            }

            string dni = SolicitarDni();
            bool salir = false;
            while (!salir)
            {
                MenuEliminar.Mostrar();
                string op = Helpers.Solicitar("\nOpción: ", s => s.Length == 1 && "12".Contains(s), MenuEliminar.Mostrar);
                if (op == "1")
                {
                    if (Alumno.Eliminar(dni, out string n, out string a))
                        Console.WriteLine($"\nSe eliminó el alumno {n} {a}");
                    else
                        Helpers.MostrarError("No se pudo eliminar.");
                    Helpers.Pausa(); salir = true;
                }
                else salir = true;
            }
        }

        // Gestiona el registro de notas por evaluación
        static void RegistroNotas()
        {
            if (Alumno.ObtenerDatos().GetLength(0) == 0)
            {
                Helpers.MostrarError("No hay alumnos registrados."); Helpers.Pausa(); return;
            }

            string dni = SolicitarDni();
            bool salir = false;
            while (!salir)
            {
                MenuNotas.Mostrar();

                string op = Helpers.Solicitar("\nEvaluación: ", s => s.Length == 1 && "12345".Contains(s), MenuNotas.Mostrar);
                if (op == "5") { salir = true; continue; }

                double nota = Helpers.Solicitar("Ingrese nota (0-20): ", (double v) => v >= 0 && v <= 20);
                if (Alumno.RegistrarNota(dni, int.Parse(op), nota, out string n, out string a))
                    Console.WriteLine($"\nSe grabó nota del alumno {n} {a}");
                else
                    Helpers.MostrarError("No se pudo grabar la nota.");
                Helpers.Pausa();
            }
        }

        // Reporte de alumnos ordenado por distintos criterios
        static void ReporteOrdenado()
        {
            if (Alumno.ObtenerDatos().GetLength(0) == 0)
            {
                Helpers.MostrarError("\nNo hay alumnos registrados."); Helpers.Pausa(); return;
            }

            bool salir = false;
            while (!salir)
            {
                MenuOrden.Mostrar();

                string op = Helpers.Solicitar("\nTipo de Orden: ", s => s.Length == 1 && "123456".Contains(s[0]), MenuOrden.Mostrar);
                if (op == "6") { salir = true; continue; }

                Helpers.ImprimirTabla(Alumno.Ordenar(int.Parse(op)));
                Helpers.Pausa();
            }
        }

        // Muestra los N mejores promedios
        static void ReporteTopN()
        {
            Console.Clear();
            int n = Helpers.Solicitar("Número de alumnos: ", (int v) => v > 0);
            Helpers.ImprimirTabla(Alumno.ObtenerTopN(n), true);
            Helpers.Pausa();
        }

        // Solicita un DNI existente en el registro; repite hasta encontrarlo
        private static string SolicitarDni()
        {
            while (true)
            {
                string dni = Helpers.Solicitar("Ingrese el DNI del alumno: ", d => d.Length == 8 && d.All(char.IsDigit));
                var datos = Alumno.ObtenerDatos();
                for (int i = 0; i < datos.GetLength(0); i++)
                    if (datos[i, 0] == dni) return dni;
                Helpers.MostrarError("DNI no encontrado.");
            }
        }

        
    }
}
