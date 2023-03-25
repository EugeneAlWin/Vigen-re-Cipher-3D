using UnityEngine;
using UnityEngine.EventSystems;

public class HowIsItWorks : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        ExaminePanel.SetPanelVisibility(true);
        Controller.HowItWorksDelegate?.Invoke(Controller.Steps.First, Controller.Actions.None);
        Camera.SetCameraPosition(Camera.CyrillicMatrixPosition, Camera.CyrillicMatrixRotation);
    }
}
