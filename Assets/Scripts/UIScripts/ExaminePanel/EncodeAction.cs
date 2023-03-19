using UnityEngine;
using UnityEngine.EventSystems;

public class EncodeAction : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Controller.CurrentAction = Controller.Actions.Encoding;

        Controller.stepsDelegate?.Invoke(Controller.Steps.Second);
    }
}
