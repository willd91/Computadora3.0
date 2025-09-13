using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Collections.Generic;

namespace ProGrafica
{
    public class Game : GameWindow
    {
        private List<Objeto3D>? objetos; // Lista de objetos a dibujar (puede ser null)
        private Shader shader;

        // Buffers
        private readonly List<int> vaos = new();
        private readonly List<int> vbos = new();
        private readonly List<int> counts = new();
        private readonly List<Vector3> colors = new();

        // Cámara
        private Vector3 cameraPos = new(0, 0, 5f);
        private Vector3 cameraFront = -Vector3.UnitZ;
        private Vector3 cameraUp = Vector3.UnitY;
        private float yaw = -90f;
        private float pitch = 0f;
        private float lastX, lastY;
        private bool firstMove = true;

        public Game(GameWindowSettings gws, NativeWindowSettings nws, List<Objeto3D>? objetos = null) : base(gws, nws)
        {
            this.objetos = objetos; // puede ser null
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(Color4.CornflowerBlue);
            GL.Enable(EnableCap.DepthTest);

            shader = new Shader("shader.vert", "shader.frag");

            if (objetos == null) return; // nada que cargar

            // Generar buffers recorriendo todos los objetos → partes → lados
            foreach (var obj in objetos)
            {
                // obtener el offset del objeto una sola vez
                float objOffX = obj.Centro.X;
                float objOffY = obj.Centro.Y;
                float objOffZ = obj.Centro.Z;

                foreach (var parte in obj.Partes)
                {
                    float parteOffX = parte.Centro.X;
                    float parteOffY = parte.Centro.Y;
                    float parteOffZ = parte.Centro.Z;

                    foreach (var lado in parte.Lados)
                    {
                     
                        float[] verticesLocal = lado.Dibujar();

                        // Crear un nuevo arreglo con los offsets parte + objeto añadidos
                        float[] verticesAbs = new float[verticesLocal.Length];
                        for (int k = 0; k < verticesLocal.Length; k += 3)
                        {
                            verticesAbs[k + 0] = verticesLocal[k + 0] + parteOffX + objOffX;
                            verticesAbs[k + 1] = verticesLocal[k + 1] + parteOffY + objOffY;
                            verticesAbs[k + 2] = verticesLocal[k + 2] + parteOffZ + objOffZ;
                        }

                        int vao = GL.GenVertexArray();
                        int vbo = GL.GenBuffer();

                        GL.BindVertexArray(vao);
                        GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
                        GL.BufferData(BufferTarget.ArrayBuffer, verticesAbs.Length * sizeof(float),
                                      verticesAbs, BufferUsageHint.StaticDraw);

                        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
                        GL.EnableVertexAttribArray(0);

                        GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
                        GL.BindVertexArray(0);

                        vaos.Add(vao);
                        vbos.Add(vbo);
                        counts.Add(verticesAbs.Length / 3);

                        // Color de la parte (si el lado no tiene color propio)
                        colors.Add(new Vector3(
                            parte.Color.X / 255f,
                            parte.Color.Y / 255f,
                            parte.Color.Z / 255f
                        ));
                    }
                }
            }
        }


        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            shader.Usar();

            // Configuración de matrices
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(
                MathHelper.DegreesToRadians(45f),
                Size.X / (float)Size.Y,
                0.1f, 100f
            );
            Matrix4 view = Matrix4.LookAt(cameraPos, cameraPos + cameraFront, cameraUp);
            Matrix4 model = Matrix4.Identity;

            shader.SetMatrix4("projection", projection);
            shader.SetMatrix4("view", view);
            shader.SetMatrix4("model", model);

            if (objetos != null)
            {
                // Dibujar cada lado cargado en buffers
                for (int i = 0; i < vaos.Count; i++)
                {
                    int colorLoc = GL.GetUniformLocation(shader.Handle, "uColor");
                    GL.Uniform4(colorLoc, new Vector4(colors[i], 1.0f));

                    GL.BindVertexArray(vaos[i]);
                    GL.DrawArrays(PrimitiveType.TriangleFan, 0, counts[i]);
                }
            }

            SwapBuffers();
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            if (KeyboardState.IsKeyDown(Keys.Escape))
                Close();

            float cameraSpeed = 2.5f * (float)args.Time;

            if (KeyboardState.IsKeyDown(Keys.W))
                cameraPos += cameraSpeed * cameraFront;
            if (KeyboardState.IsKeyDown(Keys.S))
                cameraPos -= cameraSpeed * cameraFront;
            if (KeyboardState.IsKeyDown(Keys.A))
                cameraPos -= Vector3.Normalize(Vector3.Cross(cameraFront, cameraUp)) * cameraSpeed;
            if (KeyboardState.IsKeyDown(Keys.D))
                cameraPos += Vector3.Normalize(Vector3.Cross(cameraFront, cameraUp)) * cameraSpeed;
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);

            if (firstMove)
            {
                lastX = e.X;
                lastY = e.Y;
                firstMove = false;
            }
            else
            {
                float deltaX = e.X - lastX;
                float deltaY = lastY - e.Y; // invertido porque Y crece hacia abajo
                lastX = e.X;
                lastY = e.Y;

                float sensitivity = 0.1f;
                deltaX *= sensitivity;
                deltaY *= sensitivity;

                yaw += deltaX;
                pitch += deltaY;

                pitch = MathHelper.Clamp(pitch, -89f, 89f);

                Vector3 front;
                front.X = MathF.Cos(MathHelper.DegreesToRadians(yaw)) * MathF.Cos(MathHelper.DegreesToRadians(pitch));
                front.Y = MathF.Sin(MathHelper.DegreesToRadians(pitch));
                front.Z = MathF.Sin(MathHelper.DegreesToRadians(yaw)) * MathF.Cos(MathHelper.DegreesToRadians(pitch));
                cameraFront = Vector3.Normalize(front);
            }
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
        }

        protected override void OnUnload()
        {
            base.OnUnload();
            foreach (var vbo in vbos) GL.DeleteBuffer(vbo);
            foreach (var vao in vaos) GL.DeleteVertexArray(vao);
            GL.DeleteProgram(shader.Handle);
        }
    }
}

