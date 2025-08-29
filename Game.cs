using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System.Collections.Generic;

namespace ProGrafica
{
    public class Game : GameWindow
    {


        // ========================
        // Variables de Cámara
        // ========================
        private Vector3 _cameraPosition = new Vector3(0.0f, 0.0f, 5.0f); // Posición inicial de la cámara
        private Vector3 _cameraFront = new Vector3(0.0f, 0.0f, -1.0f);   // Dirección hacia adelante
        private Vector3 _cameraUp = new Vector3(0.0f, 1.0f, 0.0f);       // Dirección hacia arriba

        private float _yaw = -90.0f;   // Rotación izquierda/derecha
        private float _pitch = 0.0f;   // Rotación arriba/abajo
        private float _fov = 45.0f;    // Campo de visión

        private Vector2 _lastMousePos;
        private bool _firstMove = true;

        private float _cameraSpeed = 2.5f; // Velocidad de movimiento
        private float _mouseSensitivity = 0.2f; // Sensibilidad del mouse
        // ========================

        private Objeto3D computadora;// Objeto 3D principal en este caso computadora
        private Shader shader;

        // Buffers
        private List<int> vaos = new List<int>();
        private List<int> vbos = new List<int>();
        private List<int> counts = new List<int>();

        // 🔹 Partes principales de la computadora
        private Parte pantalla;
        private Parte teclado;
        private Parte mouse;
        private Parte cpu;
        private Parte Mesa;
        public Game(GameWindowSettings gws, NativeWindowSettings nws) : base(gws, nws) { }

        protected override void OnLoad()
        {
            base.OnLoad();
            _cameraPosition = new Vector3(2.0f, 1.0f, 3.0f); // Vista desde un costado
            GL.ClearColor(0.53f, 0.81f, 0.98f, 1.0f);
            // Habilitamos el Z-buffer para 3D
            GL.Enable(EnableCap.DepthTest);
            
            shader = new Shader("shader.vert", "shader.frag");

            // ==================================================
            // 🔹 Construimos cada parte con sus lados
            // ==================================================
            pantalla = CrearPantalla();
            teclado = CrearTeclado();
            mouse = CrearMouse();
            cpu = CrearCPU();
            Mesa = CrearMesa();
            // 🔹 Unimos todo en el objeto "computadora"
            computadora = new Objeto3D(Vector3.Zero, new List<Parte> { pantalla, teclado,mouse ,cpu, Mesa }, Vector3.One);

            // ==================================================
            // 🔹 Generar buffers para cada lado de la computadora
            // ==================================================
            foreach (var parte in computadora.Partes)
            {
                foreach (var lado in parte.Lados)
                {
                    float[] vertices = new float[lado.Vertices.Count * 3];
                    for (int i = 0; i < lado.Vertices.Count; i++)
                    {
                        vertices[i * 3 + 0] = lado.Vertices[i].Posicion.X;
                        vertices[i * 3 + 1] = lado.Vertices[i].Posicion.Y;
                        vertices[i * 3 + 2] = lado.Vertices[i].Posicion.Z;
                    }

                    int vao = GL.GenVertexArray();
                    int vbo = GL.GenBuffer();

                    GL.BindVertexArray(vao);
                    GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
                    GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, BufferUsageHint.StaticDraw);

                    GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
                    GL.EnableVertexAttribArray(0);

                    vaos.Add(vao);
                    vbos.Add(vbo);
                    counts.Add(lado.Vertices.Count);
                }
            }
        }

