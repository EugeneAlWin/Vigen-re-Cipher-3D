using TMPro;
using UnityEngine;
using static ENUMS;
using static STATES;

public class SetStepAndDescription : MonoBehaviour
{
    [SerializeField] private TMP_Text stepField;
    [SerializeField] private TMP_Text descriptionField;
    private string stepFieldText, descriptionFieldText;
    private void Awake()
    {
        Controller.onStudyModeChanged += OnStepChanged;
        Controller.onCodedCharChanged += OnCodedLetterChanged;
    }
    private void Update()
    {
        stepField.text = stepFieldText;
        descriptionField.text = descriptionFieldText;
    }
    void OnStepChanged(STEPS newStep, ACTIONS newAction) => SetStepAndDescr();
    void OnCodedLetterChanged() => SetStepAndDescr();
    internal void SetStepAndDescr()
    {
        switch (CURRENT_EXAMINE_STEP)
        {
            case STEPS.FIRST:
                {
                    stepFieldText = "��� 1:";
                    descriptionFieldText = "���������� ������ ��������� ��� �����������, ����, ������� �����������, ����������� � ��� ";
                    break;
                }
            case STEPS.SECOND:
                {
                    stepFieldText = "��� 2:";
                    descriptionFieldText = $"����� ������ �� ����������� �������� ��������� � �����. � ����� ������ ��� `{EXAMINE_CODED_LETTER}`";
                    break;
                }
            case STEPS.THIRD:
                {
                    stepFieldText = "��� 3:";
                    descriptionFieldText = $"������ � ������� ������� �� `{EXAMINE_DEPTH}` �����, ������ �������, �� ������ ����� ��������";
                    break;
                }
            case STEPS.FOURTH:
                {
                    stepFieldText = "��� 4: ";
                    descriptionFieldText = $"������� `{EXAMINE_STEP}` ����� � �������, �� ������ ����� ��������, ��������������� �����������. R - ������, L -�����, T - �����, B - ����";
                    break;
                }
            case STEPS.FIFTH:
                {
                    stepFieldText = "��� 5:";
                    descriptionFieldText = $"���������: `{EXAMINE_CODED_LETTER}`. ��������� ���� 2-5 �� ����� ���������";
                    break;
                }
            case STEPS.SIXTH:
                {
                    stepFieldText = "������!";
                    descriptionFieldText = "";
                    break;
                }
        }
    }
}
