using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCallBeastState : PlayerInteractionState
{
    public PlayerCallBeastState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    #region Variables
    private bool callBeastFinish;
    #endregion

    #region M�todos Base de la M�quina de Estados
    public override void Enter()
    {
        EventsManager.TriggerNormalEvent("CallBeast");
        callBeastFinish = false;
        base.Enter();
        Debug.Log("Has entrado en el estado de LLAMAR A LA BESTIA");
        StartAnimation(stateMachine.Player.PlayerAnimationData.CallBeastParameterHash);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        FinishAnimation();
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Has salido del estado de LLAMAR A LA BESTIA");
        StopAnimation(stateMachine.Player.PlayerAnimationData.CallBeastParameterHash);
    }
    #endregion

    #region M�todos Propios PetBeastState
    /*
     * M�todo para comprobar que la animaci�n de acariciar se ha terminado para pasar al siguiente estado requerido.
     */
    protected override void FinishAnimation()
    {
        if (stateMachine.Player.AnimPlayer.GetCurrentAnimatorStateInfo(0).IsName("CallBeast") && stateMachine.Player.AnimPlayer.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            callBeastFinish = true;
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }

    protected override void ChangeFacePlayer()
    {
        base.ChangeFacePlayer();

        SetFaceProperty(1, new Vector2(0.33f, 0f));
        SetFaceProperty(2, new Vector2(0.375f, 0f));
        SetFaceProperty(3, new Vector2(0f, 0f));
    }
    #endregion
}
