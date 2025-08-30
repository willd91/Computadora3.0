using OpenTK.Mathematics;

namespace ProGrafica
{
    public static class Colores
    {
        // Colores básicos
        public static Vector3 Blanco => new Vector3(1.0f, 1.0f, 1.0f);
        public static Vector3 Negro => new Vector3(0.0f, 0.0f, 0.0f);
        public static Vector3 Rojo => new Vector3(1.0f, 0.0f, 0.0f);
        public static Vector3 Verde => new Vector3(0.0f, 1.0f, 0.0f);
        public static Vector3 Azul => new Vector3(0.0f, 0.0f, 1.0f);
        public static Vector3 Amarillo => new Vector3(1.0f, 1.0f, 0.0f);
        public static Vector3 Cian => new Vector3(0.0f, 1.0f, 1.0f);
        public static Vector3 Magenta => new Vector3(1.0f, 0.0f, 1.0f);
        public static Vector3 Gris => new Vector3(0.5f, 0.5f, 0.5f);
        public static Vector3 Naranja => new Vector3(1.0f, 0.65f, 0.0f);

        // Variantes de grises
        public static Vector3 GrisClaro => new Vector3(0.8f, 0.8f, 0.8f);
        public static Vector3 GrisOscuro => new Vector3(0.3f, 0.3f, 0.3f);

        // Colores cálidos
        public static Vector3 Marron => new Vector3(0.65f, 0.16f, 0.16f); // Brown
        public static Vector3 Coral => new Vector3(1.0f, 0.5f, 0.31f);
        public static Vector3 Rosa => new Vector3(1.0f, 0.75f, 0.8f);
        public static Vector3 Violeta => new Vector3(0.93f, 0.51f, 0.93f);
        public static Vector3 Oro => new Vector3(1.0f, 0.84f, 0.0f);

        // Colores fríos
        public static Vector3 AzulMarino => new Vector3(0.0f, 0.0f, 0.5f);
        public static Vector3 AzulCielo => new Vector3(0.53f, 0.81f, 0.92f);
        public static Vector3 VerdeLima => new Vector3(0.0f, 1.0f, 0.0f);
        public static Vector3 VerdeOliva => new Vector3(0.5f, 0.5f, 0.0f);
        public static Vector3 Turquesa => new Vector3(0.25f, 0.88f, 0.82f);

        // Extras
        public static Vector3 Plateado => new Vector3(0.75f, 0.75f, 0.75f);
        public static Vector3 Beige => new Vector3(0.96f, 0.96f, 0.86f);
        public static Vector3 Chocolate => new Vector3(0.82f, 0.41f, 0.12f);
        public static Vector3 Aguamarina => new Vector3(0.5f, 1.0f, 0.83f);
        public static Vector3 Indigo => new Vector3(0.29f, 0.0f, 0.51f);
    }
}
