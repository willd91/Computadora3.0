using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;

namespace ProGrafica;
#region Clase Vértice
/// <summary>
/// Representa un vértice en 3D.
/// Se define en coordenadas absolutas.
/// </summary>
public class Vertice
{
    public Vector3 Posicion { get; set; }  // Posición absoluta en el espacio
    public Vector3 Color { get; set; }     // Color del vértice

    public Vertice(Vector3 posicion, Vector3 color)
    {
        Posicion = posicion;
        Color = color;
    }
}
#endregion