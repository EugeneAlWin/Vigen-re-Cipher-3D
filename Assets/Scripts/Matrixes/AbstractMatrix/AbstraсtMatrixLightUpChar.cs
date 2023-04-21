using UnityEngine;
using static ENUMS;
using static STATES;

public abstract partial class AbstractMatrix : MonoBehaviour
{
    internal void LightUpChar(CipherVector cipherVector)
    {
        if (CURRENT_ALPHABET != MatrixType) return;
        var dict = CURRENT_ALPHABET == ALPHABETS.LATIN ? Alphabets.LatinDictionary : Alphabets.CyrillicDictionary;
        byte xpos = (byte)dict[cipherVector.Message];
        byte ypos = (byte)dict[cipherVector.Key];
        string name;
        if (STUDY_CURRENT_ACTION == ACTIONS.ENCODING)
            name = GetElementName(xpos, ypos, (byte)(cipherVector.Depth % MatrixLen));
        else
            name = GetElementName(xpos, ypos, (byte)(cipherVector.Depth % MatrixLen));
        Debug.Log(name);

        TryToLightUp(name);
    }
    internal void LightUpChar(string xChar, string yChar, int step)
    {
        if (CURRENT_ALPHABET != MatrixType) return;
        var alphabet = CURRENT_ALPHABET == ALPHABETS.LATIN ? Alphabets.LatinAlphabet : Alphabets.CyrillicAlphabet;
        var dict = CURRENT_ALPHABET == ALPHABETS.LATIN ? Alphabets.LatinDictionary : Alphabets.CyrillicDictionary;
        int moduloStep = step % MatrixLen;
        int ypos = dict[yChar];
        int xpos = 0;

        switch (STUDY_DIRECTION)
        {
            case DIRECTIONS.TOP:
                ypos -= moduloStep;
                if (ypos < 0)
                    ypos = (ypos + MatrixLen) % MatrixLen;

                break;
            case DIRECTIONS.BOTTOM:
                ypos += moduloStep;
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
            name = GetElementName((byte)xpos, (byte)ypos, (byte)(STUDY_CURRENT_STEP < STEPS.THIRD ? byte.MinValue : STUDY_DEPTH % MatrixLen));
        else
            name = GetElementName((byte)xpos, (byte)ypos, (byte)(STUDY_CURRENT_STEP == STEPS.SECOND || STUDY_CURRENT_STEP == STEPS.THIRD ? (byte)STUDY_DEPTH % MatrixLen : byte.MinValue));
        Debug.Log(name);
        TryToLightUp(name);

    }

    private void TryToLightUp(string name)
    {
        if (MatrixDictionary.TryGetValue(name, out GameObject element))
        {
            if (swapObject != null) swapObject.GetComponent<Renderer>().material.color = swapColor;
            swapObject = element.transform.GetChild(0).gameObject;
            swapColor = swapObject.GetComponent<Renderer>().material.color;
            element.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
