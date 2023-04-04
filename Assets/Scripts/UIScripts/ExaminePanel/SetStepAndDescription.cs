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
                    stepFieldText = "Шаг 1:";
                    descriptionFieldText = "Необходимо ввести сообщение для кодирования, ключ, глубину кодирования, направление и шаг ";
                    break;
                }
            case STEPS.SECOND:
                {
                    stepFieldText = "Шаг 2:";
                    descriptionFieldText = $"Найти символ на пересечении символов сообщения и ключа. В нашем случае это `{EXAMINE_CODED_LETTER}`";
                    break;
                }
            case STEPS.THIRD:
                {
                    stepFieldText = "Шаг 3:";
                    descriptionFieldText = $"Пройти в глубину матрицы на `{EXAMINE_DEPTH}` шагов, равных глубине, по модулю длины алфавита";
                    break;
                }
            case STEPS.FOURTH:
                {
                    stepFieldText = "Шаг 4: ";
                    descriptionFieldText = $"Сделать `{EXAMINE_STEP}` шагов в сторону, по модулю длины алфавита, соответствующую направлению. R - вправо, L -влево, T - вверх, B - вниз";
                    break;
                }
            case STEPS.FIFTH:
                {
                    stepFieldText = "Шаг 5:";
                    descriptionFieldText = $"Результат: `{EXAMINE_CODED_LETTER}`. Повторить шаги 2-5 до конца сообщения";
                    break;
                }
            case STEPS.SIXTH:
                {
                    stepFieldText = "Готово!";
                    descriptionFieldText = "";
                    break;
                }
        }
    }
}
