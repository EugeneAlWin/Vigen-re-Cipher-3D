using System.Collections.Generic;
using UnityEngine;
using static ENUMS;

public class CyrillicMatrix : AbstractMatrix
{
    [field: SerializeField] public override byte MatrixLen { get; set; } = 33;
    [field: SerializeField] public override string MatrixType { get; set; } = "CYRILLIC";
    internal override Dictionary<string, GameObject> MatrixDictionary { get; set; }

    internal override ElementTransform ET { get; set; }
    internal override GameObject[] Matrix { get; set; }

    void Awake()
    {
        Controller.studyModeChanged += OnStudyModeChanged;
        Controller.cipherVectorChanged += SetZLayerVisibillity;
        MatrixDictionary = new Dictionary<string, GameObject>();
        Matrix = new GameObject[MatrixLen];
        for (byte i = 0; i < MatrixLen; i++)
            Matrix[i] = (GameObject)Resources.Load($"Prefabs/Cyrillic/{Alphabets.CyrillicAlphabet[i].ToUpper()}");

        initialPosition = transform.position;
        ET = new(new Vector3(150.5f, 4.5f, 4.5f), new Vector3(0, 180, 0), new Vector3(1, 1, 1));
        GenMatrix(MatrixLen, MatrixLen, MatrixLen);
    }
    private void OnStudyModeChanged(STEPS newStep, ACTIONS action)
    {
        switch (newStep)
        {
            case STEPS.NONE:
                SetZLayerVisibillity(MatrixLen);
                break;
            case STEPS.FIRST:
                SetZLayerVisibillity(0, 3);
                break;
            default:
                break;
        }
    }
}
