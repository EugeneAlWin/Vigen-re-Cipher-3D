using UnityEngine;

public class GenerateMatrix : MonoBehaviour
{
    [SerializeField]
    GameObject container;
    public int key = 3, depth = 0, step = 1;
    public string message = "0192837465";
    [SerializeField]
    GameObject[] matrixElements;
    const int alphLen = 10;
    readonly Color[] arrayOfColors = { Color.red, Color.blue, Color.green};
    readonly int[] alphabet = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    public int side = 1;
    string resstr, resstr2;


    int Encode(int message, int key, int depth, int alphLen)
    {
        int P = side == 1 ? step : - step;
        Debug.Log(P);
        var temp = (message + key + depth + P) % alphLen;
        if (temp < 0) temp = (temp + alphLen) % alphLen;
        int result = alphabet[temp];
        return result;
    }

    int Decode(int ciphertext, int key, int depth, int alphLen)
    {
        int P = side == 1 ? -step : step;
        var temp = (ciphertext + P - key - depth) % alphLen;
        if (temp < 0) temp = (temp + alphLen) % alphLen;
        int result = alphabet[temp];
        return result;
    }

    void Start()
    {
        foreach (var element in message) {
            resstr+=$"{Encode(element-'0', key, depth, alphLen)}";
        }
        Application.targetFrameRate = 30;
        GenMatrix(matrixElements.Length, matrixElements.Length);
        Debug.Log($"Сообщение: {message}");
        Debug.Log($"Шифртекст: {resstr}");
        foreach (var element in resstr)
        {
            resstr2 += $"{Decode(element-'0', key, depth, alphLen)}";
        }
        Debug.Log($"Расшифрованное сообщение: {resstr2}");
        
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
        transform.position = new Vector3(0, 0, 0);

        for (sbyte z = 0; z < z_limit; z++)
        {
            for (sbyte y = 0; y < matrixElements.Length; y++)
            {
                for (sbyte x = 0; x < x_limit; x++)
                {
                    var instance = Instantiate(
                        matrixElements[(y + x + z) % matrixElements.Length],
                        new Vector3(x - 7.5f, -y + 4.0f, z - 2.5f),
                        Quaternion.Euler(Vector3.zero)
                        );

                    instance.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
                    instance.name = $"{z}{y}{x}";
                    instance.GetComponent<Renderer>().material.color = arrayOfColors[z % arrayOfColors.Length];
                    instance.transform.parent = transform;
                }
            }
        }
        transform.position = new Vector3(0, 5.63f, 0);
    }
}
