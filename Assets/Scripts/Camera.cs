using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private float speed = 0.3f, rotationSpeed = 0.3f;
    public static Vector3 DigitalMatrixPosition { get; } = new(11, 7, -24);
    public static Vector3 CyrillicMatrixPosition { get; } = new(-148.1f, -10.4f, -39.3f);
    public static Vector3 LatinMatrixPosition { get; } = new(-151.1f, -6.0f, -39.3f);
    //--
    public static Quaternion DigitalMatrixRotation { get; } = Quaternion.Euler(0, -45.13f, 0);
    public static Quaternion CyrillicMatrixRotation { get; } = Quaternion.Euler(0, 0, 0);
    public static Quaternion LatinMatrixRotation { get; } = Quaternion.Euler(0, 0, 0);
    //--
    public static Vector3 cameraPos = DigitalMatrixPosition;
    private static Quaternion cameraRot = DigitalMatrixRotation;

    private void Awake()
    {
        Application.targetFrameRate = 30;
    }
    private void Update()
    {
        MoveTo(cameraPos, cameraRot);
    }

    private void MoveTo(Vector3 newPosition, Quaternion newRotation)
    {
        transform.SetPositionAndRotation(
            Vector3.Lerp(transform.position, newPosition, Time.deltaTime * speed),
            Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * rotationSpeed)
            );
    }
    public static void SetCameraPosition(Vector3 newPosition, Quaternion newRotation)
    {
        cameraPos = newPosition;
        cameraRot = newRotation;
    }

}
