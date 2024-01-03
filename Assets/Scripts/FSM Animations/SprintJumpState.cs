using UnityEngine;

public class SprintJumpState : State
{
    float timePassed;
    float jumpTime;
    new private Vector3 gravityVelocity;

    public SprintJumpState(Character _character, StateMachine _stateMachine) : base(_character, _stateMachine)
    {
        character = _character;
        stateMachine = _stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        character.animator.applyRootMotion = true;
        timePassed = 0f;
        character.animator.SetTrigger("sprintJump");

        jumpTime = 0.75f;

        // Initialize gravityVelocity
        gravityVelocity = Vector3.zero;

        // Calculate and set the initial jump velocity
        float sprintJumpHeight = character.jumpHeight * 1.5f; // Adjust this multiplier as needed
        gravityVelocity.y = Mathf.Sqrt(sprintJumpHeight * -2.0f * character.gravityValue);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        // Apply gravity to gravityVelocity
        gravityVelocity.y += character.gravityValue * Time.deltaTime;

        // Apply gravityVelocity to the character
        character.controller.Move(gravityVelocity * Time.deltaTime);
    }

	public override void Exit()
{
    base.Exit();
    character.animator.applyRootMotion = false;
    character.animator.ResetTrigger("sprintJump");
}

	public override void LogicUpdate()
    {
        
        base.LogicUpdate();
		if (timePassed> jumpTime)
		{
            character.animator.SetTrigger("move");
            stateMachine.ChangeState(character.sprinting);
        }
        timePassed += Time.deltaTime;
    }



}

