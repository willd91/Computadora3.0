using OpenTK.Graphics.OpenGL4;  // Funciones de OpenGL 4
using OpenTK.Mathematics;       // Vector2, Vector4, etc.

namespace Graficos2D
{
    // Poligono hereda de Figura
    public class Poligono : Figura
    {
        private float[] vertices; // Arreglo de coordenadas X,Y
        private int vao;          // Vertex Array Object
        private int vbo;          // Vertex Buffer Object

        // Constructor recibe centro, color y los puntos que definen el polígono
        public Poligono(Vector2 centro, Vector4 color, Vector2[] puntos) : base(centro, color)
        {
            // Convertir Vector2[] a float[] (X,Y,X,Y,...)
            vertices = new float[puntos.Length * 2];
            for (int i = 0; i < puntos.Length; i++)
            {
                vertices[i * 2] = puntos[i].X + centro.X;      // X relativo al centro
                vertices[i * 2 + 1] = puntos[i].Y + centro.Y;  // Y relativo al centro
            }

            // Crear VAO y VBO
            vao = GL.GenVertexArray();  // Genera un identificador de VAO
            vbo = GL.GenBuffer();       // Genera un identificador de VBO

            // Vincular VAO
            GL.BindVertexArray(vao);

            // Vincular VBO y subir los datos
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            // Configurar layout de vértices
            GL.EnableVertexAttribArray(0);  // Habilitar atributo 0
            GL.VertexAttribPointer(
                0,                        // índice de atributo
                2,                        // 2 componentes por vértice (X,Y)
                VertexAttribPointerType.Float,
                false,
                2 * sizeof(float),        // stride: cada 2 floats
                0                         // offset: 0
            );

            // Desvincular VBO y VAO para seguridad
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
        }

        // Dibujar el polígono usando un shader
        public override void Dibujar(Shader shader)
        {
            shader.Usar();  // Activar shader
            shader.SetColor(Color.X, Color.Y, Color.Z, Color.W); // Enviar color

            // Vincular VAO y dibujar
            GL.BindVertexArray(vao);
            GL.DrawArrays(
                PrimitiveType.TriangleFan, // Tipo de primitiva: polígono relleno
                0,                         // Inicio en el primer vértice
                vertices.Length / 2         // Número de vértices
            );

            // Desvincular VAO
            GL.BindVertexArray(0);
        }
    }
}
