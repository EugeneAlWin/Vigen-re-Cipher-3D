using UnityEngine;
using UnityEngine.EventSystems;

public class CloseExaminePanel : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        ExaminePanel.SetPanelVisibility(false);
        Controller.stepsDelegate?.Invoke(Controller.Steps.None);
        Camera.SetCameraPosition(Camera.DigitalMatrixPosition, Camera.DigitalMatrixRotation);
    }
}
