using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public class ArbolBinario
{
    protected Nodo? Raiz;

    public void Insertar(int valor)
    {
        Raiz = InsertarRec(Raiz!, valor);
    }

    protected virtual Nodo InsertarRec(Nodo nodo, int valor)
    {
        if (nodo == null)
        {
            return new Nodo(valor);
        }

        if (valor < nodo.Valor)
        {
            nodo.Izquierdo = InsertarRec(nodo.Izquierdo!, valor);
        }
        else if (valor > nodo.Valor)
        {
            nodo.Derecho = InsertarRec(nodo.Derecho!, valor);
        }

        return nodo;
    }

    public bool Buscar(int valor)
    {
        var sw = Stopwatch.StartNew();
        bool resultado = BuscarRec(Raiz!, valor);
        sw.Stop();
        return resultado;
    }

    private bool BuscarRec(Nodo nodo, int valor)
    {
        if (nodo == null)
        {
            return false;
        }

        if (valor == nodo.Valor)
        {
            return true;
        }
        else if (valor < nodo.Valor)
        {
            return nodo.Izquierdo != null && BuscarRec(nodo.Izquierdo, valor);
        }
        else
        {
            return nodo.Derecho != null && BuscarRec(nodo.Derecho, valor);
        }
    }

    public void Eliminar(int valor)
    {
        Raiz = EliminarRec(Raiz, valor)!;
    }

    protected virtual Nodo? EliminarRec(Nodo? nodo, int valor)
    {
        if (nodo == null)
        {
            return null;
        }

        if (valor < nodo.Valor)
        {
            nodo.Izquierdo = EliminarRec(nodo.Izquierdo, valor);
        }
        else if (valor > nodo.Valor)
        {
            nodo.Derecho = EliminarRec(nodo.Derecho, valor);
        }
        else
        {
            // Nodo encontrado
            if (nodo.Izquierdo == null)
            {
                return nodo.Derecho;
            }
            else if (nodo.Derecho == null)
            {
                return nodo.Izquierdo;
            }

            // Nodo con dos hijos: obtener el sucesor inorden (mínimo en el subárbol derecho)
            nodo.Valor = ObtenerMinimo(nodo.Derecho).Valor;
            nodo.Derecho = EliminarRec(nodo.Derecho, nodo.Valor);
        }

        return nodo;
    }
    private Nodo ObtenerMinimo(Nodo nodo)
    {
        while (nodo.Izquierdo != null)
        {
            nodo = nodo.Izquierdo;
        }
        return nodo;
    }

    public int GetAltura()
    {
        return CalcularAlturaRec(Raiz!);
    }

    private int CalcularAlturaRec(Nodo? nodo)
    {
        if (nodo == null)
        {
            return 0;
        }

        int alturaIzquierda = CalcularAlturaRec(nodo.Izquierdo);
        int alturaDerecha = CalcularAlturaRec(nodo.Derecho);

        return Math.Max(alturaIzquierda, alturaDerecha) + 1;
    }
} 