        // ==================================================
        // 🔹 Métodos para crear cada parte
        // ==================================================
        private Parte CrearPantalla()
        {
            var lados = new List<Lado>();
            Vector3 centro = new Vector3(0, 0, 0);
            float ancho = 0.8f;
            float alto = 0.6f;
            float profundidad = 0.1f;

            // Cara frontal (marco negro)
            lados.Add(new Lado(centro,
                new List<Vector3> {
            new Vector3(-ancho/2, alto/2, profundidad/2),
            new Vector3(ancho/2, alto/2, profundidad/2),
            new Vector3(ancho/2, -alto/2, profundidad/2),
            new Vector3(-ancho/2, -alto/2, profundidad/2)
                },
                new Vector3(0, 0, 0) // negro
            ));

            // Pantalla interior (azul)
            lados.Add(new Lado(centro,
                new List<Vector3> {
            new Vector3(-ancho/2 + 0.05f, alto/2 - 0.05f, profundidad/2 + 0.01f),
            new Vector3(ancho/2 - 0.05f, alto/2 - 0.05f, profundidad/2 + 0.01f),
            new Vector3(ancho/2 - 0.05f, -alto/2 + 0.05f, profundidad/2 + 0.01f),
            new Vector3(-ancho/2 + 0.05f, -alto/2 + 0.05f, profundidad/2 + 0.01f)
                },
                new Vector3(0.39f, 0.58f, 0.93f) // azul
            ));

            // Cara trasera
            lados.Add(new Lado(centro,
                new List<Vector3> {
            new Vector3(-ancho/2, alto/2, -profundidad/2),
            new Vector3(ancho/2, alto/2, -profundidad/2),
            new Vector3(ancho/2, -alto/2, -profundidad/2),
            new Vector3(-ancho/2, -alto/2, -profundidad/2)
                },
                new Vector3(0.2f, 0.2f, 0.2f) // gris oscuro
            ));

            // Lados laterales para dar volumen
            // Lado superior
            lados.Add(new Lado(centro,
                new List<Vector3> {
            new Vector3(-ancho/2, alto/2, profundidad/2),
            new Vector3(ancho/2, alto/2, profundidad/2),
            new Vector3(ancho/2, alto/2, -profundidad/2),
            new Vector3(-ancho/2, alto/2, -profundidad/2)
                },
                new Vector3(0.3f, 0.3f, 0.3f)
            ));

            // Lado inferior
            lados.Add(new Lado(centro,
                new List<Vector3> {
            new Vector3(-ancho/2, -alto/2, profundidad/2),
            new Vector3(ancho/2, -alto/2, profundidad/2),
            new Vector3(ancho/2, -alto/2, -profundidad/2),
            new Vector3(-ancho/2, -alto/2, -profundidad/2)
                },
                new Vector3(0.3f, 0.3f, 0.3f)
            ));

            // Lado izquierdo
            lados.Add(new Lado(centro,
                new List<Vector3> {
            new Vector3(-ancho/2, alto/2, profundidad/2),
            new Vector3(-ancho/2, -alto/2, profundidad/2),
            new Vector3(-ancho/2, -alto/2, -profundidad/2),
            new Vector3(-ancho/2, alto/2, -profundidad/2)
                },
                new Vector3(0.3f, 0.3f, 0.3f)
            ));

            // Lado derecho
            lados.Add(new Lado(centro,
                new List<Vector3> {
            new Vector3(ancho/2, alto/2, profundidad/2),
            new Vector3(ancho/2, -alto/2, profundidad/2),
            new Vector3(ancho/2, -alto/2, -profundidad/2),
            new Vector3(ancho/2, alto/2, -profundidad/2)
                },
                new Vector3(0.3f, 0.3f, 0.3f)
            ));

            return new Parte(new Vector3(-0.2f, 0.1f, 0f), lados, Vector3.One);
        }

