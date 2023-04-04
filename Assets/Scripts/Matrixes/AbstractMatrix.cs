using System.Collections.Generic;
using UnityEngine;
using static ENUMS;
using static STATES;

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
    internal GameObject swapObject = null;
    private Color swapColor;
    internal string GetElementName(byte x, byte y, byte z) => $"{MatrixType}-z{z}x{y}y{x}";
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
    internal virtual void LightUpChar(CipherVector cipherVector)
    {
        byte xpos = (byte)Alphabets.CyrillicDictionary[cipherVector.Message];
        byte ypos = (byte)Alphabets.CyrillicDictionary[cipherVector.Key];
        if (MatrixDictionary.TryGetValue(GetElementName(xpos, ypos, CURRENT_EXAMINE_STEP == STEPS.SECOND ?
            byte.MinValue :
            (byte)EXAMINE_DEPTH),
            out GameObject element))
        {
            if (swapObject != null) swapObject.GetComponent<Renderer>().material.color = swapColor;

            swapObject = element;
            swapColor = element.GetComponent<Renderer>().material.color;
            element.GetComponent<Renderer>().material.color = Color.white;
            SetZLayerVisibillity(cipherVector);
        }
    }
    internal virtual void LightUpChar(string xChar, string yChar)
    {
        var alphabetLen = CURRENT_ALPHABET == ALPHABETS.CYRILLIC ? Alphabets.CyrillicAlphabet.Length : Alphabets.LatinAlphabet.Length;
        byte ypos = (byte)Alphabets.CyrillicDictionary[yChar];
        byte xpos = 0;
        while (true)
        {
            if (Alphabets.CyrillicAlphabet[(ypos + xpos + EXAMINE_DEPTH) % alphabetLen] == xChar) break;
            xpos++;
        }

        var name = GetElementName(xpos, ypos, CURRENT_EXAMINE_STEP == STEPS.SECOND ? byte.MinValue : (byte)EXAMINE_DEPTH);
        if (MatrixDictionary.TryGetValue(name, out GameObject element))
        {
            if (swapObject != null) swapObject.GetComponent<Renderer>().material.color = swapColor;
            swapObject = element;
            swapColor = element.GetComponent<Renderer>().material.color;
            element.GetComponent<Renderer>().material.color = Color.white;
        }
    }
    internal void SetZLayerVisibillity(byte howMuchToHide = 0)
    {
        for (byte z = 0; z < MatrixLen; z++)
            for (byte y = 0; y < MatrixLen; y++)
                for (byte x = 0; x < MatrixLen; x++)
                    if (MatrixDictionary.TryGetValue(GetElementName(x, y, z), out GameObject gm))
                        gm.SetActive(z >= howMuchToHide);
    }
    internal void SetZLayerVisibillity(byte start, byte end)
    {
        for (byte z = 0; z < MatrixLen; z++)
            for (byte y = 0; y < MatrixLen; y++)
                for (byte x = 0; x < MatrixLen; x++)
                    if (MatrixDictionary.TryGetValue(GetElementName(x, y, z), out GameObject gm))
                        gm.SetActive(z >= start && z <= end);
    }
    internal void SetZLayerVisibillity(CipherVector chiperVector)
    {
        for (byte z = 0; z < MatrixLen; z++)
            for (byte y = 0; y < MatrixLen; y++)
                for (byte x = 0; x < MatrixLen; x++)
                    if (MatrixDictionary.TryGetValue(GetElementName(x, y, z), out GameObject gm))
                        gm.SetActive(z == (chiperVector.Depth % (int)MatrixLen));
    }
}
