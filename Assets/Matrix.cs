using System.Collections.Generic;
using UnityEngine;

public class Matrix : MonoBehaviour
{
    [SerializeField] private float period = 4f; // time for one complete cycle in seconds
    [SerializeField] private short matrixLen = 10;

    public bool isInRotating = true;

    private Vector3 initialPosition;
    private static GameObject[] matrix;
    private static readonly Color[] colors = { Color.red, Color.blue, Color.green };
    private static readonly short colorsLen = (short)colors.Length;
    private static readonly Dictionary<string, Transform> matrixDictionary = new();

    void Awake()
    {
        matrix = new GameObject[matrixLen];
        for (short i = 0; i < matrixLen; i++)
            matrix[i] = (GameObject)Resources.Load($"Prefabs/Digits/{i}");

        initialPosition = transform.position;
        matrixLen = (short)matrix.Length;
        GenMatrix(matrixLen, matrixLen, matrixLen);
    }
    void Update()
    {
        if (isInRotating) RotateMatrix(matrixLen, matrixLen, matrixLen);
    }

    public void GenMatrix(short x_limit, short y_limit, short z_limit)
    {
        for (short z = 0; z < z_limit; z++)
            for (short y = 0; y < y_limit; y++)
                for (short x = 0; x < x_limit; x++)
                {
                    var instance = Instantiate(
                        matrix[(x + y + z) % matrixLen],
                        new Vector3(x - 4.5f, -y + 4.0f, z - 4.5f),
                        Quaternion.Euler(Vector3.zero)
                        );
                    instance.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
                    instance.name = $"{z}{y}{x}";
                    instance.GetComponent<Renderer>().material.color = colors[z % colorsLen];
                    instance.transform.parent = transform;
                    matrixDictionary[$"{z}{y}{x}"] = instance.transform;
                }
    }

    void RotateMatrix(short x_limit, short y_limit, short z_limit)
    {
        float time = Time.time;
        transform.Rotate(Vector3.up, .5f);
        float displacement = 7.0f + Mathf.Sin(2f * Mathf.PI * time / period);
        transform.position = initialPosition + new Vector3(0f, displacement, 0f);

        for (short z = 0; z < z_limit; z++)
            for (short y = 0; y < y_limit; y++)
                for (short x = 0; x < x_limit; x++)
                {
                    matrixDictionary[$"{z}{y}{x}"].Rotate(Vector3.down, .5f);
                }
    }

    public void DestroyMatrix(short x_limit, short y_limit, short z_limit)
    {
        for (short z = 0; z < z_limit; z++)
            for (short y = 0; y < y_limit; y++)
                for (short x = 0; x < x_limit; x++)
                    Destroy(matrixDictionary[$"{z}{y}{x}"].gameObject);
    }
}
