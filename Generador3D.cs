using System;
using System.Collections.Generic;

namespace ProGrafica
{
    public static class Generador3D
    {

        public static Parte CrearCubo(Vertice centro, float tamaño = 1.0f, Vertice color = null)
        {
            color ??= new Vertice(255, 255, 255);
            float h = tamaño / 2.0f;
            var lados = new List<Lado>();

            var verticesLocales = new Vertice[8];
            verticesLocales[0] = new Vertice(-h, -h, -h);
            verticesLocales[1] = new Vertice(h, -h, -h);
            verticesLocales[2] = new Vertice(h, h, -h);
            verticesLocales[3] = new Vertice(-h, h, -h);
            verticesLocales[4] = new Vertice(-h, -h, h);
            verticesLocales[5] = new Vertice(h, -h, h);
            verticesLocales[6] = new Vertice(h, h, h);
            verticesLocales[7] = new Vertice(-h, h, h);

            var centroLocal = new Vertice(0, 0, 0);

            lados.Add(CrearLadoDesdeVerticesLocales(new Vertice[] { verticesLocales[4], verticesLocales[5], verticesLocales[6], verticesLocales[7] }, centroLocal, color));
            lados.Add(CrearLadoDesdeVerticesLocales(new Vertice[] { verticesLocales[1], verticesLocales[0], verticesLocales[3], verticesLocales[2] }, centroLocal, color));
            lados.Add(CrearLadoDesdeVerticesLocales(new Vertice[] { verticesLocales[5], verticesLocales[1], verticesLocales[2], verticesLocales[6] }, centroLocal, color));
            lados.Add(CrearLadoDesdeVerticesLocales(new Vertice[] { verticesLocales[0], verticesLocales[4], verticesLocales[7], verticesLocales[3] }, centroLocal, color));
            lados.Add(CrearLadoDesdeVerticesLocales(new Vertice[] { verticesLocales[3], verticesLocales[7], verticesLocales[6], verticesLocales[2] }, centroLocal, color));
            lados.Add(CrearLadoDesdeVerticesLocales(new Vertice[] { verticesLocales[0], verticesLocales[1], verticesLocales[5], verticesLocales[4] }, centroLocal, color));

            return new Parte(centro, lados, color);
        }
        private static Lado CrearLadoDesdeVerticesLocales(Vertice[] verticesLocales, Vertice centroLado, Vertice color)
        {
            return new Lado(centroLado, new List<Vertice>(verticesLocales), color);
        }





        public static Parte CrearPrisma(Vertice centro, float ancho, float alto, float profundo, Vertice color = null)
        {
            color ??= new Vertice(200, 200, 200);

            float hx = ancho / 2.0f;
            float hy = alto / 2.0f;
            float hz = profundo / 2.0f;

            var lados = new List<Lado>();
            var centroLocal = new Vertice(0, 0, 0);

            var verticesLocales = new Vertice[8];
            verticesLocales[0] = new Vertice(-hx, -hy, -hz);
            verticesLocales[1] = new Vertice(hx, -hy, -hz);
            verticesLocales[2] = new Vertice(hx, hy, -hz);
            verticesLocales[3] = new Vertice(-hx, hy, -hz);
            verticesLocales[4] = new Vertice(-hx, -hy, hz);
            verticesLocales[5] = new Vertice(hx, -hy, hz);
            verticesLocales[6] = new Vertice(hx, hy, hz);
            verticesLocales[7] = new Vertice(-hx, hy, hz);

            lados.Add(CrearLadoDesdeVerticesLocales(new Vertice[] { verticesLocales[4], verticesLocales[5], verticesLocales[6], verticesLocales[7] }, centroLocal, color));
            lados.Add(CrearLadoDesdeVerticesLocales(new Vertice[] { verticesLocales[1], verticesLocales[0], verticesLocales[3], verticesLocales[2] }, centroLocal, color));
            lados.Add(CrearLadoDesdeVerticesLocales(new Vertice[] { verticesLocales[5], verticesLocales[1], verticesLocales[2], verticesLocales[6] }, centroLocal, color));
            lados.Add(CrearLadoDesdeVerticesLocales(new Vertice[] { verticesLocales[0], verticesLocales[4], verticesLocales[7], verticesLocales[3] }, centroLocal, color));
            lados.Add(CrearLadoDesdeVerticesLocales(new Vertice[] { verticesLocales[3], verticesLocales[7], verticesLocales[6], verticesLocales[2] }, centroLocal, color));
            lados.Add(CrearLadoDesdeVerticesLocales(new Vertice[] { verticesLocales[0], verticesLocales[1], verticesLocales[5], verticesLocales[4] }, centroLocal, color));

            return new Parte(centro, lados, color);
        }




