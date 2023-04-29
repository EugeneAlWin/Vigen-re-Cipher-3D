using UnityEngine;
using static ENUMS;
using static STATES;

public delegate void StudyModeChangedDelegate(STEPS newStep, ACTIONS action);
public delegate void CipherVectorChangedDelegate(CipherVector cipherVector);
public delegate void LightNonZZeroElementDelegate(string xChar, string yChar, int step);
public delegate void NavButtonsPressedDelegate(NAV newNavigation);
public delegate void CodedCharChangedDelegate();
public delegate void ResetFieldDelegate();

public class Controller : MonoBehaviour
{
    public static StudyModeChangedDelegate onStudyModeChanged;
    public static CipherVectorChangedDelegate onCipherVectorChanged;
    public static NavButtonsPressedDelegate onNavButtonsPressed;
    public static CodedCharChangedDelegate onCodedCharChanged;
    public static LightNonZZeroElementDelegate onLightNonZZeroElement;
    public static ResetFieldDelegate onResetField;

    void Awake()
    {
        onStudyModeChanged += SetCurrentStepAndAction;
    }

    void SetCurrentStepAndAction(STEPS newStep, ACTIONS action)
    {
        STUDY_CURRENT_STEP = newStep;
        STUDY_CURRENT_ACTION = action;
    }
}
