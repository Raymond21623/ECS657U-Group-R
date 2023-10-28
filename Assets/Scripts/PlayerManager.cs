using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    PlayerMovement playerMovement;
    CameraManager cameraManager;

    // Start is called before the first frame update
    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        cameraManager = FindAnyObjectByType<CameraManager>();
        playerMovement = GetComponent<PlayerMovement>();

    }

    

    private void Update()
    {
        inputManager.HandleAllInputs();
    }

    private void FixedUpdate()
    {
        playerMovement.HandleAllMovement();
    }

    private void LateUpdate()
    {
        cameraManager.HandleAllCameraMovement();
    }
}
