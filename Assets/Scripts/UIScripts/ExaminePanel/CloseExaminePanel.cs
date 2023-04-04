using UnityEngine;
using UnityEngine.EventSystems;
using static ENUMS;

public class CloseExaminePanel : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Controller.studyModeChanged?.Invoke(STEPS.NONE, ACTIONS.NONE);
        ExaminePanel.SetPanelVisibility(false);
        Camera.SetCameraPosition(Camera.DigitalMatrixPosition, Camera.DigitalMatrixRotation);
    }
}
