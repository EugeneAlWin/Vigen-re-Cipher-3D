using TMPro;
using UnityEngine;

public class SetStepAndDescription : MonoBehaviour
{
    [SerializeField] TMP_Text stepField;
    [SerializeField] TMP_Text descriptionField;

    private void Update()
    {
        switch (Controller.CurrentStep)
        {
            case Controller.Steps.First:
                {
                    stepField.text = "������ ������ ���� �����";
                    descriptionField.text = "������ ������ ���� �����";
                    break;
                }
        }
    }
}
