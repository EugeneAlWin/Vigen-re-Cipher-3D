using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMatrix : MonoBehaviour
{
    public abstract byte MatrixLen { get; set; }
    public abstract string MatrixType { get; set; }
    internal abstract ElementTransform ET { get; set; }

    internal GameObject[] matrix;
    internal Vector3 initialPosition;
    internal static readonly Dictionary<string, Transform> matrixDictionary = new();
    internal static readonly Color[] colors = { Color.red, Color.blue, Color.green };
    internal static readonly byte colorsLen = (byte)colors.Length;

    internal string GetElementName(byte x, byte y, byte z) => $"{MatrixType}-{z}{y}{x}";
    internal void GenMatrix(
        byte x_limit,
        byte y_limit,
        byte z_limit)
    {
        for (byte z = 0; z < z_limit; z++)
            for (byte y = 0; y < y_limit; y++)
                for (byte x = 0; x < x_limit; x++)
                {
                    var instance = Instantiate(
                        matrix[(x + y + z) % MatrixLen],
                    new Vector3(x - 4.5f, -y + 4.5f, z - 4.5f),
                    ET.Rotation
                        );
                    instance.transform.localScale = ET.Scale;
                    instance.name = GetElementName(x, y, z);
                    instance.GetComponent<Renderer>().material.color = colors[z % colorsLen];
                    instance.transform.parent = transform;
                    matrixDictionary[GetElementName(x, y, z)] = instance.transform;
                }
    }
    internal void DestroyMatrix(byte x_limit, byte y_limit, byte z_limit)
    {
        for (byte z = 0; z < z_limit; z++)
            for (byte y = 0; y < y_limit; y++)
                for (byte x = 0; x < x_limit; x++)
                    Destroy(matrixDictionary[GetElementName(x, y, z)].gameObject);
    }
}
