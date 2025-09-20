using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace ProGrafica
{
    [Serializable]
    public class Escenario
    {
        public Dictionary<string, Objeto> Objetos { get; set; }

        [JsonInclude]
        public Vertice Centro { get; set; } 

        [JsonInclude]
        public Vertice Rotacion { get; set; } 

        [JsonInclude]
        public Vertice Escala { get; set; } 

        [JsonConstructor]
        public Escenario()
        {
            Objetos = new Dictionary<string, Objeto>();
            Rotacion = new Vertice();
            Escala = new Vertice(1, 1, 1);
            Centro = new Vertice();
        }

        public Escenario(Dictionary<string, Objeto> objetos, Vertice centro = null,
                        Vertice rotacion = null, Vertice escala = null)
        {
            
            this.Centro = centro ?? new Vertice(0, 0, 0);
            this.Objetos = objetos ?? new Dictionary<string, Objeto>();
            Rotacion = rotacion ?? new Vertice(0, 0, 0);
            Escala = escala ?? new Vertice(1, 1, 1);
        }

        public void AddObjeto(string nombre, Objeto objeto)
        {
            if (!Objetos.ContainsKey(nombre))
            {
                Objetos[nombre] = objeto;
            }
            else
            {
                throw new ArgumentException($"Ya existe un objeto con el nombre '{nombre}'.");
            }
        }

        public void DelObjeto(string nombre)
        {
            if (Objetos.ContainsKey(nombre))
            {
                Objetos.Remove(nombre);
            }
            else
            {
                throw new ArgumentException($"No existe un objeto con '{nombre}'.");
            }
        }

        public Objeto GetObjeto(string nombre)
        {
            if (Objetos.ContainsKey(nombre))
                return Objetos[nombre];
            else
                throw new KeyNotFoundException($"No se encontró el objeto '{nombre}'.");
        }

        public bool RemoveObjeto(string nombre)
        {
            return Objetos.Remove(nombre);
        }

        public Matrix4 GetTransformMatrix()
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

        public void Escalar(float x, float y, float z)
        {
            Centro.X *= x;
            Centro.Y *= y;
            Centro.Z *= z;

            Escala.X *= x;
            Escala.Y *= y;
            Escala.Z *= z;
        }

        public void Dibujar(Shader shader)
        {
            Matrix4 escenaTransform = GetTransformMatrix();

            foreach (var objeto in Objetos.Values)
            {
                objeto.Dibujar(shader, escenaTransform);
            }
        }

        public void AplicarTransformacionATodos(Action<Objeto> transformacion)
        {
            foreach (var objeto in Objetos.Values)
            {
                transformacion(objeto);
            }
        }
        public void ResetearTransformaciones()
        {
            Centro = new Vertice();
            Rotacion =new Vertice();
            Escala = new Vertice(1,1,1);
        }
    }
}