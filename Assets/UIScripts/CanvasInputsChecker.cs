using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasInputsChecker : MonoBehaviour
{
    public TMP_InputField messageInput, keyInput, depthInput, stepInput;
    public TMP_Dropdown directionDropdown;
    public Toggle latToggle, cyrToggle;
    public Button encode, decode;

    private bool isLatSelected = false;
    private string currentMessage="", currentKey="", currentDepth="", currentDirection="", currentStep="";
    private enum directions
    {
        Right,
        Left,
        Top,
        Bottom,
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
    }

    private void Update()
    {
        latToggle.isOn = isLatSelected;
        cyrToggle.isOn = !isLatSelected;
    }
    private void MessageValueChanged()
    {
        string trimmedMessage = messageInput.text.Replace("<color=#fff>", "").Replace("</color>","");
        if(
            CanvasRegex.IsMatch(
            trimmedMessage.ToLower().Replace(" ", ""),
            isLatSelected? CanvasRegex.latMessage:CanvasRegex.cyrMessage
            ))
                currentMessage = "<color=#fff>" + trimmedMessage + "</color>";
        messageInput.text = currentMessage;
    }
    private void KeyValueChanged()
    {
        string trimmedMessage = keyInput.text.Replace("<color=#fff>", "").Replace("</color>", "");
        if (
            CanvasRegex.IsMatch(
            trimmedMessage.ToLower(),
            isLatSelected ? CanvasRegex.latKey : CanvasRegex.cyrKey
            ))
            currentKey = "<color=#fff>" + trimmedMessage + "</color>";
        keyInput.text = currentKey;
    }
    private void DepthValueChanged()
    {
        Debug.Log("3");
    }
    private void DirectionValueChanged()
    {
        Debug.Log(directionDropdown.value);
    }
    private void StepValueChanged()
    {
        Debug.Log("5");
    }
    private void LatValueChanged()
    {
        isLatSelected = true;
    }
    private void CyrValueChanged()
    {
        isLatSelected = false;
    }
    private void DecodeClick()
    {
    }

    private void EncodeClick()
    {
        Debug.Log("Encode");
    }
}

public static class CanvasRegex
{
    public static readonly Regex latMessage = new(@"^[a-z]*$");
    public static readonly Regex cyrMessage = new(@"^[à-ÿ]*$");
    public static readonly Regex latKey= new(@"^[a-z]*$");
    public static readonly Regex cyrKey= new(@"^[à-ÿ]*$");
    public static readonly Regex Depth= new(@"^[0-9]$");
    public static readonly Regex Direction= new(@"^(R|L|T|B)$");
    public static readonly Regex Step= new(@"^[a-z]*$");
    public static bool IsMatch(string text, Regex regex) => regex.IsMatch(text);
}
