using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHalfDeadState : PlayerDeathState
{
    public PlayerHalfDeadState(PlayerStateMachine _stateMachine) : base(_stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Has entrado en el estado de MEDIO-MUERTA");
        //statsData.CurrentTimeHalfDead = statsData.MaxTimeHalfDead;
        StartAnimation(stateMachine.Player.PlayerAnimationData.HalfDeadParameterHash);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        TimeToRevivePlayer();
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Has salido del estado de MEDIO-MUERTA");
        StopAnimation(stateMachine.Player.PlayerAnimationData.HalfDeadParameterHash);
    }

    private void TimeToRevivePlayer()
    {
        //statsData.CurrentTimeHalfDead -= Time.deltaTime;

        //if (statsData.CurrentTimeHalfDead <= 0)
        //    stateMachine.ChangeState(stateMachine.FinalDeadState);
    }
}
