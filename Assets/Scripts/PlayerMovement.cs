using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    InputManager inputManager;

    Vector3 moveDirection;
    Transform cameraObject;

    Rigidbody playerRigidBody;

    CapsuleCollider capsuleCollider;

    public float inAirTimer;
    public float leapingVelocity;
    public float fallingVelocity;
    public LayerMask groundLayer;

    public bool isSprinting;
    public bool isGrounded;
    public bool isJumping;
    
    public float sprintingSpeed = 7;
    public float movementSpeed = 5;
    public float rotationSpeed = 15;

    public float spd;

    public float jumpHeight = 7f;
    public float gravityIntensity = -15f;

    private float[] zoomStages = { -3f, -6f, -9f, -12f, -18f, -24f, -30f };
    private int currentZoomStage = 2;

    // Start is called before the first frame update
    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        playerRigidBody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        cameraObject = Camera.main.transform;

        // Lock the mouse cursor to the center of the screen when right-clicked
        Cursor.lockState = CursorLockMode.Locked;
        targetZoom = cameraObject.localPosition.z;
    }

    private void Update()
    {
        //Debug.Log(transform.position);
        //Debug.Log(playerRigidBody.position);
    }

    public void HandleAllMovement()
    {
        HandleMovement();
        HandleRotation();
        HandleFalling();
        HandleZoom(); // Handle the zoom functionality
        isGrounded = IsGrounded();
    }

    private void HandleMovement()
    {
        if (!IsGrounded())
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
        Vector3 movementVelocity = moveDirection;
        playerRigidBody.velocity = movementVelocity;
        
    }

    bool IsGrounded()
    {
        RaycastHit groundedHit;
        Vector3 targetPosition = transform.position;
        Vector3 rayCastOrigin = transform.position;

        //float sphereCastRadius = capsuleCollider.radius * 0.9f;
        float sphereCastRadius = capsuleCollider.radius;

        float sphereCastTravelDistance = capsuleCollider.bounds.extents.y - sphereCastRadius + 0.1f;
        //float sphereCastTravelDistance = capsuleCollider.bounds.extents.y - sphereCastRadius + 0.05f;


        isGrounded = Physics.SphereCast(rayCastOrigin, sphereCastRadius, Vector3.down, out groundedHit, sphereCastTravelDistance, groundLayer);
        if (isGrounded)
        {
            Debug.DrawRay(playerRigidBody.position, Vector3.down, Color.red);
            
            Vector3 rayCastHitPoint = groundedHit.point;
            targetPosition.y = rayCastHitPoint.y + 0.9f;
            //isGrounded = true;
            inAirTimer = 0;
            isJumping = false;
            
            transform.position = targetPosition;
        }
        else
        {
            //isGrounded = false;
        }
        return isGrounded;
    }


    private void HandleFalling()
    {
        if (!IsGrounded())
        {
            inAirTimer = inAirTimer + Time.deltaTime;
            playerRigidBody.AddForce(transform.forward * leapingVelocity);
            playerRigidBody.AddForce(Vector3.down * fallingVelocity * inAirTimer);
        }
    }

    private void HandleRotation()
    {
        if (!IsGrounded())
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
        
        if (IsGrounded())
        {
            //playerRigidBody.velocity = Vector3.up * jumpHeight;
            playerRigidBody.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            isJumping = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) return;
        Gizmos.color = Color.red;
        Vector3 rayCastOrigin = transform.position;
        Debug.DrawLine(rayCastOrigin, rayCastOrigin - transform.up * (capsuleCollider.bounds.extents.y - capsuleCollider.radius + 0.1f));
        Gizmos.DrawWireSphere(rayCastOrigin - transform.up * (capsuleCollider.bounds.extents.y - capsuleCollider.radius + 0.1f), capsuleCollider.radius);
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
