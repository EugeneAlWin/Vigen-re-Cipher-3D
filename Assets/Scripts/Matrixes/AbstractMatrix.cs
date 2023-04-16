using System.Collections.Generic;
using UnityEngine;
using static ENUMS;
using static STATES;

public abstract class AbstractMatrix : MonoBehaviour
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
    internal void LightUpChar(CipherVector cipherVector)
    {
        if (CURRENT_ALPHABET != MatrixType) return;
        var dict = CURRENT_ALPHABET == ALPHABETS.LATIN ? Alphabets.LatinDictionary : Alphabets.CyrillicDictionary;
        byte xpos = (byte)dict[cipherVector.Message];
        byte ypos = (byte)dict[cipherVector.Key];
        string name;
        if (STUDY_CURRENT_ACTION == ACTIONS.ENCODING)
            name = GetElementName((byte)xpos, (byte)ypos, STUDY_CURRENT_STEP == STEPS.SECOND ? byte.MinValue : (byte)STUDY_DEPTH);
        else
            name = GetElementName((byte)xpos, (byte)ypos, STUDY_CURRENT_STEP == STEPS.FIFTH || STUDY_CURRENT_STEP == STEPS.FOURTH ? byte.MinValue : (byte)STUDY_DEPTH);
        if (MatrixDictionary.TryGetValue(name, out GameObject element))
        {
            if (swapObject != null) swapObject.GetComponent<Renderer>().material.color = swapColor;

            swapObject = element.transform.GetChild(0).gameObject;
            swapColor = swapObject.GetComponent<Renderer>().material.color;
            element.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = Color.white;
            SetZLayerVisibillity(cipherVector);
        }
    }
    internal void LightUpChar(string xChar, string yChar, int step)
    {
        if (CURRENT_ALPHABET != MatrixType) return;
        var alphabet = CURRENT_ALPHABET == ALPHABETS.LATIN ? Alphabets.LatinAlphabet : Alphabets.CyrillicAlphabet;
        var dict = CURRENT_ALPHABET == ALPHABETS.LATIN ? Alphabets.LatinDictionary : Alphabets.CyrillicDictionary;
        int ypos = dict[yChar];
        int xpos = 0;

        switch (STUDY_DIRECTION)
        {
            case DIRECTIONS.TOP:
                ypos -= step;
                if (ypos < 0)
                    ypos = (ypos + MatrixLen) % MatrixLen;
                break;
            case DIRECTIONS.BOTTOM:
                ypos += step;
                if (ypos < 0)
                    ypos = (ypos + MatrixLen) % MatrixLen;
                break;
        }

        while (true)
        {
            if (alphabet[(ypos + xpos + STUDY_DEPTH) % alphabet.Length] == xChar) break;
            xpos++;
        }

        string name;
        if (STUDY_CURRENT_ACTION == ACTIONS.ENCODING)
            name = GetElementName((byte)xpos, (byte)ypos, STUDY_CURRENT_STEP == STEPS.SECOND ? byte.MinValue : (byte)STUDY_DEPTH);
        else
            name = GetElementName((byte)xpos, (byte)ypos, STUDY_CURRENT_STEP == STEPS.SECOND || STUDY_CURRENT_STEP == STEPS.THIRD ? (byte)STUDY_DEPTH : byte.MinValue);
        if (MatrixDictionary.TryGetValue(name, out GameObject element))
        {
            if (swapObject != null) swapObject.GetComponent<Renderer>().material.color = swapColor;
            swapObject = element.transform.GetChild(0).gameObject;
            swapColor = swapObject.GetComponent<Renderer>().material.color;
            element.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
    }

    internal void SetZLayerVisibillity(byte howMuchToHide = 0)
    {
        if (CURRENT_ALPHABET != MatrixType && MatrixType != ALPHABETS.DIGITAL) return;
        for (byte z = 0; z < MatrixLen; z++)
            for (byte y = 0; y < MatrixLen; y++)
                for (byte x = 0; x < MatrixLen; x++)
                    if (MatrixDictionary.TryGetValue(GetElementName(x, y, z), out GameObject gm))
                        gm.SetActive(z >= howMuchToHide);
    }
    internal void SetZLayerVisibillity(byte start, byte end)
    {
        if (CURRENT_ALPHABET != MatrixType) return;
        for (byte z = 0; z < MatrixLen; z++)
            for (byte y = 0; y < MatrixLen; y++)
                for (byte x = 0; x < MatrixLen; x++)
                    if (MatrixDictionary.TryGetValue(GetElementName(x, y, z), out GameObject gm))
                        gm.SetActive(z >= start && z <= end);
    }
    internal void SetZLayerVisibillity(CipherVector chiperVector)
    {
        if (CURRENT_ALPHABET != MatrixType) return;
        for (byte z = 0; z < MatrixLen; z++)
            for (byte y = 0; y < MatrixLen; y++)
                for (byte x = 0; x < MatrixLen; x++)
                    if (MatrixDictionary.TryGetValue(GetElementName(x, y, z), out GameObject gm))
                        gm.SetActive(z == (chiperVector.Depth % (int)MatrixLen));
    }

    internal void OnStudyModeChanged(STEPS newStep, ACTIONS action)
    {
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
