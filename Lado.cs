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

        public Vertice Rotacion { get; set; } = new Vertice(0, 0, 0);
        public Vertice Escala { get; set; } = new Vertice(1, 1, 1);
        
        private int vao, vbo;
        private int vertexCount;

        [JsonConstructor]
        public Lado(Vertice centro, List<Vertice> vertices, Vertice color, 
                   Vertice rotacion = null, Vertice escala = null) 
        {
            Centro = centro;
            Vertices = vertices;
            Color = color;
            Rotacion = rotacion ?? new Vertice(0, 0, 0);
            Escala = escala ?? new Vertice(1, 1, 1);
        }

        public Lado() : this(new Vertice(0, 0, 0), new List<Vertice>(), new Vertice(255, 255, 255)) { }

        public Matrix4 GetModelMatrix()
        {
            return Matrix4.CreateTranslation(Centro.X, Centro.Y, Centro.Z) *
            Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(Rotacion.Z)) *
            Matrix4.CreateRotationY(MathHelper.DegreesToRadians(Rotacion.Y)) *
            Matrix4.CreateRotationX(MathHelper.DegreesToRadians(Rotacion.X)) *
            Matrix4.CreateScale(Escala.X, Escala.Y, Escala.Z);
        }

        public void Mover(float x, float y, float z)
        {
            Centro.X += x;
            Centro.Y += y;
            Centro.Z += z;
        }

        public void MoverA(float x, float y, float z)
        {
            Centro.X = x;
            Centro.Y = y;
            Centro.Z = z;
        }

        public void Rotar(float x, float y, float z)
        {
            Rotacion.X += x;
            Rotacion.Y += y;
            Rotacion.Z += z;
        }

        public void Escalar(float x, float y, float z)
        {
            Escala.X *= x;
            Escala.Y *= y;
            Escala.Z *= z;
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

        public void InicializarBuffers()
        {
  
            var datos = GetVerticesFloat();
            vertexCount = Vertices.Count;
            
            vao = GL.GenVertexArray();
            vbo = GL.GenBuffer();
            
            GL.BindVertexArray(vao);
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            GL.BufferData(BufferTarget.ArrayBuffer, datos.Length * sizeof(float), datos, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
        }

        public void Dibujar(Shader shader, Matrix4 parentTransform = default)
        {
            shader.Usar();

            Matrix4 modelMatrix = parentTransform == default ? 
                GetModelMatrix() : 
                parentTransform * GetModelMatrix();
                
            shader.SetMatrix4("model", modelMatrix);

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