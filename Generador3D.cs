using System;
using System.Collections.Generic;

namespace ProGrafica
{
    public static class Generador3D
    {
        // ==================== CUBO MEJORADO ====================
        public static Parte CrearCubo(Vertice centro, float tamaño = 1.0f, Vertice color = null)
        {
            color ??= new Vertice(255, 255, 255);
            float h = tamaño / 2.0f;
            var lados = new List<Lado>();

            // Definir los 8 vértices del cubo
            var vertices = new Vertice[8];
            vertices[0] = new Vertice(centro.X - h, centro.Y - h, centro.Z - h); // abajo-izq-atrás
            vertices[1] = new Vertice(centro.X + h, centro.Y - h, centro.Z - h); // abajo-der-atrás
            vertices[2] = new Vertice(centro.X + h, centro.Y + h, centro.Z - h); // arriba-der-atrás
            vertices[3] = new Vertice(centro.X - h, centro.Y + h, centro.Z - h); // arriba-izq-atrás
            vertices[4] = new Vertice(centro.X - h, centro.Y - h, centro.Z + h); // abajo-izq-frente
            vertices[5] = new Vertice(centro.X + h, centro.Y - h, centro.Z + h); // abajo-der-frente
            vertices[6] = new Vertice(centro.X + h, centro.Y + h, centro.Z + h); // arriba-der-frente
            vertices[7] = new Vertice(centro.X - h, centro.Y + h, centro.Z + h); // arriba-izq-frente

            // Crear los 6 lados del cubo (cada lado es un cuadrado con 4 vértices)

            // Frente
            lados.Add(CrearLadoDesdeVertices(new Vertice[] { vertices[4], vertices[5], vertices[6], vertices[7] }, color));

            // Atrás
            lados.Add(CrearLadoDesdeVertices(new Vertice[] { vertices[1], vertices[0], vertices[3], vertices[2] }, color));

            // Derecha
            lados.Add(CrearLadoDesdeVertices(new Vertice[] { vertices[5], vertices[1], vertices[2], vertices[6] }, color));

            // Izquierda
            lados.Add(CrearLadoDesdeVertices(new Vertice[] { vertices[0], vertices[4], vertices[7], vertices[3] }, color));

            // Arriba
            lados.Add(CrearLadoDesdeVertices(new Vertice[] { vertices[3], vertices[7], vertices[6], vertices[2] }, color));

            // Abajo
            lados.Add(CrearLadoDesdeVertices(new Vertice[] { vertices[0], vertices[1], vertices[5], vertices[4] }, color));

            return new Parte(centro, lados, color);
        }

        // Método auxiliar para crear un lado a partir de vértices
        private static Lado CrearLadoDesdeVertices(Vertice[] vertices, Vertice color)
        {
            // Calcular el centro del lado
            float cx = (vertices[0].X + vertices[1].X + vertices[2].X + vertices[3].X) / 4;
            float cy = (vertices[0].Y + vertices[1].Y + vertices[2].Y + vertices[3].Y) / 4;
            float cz = (vertices[0].Z + vertices[1].Z + vertices[2].Z + vertices[3].Z) / 4;
            Vertice centroLado = new Vertice(cx, cy, cz);

            // Calcular vértices relativos al centro
            var verticesRel = new List<Vertice>();
            foreach (var v in vertices)
            {
                verticesRel.Add(new Vertice(v.X - centroLado.X, v.Y - centroLado.Y, v.Z - centroLado.Z));
            }

            return new Lado(centroLado, verticesRel, color);
        }

