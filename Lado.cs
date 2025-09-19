using System.Text.Json.Serialization;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace ProGrafica
{
    [Serializable]
    public class Lado
    {
        public List<Vertice> Vertices { get; set; }
        public Vertice Centro { get; set; }
        public Vertice Color { get; set; }
        private int vao, vbo;
        private int vertexCount;
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
                resultado.Add(new Vertice(
                    Centro.X + v.X,
                    Centro.Y + v.Y,
                    Centro.Z + v.Z
                ));
            }
            return resultado;
        }
        public float[] GetVerticesFloat()
        {
            float[] datos = new float[Vertices.Count * 3];
            for (int i = 0; i < Vertices.Count; i++)
            {
                datos[i * 3 + 0] = Vertices[i].X;
                datos[i * 3 + 1] = Vertices[i].Y;
                datos[i * 3 + 2] = Vertices[i].Z;
            }
            return datos;
        }
        /// <summary>
        /// Inicializa VAO y VBO con los vértices del polígono.
        /// </summary>
        public void InicializarBuffers()
        {
            var reales = CalcularVerticesReales();
            var datos = new List<float>();
            foreach (var v in reales)
            {
                datos.Add(v.X);
                datos.Add(v.Y);
                datos.Add(v.Z);
            }
            vertexCount = reales.Count;
            vao = GL.GenVertexArray();
            vbo = GL.GenBuffer();
            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, datos.Count * sizeof(float), datos.ToArray(), BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
        }

        public void Dibujar(Shader shader)
        {
            shader.Usar();

            // Pasar color al shader
            int colorLoc = GL.GetUniformLocation(shader.Handle, "uColor");
            var colorVec = new Vector4(Color.X / 255f, Color.Y / 255f, Color.Z / 255f, 1.0f);
            GL.Uniform4(colorLoc, colorVec);

            GL.BindVertexArray(vao);
            GL.DrawArrays(PrimitiveType.TriangleFan, 0, vertexCount);
            GL.BindVertexArray(0);
        }

        public void Dispose()
        {
            GL.DeleteBuffer(vbo);
            GL.DeleteVertexArray(vao);
        }
    }
}
