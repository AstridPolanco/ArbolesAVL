using System;
using System.Collections.Generic;

public class Nodo
{
    public int Valor { get; set; }
    public Nodo? Izquierdo { get; set; }
    public Nodo? Derecho { get; set; }
    public int Altura { get; set; }

    public Nodo(int valor)
    {
        Valor = valor;
        Izquierdo = null;
        Derecho = null;
        Altura = 1; // Altura inicial del nodo es 1 (solo el nodo mismo)
    }
}