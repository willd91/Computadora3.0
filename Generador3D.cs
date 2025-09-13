
using OpenTK.Mathematics;
namespace ProGrafica
{
    public static class Generador3D
    {
      
        /// <summary>
        /// Genera un objeto 3D con forma de mesa básica:
        /// - 1 tablero (parte superior)
        /// - 4 patas (en las esquinas)
        /// </summary>
        public static Objeto3D CrearMesaBasica()
        {
            var partes = new List<Parte>();

            // =======================
            // TABLERO (mesa superior)
            // =======================
            var tablero = CrearPrismaRectangular(
                centro: new Vertice(0, 1.0f, 0), // un poco elevado
                ancho: 2.0f,
                alto: 0.2f,
                largo: 2.0f,
                color: new Vertice(139, 69, 19) // marrón madera
            );
            partes.Add(tablero);

            // =======================
            // PATAS (4)
            // =======================
            float pataAltura = 1.0f;
            float pataAncho = 0.2f;
            float offset = 0.9f; // para colocarlas en las esquinas del tablero

            // Pata 1 (frontal izquierda)
            partes.Add(CrearPrismaRectangular(
                centro: new Vertice(-offset, 0.0f, -offset),
                ancho: pataAncho,
                alto: pataAltura,
                largo: pataAncho,
                color: new Vertice(101, 67, 33) // marrón más oscuro
            ));
          
            // Pata 2 (frontal derecha)
            partes.Add(CrearPrismaRectangular(
                centro: new Vertice(1, -1,0),
                ancho: pataAncho,
                alto: pataAltura,
                largo: pataAncho,
                color: new Vertice(101, 67, 33)
            ));

            // Pata 3 (trasera izquierda)
            partes.Add(CrearPrismaRectangular(
                centro: new Vertice(-offset, 0.0f, offset),
                ancho: pataAncho,
                alto: pataAltura,
                largo: pataAncho,
                color: new Vertice(101, 67, 33)
            ));

            // Pata 4 (trasera derecha)
            partes.Add(CrearPrismaRectangular(
                centro: new Vertice(offset, 0.0f, offset),
                ancho: pataAncho,
                alto: pataAltura,
                largo: pataAncho,
                color: new Vertice(101, 67, 33)
            ));

            // =======================
            // OBJETO FINAL
            // =======================
            var mesa = new Objeto3D(
                name: "MesaBasica",
                centro: new Vertice(0, 0, 0),
                partes: partes,
                color: new Vertice(160, 82, 45) // color base general
            );

            return mesa;
        }

