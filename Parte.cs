using OpenTK.Mathematics;
using ProGrafica;
using System.Text.Json.Serialization;

[Serializable]
public class Parte
{
    public List<Lado> Lados { get; set; }
    public Vertice Centro { get; set; }
    public Vertice Color { get; set; }
    public Vertice Rotacion { get; set; }
    public Vertice Escala { get; set; }

    [JsonConstructor]
    public Parte(Vertice centro, List<Lado> lados, Vertice color, Vertice rotacion = null, Vertice escala = null)
    {
        this.Centro = centro ?? new Vertice(0, 0, 0); ;
        this.Color = color;
        this.Lados = lados;
        Rotacion = rotacion ?? new Vertice(0, 0, 0);
        Escala = escala ?? new Vertice(1, 1, 1);
    }

    public Parte() : this(new Vertice(0, 0, 0), new List<Lado>(), new Vertice(0, 0, 0)) { }

    public Matrix4 GetModelMatrix()
    {
        return Matrix4.CreateScale(Escala.X, Escala.Y, Escala.Z) *
          Matrix4.CreateRotationX(MathHelper.DegreesToRadians(Rotacion.X)) *
          Matrix4.CreateRotationY(MathHelper.DegreesToRadians(Rotacion.Y)) *
          Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(Rotacion.Z)) *
          Matrix4.CreateTranslation(Centro.X, Centro.Y, Centro.Z);
    }
    public void Mover(float x, float y, float z)
    {
        Centro.X += x;
        Centro.Y += y;
        Centro.Z += z;
    }

    public void Rotar(float x, float y, float z)
    {
        Rotacion.X += x;
        Rotacion.Y += y;
        Rotacion.Z += z;
    }

    public void Escalar(float x, float y, float z)
    {
        // Escalar el centro también para mantener la cohesión
        Centro.X *= x;
        Centro.Y *= y;
        Centro.Z *= z;

        // Escalar los factores de escala
        Escala.X *= x;
        Escala.Y *= y;
        Escala.Z *= z;
    }

    public void AplicarColorALados()
    {
        foreach (var lado in Lados)
            lado.Color = this.Color;
    }
    public void Dibujar(Shader shader,Matrix4 parentTransform = default)
    {
        Matrix4 parteTransform = parentTransform == default ?
            GetModelMatrix() :
            parentTransform * GetModelMatrix();
        foreach (var lado in Lados)
        {
            lado.Dibujar(shader, parteTransform);
        }
        
    }
}
