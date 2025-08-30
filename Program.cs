using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace ProGrafica
{
    class Program
    {
        public static void Main()
        {
            var monitors = Monitors.GetMonitors(); // Obtiene lista de monitores
            foreach (var m in monitors)
            {
                Console.WriteLine($"Monitor: {m.Name} - {m.ClientArea.Size}");
            }

            // Ejemplo: elegir el segundo monitor (índice 1)
            var monitor = monitors[0];

            var gws = GameWindowSettings.Default;
            var nws = new NativeWindowSettings()
            {
                Title = "COMPUTADORA EN 3D",
                StartFocused = true,
                WindowState = WindowState.Fullscreen,
                CurrentMonitor = monitor.Handle // asigna el monitor correcto
            };

            using (var game = new Game(gws, nws))
            {
                game.Run();
            }
        }
    }
}
