using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static ENUMS;
using static STATES;
public class ExaminePanelInputsChecker : AbstractInputsChecker
{
    [SerializeField] private TMP_Text result;
    private CipherVector vect;
    [SerializeField] private Button nextButton;

    private void Update()
    {
        nextButton.gameObject.SetActive(STUDY_CURRENT_STEP != STEPS.SIXTH);
    }

    internal override void EncodeClick()
    {
        STUDY_CURRENT_CHAR_POSITION = 0;
        STUDY_CURRENT_ACTION = ACTIONS.ENCODING;
        STUDY_MESSAGE = CleanUp(CURRENT_MESSAGE).Replace(" ", "").ToLower();
        STUDY_KEY = CleanUp(CURRENT_KEY).ToLower();
        STUDY_DEPTH = int.Parse(CleanUp(CURRENT_DEPTH));
        STUDY_STEP = int.Parse(CleanUp(CURRENT_STEP));
        STUDY_DIRECTION = CURRENT_DIRECTION;
        Controller.onStudyModeChanged?.Invoke(STEPS.SECOND, ACTIONS.ENCODING);
    }

    internal override void DecodeClick()
    {
        STUDY_CURRENT_CHAR_POSITION = 0;
        STUDY_CURRENT_ACTION = ACTIONS.DECODING;
        STUDY_MESSAGE = CleanUp(CURRENT_MESSAGE).Replace(" ", "").ToLower();
        STUDY_KEY = CleanUp(CURRENT_KEY).ToLower();
        STUDY_DEPTH = int.Parse(CleanUp(CURRENT_DEPTH));
        STUDY_STEP = int.Parse(CleanUp(CURRENT_STEP));
        STUDY_DIRECTION = CURRENT_DIRECTION;
        Controller.onStudyModeChanged?.Invoke(STEPS.SECOND, ACTIONS.DECODING);
    }
    private readonly Dictionary<string, CipherVector> vectorsDict = new();
    internal override void SetResult(STEPS newStep, ACTIONS newAction)
    {
        switch (STUDY_CURRENT_STEP)
        {
            case STEPS.NONE:
                break;
            case STEPS.SECOND:
                STUDY_CURRENT_CHAR = STUDY_MESSAGE[STUDY_CURRENT_CHAR_POSITION];
                if (!vectorsDict.TryGetValue($"{STUDY_CURRENT_CHAR}{0}{STUDY_DIRECTION}{0}", out vect))
                {
                    vect = new CipherVector(
                    STUDY_CURRENT_CHAR.ToString(),
                    STUDY_KEY[STUDY_CURRENT_CHAR_POSITION % STUDY_KEY.Length].ToString(),
                    0, STUDY_DIRECTION, 0, CURRENT_ALPHABET);
                    vectorsDict.TryAdd($"{STUDY_CURRENT_CHAR}{vect.Depth}{STUDY_DIRECTION}{vect.Step}", vect);
                }
                STUDY_CODED_CHAR = Algorithm.Encode(vect);
                Controller.onCodedCharChanged?.Invoke();
                Controller.onCipherVectorChanged?.Invoke(vect);
                break;
            case STEPS.THIRD:
                if (!vectorsDict.TryGetValue($"{STUDY_CURRENT_CHAR}{STUDY_DEPTH}{STUDY_DIRECTION}{0}", out vect))
                {
                    vect = new CipherVector(
                    STUDY_CURRENT_CHAR.ToString(),
                    STUDY_KEY[STUDY_CURRENT_CHAR_POSITION % STUDY_KEY.Length].ToString(),
                    STUDY_DEPTH, STUDY_DIRECTION, 0, CURRENT_ALPHABET);
                    vectorsDict.TryAdd($"{STUDY_CURRENT_CHAR}{vect.Depth}{STUDY_DIRECTION}{vect.Step}", vect);
                }

                STUDY_CODED_CHAR = Algorithm.Encode(vect);
                Controller.onCodedCharChanged?.Invoke();
                Controller.onCipherVectorChanged?.Invoke(vect);
                break;
            case STEPS.FOURTH:
                if (!vectorsDict.TryGetValue($"{STUDY_CURRENT_CHAR}{STUDY_DEPTH}{STUDY_DIRECTION}{STUDY_STEP}", out vect))
                {
                    vect = new CipherVector(
                    STUDY_CURRENT_CHAR.ToString(),
                    STUDY_KEY[STUDY_CURRENT_CHAR_POSITION % STUDY_KEY.Length].ToString(),
                    STUDY_DEPTH, STUDY_DIRECTION, STUDY_STEP, CURRENT_ALPHABET);
                    vectorsDict.TryAdd($"{STUDY_CURRENT_CHAR}{vect.Depth}{STUDY_DIRECTION}{vect.Step}", vect);
                }

                STUDY_CODED_CHAR = Algorithm.Encode(vect);
                Controller.onCodedCharChanged?.Invoke();
                Controller.onCipherVectorChanged?.Invoke(vect);
                Controller.lightFourthStep?.Invoke(STUDY_CODED_CHAR, STUDY_KEY[STUDY_CURRENT_CHAR_POSITION % STUDY_KEY.Length].ToString());
                break;
            case STEPS.FIFTH:
                STUDY_CURRENT_CHAR = STUDY_MESSAGE[STUDY_CURRENT_CHAR_POSITION];
                if (!vectorsDict.TryGetValue($"{STUDY_CURRENT_CHAR}{STUDY_DEPTH}{STUDY_DIRECTION}{STUDY_STEP}", out vect))
                {
                    vect = new CipherVector(
                    STUDY_CURRENT_CHAR.ToString(),
                    STUDY_KEY[STUDY_CURRENT_CHAR_POSITION % STUDY_KEY.Length].ToString(),
                    STUDY_DEPTH, STUDY_DIRECTION, STUDY_STEP, CURRENT_ALPHABET);
                    vectorsDict.TryAdd($"{STUDY_CURRENT_CHAR}{vect.Depth}{STUDY_DIRECTION}{vect.Step}", vect);
                }

                STUDY_CODED_CHAR = Algorithm.Encode(vect);
                Controller.onCodedCharChanged?.Invoke();
                Controller.onCipherVectorChanged?.Invoke(vect);
                Controller.lightFourthStep?.Invoke(STUDY_CODED_CHAR, STUDY_KEY[STUDY_CURRENT_CHAR_POSITION % STUDY_KEY.Length].ToString());
                result.text += STUDY_CODED_CHAR;
                break;
            default: break;
        }
    }
}

