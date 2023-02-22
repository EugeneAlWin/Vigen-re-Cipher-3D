using UnityEngine;
using UnityEngine.EventSystems;

public class Quit : MonoBehaviour, IPointerClickHandler
{
    Matrix gm;
    void Start()
    {
        gm = GameObject.Find("Container").GetComponent<Matrix>();

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        gm.DestroyMatrix();
        Application.Quit();
    }
}
