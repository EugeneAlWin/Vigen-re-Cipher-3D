using UnityEngine;
using UnityEngine.EventSystems;
using static ENUMS;
using static STATES;

public class NextButtonClick : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        STUDY_CURRENT_STEP += 1;
        if (STUDY_CURRENT_STEP == STEPS.SIXTH)
        {
            STUDY_CURRENT_STEP = STEPS.SECOND;
            STUDY_CURRENT_CHAR_POSITION++;
            if (STUDY_CURRENT_CHAR_POSITION == STUDY_MESSAGE.Length)
            {
                STUDY_CURRENT_CHAR_POSITION--;
                Controller.onStudyModeChanged?.Invoke(STEPS.SIXTH, STUDY_CURRENT_ACTION);
                return;
            }
        }
        Controller.onStudyModeChanged?.Invoke(STUDY_CURRENT_STEP, STUDY_CURRENT_ACTION);
    }
}