using OpenTK.Mathematics;

namespace ProGrafica
{
    public class Camera
    {
        public Vector3 Position { get; private set; }
        public Vector3 Front { get; private set; } = -Vector3.UnitZ;
        public Vector3 Up { get; private set; } = Vector3.UnitY;
        public Vector3 Right => Vector3.Normalize(Vector3.Cross(Front, Up));

        private float yaw = -90f;   // giro horizontal
        private float pitch = 0f;   // giro vertical
        private float fov = 45f;    // zoom

        public Camera(Vector3 startPosition)
        {
            Position = startPosition;
        }

        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(Position, Position + Front, Up);
        }

        public Matrix4 GetProjectionMatrix(float aspectRatio)
        {
            return Matrix4.CreatePerspectiveFieldOfView(
                MathHelper.DegreesToRadians(fov),
                aspectRatio,
                0.1f,
                100f
            );
        }

        // Mover con WASD
        public void Move(Vector3 direction, float speed)
        {
            Position += direction * speed;
        }

        // Rotar con el mouse
        public void Rotate(float deltaX, float deltaY)
        {
            const float sensitivity = 0.1f;
            yaw += deltaX * sensitivity;
            pitch -= deltaY * sensitivity;

            // limitar pitch
            pitch = MathHelper.Clamp(pitch, -89f, 89f);

            Vector3 front;
            front.X = MathF.Cos(MathHelper.DegreesToRadians(yaw)) * MathF.Cos(MathHelper.DegreesToRadians(pitch));
            front.Y = MathF.Sin(MathHelper.DegreesToRadians(pitch));
            front.Z = MathF.Sin(MathHelper.DegreesToRadians(yaw)) * MathF.Cos(MathHelper.DegreesToRadians(pitch));
            Front = Vector3.Normalize(front);
        }

        // Zoom con la rueda
        public void Zoom(float delta)
        {
            fov -= delta;
            fov = MathHelper.Clamp(fov, 15f, 90f);
        }
    }
}
