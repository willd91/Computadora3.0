using OpenTK.Mathematics; // Para vectores 2D y colores (Vector2, Vector4)
using System.Collections.Generic; // Para usar listas de figuras

namespace ProGrafica
{
    // Clase que representa una computadora 2D hecha con figuras básicas
    public class Computadora
    {
        // Lista de todas las "partes" que componen la computadora
        // Cada parte es un objeto de tipo Figura (Poligono o Circulo)
        private List<Figura> partes = new List<Figura>();

        // Constructor: aquí definimos cada parte de la computadora
        public Computadora()
        {
            // =========================
            // 1️⃣ Pantalla exterior
            // =========================
            // Es un rectángulo negro
            // new Vector2(0,0) => posición central del rectángulo
            // Vector4(0,0,0,1) => color negro en RGBA
            // Vector2[] => vertices relativos al centro
            partes.Add(new Poligono(
                new Vector2(0, 0.0f),             // Centro
                new Vector4(0, 0, 0, 1),          // Color negro
                new Vector2[] {                    // Vertices del rectángulo
                    new Vector2(-0.4f, 0.3f),     // esquina superior izquierda
                    new Vector2(0.4f, 0.3f),      // esquina superior derecha
                    new Vector2(0.4f, -0.3f),     // esquina inferior derecha
                    new Vector2(-0.4f, -0.3f)     // esquina inferior izquierda
                }
            ));

            // =========================
            // 2️⃣ Pantalla interior
            // =========================
            // Rectángulo azul dentro de la pantalla
            partes.Add(new Poligono(
                new Vector2(0, 0.0f),                 // Centro
                new Vector4(0.39f, 0.58f, 0.93f, 1), // Color azul claro (CornflowerBlue)
                new Vector2[] {                        // Vertices
                    new Vector2(-0.35f, 0.25f),
                    new Vector2(0.35f, 0.25f),
                    new Vector2(0.35f, -0.25f),
                    new Vector2(-0.35f, -0.25f)
                }
            ));

            // =========================
            // 3️⃣ Teclado inclinado (trapecio invertido)
            // =========================
            // Más ancho abajo que arriba para simular perspectiva
            partes.Add(new Poligono(
                new Vector2(0, -0.3f),               // Centro del teclado
                new Vector4(0.5f, 0.5f, 0.5f, 1),   // Gris
                new Vector2[] {                       // Vertices
                    new Vector2(-0.35f, -0.1f), // esquina superior izquierda (más estrecha arriba)
                    new Vector2(0.35f, -0.1f),  // esquina superior derecha
                    new Vector2(0.4f, -0.25f),  // esquina inferior derecha (más ancho abajo)
                    new Vector2(-0.4f, -0.25f)  // esquina inferior izquierda
                }
            ));

            // =========================
            // 4️⃣ Mouse
            // =========================
            // Representado como un círculo gris a la derecha del teclado
            // Vector2(0.55f,-0.4f) => posición central
            // 0.08f => radio del círculo
            partes.Add(new Circulo(
                new Vector2(0.6f, -0.5f),
                new Vector4(0.3f, 0.3f, 0.3f, 1), // Gris oscuro
                0.08f
            ));
            // =========================
            // 5to CASE
            // =========================
            // Rectángulo azul dentro de la pantalla
            partes.Add(new Poligono(
                new Vector2(0.7f, 0.0f),                 // Centro
                new Vector4(0, 0, 0, 1), // Color azul claro (CornflowerBlue)
                new Vector2[] {                        // Vertices
                    new Vector2(-0.2f, 0.4f),
                    new Vector2(0.0f, 0.4f),
                    new Vector2(0.0f, -0.3f),
                    new Vector2(-0.2f, -0.3f)
                }
            ));
            partes.Add(new Poligono(
               new Vector2(0.7f, 0.0f),                 // Centro
               new Vector4(0.39f, 0.58f, 0.93f, 1), // Color azul claro (CornflowerBlue)
               new Vector2[] {                        // Vertices
                    new Vector2(-0.19f, 0.39f),
                    new Vector2(-0.01f, 0.39f),
                    new Vector2(-0.01f, -0.29f),
                    new Vector2(-0.19f, -0.29f)
               }
           ));
        }

        // =========================
        // Método para dibujar toda la computadora
        // =========================
        public void Dibujar(Shader shader)
        {
            // Recorre todas las partes y llama a su método Dibujar
            // Cada figura se dibuja usando el shader que le pasamos
            foreach (var parte in partes)
                parte.Dibujar(shader);
        }
    }
}
