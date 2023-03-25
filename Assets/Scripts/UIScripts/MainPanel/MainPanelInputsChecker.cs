using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static STATES;
using static ENUMS;

public partial class MainPanelInputsChecker : MonoBehaviour
{
    public TMP_InputField messageInput, keyInput, depthInput, stepInput, resultInput;
    public TMP_Dropdown directionDropdown;
    public Toggle latToggle, cyrToggle;
    public Button encode, decode;
    private static readonly string[] unitysTrash = new string[] { "<color=#fff>", "</color>" };

    private bool isLatSelected = false;
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
        depthInput.text = CURRENT_DEPTH;
        directionDropdown.value = (byte)CURRENT_DIRECTION;
        stepInput.text = CURRENT_STEP;
        //---
    }

    private void Update()
    {
        latToggle.isOn = isLatSelected;
        cyrToggle.isOn = !isLatSelected;
    }

    private void EncodeClick()
    {
        if (CleanUp(CURRENT_MESSAGE).Replace(" ", "").ToLower() == "") return;
        if (CleanUp(CURRENT_KEY).ToLower() == "") return;
        var cipherText = new CipherVector()
        {
            Message = CleanUp(CURRENT_MESSAGE).Replace(" ", "").ToLower(),
            Key = CleanUp(CURRENT_KEY).ToLower(),
            Depth = int.Parse(CleanUp(CURRENT_DEPTH)),
            Direction = (CURRENT_DIRECTION == DIRECTIONS.RIGHT || CURRENT_DIRECTION == DIRECTIONS.BOTTOM)
            ? 'R' : 'L',
            Step = int.Parse(CleanUp(CURRENT_STEP)),
            AlphabetType = isLatSelected ? "Lat" : "Cyr",
        };
        string encodedMessage = Algorithm.Encode(cipherText);
        resultInput.text = $"<color=#FFF>{encodedMessage}</color>";
    }
    private void DecodeClick()
    {
        var cipherText = new CipherVector()
        {
            Message = CleanUp(CURRENT_MESSAGE).Replace(" ", "").ToLower(),
            Key = CleanUp(CURRENT_KEY).ToLower(),
            Depth = int.Parse(CleanUp(CURRENT_DEPTH)),
            Direction = (
            CURRENT_DIRECTION == DIRECTIONS.RIGHT ||
            CURRENT_DIRECTION == DIRECTIONS.BOTTOM) ? 'R' : 'L',
            Step = int.Parse(CleanUp(CURRENT_STEP)),
            AlphabetType = isLatSelected ? "Lat" : "Cyr",
        };
        var decodedMessage = Algorithm.Decode(cipherText);
        resultInput.text = $"<color=#FFF>{decodedMessage}</color>";
    }
}