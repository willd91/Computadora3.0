
using OpenTK.Mathematics;
namespace ProGrafica
{
    #region Clase Parte (Figura 3D)
    /// <summary>
    /// Una parte es una figura 3D formada por varios lados.
    /// Se posiciona con respecto a un centro.
    /// </summary>
    public class Parte
    {
        public List<Lado> Lados { get; set; }
        public Vector3 Centro { get; set; }
        public Vector3 Color { get; set; }
        public string Name { get; set; }
        public Parte(string name, Vector3 centro, List<Lado> lados, Vector3 color)
        {
            this.Name = name;
            Centro = centro;
            Color = color;
            Lados = new List<Lado>();

            // Ajustamos la posición de cada lado en función del centro de la parte
            foreach (var lado in lados)
            {
                var desplazados = new List<Vector3>();
                foreach (var v in lado.Vertices)
                {
                    desplazados.Add(v.Posicion - lado.Centro); // desplazamiento relativo al lado original
                }
                // Re-creamos el lado pero relativo al centro de la parte
                Lados.Add(new Lado(centro + lado.Centro, desplazados, lado.Color));
            }
        }
    }
    #endregion
}
