using System.Collections;
using TMPro;
using UnityEngine;
using static ENUMS;
using static STATES;
public class ExaminePanelInputsChecker : AbstractInputsChecker
{
    [SerializeField] private TMP_Text result;
    internal override void EncodeClick()
    {
        CURRENT_EXAMINE_ACTION = ACTIONS.ENCODING;
        EXAMINE_MESSAGE = CleanUp(CURRENT_MESSAGE).Replace(" ", "").ToLower();
        EXAMINE_KEY = CleanUp(CURRENT_KEY).ToLower();
        EXAMINE_DEPTH = int.Parse(CleanUp(CURRENT_DEPTH));
        EXAMINE_STEP = int.Parse(CleanUp(CURRENT_STEP));
        EXAMINE_DIRECTION = CURRENT_DIRECTION;
        Controller.HowItWorksDelegate?.Invoke(STEPS.SECOND, ACTIONS.ENCODING);
        StartCoroutine(SetResult("f"));
        //base.EncodeClick();
    }

    internal override void DecodeClick()
    {
        CURRENT_EXAMINE_ACTION = ACTIONS.DECODING;
        EXAMINE_MESSAGE = CleanUp(CURRENT_MESSAGE).Replace(" ", "").ToLower();
        EXAMINE_KEY = CleanUp(CURRENT_KEY).ToLower();
        EXAMINE_DEPTH = int.Parse(CleanUp(CURRENT_DEPTH));
        EXAMINE_STEP = int.Parse(CleanUp(CURRENT_STEP));
        EXAMINE_DIRECTION = CURRENT_DIRECTION;
        EXAMINE_MESSAGE_LETTER = "Z";
        Controller.HowItWorksDelegate?.Invoke(STEPS.SECOND, ACTIONS.DECODING);
        StartCoroutine(SetResult("f"));
    }

    public IEnumerator SetResult(string str)
    {
        while (true)
        {
            Debug.Log("Running");
            if (CURRENT_EXAMINE_STEP == STEPS.NONE) break;
            result.text = EXAMINE_MESSAGE_LETTER;
            yield return null;
        }
    }
}

