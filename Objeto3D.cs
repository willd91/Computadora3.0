

using System.Text.Json.Serialization;

namespace ProGrafica
{
    #region Clase Objeto3D
    [Serializable]
    public class Objeto3D
    {
        public List<Parte> Partes { get; set; }
        public Vertice Centro { get; set; }
        public Vertice Color { get; set; }
        public string Name { get; set; }
        public Objeto3D(string name, Vertice centro, List<Parte> partes, Vertice color)
        {
            this.Name = name;
            this.Centro = centro;
            this.Color = color;
            this.Partes = partes;
        }
        public Objeto3D() : this("", new Vertice(0, 0, 0), new List<Parte>(), new Vertice(0, 0, 0)) { }
      /*  public List<Parte> CalcularPartesReales()
        {
            var partesReales = new List<Parte>();
            foreach (var parte in Partes)
            {
                var nuevaParte = new Parte(
                    new Vertice(
                        Centro.X + parte.Centro.X,
                        Centro.Y + parte.Centro.Y,
                        Centro.Z + parte.Centro.Z
                    ),
                    parte.Lados,
                    Color // o combinar con el color de la parte
                );
                partesReales.Add(nuevaParte);
            }
            return partesReales;
        }*/
    }
    #endregion
}
