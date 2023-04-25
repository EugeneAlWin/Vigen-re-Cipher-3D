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
        if (STUDY_CURRENT_ACTION == ACTIONS.ENCODING)
            switch (STUDY_CURRENT_STEP)
            {
                case STEPS.FIRST:
                    stepFieldText = "��� 1:";
                    descriptionFieldText = "���������� ������ ��������� ��� �����������, ����, ������� �����������, " +
                        "����������� � ���. ���� ����� ������� � ������� ����";
                    break;
                case STEPS.SECOND:
                    stepFieldText = "��� 2:";
                    descriptionFieldText = $"����� ������ �� ����������� �������� ��������� � �����." +
                        $" � ����� ������ ��� `{STUDY_CODED_CHAR}`";
                    break;
                case STEPS.THIRD:
                    stepFieldText = "��� 3:";
                    descriptionFieldText = $"������ � ������� ������� �� `{STUDY_DEPTH}` �����, ������ �������, " +
                        $"�� ������ ����� ��������. ���������: `{STUDY_CODED_CHAR}`";
                    break;
                case STEPS.FOURTH:
                    stepFieldText = "��� 4: ";
                    descriptionFieldText = $"������� `{STUDY_STEP}` ����� � �������, �� ������ ����� ��������, " +
                        $"��������������� �����������. R - ������, L -�����, T - �����, B - ����";
                    break;
                case STEPS.FIFTH:
                    stepFieldText = "��� 5:";
                    descriptionFieldText = $"���������: `{STUDY_CODED_CHAR}`. ��������� ���� 2-5 �� ����� ���������";
                    break;
                case STEPS.SIXTH:
                    stepFieldText = "������!";
                    descriptionFieldText = "";
                    break;
            }
        else
        {
            switch (STUDY_CURRENT_STEP)
            {
                case STEPS.FIRST:
                    stepFieldText = "��� 1:";
                    descriptionFieldText = "���������� ������ ��������� ��� �������������, ����, ������� �����������," +
                        " ����������� � ���. ���� ����� ������� � ������� ����.";
                    break;
                case STEPS.SECOND:
                    var currentDict = CURRENT_ALPHABET == ALPHABETS.LATIN ? Alphabets.LatinDictionary : Alphabets.CyrillicDictionary;
                    var currentAlph = CURRENT_ALPHABET == ALPHABETS.LATIN ? Alphabets.LatinAlphabet : Alphabets.CyrillicAlphabet;
                    string temp = "";
                    int step = 0;
                    if (STUDY_DIRECTION == DIRECTIONS.TOP)
                    {
                        temp = $"� ��� �� ������� ��� ����� �� `{STUDY_STEP}` �� ������ ��������";
                        step = -STUDY_STEP;
                    }
                    else if (STUDY_DIRECTION == DIRECTIONS.BOTTOM)
                    {
                        temp = $"� ��� �� ������� ��� ���� �� `{STUDY_STEP}` �� ������ ��������";
                        step = STUDY_STEP;
                    }

                    string keyChar = STUDY_KEY[STUDY_CURRENT_CHAR_POSITION % STUDY_KEY.Length].ToString(); //trash i know
                    var letterIndex = (currentDict[keyChar] + STUDY_DEPTH + step) % currentAlph.Length;
                    if (letterIndex < 0) letterIndex = (currentAlph.Length + letterIndex) % currentAlph.Length; //mod just in case | plus cuz < 0
                    string charInLeftRow = currentAlph[letterIndex];

                    stepFieldText = "��� 2:";
                    descriptionFieldText = $"� ������� ����� `{keyChar}` ��������� �������� ������� `{STUDY_DEPTH}` �� ������ ��������, " +
                        temp + $" (���������� `{charInLeftRow}`). " +
                        $"����� ���������� ����� ����� � � ���� ���� ����� ����� ���������� `{STUDY_CURRENT_CHAR}`.";
                    break;
                case STEPS.THIRD:
                    stepFieldText = "��� 3:";
                    descriptionFieldText = $"��������� �� ��� `{STUDY_STEP}` � �����������, ��������������� `{STUDY_DIRECTION}` " +
                        $"(R - ������, L -�����, T - �����, B - ����). ���������: `{STUDY_CODED_CHAR}`";
                    break;
                case STEPS.FOURTH:
                    stepFieldText = "��� 4: ";
                    descriptionFieldText = $"��������� �� �������, ������ `0`. � ������������ � ������ ����� " +
                        $"`{STUDY_KEY_CHAR}` � ������� ������ ���������� `{STUDY_CODED_CHAR}` ����� ����� ��������� �� ������� ����";
                    break;
                case STEPS.FIFTH:
                    stepFieldText = "��� 5:";
                    descriptionFieldText = $"���������: `{STUDY_CODED_CHAR}`. ��������� ���� 2-5 �� ����� ���������.";
                    break;
                case STEPS.SIXTH:
                    stepFieldText = "������!";
                    descriptionFieldText = "";
                    break;
            }
        }
    }
}
