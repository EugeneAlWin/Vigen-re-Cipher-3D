using UnityEngine;
using UnityEngine.EventSystems;
using static ENUMS;
using static STATES;

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
                Controller.onStudyModeChanged?.Invoke(STEPS.SIXTH, CURRENT_EXAMINE_ACTION);
                return;
            }
        }
        Controller.onStudyModeChanged?.Invoke(CURRENT_EXAMINE_STEP, CURRENT_EXAMINE_ACTION);
    }
}