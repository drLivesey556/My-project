using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Transform cameraTransform;
    public float sensitivity = 2.0f;
    public float pitchMin = -80f;
    public float pitchMax = 80f;

    float pitch;

    void Awake()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mx = Input.GetAxis("Mouse X") * sensitivity;
        float my = Input.GetAxis("Mouse Y") * sensitivity;

        transform.Rotate(0f, mx, 0f);

        pitch -= my;
        pitch = Mathf.Clamp(pitch, pitchMin, pitchMax);
        cameraTransform.localRotation = Quaternion.Euler(pitch, 0f, 0f);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}