        public static Parte CrearEsfera(Vertice centro, float radio = 1.0f, int segmentos = 16, Vertice color = null)
        {
            color ??= new Vertice(200, 200, 200);
            var lados = new List<Lado>();

            var centroLocal = new Vertice(0, 0, 0);

            var vertices = new Vertice[segmentos + 1, segmentos + 1];

            for (int i = 0; i <= segmentos; i++)
            {
                float theta = (float)(Math.PI * i / segmentos);

                for (int j = 0; j <= segmentos; j++)
                {
                    float phi = (float)(2 * Math.PI * j / segmentos);

                    float x = radio * (float)(Math.Sin(theta) * Math.Cos(phi));
                    float y = radio * (float)(Math.Cos(theta));
                    float z = radio * (float)(Math.Sin(theta) * Math.Sin(phi));

                    vertices[i, j] = new Vertice(x, y, z);
                }
            }

            for (int i = 0; i < segmentos; i++)
            {
                for (int j = 0; j < segmentos; j++)
                {
                    var v1 = vertices[i, j];
                    var v2 = vertices[i + 1, j];
                    var v3 = vertices[i + 1, j + 1];
                    var v4 = vertices[i, j + 1];

                    var lado = CrearLadoDesdeVerticesLocales(new Vertice[] { v1, v2, v3, v4 }, centroLocal, color);
                    lados.Add(lado);
                }
            }

            return new Parte(centro, lados, color);
        }
        private static Vertice EsfericoACartesianoLocal(float radio, float theta, float phi)
        {
            float x = radio * (float)(Math.Sin(theta) * Math.Cos(phi));
            float y = radio * (float)(Math.Cos(theta));
            float z = radio * (float)(Math.Sin(theta) * Math.Sin(phi));
            return new Vertice(x, y, z);
        }

        private static Vertice EsfericoACartesiano(float radio, float theta, float phi, Vertice centro)
        {
            float x = centro.X + radio * (float)(Math.Sin(theta) * Math.Cos(phi));
            float y = centro.Y + radio * (float)(Math.Cos(theta));
            float z = centro.Z + radio * (float)(Math.Sin(theta) * Math.Sin(phi));
            return new Vertice(x, y, z);
        }

