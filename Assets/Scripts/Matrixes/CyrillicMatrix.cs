using System.Collections.Generic;
using UnityEngine;

public class CyrillicMatrix : AbstractMatrix
{
    [field: SerializeField] public override byte MatrixLen { get; set; } = 33;
    [field: SerializeField] public override string MatrixType { get; set; } = "CYRILLIC";
    internal override Dictionary<string, GameObject> MatrixDictionary { get; set; }

    internal override ElementTransform ET { get; set; }
    internal override GameObject[] Matrix { get; set; }

    private readonly string[] cyrillicAlphabet = new string[]
    {
            "à", "á", "â", "ã", "ä", "å", "¸", "æ", "ç", "è",
            "é", "ê", "ë", "ì", "í", "î", "ï", "ð", "ñ", "ò",
            "ó", "ô", "õ", "ö", "÷", "ø", "ù", "ú", "û", "ü",
            "ý", "þ", "ÿ"
    };

    void Awake()
    {
        Controller.stepsDelegate += OnStepChanged;
        MatrixDictionary = new Dictionary<string, GameObject>();
        Matrix = new GameObject[MatrixLen];
        for (byte i = 0; i < MatrixLen; i++)
            Matrix[i] = (GameObject)Resources.Load($"Prefabs/Cyrillic/{cyrillicAlphabet[i].ToUpper()}");

        initialPosition = transform.position;
        ET = new(new Vector3(150.5f, 4.5f, 4.5f), new Vector3(0, 180, 0), new Vector3(1, 1, 1));
        GenMatrix(MatrixLen, MatrixLen, MatrixLen);
    }
    private void OnStepChanged(Controller.Steps newStep)
    {
        switch (newStep)
        {
            case Controller.Steps.None:
                SetZLayerVisibillity(MatrixLen);
                break;
            case Controller.Steps.First:
                SetZLayerVisibillity(0, 4);
                break;
            default:
                break;
        }
    }
}
