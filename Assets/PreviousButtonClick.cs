using UnityEngine;
using UnityEngine.EventSystems;
using static STATES;
using static ENUMS;

public class PreviousButtonClick : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        CURRENT_EXAMINE_STEP -= 1;
        if (CURRENT_EXAMINE_STEP == STEPS.FIRST)
        {
            CURRENT_EXAMINE_STEP = STEPS.FIFTH;
            EXAMINE_CURRENT_CHAR_POSITION--;
            if (EXAMINE_CURRENT_CHAR_POSITION == -1)
            {
                EXAMINE_CURRENT_CHAR_POSITION++;
                Controller.studyModeChanged?.Invoke(STEPS.FIRST, ACTIONS.NONE);
                return;
            }
        }
        Controller.studyModeChanged?.Invoke(CURRENT_EXAMINE_STEP, CURRENT_EXAMINE_ACTION);
    }
}