        /// <summary>
        /// Crea una parte representando un prisma rectangular (como cubo o caja).
        /// </summary>
        private static Parte CrearPrismaRectangular(Vertice centro, float ancho, float alto, float largo, Vertice color)
        {
            var lados = new List<Lado>();

            // Vertices relativos de un cubo centrado
            float x = ancho / 2f;
            float y = alto / 2f;
            float z = largo / 2f;

            // Caras (cada lado es un cuadrado con 4 vértices relativos a su centro)
            // Frente
            lados.Add(new Lado(new Vertice(0, 0, z),
                new List<Vertice>
                {
                    new Vertice(-x, -y, 0),
                    new Vertice(x, -y, 0),
                    new Vertice(x, y, 0),
                    new Vertice(-x, y, 0),
                }, color));

            // Atrás
            lados.Add(new Lado(new Vertice(0, 0, -z),
                new List<Vertice>
                {
                    new Vertice(x, -y, 0),
                    new Vertice(-x, -y, 0),
                    new Vertice(-x, y, 0),
                    new Vertice(x, y, 0),
                }, color));

            // Izquierda
            lados.Add(new Lado(new Vertice(-x, 0, 0),
                new List<Vertice>
                {
                    new Vertice(0, -y, -z),
                    new Vertice(0, -y, z),
                    new Vertice(0, y, z),
                    new Vertice(0, y, -z),
                }, color));

            // Derecha
            lados.Add(new Lado(new Vertice(x, 0, 0),
                new List<Vertice>
                {
                    new Vertice(0, -y, z),
                    new Vertice(0, -y, -z),
                    new Vertice(0, y, -z),
                    new Vertice(0, y, z),
                }, color));

            // Arriba
            lados.Add(new Lado(new Vertice(0, y, 0),
                new List<Vertice>
                {
                    new Vertice(-x, 0, -z),
                    new Vertice(x, 0, -z),
                    new Vertice(x, 0, z),
                    new Vertice(-x, 0, z),
                }, color));

            // Abajo
            lados.Add(new Lado(new Vertice(0, -y, 0),
                new List<Vertice>
                {
                    new Vertice(-x, 0, z),
                    new Vertice(x, 0, z),
                    new Vertice(x, 0, -z),
                    new Vertice(-x, 0, -z),
                }, color));

            return new Parte(centro, lados, color);
        }
        public static Parte CrearCubo(Vertice centro,float tamaño = 1.0f, Vertice color = null)
        {
            color ??= new Vertice(255, 255, 255); // blanco por defecto
            float h = tamaño / 2.0f;

            // Lados del cubo (cada lado es un cuadrado)
            var lados = new List<Lado>();

            // Frente (z = +h)
            lados.Add(new Lado(
                new Vertice(centro.X,centro.Y,centro.Z+ h), // Centro del lado
                new List<Vertice>
                {
            new Vertice(-h, -h, 0),
            new Vertice( h, -h, 0),
            new Vertice( h,  h, 0),
            new Vertice(-h,  h, 0)
                },
                color
            ));

            // Atrás (z = -h)
            lados.Add(new Lado(
                new Vertice(centro.X, centro.Y,centro.Z -h),
                new List<Vertice>
                {
            new Vertice(-h, -h, 0),
            new Vertice( h, -h, 0),
            new Vertice( h,  h, 0),
            new Vertice(-h,  h, 0)
                },
                color
            ));

            // Arriba (y = +h)
            lados.Add(new Lado(
                new Vertice(centro.X, centro.Y+h,centro.Z),
                new List<Vertice>
                {
            new Vertice(-h, 0, -h),
            new Vertice( h, 0, -h),
            new Vertice( h, 0,  h),
            new Vertice(-h, 0,  h)
                },
                 color
            ));

            // Abajo (y = -h)
            lados.Add(new Lado(
                new Vertice(centro.X, centro.Y-h, centro.Z),
                new List<Vertice>
                {
            new Vertice(-h, 0, -h),
            new Vertice( h, 0, -h),
            new Vertice( h, 0,  h),
            new Vertice(-h, 0,  h)
                },
               color
            ));

            // Derecha (x = +h)
            lados.Add(new Lado(
                new Vertice(centro.X+h, centro.Y, centro.Z),
                new List<Vertice>
                {
            new Vertice(0, -h, -h),
            new Vertice(0,  h, -h),
            new Vertice(0,  h,  h),
            new Vertice(0, -h,  h)
                },
                color
            ));

            // Izquierda (x = -h)
            lados.Add(new Lado(
                new Vertice(centro.X-h, centro.Y, centro.Z),
                new List<Vertice>
                {
            new Vertice(0, -h, -h),
            new Vertice(0,  h, -h),
            new Vertice(0,  h,  h),
            new Vertice(0, -h,  h)
                },
             color
            ));

            // Crear la parte con el centro en el origen
            return new Parte(centro, lados, color);
        }
        public static Parte CrearEsfera(Vertice centro, float radio = 1.0f, Vertice color = null)
        {
            int latitudes = 10; int longitudes = 10;
            var lados = new List<Lado>();
            color ??= new Vertice(200, 200, 200); // Gris por defecto

            for (int lat = 0; lat < latitudes; lat++)
            {
                float theta1 = (float)(Math.PI * lat / latitudes);
                float theta2 = (float)(Math.PI * (lat + 1) / latitudes);

                // Calcular factor de degradado en esta franja
                // de 1.3 (más claro) arriba hasta 0.7 (más oscuro) abajo
                float factor = 1.3f - (0.6f * (lat / (float)latitudes));

                // Calcular color degradado en base al color base
                var degradadoColor = new Vertice(
                    Math.Clamp((int)(color.X * factor), 0, 255),
                    Math.Clamp((int)(color.Y * factor), 0, 255),
                    Math.Clamp((int)(color.Z * factor), 0, 255)
                );

                for (int lon = 0; lon < longitudes; lon++)
                {
                    float phi1 = (float)(2 * Math.PI * lon / longitudes);
                    float phi2 = (float)(2 * Math.PI * (lon + 1) / longitudes);

                    // Calcular los 4 puntos de la esfera
                    var v1 = new Vertice(
                        centro.X + radio * (float)(Math.Sin(theta1) * Math.Cos(phi1)),
                        centro.Y + radio * (float)(Math.Cos(theta1)),
                        centro.Z + radio * (float)(Math.Sin(theta1) * Math.Sin(phi1))
                    );
                    var v2 = new Vertice(
                        centro.X + radio * (float)(Math.Sin(theta2) * Math.Cos(phi1)),
                        centro.Y + radio * (float)(Math.Cos(theta2)),
                        centro.Z + radio * (float)(Math.Sin(theta2) * Math.Sin(phi1))
                    );
                    var v3 = new Vertice(
                        centro.X + radio * (float)(Math.Sin(theta2) * Math.Cos(phi2)),
                        centro.Y + radio * (float)(Math.Cos(theta2)),
                        centro.Z + radio * (float)(Math.Sin(theta2) * Math.Sin(phi2))
                    );
                    var v4 = new Vertice(
                        centro.X + radio * (float)(Math.Sin(theta1) * Math.Cos(phi2)),
                        centro.Y + radio * (float)(Math.Cos(theta1)),
                        centro.Z + radio * (float)(Math.Sin(theta1) * Math.Sin(phi2))
                    );

                    // Centro aproximado del lado
                    var centroLado = new Vertice(
                        (v1.X + v2.X + v3.X + v4.X) / 4,
                        (v1.Y + v2.Y + v3.Y + v4.Y) / 4,
                        (v1.Z + v2.Z + v3.Z + v4.Z) / 4
                    );

                    // Vértices relativos al centro del lado
                    var verticesRel = new List<Vertice>
            {
                new Vertice(v1.X - centroLado.X, v1.Y - centroLado.Y, v1.Z - centroLado.Z),
                new Vertice(v2.X - centroLado.X, v2.Y - centroLado.Y, v2.Z - centroLado.Z),
                new Vertice(v3.X - centroLado.X, v3.Y - centroLado.Y, v3.Z - centroLado.Z),
                new Vertice(v4.X - centroLado.X, v4.Y - centroLado.Y, v4.Z - centroLado.Z),
            };

                    // Crear el lado con color degradado
                    lados.Add(new Lado(centroLado, verticesRel, degradadoColor));
                }
            }

            return new Parte(centro, lados, color);
        }
      