        // ==================== PRISMA RECTANGULAR MEJORADO ====================
        public static Parte CrearPrisma(Vertice centro, float ancho, float alto, float profundo, Vertice color = null)
        {
            color ??= new Vertice(200, 200, 200);

            float hx = ancho / 2.0f;
            float hy = alto / 2.0f;
            float hz = profundo / 2.0f;

            var lados = new List<Lado>();

            // Definir los 8 vértices del prisma
            var vertices = new Vertice[8];
            vertices[0] = new Vertice(centro.X - hx, centro.Y - hy, centro.Z - hz);
            vertices[1] = new Vertice(centro.X + hx, centro.Y - hy, centro.Z - hz);
            vertices[2] = new Vertice(centro.X + hx, centro.Y + hy, centro.Z - hz);
            vertices[3] = new Vertice(centro.X - hx, centro.Y + hy, centro.Z - hz);
            vertices[4] = new Vertice(centro.X - hx, centro.Y - hy, centro.Z + hz);
            vertices[5] = new Vertice(centro.X + hx, centro.Y - hy, centro.Z + hz);
            vertices[6] = new Vertice(centro.X + hx, centro.Y + hy, centro.Z + hz);
            vertices[7] = new Vertice(centro.X - hx, centro.Y + hy, centro.Z + hz);

            // Crear los 6 lados del prisma

            // Frente
            lados.Add(CrearLadoDesdeVertices(new Vertice[] { vertices[4], vertices[5], vertices[6], vertices[7] }, color));

            // Atrás
            lados.Add(CrearLadoDesdeVertices(new Vertice[] { vertices[1], vertices[0], vertices[3], vertices[2] }, color));

            // Derecha
            lados.Add(CrearLadoDesdeVertices(new Vertice[] { vertices[5], vertices[1], vertices[2], vertices[6] }, color));

            // Izquierda
            lados.Add(CrearLadoDesdeVertices(new Vertice[] { vertices[0], vertices[4], vertices[7], vertices[3] }, color));

            // Arriba
            lados.Add(CrearLadoDesdeVertices(new Vertice[] { vertices[3], vertices[7], vertices[6], vertices[2] }, color));

            // Abajo
            lados.Add(CrearLadoDesdeVertices(new Vertice[] { vertices[0], vertices[1], vertices[5], vertices[4] }, color));

            return new Parte(centro, lados, color);
        }

        // ==================== ESFERA MEJORADA ====================
        public static Parte CrearEsfera(Vertice centro, float radio = 1.0f, int segmentos = 16, Vertice color = null)
        {
            color ??= new Vertice(200, 200, 200);
            var lados = new List<Lado>();

            // Crear esfera con mejor distribución de segmentos
            for (int i = 0; i < segmentos; i++)
            {
                float theta1 = (float)(Math.PI * i / segmentos);
                float theta2 = (float)(Math.PI * (i + 1) / segmentos);

                for (int j = 0; j < segmentos; j++)
                {
                    float phi1 = (float)(2 * Math.PI * j / segmentos);
                    float phi2 = (float)(2 * Math.PI * (j + 1) / segmentos);

                    // Crear los 4 vértices del segmento
                    var v1 = EsfericoACartesiano(radio, theta1, phi1, centro);
                    var v2 = EsfericoACartesiano(radio, theta2, phi1, centro);
                    var v3 = EsfericoACartesiano(radio, theta2, phi2, centro);
                    var v4 = EsfericoACartesiano(radio, theta1, phi2, centro);

                    // Crear el lado (cuadrilátero)
                    var lado = CrearLadoDesdeVertices(new Vertice[] { v1, v2, v3, v4 }, color);
                    lados.Add(lado);
                }
            }

            return new Parte(centro, lados, color);
        }

        private static Vertice EsfericoACartesiano(float radio, float theta, float phi, Vertice centro)
        {
            float x = centro.X + radio * (float)(Math.Sin(theta) * Math.Cos(phi));
            float y = centro.Y + radio * (float)(Math.Cos(theta));
            float z = centro.Z + radio * (float)(Math.Sin(theta) * Math.Sin(phi));
            return new Vertice(x, y, z);
        }

        // ==================== CILINDRO MEJORADO ====================
        public static Parte CrearCilindro(Vertice centro, float radio, float altura, int segmentos = 20, Vertice color = null)
        {
            color ??= new Vertice(150, 150, 255);
            var lados = new List<Lado>();
            float mitadAltura = altura / 2.0f;

            // Crear la superficie lateral
            for (int i = 0; i < segmentos; i++)
            {
                float ang1 = (float)(2 * Math.PI * i / segmentos);
                float ang2 = (float)(2 * Math.PI * (i + 1) / segmentos);

                // Vértices para el lado lateral
                var v1 = new Vertice(
                    centro.X + radio * (float)Math.Cos(ang1),
                    centro.Y - mitadAltura,
                    centro.Z + radio * (float)Math.Sin(ang1)
                );
                var v2 = new Vertice(
                    centro.X + radio * (float)Math.Cos(ang2),
                    centro.Y - mitadAltura,
                    centro.Z + radio * (float)Math.Sin(ang2)
                );
                var v3 = new Vertice(
                    centro.X + radio * (float)Math.Cos(ang2),
                    centro.Y + mitadAltura,
                    centro.Z + radio * (float)Math.Sin(ang2)
                );
                var v4 = new Vertice(
                    centro.X + radio * (float)Math.Cos(ang1),
                    centro.Y + mitadAltura,
                    centro.Z + radio * (float)Math.Sin(ang1)
                );

                // Crear el lado lateral
                var ladoLateral = CrearLadoDesdeVertices(new Vertice[] { v1, v2, v3, v4 }, color);
                lados.Add(ladoLateral);
            }

            // Crear la tapa superior
            var verticesTapaSuperior = new List<Vertice>();
            for (int i = 0; i < segmentos; i++)
            {
                float ang = (float)(2 * Math.PI * i / segmentos);
                verticesTapaSuperior.Add(new Vertice(
                    centro.X + radio * (float)Math.Cos(ang),
                    centro.Y + mitadAltura,
                    centro.Z + radio * (float)Math.Sin(ang)
                ));
            }
            lados.Add(CrearLadoPoligonal(new Vertice(centro.X, centro.Y + mitadAltura, centro.Z), verticesTapaSuperior, color));

            // Crear la tapa inferior
            var verticesTapaInferior = new List<Vertice>();
            for (int i = 0; i < segmentos; i++)
            {
                float ang = (float)(2 * Math.PI * i / segmentos);
                verticesTapaInferior.Add(new Vertice(
                    centro.X + radio * (float)Math.Cos(ang),
                    centro.Y - mitadAltura,
                    centro.Z + radio * (float)Math.Sin(ang)
                ));
            }
            // Invertir el orden para que la normal apunte hacia abajo
            verticesTapaInferior.Reverse();
            lados.Add(CrearLadoPoligonal(new Vertice(centro.X, centro.Y - mitadAltura, centro.Z), verticesTapaInferior, color));

            return new Parte(centro, lados, color);
        }

