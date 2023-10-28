using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManager : MonoBehaviour
{
    InputManager inputManager;

    public Transform targetTransform; //Camera will follow this object
    public Transform cameraPivot; //Camera will pivot around this object

    private Vector3 cameraFollowVelocity = Vector3.zero;

    public float cameraFollowSpeed = 0.2f;
    public float cameraLookSpeed = 2;
    public float cameraPivotSpeed = 2;

    public float lookAngle; //up and down
    public float pivotAngle; // left and right
    public float minPivotAngle = -35;
    public float maxPivotAngle = 35;

    private void Awake()
    {
        inputManager = FindAnyObjectByType<InputManager>();
        targetTransform = FindAnyObjectByType<PlayerManager>().transform;
    }

    public void HandleAllCameraMovement()
    {
        FollowCharacter();
        RotateCamera();
    }

    private void FollowCharacter()
    {
        Vector3 targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.position, ref cameraFollowVelocity, cameraFollowSpeed);
        transform.position = targetPosition;
    }

    private void RotateCamera()
    {
        lookAngle = lookAngle + (inputManager.cameraInputX * cameraLookSpeed);
        pivotAngle = pivotAngle - (inputManager.cameraInputY * cameraPivotSpeed);
        pivotAngle = Mathf.Clamp(pivotAngle, minPivotAngle, maxPivotAngle);

        Vector3 rotation = Vector3.zero;
        rotation.y = lookAngle;
        Quaternion targetRotation = Quaternion.Euler(rotation);
        transform.rotation = targetRotation;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;
        targetRotation = Quaternion.Euler(rotation);
        cameraPivot.localRotation = targetRotation;
    }
}
