using UnityEngine;
using UnityEngine.InputSystem;

public class State
{
    public Character character;
    public StateMachine stateMachine;

    protected Vector3 gravityVelocity;
    protected Vector3 velocity;
    protected Vector2 input;

    public InputAction moveAction;
    public InputAction lookAction;
    public InputAction jumpAction;
    public InputAction crouchAction;
    public InputAction sprintAction;

    public State(Character _character, StateMachine _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;

        var playerControls = new PlayerControls();

        moveAction = playerControls.Player.Move;
        lookAction = playerControls.Player.Look;
        jumpAction = playerControls.Player.Jump;
        crouchAction = playerControls.Player.Crouch;
        sprintAction = playerControls.Player.Sprint;
        playerControls.Player.Enable();
    }


    public virtual void Enter()
    {
        Debug.Log("enter state: "+this.ToString());
    }

    public virtual void HandleInput()
    {
    }

    public virtual void LogicUpdate()
    {
    }

    public virtual void PhysicsUpdate()
    {
    }

    public virtual void Exit()
    {
    }
}

