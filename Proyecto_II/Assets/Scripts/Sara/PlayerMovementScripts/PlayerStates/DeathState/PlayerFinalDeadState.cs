/*
 * NOMBRE CLASE: PlayerFinalDeadState
 * AUTOR: Sara Yue Madruga Mart�n
 * FECHA: 
 * DESCRIPCI�N: Clase que hereda de PlayerDeathState
 * VERSI�N: 1.0. 
 */
public class PlayerFinalDeadState : PlayerDeathState
{
    public PlayerFinalDeadState(PlayerStateMachine _stateMachine) : base(_stateMachine) { }

    #region M�todos Base de la M�quina de Estados
    public override void Enter()
    {
        base.Enter();
        //Debug.Log("Has entrado en el estado de MUERTE FINAL");
        StartAnimation(stateMachine.Player.PlayerAnimationData.FinalDeadParameterHash);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }

    public override void Exit()
    {
        base.Exit();
        //Debug.Log("Has salido del estado de MUERTE FINAL");
        StopAnimation(stateMachine.Player.PlayerAnimationData.FinalDeadParameterHash);
    }
    #endregion
}
