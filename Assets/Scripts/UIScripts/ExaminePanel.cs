using UnityEngine;

public class ExaminePanel : MonoBehaviour
{
    static GameObject panel;
    private void Awake()
    {
        panel = gameObject;
        panel.SetActive(false);
    }
    static internal void SetPanelVisibility(bool isVisible)
    {
        panel.SetActive(isVisible);
    }
}
