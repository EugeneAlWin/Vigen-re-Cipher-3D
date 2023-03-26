using static STATES;
using static ENUMS;
using UnityEngine;
using UnityEngine.UI;

internal class MainPanelInputsChecker : AbstractInputsChecker
{
    public Toggle latToggle, cyrToggle;
    private readonly float waitTime = .5f;
    private float timer = 0.0f;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer <= waitTime) return;
        switch (CURRENT_ALPHABET)
        {
            case ALPHABETS.LATIN:
                latToggle.isOn = true;
                cyrToggle.isOn = false;
                break;
            case ALPHABETS.CYRILLIC:
                latToggle.isOn = false;
                cyrToggle.isOn = true;
                break;
            default:
                break;
        }
        timer = 0;
    }
}

