using UnityEngine;

public class Matrix : MonoBehaviour
{
    // Set the amplitude and period of the sin wave
    [SerializeField]
    private float period = 4f; // time for one complete cycle in seconds
    // Store the initial position of the container
    private Vector3 initialPosition;
    [SerializeField]
    GameObject container;

    [SerializeField]
    GameObject axiscontainer;

    [SerializeField]
    GameObject[] matrixElements;

    readonly Color[] arrayOfColors = { Color.red, Color.blue, Color.green };

    public string message = "0192837465", resstr, resstr2;

    void Awake()
    {
        initialPosition = transform.position;
        axiscontainer.SetActive(false);
        GenMatrix(matrixElements.Length, matrixElements.Length);
    }
    void Update()
    {
        RotateMatrix();
    }

    void RotateMatrix()
    {
        container.transform.Rotate(Vector3.up, .5f);
        float time = Time.time;
        float displacement = 7.0f + Mathf.Sin(2f * Mathf.PI * time / period);


        int childCount = gameObject.transform.childCount;
        transform.position = initialPosition + new Vector3(0f, displacement, 0f);
        for (int i = 0; i < childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.transform.Rotate(Vector3.down, .5f);
        }
    }

    public void DestroyMatrix()
    {
        int childCount = gameObject.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Destroy(gameObject.transform.GetChild(i).gameObject);
        }
    }

    public void GenMatrix(int x_limit, int z_limit)
    {
        for (sbyte z = 0; z < z_limit; z++)
        {
            for (sbyte y = 0; y < matrixElements.Length; y++)
            {
                for (sbyte x = 0; x < x_limit; x++)
                {
                    var instance = Instantiate(
                        matrixElements[(y + x + z) % matrixElements.Length],
                        new Vector3(x - 4.5f, -y + 4.0f, z - 4.5f),
                        Quaternion.Euler(Vector3.zero)
                        );

                    instance.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
                    instance.name = $"{z}{y}{x}";
                    instance.GetComponent<Renderer>().material.color = arrayOfColors[z % arrayOfColors.Length];
                    instance.transform.parent = transform;
                }
            }
        }
    }

}
