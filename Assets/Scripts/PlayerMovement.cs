using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    InputManager inputManager;
    Vector3 moveDirection;
    Transform cameraObject;
    CharacterController characterController;

    public float inAirTimer;
    public float gravityIntensity = -9.81f;

    public bool isSprinting;
    public bool isGrounded;
    public bool isJumping;

    public float sprintingSpeed = 2.0f;
    public float movementSpeed = 1.0f;
    public float rotationSpeed = 5f;

    public float jumpHeight = 1.6f;

    private float[] zoomStages = { -3f, -6f, -9f, -12f, -18f, -24f, -30f };
    private int currentZoomStage = 2;

    private bool isZooming = false;
    private float targetZoom;
    public float zoomSpeed = 5.0f;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        characterController = GetComponent<CharacterController>();
        cameraObject = Camera.main.transform;

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void Update()
    {
        HandleAllMovement();
    }

    public void HandleAllMovement()
    {
        isGrounded = characterController.isGrounded;
        if (isGrounded && isJumping)
        {
            isJumping = false;
            inAirTimer = 0;
        }
        else if (!isGrounded)
        {
            inAirTimer += Time.deltaTime;
        }

        HandleMovement();
        HandleRotation();
        HandleJumping();
        HandleZoom();
    }

    private void HandleMovement()
    {
        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection += cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;

        if (isSprinting)
        {
            moveDirection *= sprintingSpeed;
        }
        else
        {
            moveDirection *= movementSpeed;
        }

        if (!isGrounded)
        {
            moveDirection.y += gravityIntensity * inAirTimer;
        }

        characterController.Move(moveDirection * Time.deltaTime);
    }

    private void HandleRotation()
    {
        Vector3 targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection += cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }

    public void HandleJumping()
    {
        if (isGrounded)
        {
            moveDirection.y = jumpHeight;
            isJumping = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) return;
        Gizmos.color = Color.red;
        Vector3 rayCastOrigin = transform.position;
        // Adjust these values according to the CharacterController's properties
        float characterHeight = characterController.height;
        float characterRadius = characterController.radius;
        Debug.DrawLine(rayCastOrigin, rayCastOrigin - transform.up * (characterHeight / 2 - characterRadius + 0.1f));
        Gizmos.DrawWireSphere(rayCastOrigin - transform.up * (characterHeight / 2 - characterRadius + 0.1f), characterRadius);
    }

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
