using UnityEngine;

public class KS_MouseFreeLook : MonoBehaviour
{
    [Header("Mouse Settings")]
    public float mouseSensitivity = 100f;
    public float smoothing = 5f;

    [Header("Rotation Limits")]
    public float minY = -30f; // How far down the player can look
    public float maxY = 60f;  // How far up the player can look
    public float minX = -60f; // Left limit (if looking forward is 0)
    public float maxX = 60f;  // Right limit

    private Vector2 currentMouseDelta;
    private Vector2 currentRotation;
    private Vector2 smoothV;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        targetMouseDelta *= mouseSensitivity * Time.deltaTime;

        smoothV = Vector2.Lerp(smoothV, targetMouseDelta, 1f / smoothing);
        currentRotation.x += smoothV.x;
        currentRotation.y -= smoothV.y;

        // Clamp rotation
        currentRotation.y = Mathf.Clamp(currentRotation.y, minY, maxY);
        currentRotation.x = Mathf.Clamp(currentRotation.x, minX, maxX);

        transform.localRotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0f);
    }
}
