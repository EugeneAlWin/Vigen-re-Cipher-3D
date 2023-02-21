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
    private string currentMessage="", currentKey="", 
        currentDepth= $"<color=#fff>1</color>", 
        currentStep= $"<color=#fff>1</color>";
    private Directions currentDirection = Directions.Right;
    private readonly string[] unitysTrash = new string[] { "<color=#fff>", "</color>" };
    private enum Directions: byte
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
        //---
        depthInput.text= currentDepth;
        directionDropdown.value = (byte)currentDirection;
        stepInput.text= currentStep;
    }

    private void Update()
    {
        latToggle.isOn = isLatSelected;
        cyrToggle.isOn = !isLatSelected;
    }
    private void MessageValueChanged()
    {
        string trimmedMessage = CanvasRegex.Remove(messageInput.text, unitysTrash);
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
        string trimmedKey = CanvasRegex.Remove(keyInput.text, unitysTrash); 
        if (
            CanvasRegex.IsMatch(
            trimmedKey.ToLower(),
            isLatSelected ? CanvasRegex.latKey : CanvasRegex.cyrKey
            ))
            currentKey = "<color=#fff>" + trimmedKey + "</color>";
        keyInput.text = currentKey;
    }
    private void DepthValueChanged()
    {
        string trimmedDepth = CanvasRegex.Remove(depthInput.text, unitysTrash);
        if (CanvasRegex.IsMatch(trimmedDepth, CanvasRegex.Depth) && trimmedDepth.Length<=2)
            currentDepth = "<color=#fff>" + trimmedDepth + "</color>";
        depthInput.text = currentDepth;
    }
    private void DirectionValueChanged()
    {
        currentDirection = (Directions)directionDropdown.value;
    }
    private void StepValueChanged()
    {
        string trimmedStep = CanvasRegex.Remove(stepInput.text,unitysTrash);
        if (CanvasRegex.IsMatch(trimmedStep, CanvasRegex.Step) && trimmedStep.Length <= 2)
            currentStep = "<color=#fff>" + trimmedStep + "</color>";
        stepInput.text = currentStep;
    }
    private void LatValueChanged()
    {
        isLatSelected = true;
        messageInput.text = "";
        keyInput.text = "";
        
    }
    private void CyrValueChanged()
    {
        isLatSelected = false; 
        messageInput.text = "";
        keyInput.text = "";
    }
    private void DecodeClick()
    {
    }

    private void EncodeClick()
    {

    }
}

internal static class CanvasRegex
{
    internal static readonly Regex latMessage = new(@"^[a-z]*$");
    internal static readonly Regex cyrMessage = new(@"^[à-ÿ]*$");
    internal static readonly Regex latKey= new(@"^[a-z]*$");
    internal static readonly Regex cyrKey= new(@"^[à-ÿ]*$");
    internal static readonly Regex Depth= new(@"^[0-9]*$");
    internal static readonly Regex Step= new(@"^[0-9]*$");
    internal static bool IsMatch(string text, Regex regex) => regex.IsMatch(text);
    internal static string Remove(string text, string[] whatToRemove)
    {
        string newStr = text;
        foreach (var str in whatToRemove)
            newStr=newStr.Replace(str, "");
        return newStr;
    }
}
