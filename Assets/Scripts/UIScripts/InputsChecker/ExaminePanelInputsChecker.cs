using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ENUMS;
using static STATES;

public class ExaminePanelInputsChecker : AbstractInputsChecker
{
    private CipherVector vect;
    [SerializeField] private Button nextButton;
    [SerializeField] private TMP_Text currentMessageChar, currentKeyChar;
    [SerializeField] private TMP_InputField fullMessage, fullKey, result;
    private readonly Dictionary<string, CipherVector> vectorsDict = new();

    private void Update() => nextButton.gameObject.SetActive(STUDY_CURRENT_STEP != STEPS.SIXTH);
    private void GetInputs(ACTIONS action)
    {
        result.text = "";
        STUDY_CURRENT_CHAR_POSITION = 0;
        STUDY_CURRENT_ACTION = action;
        STUDY_MESSAGE = CleanUp(CURRENT_MESSAGE).Replace(" ", "").ToLower();
        STUDY_KEY = CleanUp(CURRENT_KEY).ToLower();
        STUDY_DEPTH = int.Parse(CleanUp(CURRENT_DEPTH));
        STUDY_STEP = int.Parse(CleanUp(CURRENT_STEP));
        STUDY_DIRECTION = CURRENT_DIRECTION;
        Controller.onStudyModeChanged?.Invoke(STEPS.SECOND, action);
        Controller.onCodedCharChanged?.Invoke();
    }
    internal override void EncodeClick() => GetInputs(ACTIONS.ENCODING);
    internal override void DecodeClick() => GetInputs(ACTIONS.DECODING);

    internal override void CodedCharChanged()
    {
        if (STUDY_CURRENT_STEP == STEPS.SECOND || STUDY_CURRENT_STEP == STEPS.FIFTH)
        {
            fullMessage.text = "<color=#fff>" + STUDY_MESSAGE + "</color>";
            fullKey.text = "<color=#fff>" + STUDY_KEY + "</color>";
        }
        currentMessageChar.text = STUDY_CURRENT_CHAR;
        currentKeyChar.text = STUDY_KEY_CHAR;
    }
    private void SetNewMessageAndKeyChar()
    {
        STUDY_CURRENT_CHAR = STUDY_MESSAGE[STUDY_CURRENT_CHAR_POSITION].ToString();
        STUDY_KEY_CHAR = STUDY_KEY[STUDY_CURRENT_CHAR_POSITION % STUDY_KEY.Length].ToString();
    }

    private CipherVector TryGetVector()
    {
        var buffDepth = STUDY_CURRENT_STEP > STEPS.SECOND ? STUDY_DEPTH : 0;
        var buffStep = STUDY_CURRENT_STEP > STEPS.THIRD ? STUDY_STEP : 0;
        if (!vectorsDict.TryGetValue($"{STUDY_CURRENT_CHAR}{buffDepth}{STUDY_DIRECTION}{buffStep}", out vect))
        {
            vect = new CipherVector(
            STUDY_CURRENT_CHAR,
            STUDY_KEY_CHAR,
            buffDepth,
            STUDY_DIRECTION,
            buffStep,
            CURRENT_ALPHABET);
            vectorsDict.TryAdd($"{STUDY_CURRENT_CHAR}{vect.Depth}{STUDY_DIRECTION}{vect.Step}", vect);
        }
        return vect;
    }
    internal override void SetResult(STEPS newStep, ACTIONS newAction)
    {
        if (STUDY_CURRENT_STEP == STEPS.FIRST || STUDY_CURRENT_STEP == STEPS.NONE || STUDY_CURRENT_STEP == STEPS.SIXTH) return;
        SetNewMessageAndKeyChar();
        vect = TryGetVector();
        STUDY_CODED_CHAR = STUDY_CURRENT_ACTION == ACTIONS.ENCODING ? Algorithm.Encode(vect) : Algorithm.Decode(vect);
        Controller.onCodedCharChanged?.Invoke();
        Controller.onCipherVectorChanged?.Invoke(vect);

        switch (STUDY_CURRENT_STEP)
        {
            case STEPS.FOURTH:
                Controller.lightFourthStep?.Invoke(STUDY_CODED_CHAR, STUDY_KEY_CHAR);
                break;
            case STEPS.FIFTH:
                Controller.lightFourthStep?.Invoke(STUDY_CODED_CHAR, STUDY_KEY_CHAR);

                vect = new CipherVector(
                    STUDY_MESSAGE[..(STUDY_CURRENT_CHAR_POSITION + 1)],
                    STUDY_KEY, STUDY_DEPTH, STUDY_DIRECTION, STUDY_STEP, CURRENT_ALPHABET);
                result.text = "<color=#fff>" + (STUDY_CURRENT_ACTION == ACTIONS.ENCODING ?
                    Algorithm.Encode(vect) :
                    Algorithm.Decode(vect)) + "</color>";
                break;
            default: break;
        }
    }
}