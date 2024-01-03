using UnityEngine;

public class JumpingState : State
{
    private bool grounded;
    private float gravityValue;
    private float jumpHeight;
    private float playerSpeed;
    private Vector3 airVelocity;
    new private Vector3 gravityVelocity;
    private float timeInAir;

    public JumpingState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();

        grounded = false;
        gravityValue = character.gravityValue;
        jumpHeight = character.jumpHeight;
        playerSpeed = character.playerSpeed;
        gravityVelocity = Vector3.zero;

        character.animator.SetFloat("speed", 0);
        character.animator.SetTrigger("jump");
        Jump();

        timeInAir = 0f;
    }

    public override void HandleInput()
    {
        base.HandleInput();
        input = moveAction.ReadValue<Vector2>();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        timeInAir += Time.deltaTime;
        grounded = character.controller.isGrounded;

        if (timeInAir > 0.675f && grounded)
        {
            character.animator.SetTrigger("move");
            if (input.magnitude > 0)
            {
                // Transition to sprinting state if there is input indicating movement
                stateMachine.ChangeState(character.sprinting);
            }
            else
            {
                // Transition to standing state if there is no input
                stateMachine.ChangeState(character.standing); // Ensure character.standing is defined in your Character class
            }
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        if (!grounded)
        {

            velocity = character.playerVelocity;
            airVelocity = new Vector3(input.x, 0, input.y);

            velocity = velocity.x * character.cameraTransform.right.normalized + velocity.z * character.cameraTransform.forward.normalized;
            velocity.y = 0f;
            airVelocity = airVelocity.x * character.cameraTransform.right.normalized + airVelocity.z * character.cameraTransform.forward.normalized;
            airVelocity.y = 0f;
            character.controller.Move(gravityVelocity * Time.deltaTime+ (airVelocity*character.airControl+velocity*(1- character.airControl))*playerSpeed*Time.deltaTime);
        }

        gravityVelocity.y += gravityValue * Time.deltaTime;
        grounded = character.controller.isGrounded;
    }

    void Jump()
    {
        gravityVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
    }

}

