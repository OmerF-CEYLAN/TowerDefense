using UnityEngine;

public class MouseLook : MonoBehaviour
{

    [SerializeField] InputHandler inputHandler;
    [SerializeField] float sensitivity;
    float xRotation;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LookAround();
    }

    void LookAround()
    {
        Vector3 lookVector = inputHandler.GetLookAroundVector();

        float mouseX = lookVector.x * sensitivity;

        float mouseY = lookVector.y * sensitivity;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

}
