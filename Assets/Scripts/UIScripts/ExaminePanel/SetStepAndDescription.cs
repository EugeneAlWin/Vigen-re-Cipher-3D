using TMPro;
using UnityEngine;

public class SetStepAndDescription : MonoBehaviour
{
    [SerializeField] private TMP_Text stepField;
    [SerializeField] private TMP_Text descriptionField;

    private void Update()
    {
        switch (Controller.CurrentStep)
        {
            case Controller.Steps.First:
                {
                    stepField.text = "��� 1:";
                    descriptionField.text = "���������� ������ ��������� ��� �����������, ����, ������� �����������, ����������� � ���";
                    break;
                }
        }
    }
}