        private Parte CrearTeclado()
        {
            var lados = new List<Lado>();
            Vector3 centro = new Vector3(0, 0, 0);
            float ancho = 0.8f;    // Eje X (de izquierda a derecha)
            float largo = 0.05f;    // Eje Y (de frente a atrás)
            float grosor = 0.3f;  // Eje Z (de arriba a abajo)

            // Cara superior (teclas) - Ahora en plano X-Y
            lados.Add(new Lado(centro,
                new List<Vector3> {
            new Vector3(-ancho/2, -largo/2, grosor/2),   // Esquina inferior izquierda
            new Vector3(ancho/2, -largo/2, grosor/2),    // Esquina inferior derecha
            new Vector3(ancho/2, largo/2, grosor/2),     // Esquina superior derecha
            new Vector3(-ancho/2, largo/2, grosor/2)     // Esquina superior izquierda
                },
                new Vector3(0.5f, 0.5f, 0.5f) // gris (color de las teclas)
            ));

            // Cara inferior (parte de abajo del teclado)
            lados.Add(new Lado(centro,
                new List<Vector3> {
            new Vector3(-ancho/2, -largo/2, -grosor/2),  // Esquina inferior izquierda
            new Vector3(ancho/2, -largo/2, -grosor/2),   // Esquina inferior derecha
            new Vector3(ancho/2, largo/2, -grosor/2),    // Esquina superior derecha
            new Vector3(-ancho/2, largo/2, -grosor/2)    // Esquina superior izquierda
                },
                new Vector3(0.3f, 0.3f, 0.3f) // gris oscuro
            ));

            // Lados laterales
            // Frontal (donde están las teclas de función F1-F12)
            lados.Add(new Lado(centro,
                new List<Vector3> {
            new Vector3(-ancho/2, largo/2, grosor/2),    // Superior izquierda
            new Vector3(ancho/2, largo/2, grosor/2),     // Superior derecha
            new Vector3(ancho/2, largo/2, -grosor/2),    // Inferior derecha
            new Vector3(-ancho/2, largo/2, -grosor/2)    // Inferior izquierda
                },
                new Vector3(0.4f, 0.4f, 0.4f)
            ));

            // Trasero (donde está la barra espaciadora)
            lados.Add(new Lado(centro,
                new List<Vector3> {
            new Vector3(-ancho/2, -largo/2, grosor/2),   // Superior izquierda
            new Vector3(ancho/2, -largo/2, grosor/2),    // Superior derecha
            new Vector3(ancho/2, -largo/2, -grosor/2),   // Inferior derecha
            new Vector3(-ancho/2, -largo/2, -grosor/2)   // Inferior izquierda
                },
                new Vector3(0.4f, 0.4f, 0.4f)
            ));

            // Izquierdo (lado izquierdo del teclado)
            lados.Add(new Lado(centro,
                new List<Vector3> {
            new Vector3(-ancho/2, -largo/2, grosor/2),   // Frente inferior
            new Vector3(-ancho/2, largo/2, grosor/2),    // Frente superior
            new Vector3(-ancho/2, largo/2, -grosor/2),   // Atrás superior
            new Vector3(-ancho/2, -largo/2, -grosor/2)   // Atrás inferior
                },
                new Vector3(0.4f, 0.4f, 0.4f)
            ));

            // Derecho (lado derecho del teclado)
            lados.Add(new Lado(centro,
                new List<Vector3> {
            new Vector3(ancho/2, -largo/2, grosor/2),    // Frente inferior
            new Vector3(ancho/2, largo/2, grosor/2),     // Frente superior
            new Vector3(ancho/2, largo/2, -grosor/2),    // Atrás superior
            new Vector3(ancho/2, -largo/2, -grosor/2)    // Atrás inferior
                },
                new Vector3(0.4f, 0.4f, 0.4f)
            ));

            return new Parte(new Vector3(-0.25f, -0.05f, 0.0f), lados, Vector3.One);
        }

        private Parte CrearMouse()
        {
            var lados = new List<Lado>();
            Vector3 centro = new Vector3(0, 0, 0);
            float radio = 0.08f;
            float altura = 0.04f;
            int segmentos = 16;

            // Cara superior (semi-elipse para forma de mouse)
            var verticesSuperior = new List<Vector3>();
            for (int i = 0; i <= segmentos; i++)
            {
                float ang = MathHelper.Pi * i / segmentos;
                verticesSuperior.Add(new Vector3(
                    (float)Math.Cos(ang) * radio,
                    (float)Math.Sin(ang) * radio * 0.6f,
                    altura / 2
                ));
            }
            lados.Add(new Lado(centro, verticesSuperior, new Vector3(0.3f, 0.3f, 0.3f)));

            // Cara inferior
            var verticesInferior = new List<Vector3>();
            for (int i = 0; i <= segmentos; i++)
            {
                float ang = MathHelper.Pi * i / segmentos;
                verticesInferior.Add(new Vector3(
                    (float)Math.Cos(ang) * radio,
                    (float)Math.Sin(ang) * radio * 0.6f,
                    -altura / 2
                ));
            }
            lados.Add(new Lado(centro, verticesInferior, new Vector3(0.2f, 0.2f, 0.2f)));

            // Lados laterales (conectan superior e inferior)
            for (int i = 0; i < segmentos; i++)
            {
                float ang1 = MathHelper.Pi * i / segmentos;
                float ang2 = MathHelper.Pi * (i + 1) / segmentos;

                Vector3 sup1 = new Vector3((float)Math.Cos(ang1) * radio, (float)Math.Sin(ang1) * radio * 0.6f, altura / 2);
                Vector3 sup2 = new Vector3((float)Math.Cos(ang2) * radio, (float)Math.Sin(ang2) * radio * 0.6f, altura / 2);
                Vector3 inf1 = new Vector3((float)Math.Cos(ang1) * radio, (float)Math.Sin(ang1) * radio * 0.6f, -altura / 2);
                Vector3 inf2 = new Vector3((float)Math.Cos(ang2) * radio, (float)Math.Sin(ang2) * radio * 0.6f, -altura / 2);

                lados.Add(new Lado(centro, new List<Vector3> { sup1, sup2, inf2, inf1 }, new Vector3(0.25f, 0.25f, 0.25f)));
            }

            return new Parte(new Vector3(-0.1f, 0.0f, 0f), lados, Vector3.One);
        }