        private static Lado CrearLadoPoligonal(Vertice centro, List<Vertice> vertices, Vertice color)
        {
            var verticesRel = new List<Vertice>();
            foreach (var v in vertices)
            {
                verticesRel.Add(new Vertice(v.X - centro.X, v.Y - centro.Y, v.Z - centro.Z));
            }
            return new Lado(centro, verticesRel, color);
        }

        // ==================== TECLADO MEJORADO ====================
        public static Objeto3D CrearTeclado(Vertice centro, int filas = 4, int columnas = 12,
            float anchoTecla = 0.8f, float altoTecla = 0.2f, float profundoTecla = 0.8f,
            Vertice colorCuerpo = null, Vertice colorTecla = null)
        {
            colorCuerpo ??= new Vertice(50, 50, 50);
            colorTecla ??= new Vertice(220, 220, 220);

            var teclado = new Objeto3D("teclado", centro, null, null);

            // Calcular dimensiones totales
            float espacioEntreTeclas = 0.05f;
            float anchoTotal = columnas * (anchoTecla + espacioEntreTeclas) - espacioEntreTeclas;
            float profundoTotal = filas * (profundoTecla + espacioEntreTeclas) - espacioEntreTeclas;

            // Crear base del teclado (ligeramente curvada)
            var baseCentro = new Vertice(centro.X, centro.Y - 0.1f, centro.Z);
            var baseTeclado = CrearPrisma(baseCentro, anchoTotal + 0.5f, 0.2f, profundoTotal + 0.3f, colorCuerpo);
            teclado.AgregarParte("base", baseTeclado);

            // Crear teclas
            for (int f = 0; f < filas; f++)
            {
                for (int c = 0; c < columnas; c++)
                {
                    // Calcular posición de la tecla
                    float posX = centro.X - (anchoTotal / 2) + (c * (anchoTecla + espacioEntreTeclas)) + (anchoTecla / 2);
                    float posY = centro.Y + (altoTecla / 2);
                    float posZ = centro.Z - (profundoTotal / 2) + (f * (profundoTecla + espacioEntreTeclas)) + (profundoTecla / 2);

                    // Añadir pequeña curvatura a las filas
                    posZ += (float)Math.Sin(f * 0.2f) * 0.05f;

                    var tecla = CrearPrisma(
                        new Vertice(posX, posY, posZ),
                        anchoTecla, altoTecla, profundoTecla,
                        colorTecla
                    );

                    teclado.AgregarParte($"tecla_{f}_{c}", tecla);
                }
            }

            // Crear barra espaciadora (más ancha)
            var barraCentro = new Vertice(centro.X, centro.Y + (altoTecla / 2), centro.Z + (profundoTotal / 2) + (profundoTecla / 2));
            var barraEspaciadora = CrearPrisma(
                barraCentro,
                anchoTecla * 5,
                altoTecla,
                profundoTecla,
                colorTecla
            );
            teclado.AgregarParte("barra_espaciadora", barraEspaciadora);

            return teclado;
        }

