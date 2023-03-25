using UnityEngine;
using static STATES;
using static ENUMS;

public partial class MainPanelInputsChecker : MonoBehaviour
{
    private void MessageValueChanged()
    {
        string trimmedMessage = CleanUp(messageInput.text);
        if (
            InputRegex.IsMatch(
            trimmedMessage.ToLower().Replace(" ", ""),
            CURRENT_ALPHABET == ALPHABETS.LATIN ? InputRegex.latMessage : InputRegex.cyrMessage
            ))
            CURRENT_MESSAGE = "<color=#fff>" + trimmedMessage + "</color>";
        messageInput.text = CURRENT_MESSAGE;
    }
    private void KeyValueChanged()
    {
        string trimmedKey = CleanUp(keyInput.text);
        if (
            InputRegex.IsMatch(
            trimmedKey.ToLower(),
            CURRENT_ALPHABET == ALPHABETS.LATIN ? InputRegex.latKey : InputRegex.cyrKey
            ))
            CURRENT_KEY = "<color=#fff>" + trimmedKey + "</color>";
        keyInput.text = CURRENT_KEY;
    }
    private void DepthValueChanged()
    {
        string trimmedDepth = CleanUp(depthInput.text);
        if (InputRegex.IsMatch(trimmedDepth, InputRegex.Depth) && trimmedDepth.Length <= 2)
            CURRENT_DEPTH = "<color=#fff>" + trimmedDepth + "</color>";
        depthInput.text = CURRENT_DEPTH;
    }
    private void DirectionValueChanged()
    {
        CURRENT_DIRECTION = (DIRECTIONS)directionDropdown.value;
    }
    private void StepValueChanged()
    {
        string trimmedStep = CleanUp(stepInput.text);
        if (InputRegex.IsMatch(trimmedStep, InputRegex.Step) && trimmedStep.Length <= 2)
            CURRENT_STEP = "<color=#fff>" + trimmedStep + "</color>";
        stepInput.text = CURRENT_STEP;
    }
    private void LatValueChanged()
    {
        CURRENT_ALPHABET = ALPHABETS.LATIN;
        messageInput.text = "";
        keyInput.text = "";

    }
    private void CyrValueChanged()
    {
        CURRENT_ALPHABET = ALPHABETS.CYRILLIC;
        messageInput.text = "";
        keyInput.text = "";
    }
}

