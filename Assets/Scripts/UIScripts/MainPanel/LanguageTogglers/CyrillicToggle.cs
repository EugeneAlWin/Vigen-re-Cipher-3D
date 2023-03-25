using UnityEngine;
using UnityEngine.EventSystems;
using static ENUMS;
using static STATES;

internal class CyrillicToggle : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        CURRENT_ALPHABET = ALPHABETS.CYRILLIC;
    }
}