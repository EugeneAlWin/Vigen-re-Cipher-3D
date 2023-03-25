using UnityEngine;
using UnityEngine.EventSystems;

public class CloseExaminePanel : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        ExaminePanel.SetPanelVisibility(false);
        Controller.HowItWorksDelegate?.Invoke(Controller.Steps.None, Controller.Actions.None);
        Camera.SetCameraPosition(Camera.DigitalMatrixPosition, Camera.DigitalMatrixRotation);
    }
}
