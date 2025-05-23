using UnityEngine;
using UnityEngine.InputSystem;

/*
 * NOMBRE CLASE: PlayerRunState
 * AUTOR: Sara Yue Madruga Mart�n
 * FECHA: 10/04/2025
 * DESCRIPCI�N: Clase que hereda de PlayerMovedState.
 *              Subestado que gestiona la acci�n de correr.
 * VERSI�N: 1.0. 
 */

public class PlayerRunState : PlayerMovedState
{
    public PlayerRunState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    #region M�todos Base de la M�quina de Estados
    public override void Enter()
    {
        stateMachine.MovementData.MovementSpeedModifier = groundedData.RunData.RunSpeedModif;
        base.Enter();
        StartAnimation(stateMachine.Player.PlayerAnimationData.RunParameterHash);
        //Debug.Log("Has entrado en el estado de CORRER.");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        stateMachine.Player.SfxPlayer.PlayRandomSFX(BrisaSFXType.Run);
        //AudioManager.Instance.PlaySFX(AudioManager.Instance.run);
        //audioManager.PlaySFX(AudioManager.Instance.run);
        // Brisa no puede correr si no est� en movimiento.
        if (stateMachine.MovementData.MovementInput == Vector2.zero)
            stateMachine.ChangeState(stateMachine.IdleState);
    }

    public override void Exit()
    {
        base.Exit();
        stateMachine.Player.SfxPlayer.StopSound(BrisaSFXType.Run);
        //AudioManager.Instance.StopSFX();
        //audioManager.StopSFX();
        StopAnimation(stateMachine.Player.PlayerAnimationData.RunParameterHash);
        //Debug.Log("Has salido del estado de CORRER.");
    }
    #endregion

    #region M�todos Propios RunState
    /// <summary>
    /// M�todo sobreescrito para cambiar la expresi�n de Brisa cuando est� corriendo.
    /// </summary>
    protected override void ChangeFacePlayer()
    {
        base.ChangeFacePlayer();

        SetFaceProperty(1, new Vector2(0.78f, 0f));
        SetFaceProperty(2, new Vector2(0f, 0f));
        SetFaceProperty(3, new Vector2(0f, 0f));
    }
    #endregion

    #region M�todo Cancelar Entrada Input
    /// <summary>
    /// M�todo sobrescrito que se ejecuta cuando se cancela la entrada de movimiento.
    /// </summary>
    /// <param name="context">Informaci�n del input asociado a la acci�n.</param>
    protected override void OnMovementCanceled(InputAction.CallbackContext context)
    {
        OnStop();
    }
    #endregion
}
