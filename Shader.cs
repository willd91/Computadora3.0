using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.IO;

namespace ProGrafica
{
    public class Shader
    {
        public int Handle { get; private set; }

        public Shader(string vertexPath, string fragmentPath)
        {
            string vertexSource = File.ReadAllText(vertexPath);
            string fragmentSource = File.ReadAllText(fragmentPath);

            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertexSource);
            GL.CompileShader(vertexShader);
            CheckCompileErrors(vertexShader, "VERTEX");

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fragmentSource);
            GL.CompileShader(fragmentShader);
            CheckCompileErrors(fragmentShader, "FRAGMENT");

            Handle = GL.CreateProgram();
            GL.AttachShader(Handle, vertexShader);
            GL.AttachShader(Handle, fragmentShader);
            GL.LinkProgram(Handle);
            CheckCompileErrors(Handle, "PROGRAM");

            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
        }

        public void Usar() => GL.UseProgram(Handle);

        // ===============================
        // Helpers para uniforms
        // ===============================
        public void SetMatrix4(string name, Matrix4 matrix)
        {
            int loc = GL.GetUniformLocation(Handle, name);
            GL.UniformMatrix4(loc, false, ref matrix);
        }

        public void SetVector3(string name, Vector3 vector)
        {
            int loc = GL.GetUniformLocation(Handle, name);
            GL.Uniform3(loc, vector);
        }

        private void CheckCompileErrors(int shader, string type)
        {
            if (type != "PROGRAM")
            {
                GL.GetShader(shader, ShaderParameter.CompileStatus, out int success);
                if (success == 0)
                {
                    string infoLog = GL.GetShaderInfoLog(shader);
                    throw new Exception($"Error compilando {type} shader:\n{infoLog}");
                }
            }
            else
            {
                GL.GetProgram(shader, GetProgramParameterName.LinkStatus, out int success);
                if (success == 0)
                {
                    string infoLog = GL.GetProgramInfoLog(shader);
                    throw new Exception($"Error enlazando programa shader:\n{infoLog}");
                }
            }
        }
    }
}
