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
   
         //  Objeto3D mesa= Generador3D.CrearMesaBasica();
           // JsonHelper.SerializeToFile(mesa, "mesa.json");
            var gws = GameWindowSettings.Default;
            var monitors = Monitors.GetMonitors();
            var monitor = monitors[1];           
           // Objeto3D mesa1 = JsonHelper.DeserializeFromFile<Objeto3D>("mesa.json");
           //Parte cubo1=Generador3D.CrearCubo(new Vertice(0.5f,0.5f,0f),2f,new Vertice(255, 0, 0));
           //Parte esfera =Generador3D.CrearEsfera(new Vertice(-2f, 0.5f, 0f), 1f,new Vertice(0, 255, 0));
           // Objeto3D esferas= new Objeto3D("esfera",new Vertice(0, 0, 0), new List<Parte> { esfera }, new Vertice(0, 0, 0));
            //JsonHelper.SerializeToFile(esferas, "esfera.json");

            List<Objeto3D> objetos = new List<Objeto3D>();
            // Crear un objeto3D vacío
           /*     Objeto3D objeto = new Objeto3D
                {
                    Name = "EscritorioCompleto",
                    Centro = new Vertice(0, 0, 0),
                    Partes = new List<Parte>()
                };

            // ----------------- Agregar partes -----------------
            objeto.Partes.Add(Generador3D.CrearEscritorio());   // Escritorio
            objeto.Partes.Add(Generador3D.CrearPantalla());    // Pantalla
            objeto.Partes.Add(Generador3D.CrearCPU());         // CPU
            objeto.Partes.Add(Generador3D.CrearTeclado());     // Teclado
            objeto.Partes.Add(Generador3D.CrearMouse());       // Mouse
                                                         */       //objeto.Partes.Add(Generador3D.CrearPantalla(new Vertice(0,0,0),new Vertice(255, 99, 71)));
                                                                // objeto.Name="pantalla";
                                                                // Serializador.SerializeToFile(objeto,objeto.Name+".json");
           // Serializador.SerializeToFile(objeto, "escritorioCompleto.json");
            objetos.Add(Serializador.DeserializeFromFile<Objeto3D>("escritorioCompleto.json"));
            var nws = new NativeWindowSettings()
            {
                Title = "COMPUTADORA EN 3D",
                StartFocused = true,
                WindowState = WindowState.Fullscreen,
                CurrentMonitor = monitor.Handle 
            };
            using (var game = new Game(gws, nws,objetos))
            {
                game.Run();
            }
        }

    }
}

