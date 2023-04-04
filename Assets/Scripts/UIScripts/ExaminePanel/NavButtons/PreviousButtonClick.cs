using UnityEngine;
using UnityEngine.EventSystems;
using static ENUMS;
using static STATES;

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
                Controller.onStudyModeChanged?.Invoke(STEPS.FIRST, ACTIONS.NONE);
                return;
            }
        }
        Controller.onStudyModeChanged?.Invoke(CURRENT_EXAMINE_STEP, CURRENT_EXAMINE_ACTION);
    }
}