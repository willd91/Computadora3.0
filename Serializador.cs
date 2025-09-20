using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProGrafica
{
    public static class Serializador
    {
        private static readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true,             
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,  
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull 
        };

        public static string Serialize<T>(T obj)
        {
            return JsonSerializer.Serialize(obj, options);
        }

        public static T Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, options);
        }

        public static void SerializeToFile<T>(T obj, string filePath)
        {
            string json = Serialize(obj);
            File.WriteAllText(filePath, json);
        }

        public static T DeserializeFromFile<T>(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("Archivo JSON no encontrado", filePath);

            string json = File.ReadAllText(filePath);
            return Deserialize<T>(json);
        }
    }
}
