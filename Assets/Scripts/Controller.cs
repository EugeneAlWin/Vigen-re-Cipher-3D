using UnityEngine;

public delegate void StepsDelegate(Controller.Steps newStep);
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
        Encoding,
        Decoding
    }
    public static Steps CurrentStep { get; set; } = Steps.None;

    public static Actions CurrentAction = Actions.Encoding;
    public static StepsDelegate stepsDelegate;

    private void Awake()
    {
        stepsDelegate += SetCurrentStep;
    }

    void SetCurrentStep(Steps newStep)
    {
        CurrentStep = newStep;
    }
}
