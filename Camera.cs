using OpenTK.Mathematics;

public class Camera
{
    public Vector3 Position { get; private set; }
    public Vector3 Front { get; private set; } = -Vector3.UnitZ;
    public Vector3 Up { get; private set; } = Vector3.UnitY;
    public Vector3 Right => Vector3.Normalize(Vector3.Cross(Front, Up));

    private float yaw = -90f;
    private float pitch = 0f;
    private float fov = 45f;

    public Camera(Vector3 startPosition)
    {
        Position = startPosition;
        UpdateVectors();
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

    // MOVIMIENTO CORREGIDO - usa las direcciones relativas a la cámara
    public void Move(Vector3 direction, float speed)
    {
        if (direction == Vector3.UnitZ) // Adelante
            Position += Front * speed;
        else if (direction == -Vector3.UnitZ) // Atrás
            Position -= Front * speed;
        else if (direction == Vector3.UnitX) // Derecha
            Position += Right * speed;
        else if (direction == -Vector3.UnitX) // Izquierda
            Position -= Right * speed;
        else if (direction == Vector3.UnitY) // Arriba
            Position += Up * speed;
        else if (direction == -Vector3.UnitY) // Abajo
            Position -= Up * speed;
    }

    public void Rotate(float deltaX, float deltaY)
    {
        const float sensitivity = 0.1f;
        yaw += deltaX * sensitivity;
        pitch -= deltaY * sensitivity;

        pitch = MathHelper.Clamp(pitch, -89f, 89f);
        UpdateVectors();
    }

    public void Zoom(float delta)
    {
        fov -= delta;
        fov = MathHelper.Clamp(fov, 15f, 90f);
    }

    private void UpdateVectors()
    {
        // Calcular nueva dirección Front
        Vector3 front;
        front.X = MathF.Cos(MathHelper.DegreesToRadians(yaw)) * MathF.Cos(MathHelper.DegreesToRadians(pitch));
        front.Y = MathF.Sin(MathHelper.DegreesToRadians(pitch));
        front.Z = MathF.Sin(MathHelper.DegreesToRadians(yaw)) * MathF.Cos(MathHelper.DegreesToRadians(pitch));
        Front = Vector3.Normalize(front);
    }
}