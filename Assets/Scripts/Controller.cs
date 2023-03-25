using UnityEngine;

public delegate void HowItWorksDelegate(Controller.Steps newStep, Controller.Actions action);
public class Controller : MonoBehaviour
{
    public enum Steps
    {
        None,
        First,
        Second,
        Third,
        Fourth,
        Fifth
    }
    public enum Actions
    {
        None,
        Encoding,
        Decoding
    }
    public static Steps CurrentStep { get; set; } = Steps.None;

    public static Actions CurrentAction = Actions.None;
    public static HowItWorksDelegate HowItWorksDelegate;

    private void Awake()
    {
        HowItWorksDelegate += SetCurrentStepAndAction;
    }

    void SetCurrentStepAndAction(Steps newStep, Actions action)
    {
        CurrentStep = newStep;
        CurrentAction = action;
    }
}
