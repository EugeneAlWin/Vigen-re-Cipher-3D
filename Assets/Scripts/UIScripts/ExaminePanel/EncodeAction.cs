using UnityEngine;
using UnityEngine.EventSystems;
using static ENUMS;

public class EncodeAction : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Controller.HowItWorksDelegate?.Invoke(STEPS.SECOND, ACTIONS.ENCODING);
    }
}
