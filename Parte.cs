using ProGrafica;
using System.Text.Json.Serialization;

[Serializable]
public class Parte
{
    public List<Lado> Lados { get; set; }
    public Vertice Centro { get; set; }
    public Vertice Color { get; set; }

    [JsonConstructor]
    public Parte(Vertice centro, List<Lado> lados, Vertice color)
    {
        this.Centro = centro;
        this.Color = color;
        this.Lados = lados;
    }

    public Parte() : this(new Vertice(0, 0, 0), new List<Lado>(), new Vertice(0, 0, 0)) { }

    public void AplicarColorALados()
    {
        foreach (var lado in Lados)
            lado.Color = this.Color;
    }
   /* public float[] DibujarParte()
    {
        var datos = new List<float>();
        foreach (var lado in Lados)
        {
            // Calcula vértices del lado con su propio centro
            var verticesReales = lado.CalcularVerticesReales();
            // Luego sumamos el offset del centro de la parte
            foreach (var v in verticesReales)
            {
                datos.Add(v.X + Centro.X);
                datos.Add(v.Y + Centro.Y);
                datos.Add(v.Z + Centro.Z);
            }
        }
        return datos.ToArray();
    }*/
}
