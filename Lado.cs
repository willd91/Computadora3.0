
using OpenTK.Mathematics;

namespace ProGrafica
{
    #region Clase Lado (Polígono 2D en 3D)
    /// <summary>
    /// Representa un polígono (cara de una figura).
    /// Se define con un centro y una lista de desplazamientos relativos de vértices.
    /// El color se aplica tanto al lado como a los vértices.
    /// </summary>
    public class Lado
    {
        public List<Vertice> Vertices { get; set; }
        public Vector3 Centro { get; set; }
        public Vector3 Color { get; set; }

        /// <param name="centro">Centro geométrico del lado</param>
        /// <param name="desplazamientos">Lista de posiciones relativas al centro</param>
        /// <param name="color">Color del lado (se pasa también a los vértices)</param>
        public Lado(Vector3 centro, List<Vector3> desplazamientos, Vector3 color)
        {
            this.Centro = centro;
            this.Color = color;
            this.Vertices = new List<Vertice>();

            // Cada desplazamiento se convierte en un vértice en coordenadas absolutas
            foreach (var d in desplazamientos)
            {
                var pos = centro + d;
                this.Vertices.Add(new Vertice(pos, color));
            }
        }
    }
    #endregion
}
