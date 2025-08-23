// Archivo: Shader.cs
// Maneja la creación y uso de shaders GLSL
using OpenTK.Graphics.OpenGL4;
using System;
using System.IO;

namespace Graficos2D
{
    public class Shader
    {
        public int Handle { get; private set; }

        // Constructor: recibe rutas de los archivos de shader
        public Shader(string vertexPath, string fragmentPath)
        {
            string vertexSource = File.ReadAllText(vertexPath);
            string fragmentSource = File.ReadAllText(fragmentPath);

            // Crear y compilar vertex shader
            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertexSource);
            GL.CompileShader(vertexShader);
            GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out int success);
            if (success == 0)
                throw new Exception(GL.GetShaderInfoLog(vertexShader));

            // Crear y compilar fragment shader
            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fragmentSource);
            GL.CompileShader(fragmentShader);
            GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out success);
            if (success == 0)
                throw new Exception(GL.GetShaderInfoLog(fragmentShader));

            // Crear programa y linkear shaders
            Handle = GL.CreateProgram();
            GL.AttachShader(Handle, vertexShader);
            GL.AttachShader(Handle, fragmentShader);
            GL.LinkProgram(Handle);
            GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out success);
            if (success == 0)
                throw new Exception(GL.GetProgramInfoLog(Handle));

            // Limpiar shaders individuales
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
        }

        public void Usar() => GL.UseProgram(Handle);

        // Cambia el color uniforme del shader
        public void SetColor(float r, float g, float b, float a)
        {
            int location = GL.GetUniformLocation(Handle, "uColor");
            GL.Uniform4(location, r, g, b, a);
        }
    }
}

