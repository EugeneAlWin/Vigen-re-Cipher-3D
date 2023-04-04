using UnityEngine;
using static ENUMS;
using static STATES;

public delegate void StudyModeChangedDelegate(STEPS newStep, ACTIONS action);
public delegate void CipherVectorChangedDelegate(CipherVector cipherVector);
public delegate void NavButtonsPressedDelegate(NAV newNavigation);
public delegate void CodedCharChangedDelegate();

public class Controller : MonoBehaviour
{
    public static StudyModeChangedDelegate studyModeChanged;
    public static CipherVectorChangedDelegate cipherVectorChanged;
    public static NavButtonsPressedDelegate OnNavButtonsPressed;
    public static CodedCharChangedDelegate OnCcodedCharChanged;

    void Awake()
    {
        studyModeChanged += SetCurrentStepAndAction;
    }

    void SetCurrentStepAndAction(STEPS newStep, ACTIONS action)
    {
        CURRENT_EXAMINE_STEP = newStep;
        CURRENT_EXAMINE_ACTION = action;
    }
}