        // ----------------- Escritorio -----------------
        public static Parte CrearEscritorio(Vertice centro = null, Vertice color = null)
        {
            centro ??= new Vertice(0, 0, 0);
            color ??= new Vertice(139, 69, 19); // marrón
            float ancho = 5f;
            float alto = 0.3f;
            float profundidad = 3f;
            return CrearCubo(centro, ancho, profundidad, alto, color);
        }

        // ----------------- Pantalla -----------------
        public static Parte CrearPantalla(Vertice centro = null, Vertice color = null)
        {
            centro ??= new Vertice(0, 1.5f, -1f);
            color ??= new Vertice(0, 0, 255); // azul
            float ancho = 2f;
            float alto = 1.5f;
            float profundidad = 0.1f;
            return CrearCubo(centro, ancho, profundidad, alto, color);
        }

        // ----------------- CPU -----------------
        public static Parte CrearCPU(Vertice centro = null, Vertice color = null)
        {
            centro ??= new Vertice(-2f, 0.75f, -0.5f);
            color ??= new Vertice(128, 128, 128); // gris
            float ancho = 1f;
            float alto = 1.5f;
            float profundidad = 0.5f;
            return CrearCubo(centro, ancho, profundidad, alto, color);
        }

        // ----------------- Teclado -----------------
        public static Parte CrearTeclado(Vertice centro = null, Vertice color = null)
        {
            centro ??= new Vertice(0, 0.8f, 0.5f);
            color ??= new Vertice(50, 50, 50); // oscuro
            float ancho = 2f;
            float alto = 0.2f;
            float profundidad = 0.5f;
            return CrearCubo(centro, ancho, profundidad, alto, color);
        }

