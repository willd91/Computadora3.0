using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ProGrafica
{
    #region Clase Objeto3D
    [Serializable]
    public class Objeto
    {
        public Dictionary<string, Parte> Partes { get; set; }
        public Vertice Centro { get; set; }
        public Vertice Rotacion { get; set; }
        public Vertice Escala { get; set; }
        public Vertice Color { get; set; }
        public string Name { get; set; }

        [JsonConstructor]
        public Objeto(string name, Vertice centro, Dictionary<string, Parte> partes, Vertice color, Vertice rotacion = null, Vertice escala = null)
        {
            this.Name = name;
            this.Centro = centro ?? new Vertice(0, 0, 0);
            this.Color = color;
            this.Partes = partes ?? new Dictionary<string, Parte>();
            Rotacion = rotacion ?? new Vertice(0, 0, 0);
            Escala = escala ?? new Vertice(1, 1, 1);
        }

        public Objeto() : this("", new Vertice(0, 0, 0), new Dictionary<string, Parte>(), new Vertice(0, 0, 0)) { }
        public Matrix4 GetModelMatrix()
        {
            return Matrix4.CreateScale(Escala.X, Escala.Y, Escala.Z) *
         Matrix4.CreateRotationX(MathHelper.DegreesToRadians(Rotacion.X)) *
         Matrix4.CreateRotationY(MathHelper.DegreesToRadians(Rotacion.Y)) *
         Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(Rotacion.Z)) *
         Matrix4.CreateTranslation(Centro.X, Centro.Y, Centro.Z);
        }
        
        public void Mover(float x, float y, float z)
        {
            Centro.X += x;
            Centro.Y += y;
            Centro.Z += z;
        }

        public void Rotar(float x, float y, float z)
        {
            Rotacion.X += x;
            Rotacion.Y += y;
            Rotacion.Z += z;
        }
        public void RotarA(float x, float y, float z)
        {
            Rotacion.X += x;
            Rotacion.Y += y;
            Rotacion.Z += z;
        }
        public void Escalar(float x, float y, float z)
        {
            Centro.X *= x;
            Centro.Y *= y;
            Centro.Z *= z;

            Escala.X *= x;
            Escala.Y *= y;
            Escala.Z *= z;
        }

        public void Dibujar(Shader shader, Matrix4 parentTransform = default)
        {
            Matrix4 objetoTransform = parentTransform == default ?
                GetModelMatrix() :
                parentTransform * GetModelMatrix();
            foreach (var parte in Partes.Values)
            {
                parte.Dibujar(shader,objetoTransform);
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
