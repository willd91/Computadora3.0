using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProGrafica
{
    public static class Serializador
    {
        // Opciones comunes de serialización
        private static readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true,               // JSON "bonito"
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase, // nombres en camelCase
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull // omitir nulos
        };

        /// <summary>
        /// Serializa un objeto a JSON en forma de string.
        /// </summary>
        public static string Serialize<T>(T obj)
        {
            return JsonSerializer.Serialize(obj, options);
        }

        /// <summary>
        /// Deserializa un string JSON a objeto del tipo especificado.
        /// </summary>
        public static T Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, options);
        }

        /// <summary>
        /// Guarda un objeto en un archivo JSON.
        /// </summary>
        public static void SerializeToFile<T>(T obj, string filePath)
        {
            string json = Serialize(obj);
            File.WriteAllText(filePath, json);
        }

        /// <summary>
        /// Carga un objeto desde un archivo JSON.
        /// </summary>
        public static T DeserializeFromFile<T>(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Archivo JSON no encontrado", filePath);

            string json = File.ReadAllText(filePath);
            return Deserialize<T>(json);
        }
    }
}