        public static Parte CrearCilindro(Vertice centro, float radio, float altura, int segmentos = 20, Vertice color = null)
        {
            color ??= new Vertice(150, 150, 255);
            var lados = new List<Lado>();
            float mitadAltura = altura / 2.0f;

            var centroLocal = new Vertice(0, 0, 0);

            var angulos = new float[segmentos];
            var verticesBaseSuperior = new Vertice[segmentos];
            var verticesBaseInferior = new Vertice[segmentos];

            for (int i = 0; i < segmentos; i++)
            {
                float angulo = (float)(2 * Math.PI * i / segmentos);
                angulos[i] = angulo;

                float cos = radio * (float)Math.Cos(angulo);
                float sin = radio * (float)Math.Sin(angulo);

                verticesBaseSuperior[i] = new Vertice(cos, mitadAltura, sin);
                verticesBaseInferior[i] = new Vertice(cos, -mitadAltura, sin);
            }

            for (int i = 0; i < segmentos; i++)
            {
                int next = (i + 1) % segmentos;

                var v1 = verticesBaseInferior[i];    
                var v2 = verticesBaseInferior[next];
                var v3 = verticesBaseSuperior[next];
                var v4 = verticesBaseSuperior[i]; 

                var lado = CrearLadoDesdeVerticesLocales(new Vertice[] { v1, v2, v3, v4 }, centroLocal, color);
                lados.Add(lado);
            }

            lados.Add(CrearLadoPoligonalLocal(centroLocal, new List<Vertice>(verticesBaseSuperior), color));

            lados.Add(CrearLadoPoligonalLocal(centroLocal, new List<Vertice>(verticesBaseInferior), color));

            return new Parte(centro, lados, color);
        }

        private static Lado CrearLadoPoligonalLocal(Vertice centro, List<Vertice> verticesLocales, Vertice color)
        {
            return new Lado(centro, verticesLocales, color);
        }

        public static Objeto CrearMesa(Vertice centro, float ancho = 20f, float alto = 10.0f, float profundo = 10f, Vertice color = null)
        {
            color ??= new Vertice(139, 69, 19);
            var mesa = new Objeto("mesa", centro, new Dictionary<string, Parte>(), color);

            float grosorTablero = 0.3f;
            var centroTablero = new Vertice(centro.X, centro.Y + (alto / 2) - (grosorTablero / 2), centro.Z);
            var superficie = CrearPrisma(centroTablero, ancho, grosorTablero, profundo, new Vertice(160, 82, 45));
            mesa.AgregarParte("superficie", superficie);

            float alturaPata = alto - grosorTablero;
            float separacionX = (ancho / 2) - 0.5f;
            float separacionZ = (profundo / 2) - 0.5f;
            var colorPata = new Vertice(101, 67, 33);

            var centroPataFrenteIzq = new Vertice(centro.X - separacionX, centro.Y - (alto / 2) + (alturaPata / 2), centro.Z - separacionZ);
            var centroPataFrenteDer = new Vertice(centro.X + separacionX, centro.Y - (alto / 2) + (alturaPata / 2), centro.Z - separacionZ);
            var centroPataTrasIzq = new Vertice(centro.X - separacionX, centro.Y - (alto / 2) + (alturaPata / 2), centro.Z + separacionZ);
            var centroPataTrasDer = new Vertice(centro.X + separacionX, centro.Y - (alto / 2) + (alturaPata / 2), centro.Z + separacionZ);

            mesa.AgregarParte("pata_frente_izq", CrearPrisma(centroPataFrenteIzq, 0.5f, alturaPata, 0.5f, colorPata));
            mesa.AgregarParte("pata_frente_der", CrearPrisma(centroPataFrenteDer, 0.5f, alturaPata, 0.5f, colorPata));
            mesa.AgregarParte("pata_tras_izq", CrearPrisma(centroPataTrasIzq, 0.5f, alturaPata, 0.5f, colorPata));
            mesa.AgregarParte("pata_tras_der", CrearPrisma(centroPataTrasDer, 0.5f, alturaPata, 0.5f, colorPata));

            return mesa;
        }
        public static Objeto CrearTeclado(Vertice centro, int filas = 4, int columnas = 12,
            float anchoTecla = 0.8f, float altoTecla = 0.2f, float profundoTecla = 0.8f,
            Vertice colorCuerpo = null, Vertice colorTecla = null)
        {
            colorCuerpo ??= new Vertice(50, 50, 50);
            colorTecla ??= new Vertice(220, 220, 220);

            var teclado = new Objeto("teclado", centro, new Dictionary<string, Parte>(), colorCuerpo);

            float espacioEntreTeclas = 0.05f;
            float anchoTotal = columnas * (anchoTecla + espacioEntreTeclas) - espacioEntreTeclas;
            float profundoTotal = filas * (profundoTecla + espacioEntreTeclas) - espacioEntreTeclas;

            var baseCentro = new Vertice(centro.X, centro.Y - 0.1f, centro.Z);
            var baseTeclado = CrearPrisma(baseCentro, anchoTotal + 0.5f, 0.2f, profundoTotal + 0.3f, colorCuerpo);
            teclado.AgregarParte("base", baseTeclado);

            for (int f = 0; f < filas; f++)
            {
                for (int c = 0; c < columnas; c++)
                {

                    float posX = centro.X - (anchoTotal / 2) + (c * (anchoTecla + espacioEntreTeclas)) + (anchoTecla / 2);
                    float posY = centro.Y + (altoTecla / 2);
                    float posZ = centro.Z - (profundoTotal / 2) + (f * (profundoTecla + espacioEntreTeclas)) + (profundoTecla / 2);

                    posZ += (float)Math.Sin(f * 0.2f) * 0.05f;

                    var teclaCentro = new Vertice(posX, posY, posZ);
                    var tecla = CrearPrisma(teclaCentro, anchoTecla, altoTecla, profundoTecla, colorTecla);
                    teclado.AgregarParte($"tecla_{f}_{c}", tecla);
                }
            }

            var barraCentro = new Vertice(centro.X, centro.Y + (altoTecla / 2), centro.Z + (profundoTotal / 2) + (profundoTecla / 2));
            var barraEspaciadora = CrearPrisma(barraCentro, anchoTecla * 5, altoTecla, profundoTecla, colorTecla);
            teclado.AgregarParte("barra_espaciadora", barraEspaciadora);

            return teclado;
        }
        public static Objeto CrearMouse(Vertice centro, float ancho = 1.8f, float alto = 0.8f, float profundo = 3.0f, Vertice color = null)
        {
            color ??= new Vertice(80, 80, 80);
            var mouse = new Objeto("mouse", centro, null, color);

            var cuerpo = CrearPrisma(centro, ancho, alto, profundo, color);
            mouse.AgregarParte("cuerpo", cuerpo);

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

            var rueda = CrearCilindro(
                new Vertice(centro.X, centro.Y + alto / 2 + 0.08f, centro.Z),
                0.1f, 0.6f, 12, new Vertice(120, 120, 120)
            );
            mouse.AgregarParte("rueda_scroll", rueda);

            return mouse;
        }

