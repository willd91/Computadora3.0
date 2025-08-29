using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
namespace ProGrafica
{
    #region Clase Objeto3D
    /// <summary>
    /// Objeto completo, formado por varias partes.
    /// </summary>
    public class Objeto3D
    {
        public List<Parte> Partes { get; set; }
        public Vector3 Centro { get; set; }
        public Vector3 Color { get; set; }

        public Objeto3D(Vector3 centro, List<Parte> partes, Vector3 color)
        {
            Centro = centro;
            Color = color;
            Partes = new List<Parte>();

            foreach (var parte in partes)
            {
                var ladosRelativos = new List<Lado>();
                foreach (var lado in parte.Lados)
                {
                    var desplazamientos = new List<Vector3>();
                    foreach (var v in lado.Vertices)
                    {
                        desplazamientos.Add(v.Posicion - lado.Centro);
                    }
                    ladosRelativos.Add(new Lado(centro + parte.Centro, desplazamientos, lado.Color));
                }
                Partes.Add(new Parte(centro + parte.Centro, ladosRelativos, parte.Color));
            }
        }
    }
    #endregion
}
