using OpenTK.Mathematics;  // Para Vector2 y Vector4

namespace Graficos2D
{
    // Clase abstracta Figura: no se puede instanciar directamente
    public abstract class Figura
    {
        // Propiedades públicas
        public Vector2 Centro { get; set; } // Posición central de la figura
        public Vector4 Color { get; set; }  // Color RGBA (Rojo, Verde, Azul, Alpha)

        // Constructor
        public Figura(Vector2 centro, Vector4 color)
        {
            Centro = centro;  // Inicializar centro
            Color = color;    // Inicializar color
        }

        // Método abstracto que debe implementarse en todas las clases hijas
        public abstract void Dibujar(Shader shader);
    }
}
