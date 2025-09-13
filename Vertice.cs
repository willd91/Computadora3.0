using System.Text.Json.Serialization;

namespace ProGrafica
{
    #region Clase Vértice
    [Serializable]
    public class Vertice
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public Vertice(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
        [JsonConstructor]
        public Vertice() : this(0, 0, 0) { }
        public override string ToString() => $"({X}, {Y}, {Z})";
    }
    #endregion
}