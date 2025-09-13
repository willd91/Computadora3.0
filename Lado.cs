using ProGrafica;
using System.Text.Json.Serialization;

[Serializable]
public class Lado
{
    public List<Vertice> Vertices { get; set; }
    public Vertice Centro { get; set; }
    public Vertice Color { get; set; }
    [JsonConstructor] 
    public Lado(Vertice centro, List<Vertice> vertices, Vertice color)
    {
        Centro = centro;
        Vertices = vertices;
        Color = color;
    }
    public Lado() : this(new Vertice(0, 0, 0), new List<Vertice>(), new Vertice(255, 255, 255)) { }
    public List<Vertice> CalcularVerticesReales()
    {
        var resultado = new List<Vertice>();
        foreach (var v in Vertices)
        {
            resultado.Add(new Vertice(Centro.X + v.X, Centro.Y + v.Y, Centro.Z + v.Z));
        }
        return resultado;
    }
    public float[] Dibujar()
    {
        var reales = CalcularVerticesReales();
        var datos = new List<float>();
        foreach (var v in reales)
        {
            datos.Add(v.X);
            datos.Add(v.Y);
            datos.Add(v.Z);
        }
        return datos.ToArray();
    }
}
