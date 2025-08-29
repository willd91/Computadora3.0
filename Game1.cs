using OpenTK.Windowing.Desktop;   // Para crear ventanas de OpenTK
using OpenTK.Graphics.OpenGL4;     // Para usar OpenGL 4 (funciones gráficas)
using OpenTK.Mathematics;          // Para vectores, colores y matrices

namespace ProGrafica
{
    // Clase principal del juego/ventana que hereda de GameWindow
    // GameWindow es la estructura típica de OpenTK para manejar ventanas y gráficos
    public class Game1 : GameWindow
    {
        private Computadora computadora; // Nuestra figura principal (la computadora)
        private Shader shader;           // Shader para dibujar las figuras (vertex + fragment)

        // Constructor de la ventana, recibe configuraciones de ventana y juego
        public Game1(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) { }

        // Este método se ejecuta una vez al iniciar la ventana
        protected override void OnLoad()
        {
            base.OnLoad();

            // Color de fondo de la ventana (blanco)
            GL.ClearColor(Color4.White);

            // Inicializamos el shader con los archivos de vertex y fragment shader
            // Estos archivos contienen el código que la GPU usará para dibujar
            shader = new Shader("vertex_shader.glsl", "fragment_shader.glsl");

            // Inicializamos nuestra computadora
            computadora = new Computadora();
        }

        // Este método se llama automáticamente cada vez que hay que dibujar un frame
        protected override void OnRenderFrame(OpenTK.Windowing.Common.FrameEventArgs args)
        {
            base.OnRenderFrame(args);

            // Limpiamos la pantalla antes de dibujar (ColorBufferBit limpia el color)
            GL.Clear(ClearBufferMask.ColorBufferBit);

            // Dibujamos la computadora usando nuestro shader
            computadora.Dibujar(shader);

            // Intercambiamos buffers para mostrar el frame dibujado
            SwapBuffers();
        }
    }
}

