using System;
using System.Collections.Generic;

namespace ProGrafica
{
    public class Escenario
    {
        // Ahora usamos un diccionario en lugar de lista
        public Dictionary<string, Objeto3D> Objetos { get; set; }
        public Escenario()
        {
            Objetos = new Dictionary<string, Objeto3D>();
        }

        // Agregar un objeto con clave (nombre único)
        public void AddObjeto(string nombre, Objeto3D objeto)
        {
            if (!Objetos.ContainsKey(nombre))  // evita duplicados
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
            if (Objetos.ContainsKey(nombre))  // evita duplicados
            {
                Objetos.Remove(nombre);
            }
            else
            {
                throw new ArgumentException($"No existe un obejeto con '{nombre}'.");
            }
        }

        // Obtener un objeto por nombre
        public Objeto3D GetObjeto(string nombre)
        {
            if (Objetos.ContainsKey(nombre))
                return Objetos[nombre];
            else
                throw new KeyNotFoundException($"No se encontró el objeto '{nombre}'.");
        }

        // Eliminar un objeto por nombre
        public bool RemoveObjeto(string nombre)
        {
            return Objetos.Remove(nombre);
        }

        // Dibujar todos los objetos
        public void Dibujar(Shader shader)
        {
            foreach (var item in Objetos.Values) // recorremos solo los valores
            {
                item.Dibujar(shader);
            }
        }
    }
}