using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private float speed = 0.3f, rotationSpeed = 0.3f;
    private static Vector3 cameraPos = new(11, 7, -24);
    private static Quaternion cameraRot = Quaternion.Euler(0, -45.13f, 0);
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
