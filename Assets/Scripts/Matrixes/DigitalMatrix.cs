using System.Collections.Generic;
using UnityEngine;
using static ENUMS;

public class DigitalMatrix : AbstractMatrix
{
    public bool isInRotating = true;

    [SerializeField] private float period = 4f; // time for one complete cycle in seconds
    [field: SerializeField] public override byte MatrixLen { get; set; } = 10;
    [field: SerializeField] public override string MatrixType { get; set; } = "DIGITAL";
    internal override Dictionary<string, GameObject> MatrixDictionary { get; set; }

    internal override GameObject[] Matrix { get; set; }
    internal override ElementTransform ET { get; set; }

    void Awake()
    {
        Controller.studyModeChanged += OnStepChanged;
        MatrixDictionary = new Dictionary<string, GameObject>();
        Matrix = new GameObject[MatrixLen];
        for (byte i = 0; i < MatrixLen; i++)
            Matrix[i] = (GameObject)Resources.Load($"Prefabs/Digits/{i}");

        initialPosition = transform.position;
        ET = new(new Vector3(4.5f, 4.5f, 4.5f), Vector3.zero, new Vector3(0.03f, 0.03f, 0.03f));
        GenMatrix(MatrixLen, MatrixLen, MatrixLen);
        SetZLayerVisibillity(0);
    }

    void Update()
    {
        if (isInRotating) RotateMatrix(MatrixLen, MatrixLen, MatrixLen);
    }

    void RotateMatrix(byte x_limit, byte y_limit, byte z_limit)
    {
        float time = Time.time;
        float displacement = 7.0f + Mathf.Sin(2f * Mathf.PI * time / period);
        transform.position = initialPosition + new Vector3(0f, displacement, 0f);
        transform.Rotate(Vector3.up, .5f);

        for (byte z = 0; z < z_limit; z++)
            for (byte y = 0; y < y_limit; y++)
                for (byte x = 0; x < x_limit; x++)
                    MatrixDictionary[GetElementName(x, y, z)].transform.Rotate(Vector3.down, .5f);
    }
    private void OnStepChanged(STEPS newStep, ACTIONS action)
    {
        isInRotating = newStep == STEPS.NONE;
        SetZLayerVisibillity(isInRotating ? byte.MinValue : MatrixLen);
    }
}
