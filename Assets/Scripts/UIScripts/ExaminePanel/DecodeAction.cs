using UnityEngine;
using UnityEngine.EventSystems;

public class DecodeAction : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Controller.CurrentAction = Controller.Actions.Decoding;
    }
}