        private Parte CrearCPU()
        {
            var lados = new List<Lado>();
            Vector3 centro = new Vector3(0, 0, 0);
            float ancho = 0.2f;
            float alto = 0.7f;
            float profundidad = 0.4f;

            // Cara frontal (exterior negro)
            lados.Add(new Lado(centro,
                new List<Vector3> {
            new Vector3(-ancho/2, alto/2, profundidad/2),
            new Vector3(ancho/2, alto/2, profundidad/2),
            new Vector3(ancho/2, -alto/2, profundidad/2),
            new Vector3(-ancho/2, -alto/2, profundidad/2)
                },
                new Vector3(0, 0, 0) // negro
            ));

            // Ventana frontal (interior azul)
            lados.Add(new Lado(centro,
                new List<Vector3> {
            new Vector3(-ancho/2 + 0.01f, alto/2 - 0.01f, profundidad/2 + 0.01f),
            new Vector3(ancho/2 - 0.01f, alto/2 - 0.01f, profundidad/2 + 0.01f),
            new Vector3(ancho/2 - 0.01f, -alto/2 + 0.01f, profundidad/2 + 0.01f),
            new Vector3(-ancho/2 + 0.01f, -alto/2 + 0.01f, profundidad/2 + 0.01f)
                },
                new Vector3(0.39f, 0.58f, 0.93f) // azul
            ));

            // Cara trasera
            lados.Add(new Lado(centro,
                new List<Vector3> {
            new Vector3(-ancho/2, alto/2, -profundidad/2),
            new Vector3(ancho/2, alto/2, -profundidad/2),
            new Vector3(ancho/2, -alto/2, -profundidad/2),
            new Vector3(-ancho/2, -alto/2, -profundidad/2)
                },
                new Vector3(0.1f, 0.1f, 0.1f) // negro muy oscuro
            ));

            // Lado superior
            lados.Add(new Lado(centro,
                new List<Vector3> {
            new Vector3(-ancho/2, alto/2, profundidad/2),
            new Vector3(ancho/2, alto/2, profundidad/2),
            new Vector3(ancho/2, alto/2, -profundidad/2),
            new Vector3(-ancho/2, alto/2, -profundidad/2)
                },
                new Vector3(0.2f, 0.2f, 0.2f)
            ));

            // Lado inferior
            lados.Add(new Lado(centro,
                new List<Vector3> {
            new Vector3(-ancho/2, -alto/2, profundidad/2),
            new Vector3(ancho/2, -alto/2, profundidad/2),
            new Vector3(ancho/2, -alto/2, -profundidad/2),
            new Vector3(-ancho/2, -alto/2, -profundidad/2)
                },
                new Vector3(0.2f, 0.2f, 0.2f)
            ));

            // Lado izquierdo
            lados.Add(new Lado(centro,
                new List<Vector3> {
            new Vector3(-ancho/2, alto/2, profundidad/2),
            new Vector3(-ancho/2, -alto/2, profundidad/2),
            new Vector3(-ancho/2, -alto/2, -profundidad/2),
            new Vector3(-ancho/2, alto/2, -profundidad/2)
                },
                new Vector3(0.2f, 0.2f, 0.2f)
            ));

            // Lado derecho
            lados.Add(new Lado(centro,
                new List<Vector3> {
            new Vector3(ancho/2, alto/2, profundidad/2),
            new Vector3(ancho/2, -alto/2, profundidad/2),
            new Vector3(ancho/2, -alto/2, -profundidad/2),
            new Vector3(ancho/2, alto/2, -profundidad/2)
                },
                new Vector3(0.2f, 0.2f, 0.2f)
            ));

            return new Parte(new Vector3(0.0f, 0.1f, -0.1f), lados, Vector3.One);
        }
        private Parte CrearMesa() {
            var lados = new List<Lado>();
            Vector3 centro = new Vector3(0, 0, 0);
            float ancho = 2.0f;    // Eje X (de izquierda a derecha)
            float largo = 0.05f;    // Eje Y (de frente a atrás)
            float grosor = 1.0f;  // Eje Z (de arriba a abajo)

            // Color de mesa (blanco roto/gris muy claro)
            Vector3 colorMesa = new Vector3(0.9f, 0.9f, 0.85f); // Blanco ligeramente cálido
            Vector3 colorMesaOscuro = new Vector3(0.8f, 0.8f, 0.75f); // Versión más oscura
            Vector3 colorBordeMesa = new Vector3(0.7f, 0.7f, 0.65f); // Para los bordes


            // Cara superior (teclas) - Ahora en plano X-Y
            lados.Add(new Lado(centro,
                new List<Vector3> {
            new Vector3(-ancho/2, -largo/2, grosor/2),   // Esquina inferior izquierda
            new Vector3(ancho/2, -largo/2, grosor/2),    // Esquina inferior derecha
            new Vector3(ancho/2, largo/2, grosor/2),     // Esquina superior derecha
            new Vector3(-ancho/2, largo/2, grosor/2)     // Esquina superior izquierda
                }, colorMesa // gris (color de las teclas)
            ));

            // Cara inferior (parte de abajo del teclado)
            lados.Add(new Lado(centro,
                new List<Vector3> {
            new Vector3(-ancho/2, -largo/2, -grosor/2),  // Esquina inferior izquierda
            new Vector3(ancho/2, -largo/2, -grosor/2),   // Esquina inferior derecha
            new Vector3(ancho/2, largo/2, -grosor/2),    // Esquina superior derecha
            new Vector3(-ancho/2, largo/2, -grosor/2)    // Esquina superior izquierda
                }, colorMesaOscuro // gris oscuro
            ));

            // Lados laterales
            // Frontal (donde están las teclas de función F1-F12)
            lados.Add(new Lado(centro,
                new List<Vector3> {
            new Vector3(-ancho/2, largo/2, grosor/2),    // Superior izquierda
            new Vector3(ancho/2, largo/2, grosor/2),     // Superior derecha
            new Vector3(ancho/2, largo/2, -grosor/2),    // Inferior derecha
            new Vector3(-ancho/2, largo/2, -grosor/2)    // Inferior izquierda
                }, colorBordeMesa
            ));

            // Trasero (donde está la barra espaciadora)
            lados.Add(new Lado(centro,
                new List<Vector3> {
            new Vector3(-ancho/2, -largo/2, grosor/2),   // Superior izquierda
            new Vector3(ancho/2, -largo/2, grosor/2),    // Superior derecha
            new Vector3(ancho/2, -largo/2, -grosor/2),   // Inferior derecha
            new Vector3(-ancho/2, -largo/2, -grosor/2)   // Inferior izquierda
                }, colorBordeMesa
            ));

            // Izquierdo (lado izquierdo del teclado)
            lados.Add(new Lado(centro,
                new List<Vector3> {
            new Vector3(-ancho/2, -largo/2, grosor/2),   // Frente inferior
            new Vector3(-ancho/2, largo/2, grosor/2),    // Frente superior
            new Vector3(-ancho/2, largo/2, -grosor/2),   // Atrás superior
            new Vector3(-ancho/2, -largo/2, -grosor/2)   // Atrás inferior
                }, colorBordeMesa
            ));

            // Derecho (lado derecho del teclado)
            lados.Add(new Lado(centro,
                new List<Vector3> {
            new Vector3(ancho/2, -largo/2, grosor/2),    // Frente inferior
            new Vector3(ancho/2, largo/2, grosor/2),     // Frente superior
            new Vector3(ancho/2, largo/2, -grosor/2),    // Atrás superior
            new Vector3(ancho/2, -largo/2, -grosor/2)    // Atrás inferior
                }, colorBordeMesa
            ));

            return new Parte(new Vector3(-0.3f, -0.1f, -0.2f), lados, Vector3.One);
        }
        // ==================================================
        // 🔹 Render
        // ==================================================
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            shader.Usar();

