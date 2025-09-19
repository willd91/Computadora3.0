using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System.Runtime.CompilerServices;
using OpenTK.Mathematics;
namespace ProGrafica
{
    class Program
    {
       
        public static void Main()
        {
            var gws = GameWindowSettings.Default;
            var monitors = Monitors.GetMonitors();
            var monitor = monitors[0];
            Objeto3D teclado = new Objeto3D();
            teclado=Generador3D.CrearTeclado(new Vertice(0, -1.5f, -1f));
            teclado.Name="teclado";
            Objeto3D monitor3d = new Objeto3D();
            monitor3d=Generador3D.CrearPantalla(new Vertice(0, 2.5f, -4f));
            monitor3d.Name="monitor";
            Objeto3D torre = new Objeto3D();
            torre=Generador3D.CrearPC(new Vertice(-8f, 2f, -3f));
            torre.Name="torre";
            Objeto3D mouse = new Objeto3D();
            mouse=Generador3D.CrearMouse(new Vertice(6.5f, -1.5f, -2f));
            mouse.Name="mouse";
            Objeto3D mesa = new Objeto3D();
            mesa=Generador3D.CrearMesa(new Vertice(0f, -7f, -3f));
            mesa.Name="mesa";
            Objeto3D silla= new Objeto3D();
            silla=Generador3D.CrearSilla(new Vertice(0f, -8f, 6f));
            Escenario escena = new Escenario();
            escena.AddObjeto(teclado.Name,teclado);
            escena.AddObjeto(monitor3d.Name,monitor3d);
            escena.AddObjeto(torre.Name,torre);
            escena.AddObjeto(mouse.Name,mouse);
            escena.AddObjeto(silla.Name,silla);
            escena.AddObjeto(mesa.Name,mesa);

           // Serializador.SerializeToFile<Escenario>(escena, "escritorioCompleto.json");
            Escenario escenario1 = new Escenario();
            
            escenario1=Serializador.DeserializeFromFile<Escenario>("escritorioCompleto.json");
           
            var nws = new NativeWindowSettings()
            {
                Title = "COMPUTADORA EN 3D",
                StartFocused = true,
                WindowState = WindowState.Fullscreen,
                CurrentMonitor = monitor.Handle 
            };
            using (var game = new Game(gws, nws, escenario1))
            {
                game.Run();
            }
        }

    }
}

