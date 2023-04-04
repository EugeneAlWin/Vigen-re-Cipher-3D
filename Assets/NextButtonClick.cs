using UnityEngine;
using UnityEngine.EventSystems;
using static STATES;
using static ENUMS;

public class NextButtonClick : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        CURRENT_EXAMINE_STEP += 1;
        if (CURRENT_EXAMINE_STEP == STEPS.SIXTH)
        {
            CURRENT_EXAMINE_STEP = STEPS.SECOND;
            EXAMINE_CURRENT_CHAR_POSITION++;
            if (EXAMINE_CURRENT_CHAR_POSITION == EXAMINE_MESSAGE.Length)
            {
                EXAMINE_CURRENT_CHAR_POSITION--;
                Controller.studyModeChanged?.Invoke(STEPS.SIXTH, CURRENT_EXAMINE_ACTION);
                return;
            }
        }
        Controller.studyModeChanged?.Invoke(CURRENT_EXAMINE_STEP, CURRENT_EXAMINE_ACTION);
    }
}