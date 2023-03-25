using UnityEngine;
using UnityEngine.EventSystems;
using static ENUMS;

public class HowIsItWorks : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        ExaminePanel.SetPanelVisibility(true);
        Controller.HowItWorksDelegate?.Invoke(STEPS.FIRST, ACTIONS.NONE);
        Camera.SetCameraPosition(Camera.CyrillicMatrixPosition, Camera.CyrillicMatrixRotation);
    }
}
