using UnityEngine;

class ElementTransform
{
    public Vector3 Position { get; set; }
    public Quaternion Rotation { get; set; }
    public Vector3 Scale { get; set; }
    public ElementTransform(Vector3 position, Vector3 rotation, Vector3 scale)
    {
        Position = position;
        Rotation = Quaternion.Euler(rotation);
        Scale = scale;
    }
    public Vector3 GetPositionWithOffset(byte x, byte y, byte z) => new(x - Position.x, -y + Position.y, z - Position.z);
}
