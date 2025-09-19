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
    public void Dibujar(Shader shader)
    {  
        foreach (var lado in Lados)
        {
            lado.Dibujar(shader);//mandamos a dibujar a los lados
        }
        
    }
}
