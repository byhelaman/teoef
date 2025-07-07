using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Alumno
    {
        /// Número máximo de alumnos que se pueden almacenar
        private const int MAX_ALUMNOS = 100;

        // Número de columnas (campos) que cada registro posee
        private const int COLUMNAS = 10;

        // Matriz que contiene los datos
        private static readonly string[,] _data = new string[MAX_ALUMNOS, COLUMNAS];

        // Cantidad actual de alumnos registrados (también indica la siguiente fila libre)
        private static int _contador = 0;

        // Indices de columnas
        private const int IDX_DNI = 0;
        private const int IDX_NOMBRES = 1;
        private const int IDX_APELLIDOS = 2;
        private const int IDX_CORREO = 3;
        private const int IDX_CELULAR = 4;
        private const int IDX_T1 = 5;
        private const int IDX_PARCIAL = 6;
        private const int IDX_T2 = 7;
        private const int IDX_FINAL = 8;
        private const int IDX_NF = 9; // Nota Final (promedio)

        // Agrega un nuevo alumno al arreglo.
        // Retorna True si la operación fue exitosa; False si la lista está llena o el DNI ya existe
        public static bool Agregar(string dni, string nombres, string apellidos, string correo, string celular)
        {
            // Verificar capacidad y unicidad del DNI
            if (_contador >= MAX_ALUMNOS || BuscarIndice(dni) != -1)
                return false; // Lista llena o DNI duplicado

            // Registrar los datos en la fila disponible
            _data[_contador, IDX_DNI] = dni;
            _data[_contador, IDX_NOMBRES] = nombres;
            _data[_contador, IDX_APELLIDOS] = apellidos;
            _data[_contador, IDX_CORREO] = correo;
            _data[_contador, IDX_CELULAR] = celular;
            _contador++;

            return true;
        }

        // Modifica un campo de un alumno existente identificado por su DNI
        // Retorna True si se modificó; False si el alumno no existe o el código es inválido
        public static bool ModificarCampo(string dni, int campo, string nuevoValor)
        {
            int i = BuscarIndice(dni);
            if (i == -1) return false; // Alumno no encontrado

            switch (campo)
            {
                case 1: _data[i, IDX_NOMBRES] = nuevoValor; break;
                case 2: _data[i, IDX_APELLIDOS] = nuevoValor; break;
                case 3: _data[i, IDX_CORREO] = nuevoValor; break;
                case 4: _data[i, IDX_CELULAR] = nuevoValor; break;
                default: return false; // Campo inválido
            }
            return true;
        }

        // Elimina un alumno del arreglo y devuelve sus nombres y apellidos
        // Retorna True si se eliminó; False si no se encontró
        public static bool Eliminar(string dni, out string nombres, out string apellidos)
        {
            nombres = apellidos = string.Empty;
            int idx = BuscarIndice(dni);
            if (idx == -1) return false;

            // Copiar datos para devolverlos
            nombres = _data[idx, IDX_NOMBRES];
            apellidos = _data[idx, IDX_APELLIDOS];

            // Desplazar las filas hacia arriba para tapar el hueco
            for (int i = idx; i < _contador - 1; i++)
                for (int j = 0; j < COLUMNAS; j++)
                    _data[i, j] = _data[i + 1, j];

            _contador--; // Ajustar contador
            return true;
        }

        // Registra una nota en la evaluación indicada y recalcula la nota final (NF)
        // Retorna True si la operación fue exitosa
        public static bool RegistrarNota(string dni, int evaluacion, double nota, out string nombres, out string apellidos)
        {
            nombres = apellidos = string.Empty;
            int i = BuscarIndice(dni);
            if (i == -1) return false;

            // Registrar la nota formateada con dos decimales
            switch (evaluacion)
            {
                case 1: _data[i, IDX_T1] = nota.ToString("0.00"); break;
                case 2: _data[i, IDX_PARCIAL] = nota.ToString("0.00"); break;
                case 3: _data[i, IDX_T2] = nota.ToString("0.00"); break;
                case 4: _data[i, IDX_FINAL] = nota.ToString("0.00"); break;
                default: return false; // Evaluación inválida
            }

            RecalcularNF(i); // Actualizar nota final
            nombres = _data[i, IDX_NOMBRES];
            apellidos = _data[i, IDX_APELLIDOS];
            return true;
        }

        // Devuelve una copia de los datos actuales (sólo filas utilizadas)
        public static string[,] ObtenerDatos()
        {
            var copia = new string[_contador, COLUMNAS];
            for (int i = 0; i < _contador; i++)
                for (int j = 0; j < COLUMNAS; j++)
                    copia[i, j] = _data[i, j];
            return copia;
        }

        // Ordena y devuelve una copia de los datos según el tipo especificado
        // 1=DNI, 2=Nombres, 3=Apellidos, 4=Correo, 5=Celular
        public static string[,] Ordenar(int tipo)
        {
            var copia = ObtenerDatos();

            for (int i = 0; i < copia.GetLength(0) - 1; i++)
            {
                for (int j = i + 1; j < copia.GetLength(0); j++)
                    if (Comparar(copia, j, i, tipo) < 0)
                        Intercambiar(copia, j, i);

            }
            return copia;
        }

        //public static string[,] Ordenar(int tipo)
        //{
        //    var copia = ObtenerDatos();
        //    for (int i = 0; i < copia.GetLength(0) - 1; i++)
        //    {
        //        int min = i;
        //        for (int j = i + 1; j < copia.GetLength(0); j++)
        //            if (Comparar(copia, j, min, tipo) < 0) min = j;
        //        if (min != i) Intercambiar(copia, i, min);
        //    }
        //    return copia;
        //}


        // Retorna los N alumnos con mayor nota final
        public static string[,] ObtenerTopN(int n)
        {
            if (n > _contador) n = _contador; // Ajustar si N excede el total
            var ord = OrdenarPorNFDesc();
            var top = new string[n, COLUMNAS];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < COLUMNAS; j++)
                    top[i, j] = ord[i, j];
            return top;
        }

        // Busca el índice (fila) de un alumno por su DNI
        private static int BuscarIndice(string dni)
        {
            for (int i = 0; i < _contador; i++)
                if (_data[i, IDX_DNI] == dni) return i;
            return -1; // No encontrado
        }

        // Convierte un string a double, devolviendo 0 si es nulo o inválido
        private static double Parse(string s) => double.TryParse(s, out var v) ? v : 0;

        // Recalcula la nota final (NF) de la fila indicada usando ponderaciones fijas
        private static void RecalcularNF(int i)
        {
            double t1 = Parse(_data[i, IDX_T1]);
            double par = Parse(_data[i, IDX_PARCIAL]);
            double t2 = Parse(_data[i, IDX_T2]);
            double fin = Parse(_data[i, IDX_FINAL]);

            double nf = t1 * 0.15 + par * 0.30 + t2 * 0.15 + fin * 0.40;
            _data[i, IDX_NF] = nf.ToString("0.00");
        }

        // Compara dos filas según la columna indicada por "tipo"
        private static int Comparar(string[,] arr, int a, int b, int tipo)
        {
            int col;
            switch (tipo)
            {
                case 1: col = IDX_DNI; break;
                case 2: col = IDX_NOMBRES; break;
                case 3: col = IDX_APELLIDOS; break;
                case 4: col = IDX_CORREO; break;
                case 5: col = IDX_CELULAR; break;
                default: col = IDX_DNI; break; // Fallback
            }
            return string.Compare(arr[a, col], arr[b, col], StringComparison.OrdinalIgnoreCase);
        }

        public static string[,] OrdenarPorNFDesc()
        {
            var copia = ObtenerDatos();

            for (int i = 0; i < copia.GetLength(0) - 1; i++)
            {
                for (int j = i + 1; j < copia.GetLength(0); j++)
                    if (Parse(copia[j, IDX_NF]) > Parse(copia[i, IDX_NF]))
                        Intercambiar(copia, j, i);

            }
            return copia;
        }

        // Intercambia dos filas completas en la matriz
        private static void Intercambiar(string[,] arr, int r1, int r2)
        {
            for (int i = 0; i < COLUMNAS; i++)
            {
                var tmp = arr[r1, i];
                arr[r1, i] = arr[r2, i];
                arr[r2, i] = tmp;
            }
        }
    }
}
