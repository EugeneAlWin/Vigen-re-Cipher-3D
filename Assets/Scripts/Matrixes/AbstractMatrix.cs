using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractMatrix : MonoBehaviour
{
    internal Vector3 initialPosition;
    public abstract byte MatrixLen { get; set; }
    public abstract string MatrixType { get; set; }
    internal abstract ElementTransform ET { get; set; }

    internal abstract GameObject[] Matrix { get; set; }
    internal abstract Dictionary<string, GameObject> MatrixDictionary { get; set; }
    internal static readonly Color[] colors = { Color.red, Color.blue, Color.green };
    internal static readonly byte colorsLen = (byte)colors.Length;

    internal string GetElementName(byte x, byte y, byte z) => $"{MatrixType}-z{z}x{y}y{x}";
    internal void GenMatrix(byte x_limit, byte y_limit, byte z_limit)
    {
        for (byte z = 0; z < z_limit; z++)
            for (byte y = 0; y < y_limit; y++)
                for (byte x = 0; x < x_limit; x++)
                {
                    var instance = Instantiate(
                        Matrix[(x + y + z) % MatrixLen],
                        ET.GetPosition(x, y, z),
                        ET.Rotation,
                        transform);
                    instance.transform.localScale = ET.Scale;
                    instance.name = GetElementName(x, y, z);
                    instance.GetComponent<Renderer>().material.color = colors[z % colorsLen];
                    instance.SetActive(false);
                    MatrixDictionary.Add(instance.name, instance);
                }
    }

    internal void SetZLayerVisibillity(byte howMuchToHide = 0)
    {
        for (byte z = 0; z < MatrixLen; z++)
            for (byte y = 0; y < MatrixLen; y++)
                for (byte x = 0; x < MatrixLen; x++)
                {
                    Debug.Log(MatrixDictionary[GetElementName(x, y, z)]);
                    MatrixDictionary[GetElementName(x, y, z)].SetActive(z >= howMuchToHide);
                }
    }
}
