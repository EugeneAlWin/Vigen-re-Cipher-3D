using UnityEngine;
using UnityEngine.EventSystems;
using static ENUMS;
using static STATES;

public class HowIsItWorks : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        ExaminePanel.SetPanelVisibility(true);
        Controller.onStudyModeChanged?.Invoke(STEPS.FIRST, ACTIONS.NONE);
        switch (CURRENT_ALPHABET)
        {
            case ALPHABETS.LATIN:
                Camera.SetCameraPosition(Camera.LatinMatrixPosition, Camera.LatinMatrixRotation);
                break;
            case ALPHABETS.CYRILLIC:
                Camera.SetCameraPosition(Camera.CyrillicMatrixPosition, Camera.CyrillicMatrixRotation);
                break;
        }
    }
}
