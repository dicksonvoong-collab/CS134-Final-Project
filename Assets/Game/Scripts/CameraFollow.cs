using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    public float mouseSens = 1000f;

    private float rotation;
    
    // sets offset and locks cursor at start of game
    void Start()
    {
        offset = new Vector3(0f, 4f, -9f);
        Cursor.lockState = CursorLockMode.Locked;
    }

    // camera orbits and looks at the player
    void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        rotation += mouseX * mouseSens * Time.deltaTime;

        Quaternion targetRot = Quaternion.Euler(0f, rotation, 0f);
        Vector3 rotatedOffset = targetRot * offset;
        if (player != null)
        {
            transform.position = player.position + rotatedOffset;
            
            transform.LookAt(player);
        }
    }
}
