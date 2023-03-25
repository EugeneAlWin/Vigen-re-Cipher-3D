using UnityEngine;
using UnityEngine.EventSystems;
using static ENUMS;

public class CloseExaminePanel : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        ExaminePanel.SetPanelVisibility(false);
        Controller.HowItWorksDelegate?.Invoke(STEPS.NONE, ACTIONS.NONE);
        Camera.SetCameraPosition(Camera.DigitalMatrixPosition, Camera.DigitalMatrixRotation);
    }
}
