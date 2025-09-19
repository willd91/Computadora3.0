using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace ProGrafica
{
    public class Game: GameWindow
    {   
        private Escenario escena;
        private Shader shader ;
        private Camera camera;
        private Vector2 lastMousePos;
        private bool firstMove = true;
        public Game(GameWindowSettings gws, NativeWindowSettings nws,Escenario escena) : base(gws, nws)
        {
            this.escena = escena;
        }
        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            GL.Enable(EnableCap.DepthTest);

            shader = new Shader("shader.vert", "shader.frag");
            camera = new Camera(new Vector3(0, 0, 20));
            // Inicializar buffers para todos los lados de todos los objetos
            foreach (var objeto in escena.Objetos.Values)
            {
                foreach (var parte in objeto.Partes.Values)
                {
                    foreach (var lado in parte.Lados)
                    {
                        lado.InicializarBuffers();
                    }
                }
            }
            //CursorGrabbed = true; // ocultar y fijar cursor
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            shader.Usar();

            Matrix4 model = Matrix4.Identity;
            Matrix4 view = camera.GetViewMatrix();
            Matrix4 projection = camera.GetProjectionMatrix(Size.X / (float)Size.Y);
         
            shader.SetMatrix4("model", model);
            shader.SetMatrix4("view", view);
            shader.SetMatrix4("projection", projection);

            escena.Dibujar(shader);

            SwapBuffers();
        }
        protected override void OnFramebufferResize(FramebufferResizeEventArgs e)
        {
            base.OnFramebufferResize(e);
            GL.Viewport(0, 0, e.Width, e.Height);
        }
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
        }
        protected override void OnUnload()
        {
            // Unbind all the resources by binding the targets to 0/null.
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            GL.DeleteProgram(shader.Handle);

            base.OnUnload();
        }
        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            if (firstMove)
            {
                lastMousePos = new Vector2(e.X, e.Y);
                firstMove = false;
            }
            else
            {
                var deltaX = e.X - lastMousePos.X;
                var deltaY = e.Y - lastMousePos.Y;
                lastMousePos = new Vector2(e.X, e.Y);

                camera.Rotate(deltaX, deltaY);
            }
        }
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            camera.Zoom(e.OffsetY); // scroll = zoom
        }
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            var input = KeyboardState;
            float speed = 2.5f * (float)args.Time;

            if (input.IsKeyDown(Keys.W)) camera.Move(camera.Front, speed);
            if (input.IsKeyDown(Keys.S)) camera.Move(-camera.Front, speed);
            if (input.IsKeyDown(Keys.A)) camera.Move(-camera.Right, speed);
            if (input.IsKeyDown(Keys.D)) camera.Move(camera.Right, speed);

            if (input.IsKeyDown(Keys.Escape)) Close();
        }

    }
}
