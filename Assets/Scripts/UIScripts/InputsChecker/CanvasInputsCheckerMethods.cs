using UnityEngine;

public partial class CanvasInputsChecker : MonoBehaviour
{
    private void MessageValueChanged()
    {
        string trimmedMessage = CleanUp(messageInput.text);
        if (
            InputRegex.IsMatch(
            trimmedMessage.ToLower().Replace(" ", ""),
            isLatSelected ? InputRegex.latMessage : InputRegex.cyrMessage
            ))
            currentMessage = "<color=#fff>" + trimmedMessage + "</color>";
        messageInput.text = currentMessage;
    }
    private void KeyValueChanged()
    {
        string trimmedKey = CleanUp(keyInput.text);
        if (
            InputRegex.IsMatch(
            trimmedKey.ToLower(),
            isLatSelected ? InputRegex.latKey : InputRegex.cyrKey
            ))
            currentKey = "<color=#fff>" + trimmedKey + "</color>";
        keyInput.text = currentKey;
    }
    private void DepthValueChanged()
    {
        string trimmedDepth = CleanUp(depthInput.text);
        if (InputRegex.IsMatch(trimmedDepth, InputRegex.Depth) && trimmedDepth.Length <= 2)
            currentDepth = "<color=#fff>" + trimmedDepth + "</color>";
        depthInput.text = currentDepth;
    }
    private void DirectionValueChanged()
    {
        currentDirection = (Directions)directionDropdown.value;
    }
    private void StepValueChanged()
    {
        string trimmedStep = CleanUp(stepInput.text);
        if (InputRegex.IsMatch(trimmedStep, InputRegex.Step) && trimmedStep.Length <= 2)
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
}

