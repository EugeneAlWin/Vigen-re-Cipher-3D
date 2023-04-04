using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static STATES;

public abstract partial class AbstractInputsChecker : MonoBehaviour
{
    public TMP_InputField messageInput, keyInput, depthInput, stepInput, resultInput;
    public TMP_Dropdown directionDropdown;
    [field: SerializeField] public Button Encode { get; set; }
    [field: SerializeField] public Button Decode { get; set; }
    private static readonly string[] unitysTrash = new string[] { "<color=#fff>", "</color>" };

    internal string CleanUp(string textToCleanUp)
    {
        string newStr = textToCleanUp;
        foreach (var str in unitysTrash)
            newStr = newStr.Replace(str, "");
        return newStr;
    }

    internal virtual void Awake()
    {
        messageInput.onValueChanged.AddListener(delegate { MessageValueChanged(); });
        keyInput.onValueChanged.AddListener(delegate { KeyValueChanged(); });
        depthInput.onValueChanged.AddListener(delegate { DepthValueChanged(); });
        directionDropdown.onValueChanged.AddListener(delegate { DirectionValueChanged(); });
        stepInput.onValueChanged.AddListener(delegate { StepValueChanged(); });
        Encode.onClick.AddListener(delegate { EncodeClick(); });
        Decode.onClick.AddListener(delegate { DecodeClick(); });
        //---
        depthInput.text = CURRENT_DEPTH;
        directionDropdown.value = (byte)CURRENT_DIRECTION;
        stepInput.text = CURRENT_STEP;
        //---
    }

    internal virtual void EncodeClick()
    {
        if (CleanUp(CURRENT_MESSAGE).Replace(" ", "").ToLower() == "") return;
        if (CleanUp(CURRENT_KEY).ToLower() == "") return;
        var cipherText = new CipherVector()
        {
            Message = CleanUp(CURRENT_MESSAGE).Replace(" ", "").ToLower(),
            Key = CleanUp(CURRENT_KEY).ToLower(),
            Depth = int.Parse(CleanUp(CURRENT_DEPTH)),
            Direction = CURRENT_DIRECTION,
            Step = int.Parse(CleanUp(CURRENT_STEP)),
            AlphabetType = CURRENT_ALPHABET
        };
        string encodedMessage = Algorithm.Encode(cipherText);
        resultInput.text = $"<color=#FFF>{encodedMessage}</color>";
    }
    internal virtual void DecodeClick()
    {
        var cipherText = new CipherVector()
        {
            Message = CleanUp(CURRENT_MESSAGE).Replace(" ", "").ToLower(),
            Key = CleanUp(CURRENT_KEY).ToLower(),
            Depth = int.Parse(CleanUp(CURRENT_DEPTH)),
            Direction = CURRENT_DIRECTION,
            Step = int.Parse(CleanUp(CURRENT_STEP)),
            AlphabetType = CURRENT_ALPHABET
        };
        var decodedMessage = Algorithm.Decode(cipherText);
        resultInput.text = $"<color=#FFF>{decodedMessage}</color>";
    }
}