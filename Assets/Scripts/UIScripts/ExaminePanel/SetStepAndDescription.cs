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
                    stepFieldText = "Шаг 1:";
                    descriptionFieldText = "Необходимо ввести сообщение для кодирования, ключ, глубину кодирования, " +
                        "направление и шаг. Язык можно сменить в главном меню";
                    break;
                case STEPS.SECOND:
                    stepFieldText = "Шаг 2:";
                    descriptionFieldText = $"Найти символ на пересечении символов сообщения и ключа." +
                        $" В нашем случае это `{STUDY_CODED_CHAR}`";
                    break;
                case STEPS.THIRD:
                    stepFieldText = "Шаг 3:";
                    descriptionFieldText = $"Пройти в глубину матрицы на `{STUDY_DEPTH}` шагов, равных глубине, " +
                        $"по модулю длины алфавита. Результат: `{STUDY_CODED_CHAR}`";
                    break;
                case STEPS.FOURTH:
                    stepFieldText = "Шаг 4: ";
                    descriptionFieldText = $"Сделать `{STUDY_STEP}` шагов в сторону, по модулю длины алфавита, " +
                        $"соответствующую направлению. R - вправо, L -влево, T - вверх, B - вниз";
                    break;
                case STEPS.FIFTH:
                    stepFieldText = "Шаг 5:";
                    descriptionFieldText = $"Результат: `{STUDY_CODED_CHAR}`. Повторить шаги 2-5 до конца сообщения";
                    break;
                case STEPS.SIXTH:
                    stepFieldText = "Готово!";
                    descriptionFieldText = "";
                    break;
            }
        else
        {
            switch (STUDY_CURRENT_STEP)
            {
                case STEPS.FIRST:
                    stepFieldText = "Шаг 1:";
                    descriptionFieldText = "Необходимо ввести сообщение для декодирования, ключ, глубину кодирования," +
                        " направление и шаг. Язык можно сменить в главном меню.";
                    break;
                case STEPS.SECOND:
                    var currentDict = CURRENT_ALPHABET == ALPHABETS.LATIN ? Alphabets.LatinDictionary : Alphabets.CyrillicDictionary;
                    var currentAlph = CURRENT_ALPHABET == ALPHABETS.LATIN ? Alphabets.LatinAlphabet : Alphabets.CyrillicAlphabet;
                    string temp = "";
                    int step = 0;
                    if (STUDY_DIRECTION == DIRECTIONS.TOP)
                    {
                        temp = $"а так же сделать шаг вверх на `{STUDY_STEP}` по модулю алфавита";
                        step = -STUDY_STEP;
                    }
                    else if (STUDY_DIRECTION == DIRECTIONS.BOTTOM)
                    {
                        temp = $"а так же сделать шаг вниз на `{STUDY_STEP}` по модулю алфавита";
                        step = STUDY_STEP;
                    }

                    string keyChar = STUDY_KEY[STUDY_CURRENT_CHAR_POSITION % STUDY_KEY.Length].ToString(); //trash i know
                    var letterIndex = (currentDict[keyChar] + STUDY_DEPTH + step) % currentAlph.Length;
                    if (letterIndex < 0) letterIndex = (currentAlph.Length + letterIndex) % currentAlph.Length; //mod just in case | plus cuz < 0
                    string charInLeftRow = currentAlph[letterIndex];

                    stepFieldText = "Шаг 2:";
                    descriptionFieldText = $"К индексу ключа `{keyChar}` прибавить значение глубины `{STUDY_DEPTH}` по модулю алфавита, " +
                        temp + $" (Получается `{charInLeftRow}`). " +
                        $"Найти полученную букву слева и в этом ряду найти букву шифртекста `{STUDY_CURRENT_CHAR}`.";
                    break;
                case STEPS.THIRD:
                    stepFieldText = "Шаг 3:";
                    descriptionFieldText = $"Сдвинутся на шаг `{STUDY_STEP}` в направлении, противоположном `{STUDY_DIRECTION}` " +
                        $"(R - вправо, L -влево, T - вверх, B - вниз). Результат: `{STUDY_CODED_CHAR}`";
                    break;
                case STEPS.FOURTH:
                    stepFieldText = "Шаг 4: ";
                    descriptionFieldText = $"Вернуться на глубину, равную `0`. В соответствии с буквой ключа " +
                        $"`{STUDY_KEY_CHAR}` и текущей буквой шифртекста `{STUDY_CODED_CHAR}` найти букву сообщения на верхнем ряду";
                    break;
                case STEPS.FIFTH:
                    stepFieldText = "Шаг 5:";
                    descriptionFieldText = $"Результат: `{STUDY_CODED_CHAR}`. Повторить шаги 2-5 до конца сообщения.";
                    break;
                case STEPS.SIXTH:
                    stepFieldText = "Готово!";
                    descriptionFieldText = "";
                    break;
            }
        }
    }
}
