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
          /*  Objeto teclado = new Objeto();
            teclado=Generador3D.CrearTeclado(new Vertice(0, 0.1f, -2f));
            teclado.Name="teclado";
            Objeto monitor3d = new Objeto();
            monitor3d=Generador3D.CrearPantalla(new Vertice(0, 2.5f, -4f));
            monitor3d.Name="monitor";
            Objeto torre = new Objeto();
            torre=Generador3D.CrearPC(new Vertice(-4f, 2f, -3f));
            torre.Name="torre";
            Objeto mouse = new Objeto();
            mouse=Generador3D.CrearMouse(new Vertice(3.5f, 0f, -2f));
            mouse.Name="mouse";
            Objeto mesa = new Objeto();
            mesa=Generador3D.CrearMesa(new Vertice(0f, -2.5f, -3f));
            mesa.Name="mesa";
            Objeto silla= new Objeto();
            silla=Generador3D.CrearSilla(new Vertice(0f, -3f, 2f));
            Escenario escena = new Escenario();
            escena.AddObjeto(teclado.Name,teclado);
            escena.AddObjeto(monitor3d.Name,monitor3d);
            escena.AddObjeto(torre.Name,torre);
            escena.AddObjeto(mouse.Name,mouse);
            escena.AddObjeto(silla.Name,silla);
            escena.AddObjeto(mesa.Name,mesa);*/
          
           // Serializador.SerializeToFile<Escenario>(escena, "escritorioCompleto.json");
            Escenario escenario1 = new Escenario();          
            escenario1=Serializador.DeserializeFromFile<Escenario>("escritorioCompleto.json");
            //escenario1.Rotar(90,0,0);    
            escenario1.Mover(escenario1.Centro.X, escenario1.Centro.Y, escenario1.Centro.Z);
            escenario1.Escalar(2f,2f,2f);
            escenario1.Escalar(0.5f,0.5f,0.5f);
            
            // escenario1.Objetos["monitor"].Rotar(0,90,0);

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