        public static Objeto CrearPC(Vertice centro, float ancho = 2.5f, float alto = 8f, float profundo = 5f, Vertice color = null)
        {
            color ??= new Vertice(40, 40, 40);
            var pc = new Objeto("pc", centro, new Dictionary<string, Parte>(), color);

            var torre = CrearPrisma(centro, ancho, alto, profundo, color);
            pc.AgregarParte("torre", torre);

            var panelFrontalCentro = new Vertice(centro.X - ancho / 2 + 0.1f, centro.Y, centro.Z);
            var panelFrontal = CrearPrisma(panelFrontalCentro, 0.1f, alto - 0.5f, profundo - 0.5f, new Vertice(30, 30, 30));
            pc.AgregarParte("panel_frontal", panelFrontal);

            var botonEncendidoCentro = new Vertice(centro.X - ancho / 2 + 0.15f, centro.Y + alto / 2 - 1.0f, centro.Z);
            var botonEncendido = CrearCilindro(botonEncendidoCentro, 0.2f, 0.1f, 12, new Vertice(255, 0, 0));
            pc.AgregarParte("boton_encendido", botonEncendido);

            for (int i = 0; i < 3; i++)
            {
                var usbCentro = new Vertice(centro.X - ancho / 2 + 0.15f, centro.Y + alto / 2 - 2.5f - i * 0.6f, centro.Z);
                var usb = CrearPrisma(usbCentro, 0.1f, 0.3f, 0.8f, new Vertice(20, 20, 20));
                pc.AgregarParte($"usb_{i}", usb);
            }

            return pc;
        }

