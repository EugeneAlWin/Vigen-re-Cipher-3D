using System.Collections.Generic;
using UnityEngine;
using static ENUMS;

public abstract partial class AbstractMatrix : MonoBehaviour
{
    internal Vector3 initialPosition;
    public abstract byte MatrixLen { get; set; }
    public abstract string MatrixPrefix { get; set; }
    internal abstract ALPHABETS MatrixType { get; set; }
    internal abstract ElementTransform ET { get; set; }
    internal abstract GameObject[] Matrix { get; set; }
    internal abstract Dictionary<string, GameObject> MatrixDictionary { get; set; }
    internal static readonly Color[] colors = { Color.red, Color.blue, Color.green };
    internal static readonly byte colorsLen = (byte)colors.Length;
    internal GameObject swapObject = null;
    private Color swapColor;

    internal string GetElementName(byte x, byte y, byte z) => $"{MatrixPrefix}-z{z}x{y}y{x}";
    internal void GenMatrix(byte x_limit, byte y_limit, byte z_limit)
    {
        for (byte z = 0; z < z_limit; z++)
            for (byte y = 0; y < y_limit; y++)
                for (byte x = 0; x < x_limit; x++)
                {
                    var instance = Instantiate(
                        Matrix[(x + y + z) % MatrixLen],
                        ET.GetPositionWithOffset(x, y, z),
                        ET.Rotation,
                        transform);
                    instance.transform.localScale = ET.Scale;
                    instance.name = GetElementName(x, y, z);
                    instance.GetComponent<Renderer>().material.color = colors[z % colorsLen];
                    instance.SetActive(false);
                    MatrixDictionary.Add(instance.name, instance);
                }
    }

    internal void OnStudyModeChanged(STEPS newStep, ACTIONS action)
    {
        _ = action;
        switch (newStep)
        {
            case STEPS.NONE:
                SetZLayerVisibillity(MatrixLen);
                break;
            case STEPS.FIRST:
                SetZLayerVisibillity(0, 2);
                break;
            default:
                break;
        }
    }
}
