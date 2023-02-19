using UnityEngine;
using UnityEngine.EventSystems;

public class Quit : MonoBehaviour, IPointerClickHandler
{
    GenerateMatrix gm;
    void Start() 
    {
        gm = GameObject.Find("Container").GetComponent<GenerateMatrix>();

    }
    public void OnPointerClick(PointerEventData eventData)
    {
        gm.DestroyMatrix();
        Application.Quit();
    }
}
