using UnityEngine;
using UnityEngine.EventSystems;
using static ENUMS;
using static STATES;

public class HowIsItWorks : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        ExaminePanel.SetPanelVisibility(true);
        Controller.HowItWorksDelegate?.Invoke(STEPS.FIRST, ACTIONS.NONE);
        switch (CURRENT_ALPHABET)
        {
            case ALPHABETS.LATIN:
                //Camera.SetCameraPosition(Camera.CyrillicMatrixPosition, Camera.CyrillicMatrixRotation);
                break;
            case ALPHABETS.CYRILLIC:
                Camera.SetCameraPosition(Camera.CyrillicMatrixPosition, Camera.CyrillicMatrixRotation);
                break;
        }
    }
}
