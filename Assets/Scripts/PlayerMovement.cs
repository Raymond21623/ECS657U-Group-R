using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    InputManager inputManager;

    Vector3 moveDirection;
    Transform cameraObject;

    Rigidbody playerRigidBody;

    public float movementSpeed = 7;
    public float rotationSpeed = 15;

    public float mouseSensitivity = 100.0f;
    private float currentYRotation = 0.0f;
    private float currentXRotation = 0.0f;

    private float[] zoomStages = { -3f, -6f, -9f, -12f, -18f, -24f, -30f };
    private int currentZoomStage = 2;

    // Start is called before the first frame update
    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerRigidBody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
        currentYRotation = transform.eulerAngles.y;
        currentXRotation = cameraObject.localEulerAngles.x;

        // Ensure they are applied
        transform.localRotation = Quaternion.Euler(0.0f, currentYRotation, 0.0f);
        cameraObject.localRotation = Quaternion.Euler(currentXRotation, 0.0f, 0.0f);

        // Lock the mouse cursor to the center of the screen when right-clicked
        Cursor.lockState = CursorLockMode.None;
        targetZoom = cameraObject.localPosition.z;
    }

    public void HandleMovement()
    {
        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;
        moveDirection = moveDirection * movementSpeed;

        Vector3 movementVelocity = moveDirection;
        playerRigidBody.velocity = movementVelocity;
    }

    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
        HandleZoom(); // Handle the zoom functionality
    }


    public void HandleRotation()
    {
        if (Input.GetMouseButton(1)) // Check if the right mouse button is held down
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            currentYRotation += mouseX * mouseSensitivity * Time.deltaTime;
            currentXRotation -= mouseY * mouseSensitivity * Time.deltaTime;

            // Clamp the X rotation to prevent flipping the camera upside down
            currentXRotation = Mathf.Clamp(currentXRotation, -90.0f, 90.0f);

            // Rotate the player's body for Y-axis (horizontal) rotation
            transform.localRotation = Quaternion.Euler(0.0f, currentYRotation, 0.0f);

            // Rotate the camera for X-axis (vertical) rotation
            cameraObject.localRotation = Quaternion.Euler(currentXRotation, 0.0f, 0.0f);
            
            // Lock the cursor to the center of the screen while right-clicking
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            // Unlock the cursor when not right-clicking
            Cursor.lockState = CursorLockMode.None;
        }
    }


private bool isZooming = false;
private float targetZoom;
public float zoomSpeed = 5.0f;

public void HandleZoom()
{
    float scrollInput = Input.GetAxis("Mouse ScrollWheel");

    if (scrollInput != 0f && !isZooming)
    {
        if (scrollInput > 0f && currentZoomStage > 0) // Scrolling up
        {
            currentZoomStage--;
        }
        else if (scrollInput < 0f && currentZoomStage < zoomStages.Length - 1) // Scrolling down
        {
            currentZoomStage++;
        }
        
        float desiredZoom = zoomStages[currentZoomStage];
        StopAllCoroutines();  // Stop any existing zoom coroutines
        StartCoroutine(SmoothZoom(desiredZoom));
    }
}



private IEnumerator SmoothZoom(float targetZoomValue)
{
    isZooming = true;

    float startZoom = cameraObject.localPosition.z;
    float progress = 0f;

    while (progress < 1f)
    {
        progress += Time.deltaTime * zoomSpeed;

        float currentZoom = Mathf.Lerp(startZoom, targetZoomValue, progress);
        cameraObject.localPosition = new Vector3(cameraObject.localPosition.x, cameraObject.localPosition.y, currentZoom);

        yield return null; // Wait for the next frame before continuing the loop
    }

    isZooming = false;
}








}
