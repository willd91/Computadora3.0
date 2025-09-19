using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ProGrafica
{
    #region Clase Objeto3D
    [Serializable]
    public class Objeto3D
    {
        // Diccionario: clave = nombre de la parte, valor = Parte
        public Dictionary<string, Parte> Partes { get; set; }
        public Vertice Centro { get; set; }
        public Vertice Color { get; set; }
        public string Name { get; set; }

        [JsonConstructor]
        public Objeto3D(string name, Vertice centro, Dictionary<string, Parte> partes, Vertice color)
        {
            this.Name = name;
            this.Centro = centro;
            this.Color = color;
            this.Partes = partes ?? new Dictionary<string, Parte>();
        }

        public Objeto3D() : this("", new Vertice(0, 0, 0), new Dictionary<string, Parte>(), new Vertice(0, 0, 0)) { }

        public void Dibujar(Shader shader)
        {
            foreach (var parte in Partes.Values)
            {
                parte.Dibujar(shader);
            }
        }

        public void AgregarParte(string nombre, Parte parte)
        {
            Partes[nombre] = parte;
        }
        public void DelObjeto(string nombre)
        {
            if (Partes.ContainsKey(nombre))  
            {
                Partes.Remove(nombre);
            }
            else
            {
                throw new ArgumentException($"No existe una parte con '{nombre}'.");
            }
        }
        public Parte ObtenerParte(string nombre)
        {
            Partes.TryGetValue(nombre, out var parte);
            return parte;
        }

        public bool EliminarParte(string nombre)
        {
            return Partes.Remove(nombre);
        }
    }
    #endregion
}
