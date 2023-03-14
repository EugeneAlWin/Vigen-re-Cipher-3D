using UnityEngine;

class ElementTransform
{
    public Quaternion Rotation { get; set; }
    public Vector3 Scale { get; set; }
    public ElementTransform(Vector3 rotation, Vector3 scale)
    {
        Rotation = Quaternion.Euler(rotation);
        Scale = scale;
    }
}
