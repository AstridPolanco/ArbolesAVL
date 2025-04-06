using System;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;

public class ArbolAVL : ArbolBinario
{
    protected override Nodo InsertarRec(Nodo nodo, int valor)
    {
        nodo = base.InsertarRec(nodo, valor);
        if (nodo == null)
        {
            return null;
        }

        nodo.Altura = 1 + Math.Max(GetAltura(nodo.Izquierdo), GetAltura(nodo.Derecho));
        int balance = GetBalance(nodo);

        //Rotaciones
        if (balance > 1 && valor < nodo.Izquierdo.Valor) // Izquierda Izquierda
        {
            return RotarDerecha(nodo);
        }
        if (balance < -1 && valor > nodo.Derecho.Valor) // Derecha Derecha
        {
            return RotarIzquierda(nodo);
        }
        if (balance > 1 && valor > nodo.Izquierdo.Valor) // Izquierda Derecha
        {
            nodo.Izquierdo = RotarIzquierda(nodo.Izquierdo);
            return RotarDerecha(nodo);
        }
        if (balance < -1 && valor < nodo.Derecho.Valor) // Derecha Izquierda
        {
            nodo.Derecho = RotarDerecha(nodo.Derecho);
            return RotarIzquierda(nodo);
        }
        return nodo;
    }

    private int GetAltura(Nodo nodo)
    {
        return nodo == null ? 0 : nodo.Altura;
    }

    private int GetBalance(Nodo nodo)
    {
        if (nodo == null)
        {
            return 0;
        }
        return GetAltura(nodo.Izquierdo) - GetAltura(nodo.Derecho);

    }

    public bool EstaBalanceado()
    {
        return EstaBalanceado(Raiz);
    }
    private bool EstaBalanceado(Nodo? nodo)
    {
        if (nodo == null)
        {
            return true;
        }

        int balance = GetBalance(nodo);
        if (Math.Abs(balance) > 1)
        return false;

        return EstaBalanceado(nodo.Izquierdo) && EstaBalanceado(nodo.Derecho);
    }

    private Nodo RotarDerecha(Nodo y)
    {
        var x = y.Izquierdo;
        y.Izquierdo = x.Derecho;
        x.Derecho = y;

        ActualizarAltura(y);
        ActualizarAltura(x);

        return x;
    }

    private Nodo RotarIzquierda(Nodo x)
    {
        var y = x.Derecho;
        x.Derecho = y?.Izquierdo;
        if (y != null)
        {
            y.Izquierdo = x;
        }

        ActualizarAltura(x);
        ActualizarAltura(y);

        return y;
    }
    private void ActualizarAltura(Nodo nodo)
    {
        nodo.Altura = 1 + Math.Max(GetAltura(nodo.Izquierdo), GetAltura(nodo.Derecho));
        
    }

}