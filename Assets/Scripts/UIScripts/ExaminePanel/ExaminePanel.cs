using UnityEngine;
using static ENUMS;
public class ExaminePanel : MonoBehaviour
{
    [SerializeField] private GameObject oneTimeElements, afterStep1Panel;
    static GameObject panel;
    private void Awake()
    {
        Controller.onStudyModeChanged += SetOneTimeElementsVisibillity;
        Controller.onStudyModeChanged += SetAfterStep1PanelVisibillity;
        panel = gameObject;
        panel.SetActive(false);
        oneTimeElements.SetActive(true);
        afterStep1Panel.SetActive(false);
    }

    static internal void SetPanelVisibility(bool isVisible)
    {
        panel.SetActive(isVisible);
    }

    void SetOneTimeElementsVisibillity(STEPS newStep, ACTIONS newAction)
    {
        oneTimeElements.SetActive(newStep == STEPS.FIRST);
    }
    void SetAfterStep1PanelVisibillity(STEPS newStep, ACTIONS newAction)
    {
        afterStep1Panel.SetActive(newStep != STEPS.FIRST);
    }
}