        // ==================== MOUSE MEJORADO ====================
        public static Objeto3D CrearMouse(Vertice centro, float ancho = 1.8f, float alto = 0.8f, float profundo = 3.0f, Vertice color = null)
        {
            color ??= new Vertice(80, 80, 80);
            var mouse = new Objeto3D("mouse", centro, null, color);

            // Cuerpo principal del mouse (forma ovalada)
            var cuerpo = CrearPrisma(centro, ancho, alto, profundo, color);
            mouse.AgregarParte("cuerpo", cuerpo);

            // Botones
            var colorBotones = new Vertice(70, 70, 70);
            var botonIzq = CrearPrisma(
                new Vertice(centro.X - ancho / 4, centro.Y + alto / 2 + 0.05f, centro.Z - profundo / 4),
                ancho / 2, 0.1f, profundo / 2,
                colorBotones
            );
            mouse.AgregarParte("boton_izquierdo", botonIzq);

            var botonDer = CrearPrisma(
                new Vertice(centro.X + ancho / 4, centro.Y + alto / 2 + 0.05f, centro.Z - profundo / 4),
                ancho / 2, 0.1f, profundo / 2,
                colorBotones
            );
            mouse.AgregarParte("boton_derecho", botonDer);

            // Rueda de scroll
            var rueda = CrearCilindro(
                new Vertice(centro.X, centro.Y + alto / 2 + 0.08f, centro.Z),
                0.1f, 0.6f, 12, new Vertice(120, 120, 120)
            );
            mouse.AgregarParte("rueda_scroll", rueda);

            return mouse;
        }

        // ==================== MESA CORREGIDA ====================
        public static Objeto3D CrearMesa(Vertice centro, float ancho = 20f, float alto = 10.0f, float profundo = 10f, Vertice color = null)
        {
            color ??= new Vertice(139, 69, 19); // marrón
            var mesa = new Objeto3D("mesa", centro, null, color);

            // Tablero de la mesa (más grueso)
            float grosorTablero = 0.3f;
            var superficie = CrearPrisma(
                new Vertice(centro.X, centro.Y + (alto / 2) - (grosorTablero / 2), centro.Z),
                ancho, grosorTablero, profundo,
                new Vertice(160, 82, 45) // color madera más claro
            );
            mesa.AgregarParte("superficie", superficie);

            // Patas - CORREGIDAS: ahora están conectadas a la superficie
            float alturaPata = alto - grosorTablero; // Altura desde el suelo hasta la superficie
            float separacionX = (ancho / 2) - 0.5f;
            float separacionZ = (profundo / 2) - 0.5f;
            var colorPata = new Vertice(101, 67, 33); // marrón oscuro

            // Calcular la posición Y correcta para las patas
            // La base de la pata está en centro.Y - alto/2
            // El centro de la pata está a mitad de su altura
            float centroYPata = centro.Y - (alto / 2) + (alturaPata / 2);

            // Pata frontal izquierda
            mesa.AgregarParte("pata_frente_izq",
                CrearPrisma(
                    new Vertice(centro.X - separacionX, centroYPata, centro.Z - separacionZ),
                    0.5f, alturaPata, 0.5f, colorPata));

            // Pata frontal derecha
            mesa.AgregarParte("pata_frente_der",
                CrearPrisma(
                    new Vertice(centro.X + separacionX, centroYPata, centro.Z - separacionZ),
                    0.5f, alturaPata, 0.5f, colorPata));

            // Pata trasera izquierda
            mesa.AgregarParte("pata_tras_izq",
                CrearPrisma(
                    new Vertice(centro.X - separacionX, centroYPata, centro.Z + separacionZ),
                    0.5f, alturaPata, 0.5f, colorPata));

            // Pata trasera derecha
            mesa.AgregarParte("pata_tras_der",
                CrearPrisma(
                    new Vertice(centro.X + separacionX, centroYPata, centro.Z + separacionZ),
                    0.5f, alturaPata, 0.5f, colorPata));

            return mesa;
        }
        // ==================== TORRE PC MEJORADA ====================
        public static Objeto3D CrearPC(Vertice centro, float ancho = 2.5f, float alto = 8f, float profundo = 5f, Vertice color = null)
        {
            color ??= new Vertice(40, 40, 40);
            var pc = new Objeto3D("pc", centro, null, color);

            // Caja principal
            var torre = CrearPrisma(centro, ancho, alto, profundo, color);
            pc.AgregarParte("torre", torre);

            // Panel frontal con detalles
            var panelFrontal = CrearPrisma(
                new Vertice(centro.X - ancho / 2 + 0.1f, centro.Y, centro.Z),
                0.1f, alto - 0.5f, profundo - 0.5f,
                new Vertice(30, 30, 30)
            );
            pc.AgregarParte("panel_frontal", panelFrontal);

            // Botón de encendido
            var botonEncendido = CrearCilindro(
                new Vertice(centro.X - ancho / 2 + 0.15f, centro.Y + alto / 2 - 1.0f, centro.Z),
                0.2f, 0.1f, 12, new Vertice(255, 0, 0)
            );
            pc.AgregarParte("boton_encendido", botonEncendido);

            // Ranuras USB
            for (int i = 0; i < 3; i++)
            {
                var usb = CrearPrisma(
                    new Vertice(centro.X - ancho / 2 + 0.15f, centro.Y + alto / 2 - 2.5f - i * 0.6f, centro.Z),
                    0.1f, 0.3f, 0.8f, new Vertice(20, 20, 20)
                );
                pc.AgregarParte($"usb_{i}", usb);
            }

            return pc;
        }

