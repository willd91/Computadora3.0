using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Graficos2D
{
    public class Circulo : Figura
    {
        private float[] vertices; // Array de coordenadas XY para el círculo
        private int vao;          // Vertex Array Object
        private int vbo;          // Vertex Buffer Object
        private int segmentos;    // Cantidad de segmentos para aproximar el círculo

        // Constructor
        public Circulo(Vector2 centro, Vector4 color, float radio, int segmentos = 40) : base(centro, color)
        {
            this.segmentos = segmentos;

            // Creamos el array de vértices: centro + puntos en el perímetro
            vertices = new float[(segmentos + 2) * 2]; // +2 porque incluimos el centro y cerramos el círculo
            vertices[0] = centro.X;
            vertices[1] = centro.Y;

            // Calcular los vértices del círculo
            for (int i = 0; i <= segmentos; i++)
            {
                double angulo = i * 2.0 * MathHelper.Pi / segmentos;
                vertices[(i + 1) * 2] = centro.X + (float)Math.Cos(angulo) * radio;
                vertices[(i + 1) * 2 + 1] = centro.Y + (float)Math.Sin(angulo) * radio;
            }

            // Configuración de VAO y VBO
            vao = GL.GenVertexArray();
            vbo = GL.GenBuffer();
            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

            GL.EnableVertexAttribArray(0);
            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, 2 * sizeof(float), 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
        }

        // Dibujar el círculo usando el shader
        public override void Dibujar(Shader shader)
        {
            shader.Usar();
            shader.SetColor(Color.X, Color.Y, Color.Z, Color.W);

            GL.BindVertexArray(vao);
            GL.DrawArrays(PrimitiveType.TriangleFan, 0, vertices.Length / 2);
            GL.BindVertexArray(0);
        }
    }
}