            // Matrices de proyección y vista
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(
                MathHelper.DegreesToRadians(_fov),
                Size.X / (float)Size.Y,
                0.1f,
                100.0f);

            Matrix4 view = Matrix4.LookAt(_cameraPosition, _cameraPosition + _cameraFront, _cameraUp);

            shader.SetMatrix4("projection", projection);
            shader.SetMatrix4("view", view);

            int index = 0;
            foreach (var parte in computadora.Partes)
            {
                // Matriz de modelo para esta parte
                Matrix4 model = Matrix4.CreateTranslation(parte.Centro);
                shader.SetMatrix4("model", model);

                foreach (var lado in parte.Lados)
                {
                    shader.SetColor(lado.Color.X, lado.Color.Y, lado.Color.Z, 1.0f);
                    GL.BindVertexArray(vaos[index]);
                    GL.DrawArrays(PrimitiveType.TriangleFan, 0, counts[index]);
                    index++;
                }
            }

            SwapBuffers();
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
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            // Tecla Escape → cerrar juego
            if (KeyboardState.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.Escape))
                Close();

            // ========================
            // Movimiento de la Cámara
            // ========================
            float cameraSpeed = _cameraSpeed * (float)args.Time;

            if (KeyboardState.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.W))
                _cameraPosition += _cameraFront * cameraSpeed;
            if (KeyboardState.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.S))
                _cameraPosition -= _cameraFront * cameraSpeed;
            if (KeyboardState.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.A))
                _cameraPosition -= Vector3.Normalize(Vector3.Cross(_cameraFront, _cameraUp)) * cameraSpeed;
            if (KeyboardState.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.D))
                _cameraPosition += Vector3.Normalize(Vector3.Cross(_cameraFront, _cameraUp)) * cameraSpeed;
            if (KeyboardState.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.Space))
                _cameraPosition += _cameraUp * cameraSpeed;
            if (KeyboardState.IsKeyDown(OpenTK.Windowing.GraphicsLibraryFramework.Keys.LeftShift))
                _cameraPosition -= _cameraUp * cameraSpeed;
        }
        // ========================
        // Movimiento con el Mouse
        // ========================
        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            base.OnMouseMove(e);

            if (_firstMove)
            {
                _lastMousePos = new Vector2(e.X, e.Y);
                _firstMove = false;
            }
            else
            {
                float deltaX = e.X - _lastMousePos.X;
                float deltaY = _lastMousePos.Y - e.Y; // invertido porque Y aumenta hacia abajo

                _lastMousePos = new Vector2(e.X, e.Y);

                deltaX *= _mouseSensitivity;
                deltaY *= _mouseSensitivity;

                _yaw += deltaX;
                _pitch += deltaY;

                // Limitamos el pitch para no voltear demasiado
                if (_pitch > 89.0f) _pitch = 89.0f;
                if (_pitch < -89.0f) _pitch = -89.0f;

                // Recalculamos la dirección de la cámara
                Vector3 front;
                front.X = MathF.Cos(MathHelper.DegreesToRadians(_yaw)) * MathF.Cos(MathHelper.DegreesToRadians(_pitch));
                front.Y = MathF.Sin(MathHelper.DegreesToRadians(_pitch));
                front.Z = MathF.Sin(MathHelper.DegreesToRadians(_yaw)) * MathF.Cos(MathHelper.DegreesToRadians(_pitch));
                _cameraFront = Vector3.Normalize(front);
            }
        }


        // ========================
        // Zoom con la rueda del Mouse
        // ========================
        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);

            _fov -= e.OffsetY;
            if (_fov < 1.0f) _fov = 1.0f;
            if (_fov > 90.0f) _fov = 90.0f;
        }
    }

}