        public static Objeto CrearPantalla(Vertice centro, float ancho = 12f, float alto = 7f, float grosor = 0.5f, Vertice color = null)
        {
            color ??= new Vertice(20, 20, 20);
            var pantalla = new Objeto("pantalla", centro, new Dictionary<string, Parte>(), color);

            var marco = CrearPrisma(centro, ancho + 0.3f, alto + 0.3f, grosor, color);
            pantalla.AgregarParte("marco", marco);

            var panelCentro = new Vertice(centro.X, centro.Y, centro.Z + grosor / 2 + 0.01f);
            var panel = CrearPrisma(panelCentro, ancho, alto, 0.02f, new Vertice(10, 10, 30));
            pantalla.AgregarParte("panel", panel);

            var baseCentro = new Vertice(centro.X, centro.Y - alto / 2 - 1.0f, centro.Z);
            var basePantalla = CrearPrisma(baseCentro, ancho / 3, 1.0f, grosor * 3, color);
            pantalla.AgregarParte("base", basePantalla);

            var soporteCentro = new Vertice(centro.X, centro.Y - alto / 2 - 0.5f, centro.Z);
            var soporte = CrearPrisma(soporteCentro, ancho / 6, 1.0f, grosor, color);
            pantalla.AgregarParte("soporte", soporte);

            return pantalla;
        }

        public static Objeto CrearSilla(Vertice centro, float altura = 5f, Vertice color = null)
        {
            color ??= new Vertice(50, 50, 150);
            var silla = new Objeto("silla", centro, new Dictionary<string, Parte>(), color);

            float grosorAsiento = 0.4f;
            var asientoCentro = new Vertice(centro.X, centro.Y + altura / 2 - grosorAsiento / 2, centro.Z);
            var asiento = CrearPrisma(asientoCentro, 6f, grosorAsiento, 6f, new Vertice(100, 100, 200));
            silla.AgregarParte("asiento", asiento);

            float grosorRespaldo = 0.3f;
            var respaldoCentro = new Vertice(centro.X, centro.Y + altura / 2 + 1.0f, centro.Z + 2.8f);
            var respaldo = CrearPrisma(respaldoCentro, 6f, 2.5f, grosorRespaldo, new Vertice(100, 100, 200));
            silla.AgregarParte("respaldo", respaldo);

            float alturaPata = altura - grosorAsiento;
            float separacion = 2.0f;
            var pataColor = new Vertice(40, 40, 40);

            var centroPataFrenteIzq = new Vertice(centro.X - separacion, centro.Y - (altura / 2) + (alturaPata / 2), centro.Z - separacion);
            var centroPataFrenteDer = new Vertice(centro.X + separacion, centro.Y - (altura / 2) + (alturaPata / 2), centro.Z - separacion);
            var centroPataTrasIzq = new Vertice(centro.X - separacion, centro.Y - (altura / 2) + (alturaPata / 2), centro.Z + separacion);
            var centroPataTrasDer = new Vertice(centro.X + separacion, centro.Y - (altura / 2) + (alturaPata / 2), centro.Z + separacion);

            silla.AgregarParte("pata_frente_izq", CrearPrisma(centroPataFrenteIzq, 0.5f, alturaPata, 0.5f, pataColor));
            silla.AgregarParte("pata_frente_der", CrearPrisma(centroPataFrenteDer, 0.5f, alturaPata, 0.5f, pataColor));
            silla.AgregarParte("pata_tras_izq", CrearPrisma(centroPataTrasIzq, 0.5f, alturaPata, 0.5f, pataColor));
            silla.AgregarParte("pata_tras_der", CrearPrisma(centroPataTrasDer, 0.5f, alturaPata, 0.5f, pataColor));

            return silla;
        }
    }
}