using TMPro;
using UnityEngine;
using static ENUMS;
using static STATES;

public class SetStepAndDescription : MonoBehaviour
{
    [SerializeField] private TMP_Text stepField;
    [SerializeField] private TMP_Text descriptionField;

    private void Update()
    {
        switch (CURRENT_EXAMINE_STEP)
        {
            case STEPS.FIRST:
                {
                    stepField.text = "��� 1:";
                    descriptionField.text = "���������� ������ ��������� ��� �����������, ����, ������� �����������, ����������� � ���";
                    break;
                }
        }
    }
}
