using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TEO_EF.Menu
{
    internal class MenuOrden
    {
        public static void Mostrar()
        {
            Console.Clear();
            Console.WriteLine("=== TIPO DE ORDEN ===");
            Console.WriteLine("1. Nro DNI");
            Console.WriteLine("2. Nombres");
            Console.WriteLine("3. Apellidos");
            Console.WriteLine("4. Correo");
            Console.WriteLine("5. Celular");
            Console.WriteLine("6. Salir");
        }
    }
}
