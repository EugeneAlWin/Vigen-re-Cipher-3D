using UnityEngine;
using static ENUMS;
using static STATES;

public abstract partial class AbstractMatrix : MonoBehaviour
{
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
}