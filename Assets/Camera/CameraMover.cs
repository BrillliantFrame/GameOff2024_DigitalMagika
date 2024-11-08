using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField]
    [Min(0)]
    private int _cameraActivate = 0;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
            CameraManager.Instance?.ActivateCamera(_cameraActivate);
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
            CameraManager.Instance?.ActivateMainCamera();
    }
}
