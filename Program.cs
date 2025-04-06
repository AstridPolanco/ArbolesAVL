using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        var datos = GenerarDatos(100000);
        var resultados = new Resultados();

        var arbolBinario = new ArbolBinario();
        ProbarArbol(datos, resultados, "Arbol Binario", arbolBinario);

        var arbolAVL = new ArbolAVL();
        ProbarArbol(datos, resultados, "Arbol AVL", arbolAVL);

        MostrarResultados(resultados);
    }

    static int[] GenerarDatos(int cantidad)
    {
        var random = new Random();
        return Enumerable.Range(0, cantidad).Select(_ => random.Next(1000000)).ToArray();
    }

    static void ProbarArbol(int[] datos, Resultados resultados, string nombreArbol, ArbolBinario arbol)
    {
        var stopwatch = new Stopwatch();

        // Insertar
        stopwatch.Restart();
        foreach (var dato in datos)
        {
            arbol.Insertar(dato);
        }
        resultados.TiemposInsercion[nombreArbol] = stopwatch.ElapsedMilliseconds;
        resultados.AlturasPostInsercion[nombreArbol] = arbol.GetAltura();

        // Buscar
        var casosBusqueda = new int[] { datos[0], datos[datos.Length / 2], datos[^1], -1 };
        stopwatch.Restart();
        foreach (var valor in casosBusqueda)
        {
            for (int i = 0; i < 1000; i++)
            {
                arbol.Buscar(valor);
            }
        }
        resultados.TiemposBusqueda[nombreArbol] = stopwatch.Elapsed.TotalMilliseconds / (casosBusqueda.Length * 1000.0);

        // Eliminar
        stopwatch.Restart();
        foreach (var dato in datos)
        {
            arbol.Eliminar(dato);
        }
        resultados.TiemposEliminacion[nombreArbol] = stopwatch.ElapsedMilliseconds;

        resultados.AlturasPostEliminacion[nombreArbol] = arbol.GetAltura();

        //Verificar balanceo
        if (arbol is ArbolAVL arbolAVL)
        {
           resultados.Balanceado[nombreArbol] = arbolAVL.EstaBalanceado();
        }
    }
    static void MostrarResultados(Resultados resultados)
    {
        Console.WriteLine("Resultados de las pruebas de rendimiento:");
        Console.WriteLine("Operación\tBST\tAVL");
        Console.WriteLine("-------------------------------------------------");
                Console.WriteLine("\n[TIEMPOS (ms)]");
        Console.WriteLine("Operación\tBST\t\tAVL");
        Console.WriteLine($"Inserción\t{resultados.TiemposInsercion["Arbol Binario"]}\t\t{resultados.TiemposInsercion["Arbol AVL"]}");
        Console.WriteLine($"Búsqueda\t{resultados.TiemposBusqueda["Arbol Binario"]:F4}\t{resultados.TiemposBusqueda["Arbol AVL"]:F4}");
        Console.WriteLine($"Eliminación\t{resultados.TiemposEliminacion["Arbol Binario"]}\t\t{resultados.TiemposEliminacion["Arbol AVL"]}");

        Console.WriteLine("\n[ALTURAS]");
        Console.WriteLine($"BST: {resultados.AlturasPostInsercion["Arbol Binario"]}");
        Console.WriteLine($"AVL: {resultados.AlturasPostInsercion["Arbol AVL"]}");

        Console.WriteLine("\n[BALANCEO]");
        Console.WriteLine($"AVL está balanceado: {resultados.Balanceado["Arbol AVL"]}");
    }
}

public class Resultados
{
    public Dictionary<string, long> TiemposInsercion { get; } = new();
    public Dictionary<string, double> TiemposBusqueda { get; } = new();
    public Dictionary<string, long> TiemposEliminacion { get; } = new();
    public Dictionary<string, int> AlturasPostInsercion { get; } = new();
    public Dictionary<string, int> AlturasPostEliminacion { get; } = new();
    public Dictionary<string, bool> Balanceado { get; } = new();
}
