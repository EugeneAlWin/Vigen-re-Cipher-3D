using System.Collections.Generic;
using UnityEngine;
using static ENUMS;

public class LatinMatrix : AbstractMatrix
{
    [field: SerializeField] public override byte MatrixLen { get; set; } = 26;
    internal override ALPHABETS MatrixType { get; set; } = ALPHABETS.LATIN;
    [field: SerializeField] public override string MatrixPrefix { get; set; } = "LATIN";
    internal override Dictionary<string, GameObject> MatrixDictionary { get; set; }

    internal override ElementTransform ET { get; set; }
    internal override GameObject[] Matrix { get; set; }

    void Awake()
    {
        Controller.onStudyModeChanged += OnStudyModeChanged;
        Controller.onCipherVectorChanged += LightUpChar;
        Controller.onLightNonZZeroElement += LightUpChar;
        Controller.onCipherVectorChanged += SetZLayerVisibillity;
        MatrixDictionary = new Dictionary<string, GameObject>();
        Matrix = new GameObject[MatrixLen];
        for (byte i = 0; i < MatrixLen; i++)
            Matrix[i] = (GameObject)Resources.Load($"Prefabs/Latin/{Alphabets.LatinAlphabet[i].ToUpper()}");

        initialPosition = transform.position;
        ET = new(new Vector3(150.5f, 4.5f, 4.5f), new Vector3(0, 180, 0), new Vector3(100, 100, 100));
        GenMatrix(MatrixLen, MatrixLen, MatrixLen);
    }
}
