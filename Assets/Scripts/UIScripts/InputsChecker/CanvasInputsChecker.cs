using TMPro;
using UnityEngine;
using UnityEngine.UI;

public partial class CanvasInputsChecker : MonoBehaviour
{
    public TMP_InputField messageInput, keyInput, depthInput, stepInput, resultInput;
    public TMP_Dropdown directionDropdown;
    public Toggle latToggle, cyrToggle;
    public Button encode, decode;
    private static readonly string[] unitysTrash = new string[] { "<color=#fff>", "</color>" };

    private bool isLatSelected = false;
    private string currentMessage = "", currentKey = "",
        currentDepth = $"<color=#fff>1</color>",
        currentStep = $"<color=#fff>1</color>";
    private Directions currentDirection = Directions.Right;
    private enum Directions : byte
    {
        Right,
        Left,
        Top,
        Bottom,
    }

    private Algorithm algorithm;
    internal string CleanUp(string textToCleanUp)
    {
        string newStr = textToCleanUp;
        foreach (var str in unitysTrash)
            newStr = newStr.Replace(str, "");
        return newStr;
    }

    void Awake()
    {
        messageInput.onValueChanged.AddListener(delegate { MessageValueChanged(); });
        keyInput.onValueChanged.AddListener(delegate { KeyValueChanged(); });
        depthInput.onValueChanged.AddListener(delegate { DepthValueChanged(); });
        directionDropdown.onValueChanged.AddListener(delegate { DirectionValueChanged(); });
        stepInput.onValueChanged.AddListener(delegate { StepValueChanged(); });
        latToggle.onValueChanged.AddListener(delegate { LatValueChanged(); });
        cyrToggle.onValueChanged.AddListener(delegate { CyrValueChanged(); });
        encode.onClick.AddListener(delegate { EncodeClick(); });
        decode.onClick.AddListener(delegate { DecodeClick(); });
        //---
        depthInput.text = currentDepth;
        directionDropdown.value = (byte)currentDirection;
        stepInput.text = currentStep;
        //---
        algorithm = GetComponent<Algorithm>();
    }

    private void Update()
    {
        latToggle.isOn = isLatSelected;
        cyrToggle.isOn = !isLatSelected;
    }

    private void EncodeClick()
    {
        if (CleanUp(currentMessage).Replace(" ", "").ToLower() == "") return;
        if (CleanUp(currentKey).ToLower() == "") return;
        var cipherText = new CipherVector()
        {
            Message = CleanUp(currentMessage).Replace(" ", "").ToLower(),
            Key = CleanUp(currentKey).ToLower(),
            Depth = int.Parse(CleanUp(currentDepth)),
            Direction = (currentDirection == Directions.Right || currentDirection == Directions.Bottom)
            ? 'R' : 'L',
            Step = int.Parse(CleanUp(currentStep)),
            AlphabetType = isLatSelected ? "Lat" : "Cyr",
        };
        string encodedMessage = algorithm.Encode(cipherText);
        resultInput.text = $"<color=#FFF>{encodedMessage}</color>";
    }
    private void DecodeClick()
    {
        var cipherText = new CipherVector()
        {
            Message = CleanUp(currentMessage).Replace(" ", "").ToLower(),
            Key = CleanUp(currentKey).ToLower(),
            Depth = int.Parse(CleanUp(currentDepth)),
            Direction = (
            currentDirection == Directions.Right ||
            currentDirection == Directions.Bottom) ? 'R' : 'L',
            Step = int.Parse(CleanUp(currentStep)),
            AlphabetType = isLatSelected ? "Lat" : "Cyr",
        };
        var decodedMessage = algorithm.Decode(cipherText);
        resultInput.text = $"<color=#FFF>{decodedMessage}</color>";
    }
}