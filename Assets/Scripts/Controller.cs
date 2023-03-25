using UnityEngine;
using static ENUMS;
using static STATES;

public delegate void HowItWorksDelegate(STEPS newStep, ACTIONS action);
public class Controller : MonoBehaviour
{
    public static HowItWorksDelegate HowItWorksDelegate;

    private void Awake()
    {
        HowItWorksDelegate += SetCurrentStepAndAction;
    }

    void SetCurrentStepAndAction(STEPS newStep, ACTIONS action)
    {
        CURRENT_EXAMINE_STEP = newStep;
        CURRENT_EXAMINE_ACTION = action;
    }
}
