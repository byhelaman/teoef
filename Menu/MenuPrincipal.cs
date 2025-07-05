using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEO_EF.Menu
{
    internal class MenuPrincipal
    {
        public static void Mostrar()
        {
            Console.WriteLine("=== SISTEMA DE REGISTRO DE ALUMNOS ===");
            Console.WriteLine("1. Listar alumnos");
            Console.WriteLine("2. Registrar alumno");
            Console.WriteLine("3. Modificar alumno");
            Console.WriteLine("4. Eliminar alumno");
            Console.WriteLine("5. Registro de Notas");
            Console.WriteLine("6. Reporte Ordenado");
            Console.WriteLine("7. Reporte N notas");
            Console.WriteLine("8. Salir");
        }
    }
}