        // ==================== PANTALLA MEJORADA ====================
        public static Objeto3D CrearPantalla(Vertice centro, float ancho = 12f, float alto = 7f, float grosor = 0.5f, Vertice color = null)
        {
            color ??= new Vertice(20, 20, 20);
            var pantalla = new Objeto3D("pantalla", centro, null, color);

            // Marco de la pantalla
            var marco = CrearPrisma(centro, ancho + 0.3f, alto + 0.3f, grosor, color);
            pantalla.AgregarParte("marco", marco);

            // Pantalla (área de visualización)
            var panel = CrearPrisma(
                new Vertice(centro.X, centro.Y, centro.Z + grosor / 2 + 0.01f),
                ancho, alto, 0.02f,
                new Vertice(10, 10, 30) // azul oscuro para simular pantalla apagada
            );
            pantalla.AgregarParte("panel", panel);

            // Base de la pantalla
            var baseCentro = new Vertice(centro.X, centro.Y - alto / 2 - 1.0f, centro.Z);
            var basePantalla = CrearPrisma(
                baseCentro,
                ancho / 3, 1.0f, grosor * 3,
                color
            );
            pantalla.AgregarParte("base", basePantalla);

            // Soporte entre pantalla y base
            var soporte = CrearPrisma(
                new Vertice(centro.X, centro.Y - alto / 2 - 0.5f, centro.Z),
                ancho / 6, 1.0f, grosor,
                color
            );
            pantalla.AgregarParte("soporte", soporte);

            return pantalla;
        }

        // ==================== SILLA CORREGIDA ====================
        public static Objeto3D CrearSilla(Vertice centro, float altura = 5f, Vertice color = null)
        {
            color ??= new Vertice(50, 50, 150);
            var silla = new Objeto3D("silla", centro, null, color);

            // Asiento (más grueso)
            float grosorAsiento = 0.4f;
            var asiento = CrearPrisma(
                new Vertice(centro.X, centro.Y + altura / 2 - grosorAsiento / 2, centro.Z),
                6f, grosorAsiento, 6f,
                new Vertice(100, 100, 200)
            );
            silla.AgregarParte("asiento", asiento);

            // Respaldo
            float grosorRespaldo = 0.3f;
            var respaldo = CrearPrisma(
                new Vertice(centro.X, centro.Y + altura / 2 + 1.0f, centro.Z + 2.8f),
                6f, 2.5f, grosorRespaldo,
                new Vertice(100, 100, 200)
            );
            silla.AgregarParte("respaldo", respaldo);

            // Patas - CORREGIDAS: ahora están conectadas al asiento
            float alturaPata = altura - grosorAsiento;
            float separacion = 2.0f;
            var pataColor = new Vertice(40, 40, 40);

            // Centro Y de las patas
            float centroYPata = centro.Y - (altura / 2) + (alturaPata / 2);

            silla.AgregarParte("pata_frente_izq",
                CrearPrisma(
                    new Vertice(centro.X - separacion, centroYPata, centro.Z - separacion),
                    0.5f, alturaPata, 0.5f, pataColor));

            silla.AgregarParte("pata_frente_der",
                CrearPrisma(
                    new Vertice(centro.X + separacion, centroYPata, centro.Z - separacion),
                    0.5f, alturaPata, 0.5f, pataColor));

            silla.AgregarParte("pata_tras_izq",
                CrearPrisma(
                    new Vertice(centro.X - separacion, centroYPata, centro.Z + separacion),
                    0.5f, alturaPata, 0.5f, pataColor));

            silla.AgregarParte("pata_tras_der",
                CrearPrisma(
                    new Vertice(centro.X + separacion, centroYPata, centro.Z + separacion),
                    0.5f, alturaPata, 0.5f, pataColor));

            return silla;
        }
    }
}