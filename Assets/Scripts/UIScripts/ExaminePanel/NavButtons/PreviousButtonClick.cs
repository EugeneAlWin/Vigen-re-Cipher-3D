using UnityEngine;
using UnityEngine.EventSystems;
using static ENUMS;
using static STATES;

public class PreviousButtonClick : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        STUDY_CURRENT_STEP -= 1;
        if (STUDY_CURRENT_STEP == STEPS.FIRST)
        {
            STUDY_CURRENT_STEP = STEPS.FIFTH;
            STUDY_CURRENT_CHAR_POSITION--;
            if (STUDY_CURRENT_CHAR_POSITION == -1)
            {
                STUDY_CURRENT_CHAR_POSITION++;
                Controller.onStudyModeChanged?.Invoke(STEPS.FIRST, ACTIONS.NONE);
                return;
            }
        }
        Controller.onStudyModeChanged?.Invoke(STUDY_CURRENT_STEP, STUDY_CURRENT_ACTION);
    }
}