using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    InputManager inputManager;

    Vector3 moveDirection;
    Transform cameraObject;

    Rigidbody playerRigidBody;

    public float inAirTimer;
    public float leapingVelocity;
    public float fallingVelocity;
    public LayerMask groundLayer;
    public float rayCastHeightOffset = 0.5f;
    public float maxDistance = 1;

    public bool isSprinting;
    public bool isGrounded;
    public bool isJumping;

    public float sprintingSpeed = 7;
    public float movementSpeed = 5;
    public float rotationSpeed = 15;

    public float spd;

    public float jumpHeight = 7f;
    public float gravityIntensity = -15;

    private float[] zoomStages = { -3f, -6f, -9f, -12f, -18f, -24f, -30f };
    private int currentZoomStage = 2;

    // Start is called before the first frame update
    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerRigidBody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;

        // Lock the mouse cursor to the center of the screen when right-clicked
        Cursor.lockState = CursorLockMode.None;
        targetZoom = cameraObject.localPosition.z;
    }

    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
        HandleFalling();
        HandleZoom(); // Handle the zoom functionality
    }

    private void HandleMovement()
    {
        if (isJumping || !isGrounded)
        {
            return;
        }

        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;

        if (isSprinting)
        {
            moveDirection = moveDirection * sprintingSpeed;
        }
        else
        {
            moveDirection = moveDirection * movementSpeed;
        }
        if (isGrounded && !isJumping)
        {
            Vector3 movementVelocity = moveDirection;
            playerRigidBody.velocity = movementVelocity;
        }
        
    }

    private void HandleFalling()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        Debug.DrawRay(transform.position, Vector3.down, Color.red);
        rayCastOrigin.y = rayCastOrigin.y + rayCastHeightOffset;

        if (!isGrounded && !isJumping)
        {
            inAirTimer = inAirTimer + Time.deltaTime;
            playerRigidBody.AddForce(transform.forward * leapingVelocity);
            playerRigidBody.AddForce(-Vector3.up * fallingVelocity * inAirTimer);
            spd = playerRigidBody.velocity.y;
        }

        if (Physics.SphereCast(rayCastOrigin, 0.2f, -Vector3.up, out hit, maxDistance, groundLayer))
        {
            inAirTimer = 0;
            isGrounded = true;
        }
        /*if (Physics.Raycast(rayCastOrigin, Vector3.down))
        {
            inAirTimer = 0;
            isGrounded = true;
        }*/
        else
        {
            isGrounded = false;
        }
        
    }

    private void HandleRotation()
    {
        if (isJumping || !isGrounded)
        {
            return;
        }
        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward;//Keep player facing the direction that they were last in (stops player from snapping back to straight forward)

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    public void HandleJumping()
    {
        
        if (isGrounded)
        {
            /*float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
            Vector3 playerVelocity = moveDirection;
            playerVelocity.y = jumpingVelocity;
            playerRigidBody.velocity = playerVelocity;*/
            playerRigidBody.AddForce(Vector3.up * jumpHeight, ForceMode.VelocityChange);
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
