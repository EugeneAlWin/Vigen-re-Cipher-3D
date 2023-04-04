using UnityEngine;
using static ENUMS;
using static STATES;

public delegate void StudyModeChangedDelegate(STEPS newStep, ACTIONS action);
public delegate void CipherVectorChangedDelegate(CipherVector cipherVector);
public delegate void LightFourthStepDelegate(string xChar, string yChar);
public delegate void NavButtonsPressedDelegate(NAV newNavigation);
public delegate void CodedCharChangedDelegate();

public class Controller : MonoBehaviour
{
    public static StudyModeChangedDelegate onStudyModeChanged;
    public static CipherVectorChangedDelegate onCipherVectorChanged;
    public static NavButtonsPressedDelegate onNavButtonsPressed;
    public static CodedCharChangedDelegate onCodedCharChanged;
    public static LightFourthStepDelegate lightFourthStep;

    void Awake()
    {
        onStudyModeChanged += SetCurrentStepAndAction;
    }

    void SetCurrentStepAndAction(STEPS newStep, ACTIONS action)
    {
        CURRENT_EXAMINE_STEP = newStep;
        CURRENT_EXAMINE_ACTION = action;
    }
}
