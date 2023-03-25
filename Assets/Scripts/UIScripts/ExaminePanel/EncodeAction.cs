using UnityEngine;
using UnityEngine.EventSystems;

public class EncodeAction : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Controller.HowItWorksDelegate?.Invoke(Controller.Steps.Second, Controller.Actions.Encoding);
    }
}
