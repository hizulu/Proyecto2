using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackState : PlayerGroundedState
{
    public PlayerAttackState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    protected Animator animator;

    protected bool attackFinish;

    protected float attackTimeElapsed;
    protected float maxTimeToNextAttack = 1f;
    protected int currentNumAttack;

    protected bool canContinueCombo;
    protected bool isWaitingForInput;

    public override void Enter()
    {
        animator = stateMachine.Player.AnimPlayer;
        base.Enter();
        StartAnimation(stateMachine.Player.PlayerAnimationData.AttackParameterHash);
        //Debug.Log("Has entrado en el estado de ATACAR");
    }

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.PlayerAnimationData.AttackParameterHash);
        //Debug.Log("Has salido del estado de ATACAR");
    }

    protected virtual void FinishAttack() { }
}
