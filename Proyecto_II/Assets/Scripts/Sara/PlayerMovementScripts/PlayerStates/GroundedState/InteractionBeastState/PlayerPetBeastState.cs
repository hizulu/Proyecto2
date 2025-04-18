/*
 * NOMBRE CLASE: PlayerPetBeastState
 * AUTOR: Sara Yue Madruga Mart�n
 * FECHA: 
 * DESCRIPCI�N: Clase que hereda de PlayerInteractionState
 * VERSI�N: 1.0. 
 */
public class PlayerPetBeastState : PlayerInteractionState
{
    public PlayerPetBeastState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    #region Variables
    private bool petBeastFinish;
    #endregion

    #region M�todos Base de la M�quina de Estados
    public override void Enter()
    {
        petBeastFinish = false;
        base.Enter();
        //Debug.Log("Has entrado en estado de Acariciar a la Bestia.");
        StartAnimation(stateMachine.Player.PlayerAnimationData.PetBeastParameterHash);
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        FinishAnimation();
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
    }

    public override void Exit()
    {
        base.Exit();
        //Debug.Log("Has salido del estado de Acariciar a la Bestia.");
        StopAnimation(stateMachine.Player.PlayerAnimationData.PetBeastParameterHash);
    }
    #endregion

    #region M�todos Propios PetBeastState
    /*
     * M�todo para comprobar que la animaci�n de acariciar se ha terminado para pasar al siguiente estado requerido.
     */
    protected override void FinishAnimation()
    {
        if (stateMachine.Player.AnimPlayer.GetCurrentAnimatorStateInfo(0).IsName("AcariciarBestia_Brisa") && stateMachine.Player.AnimPlayer.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            petBeastFinish = true;
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }
    #endregion
}
