using UnityEngine;
using UnityEngine.EventSystems;
using static ENUMS;
using static STATES;

public class DecodeAction : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        CURRENT_EXAMINE_ACTION = ACTIONS.DECODING;
    }
}
