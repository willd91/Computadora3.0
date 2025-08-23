using OpenTK.Windowing.Desktop;

namespace Graficos2D
{
    class Program
    {
        static void Main(string[] args)
        {
            var gameSettings = GameWindowSettings.Default;
            var nativeSettings = new NativeWindowSettings()
            {
                Size = new OpenTK.Mathematics.Vector2i(800, 600),
                Title = "Computadora 2D con OpenTK"
            };

            using (var juego = new Game(gameSettings, nativeSettings))
            {
                juego.Run();
            }
        }
    }
}
