using TMPro;
using UnityEngine;
using static ENUMS;
using static STATES;
public class ExaminePanelInputsChecker : AbstractInputsChecker
{
    [SerializeField] private TMP_Text result;
    private CipherVector vect;
    internal override void EncodeClick()
    {
        Controller.studyModeChanged += SetResult;
        EXAMINE_CURRENT_CHAR_POSITION = 0;
        CURRENT_EXAMINE_ACTION = ACTIONS.ENCODING;
        EXAMINE_MESSAGE = CleanUp(CURRENT_MESSAGE).Replace(" ", "").ToLower();
        EXAMINE_KEY = CleanUp(CURRENT_KEY).ToLower();
        EXAMINE_DEPTH = int.Parse(CleanUp(CURRENT_DEPTH));
        EXAMINE_STEP = int.Parse(CleanUp(CURRENT_STEP));
        EXAMINE_DIRECTION = CURRENT_DIRECTION;
        Controller.studyModeChanged?.Invoke(STEPS.SECOND, ACTIONS.ENCODING);
    }

    internal override void DecodeClick()
    {
        Controller.studyModeChanged += SetResult;
        EXAMINE_CURRENT_CHAR_POSITION = 0;
        CURRENT_EXAMINE_ACTION = ACTIONS.DECODING;
        EXAMINE_MESSAGE = CleanUp(CURRENT_MESSAGE).Replace(" ", "").ToLower() + '\0';
        EXAMINE_KEY = CleanUp(CURRENT_KEY).ToLower();
        EXAMINE_DEPTH = int.Parse(CleanUp(CURRENT_DEPTH));
        EXAMINE_STEP = int.Parse(CleanUp(CURRENT_STEP));
        EXAMINE_DIRECTION = CURRENT_DIRECTION;
        Controller.studyModeChanged?.Invoke(STEPS.SECOND, ACTIONS.DECODING);
    }

    public void SetResult(STEPS newStep, ACTIONS newAction)
    {
        switch (CURRENT_EXAMINE_STEP)
        {
            case STEPS.NONE:
                break;
            case STEPS.SECOND:
                EXAMINE_CURRENT_LETTER = EXAMINE_MESSAGE[EXAMINE_CURRENT_CHAR_POSITION];
                vect = new CipherVector(
                    EXAMINE_CURRENT_LETTER.ToString(),
                    EXAMINE_KEY[EXAMINE_CURRENT_CHAR_POSITION % EXAMINE_KEY.Length].ToString(),
                    0, EXAMINE_DIRECTION, 0, CURRENT_ALPHABET);


                EXAMINE_CODED_LETTER = Algorithm.Encode(vect);
                Controller.OnCcodedCharChanged?.Invoke();
                Controller.cipherVectorChanged?.Invoke(vect);
                break;
            case STEPS.THIRD:
                vect = new CipherVector(
                    EXAMINE_CURRENT_LETTER.ToString(),
                    EXAMINE_KEY[EXAMINE_CURRENT_CHAR_POSITION % EXAMINE_KEY.Length].ToString(),
                    EXAMINE_DEPTH, EXAMINE_DIRECTION, 0, CURRENT_ALPHABET);
                EXAMINE_CODED_LETTER = Algorithm.Encode(vect);
                Controller.OnCcodedCharChanged?.Invoke();
                Controller.cipherVectorChanged?.Invoke(vect);
                break;
            case STEPS.FOURTH:
                vect = new CipherVector(
                     EXAMINE_CURRENT_LETTER.ToString(),
                     EXAMINE_KEY[EXAMINE_CURRENT_CHAR_POSITION % EXAMINE_KEY.Length].ToString(),
                     EXAMINE_DEPTH, EXAMINE_DIRECTION, EXAMINE_STEP, CURRENT_ALPHABET);
                EXAMINE_CODED_LETTER = Algorithm.Encode(vect);
                Controller.OnCcodedCharChanged?.Invoke();
                Controller.cipherVectorChanged?.Invoke(vect);
                result.text += EXAMINE_CODED_LETTER;
                break;
            case STEPS.FIFTH:
                break;
            default: break;
        }
    }
}