        // ----------------- Mouse -----------------
        public static Parte CrearMouse(Vertice centro = null, Vertice color = null)
        {
            centro ??= new Vertice(1.2f, 0.75f, 0.5f);
            color ??= new Vertice(255, 0, 0); // rojo
            float ancho = 0.5f;
            float alto = 0.2f;
            float profundidad = 0.8f;
            return CrearCubo(centro, ancho, profundidad, alto, color);
        }

        // ----------------- Función general para crear un cubo como Parte -----------------
        public static Parte CrearCubo(Vertice centro, float ancho, float profundidad, float alto, Vertice color)
        {
            float hx = ancho / 2;
            float hy = alto / 2;
            float hz = profundidad / 2;
            var lados = new List<Lado>();

            // Frente
            lados.Add(new Lado(new Vertice(centro.X, centro.Y, centro.Z + hz),
                new List<Vertice> { new Vertice(-hx, -hy, 0), new Vertice(hx, -hy, 0), new Vertice(hx, hy, 0), new Vertice(-hx, hy, 0) },
                color));

            // Atrás
            lados.Add(new Lado(new Vertice(centro.X, centro.Y, centro.Z - hz),
                new List<Vertice> { new Vertice(-hx, -hy, 0), new Vertice(hx, -hy, 0), new Vertice(hx, hy, 0), new Vertice(-hx, hy, 0) },
                new Vertice(color.X * 0.5f, color.Y * 0.5f, color.Z * 0.5f)));

            // Arriba
            lados.Add(new Lado(new Vertice(centro.X, centro.Y + hy, centro.Z),
                new List<Vertice> { new Vertice(-hx, 0, -hz), new Vertice(hx, 0, -hz), new Vertice(hx, 0, hz), new Vertice(-hx, 0, hz) },
                new Vertice(color.X * 0.8f, color.Y * 0.8f, color.Z * 0.8f)));

            // Abajo
            lados.Add(new Lado(new Vertice(centro.X, centro.Y - hy, centro.Z),
                new List<Vertice> { new Vertice(-hx, 0, -hz), new Vertice(hx, 0, -hz), new Vertice(hx, 0, hz), new Vertice(-hx, 0, hz) },
                new Vertice(color.X * 0.6f, color.Y * 0.6f, color.Z * 0.6f)));

            // Derecha
            lados.Add(new Lado(new Vertice(centro.X + hx, centro.Y, centro.Z),
                new List<Vertice> { new Vertice(0, -hy, -hz), new Vertice(0, hy, -hz), new Vertice(0, hy, hz), new Vertice(0, -hy, hz) },
                new Vertice(color.X * 0.7f, color.Y * 0.7f, color.Z * 0.7f)));

            // Izquierda
            lados.Add(new Lado(new Vertice(centro.X - hx, centro.Y, centro.Z),
                new List<Vertice> { new Vertice(0, -hy, -hz), new Vertice(0, hy, -hz), new Vertice(0, hy, hz), new Vertice(0, -hy, hz) },
                new Vertice(color.X * 0.7f, color.Y * 0.7f, color.Z * 0.7f)));

            return new Parte(centro, lados, color);
        }
    }
}
