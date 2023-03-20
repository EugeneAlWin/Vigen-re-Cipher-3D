using UnityEngine;
using UnityEngine.EventSystems;

public class HowIsItWorks : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        ExaminePanel.SetPanelVisibility(true);
        Controller.stepsDelegate?.Invoke(Controller.Steps.First);
        Camera.SetCameraPosition(new Vector3(-1569, 7, -24), Quaternion.identity);
    }
}
