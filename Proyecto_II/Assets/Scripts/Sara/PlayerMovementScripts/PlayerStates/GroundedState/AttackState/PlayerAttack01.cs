using UnityEngine;

/*
 * NOMBRE CLASE: PlayerAttack01
 * AUTOR: Sara Yue Madruga Mart�n
 * FECHA: 
 * DESCRIPCI�N: Clase que hereda de PlayerAttackState
 * VERSI�N: 1.0. 
 */
public class PlayerAttack01 : PlayerAttackState
{
    public PlayerAttack01(PlayerStateMachine stateMachine) : base(stateMachine) { }

    #region M�todos Base de la M�quina de Estados
    public override void Enter()
    {
        maxTimeToNextAttack = 0.5f;
        attackTimeElapsed = 0;
        attackFinish = false;
        attackDamageModifierMin = 1f;
        attackDamageModifierMax = 1.3f;
        base.Enter();
        stateMachine.Player.GolpearPrueba();
        StartAnimation(stateMachine.Player.PlayerAnimationData.Attack01ParameterHash);
        float attackDamageModifier = UnityEngine.Random.Range(attackDamageModifierMin, attackDamageModifierMax);
        float attackDamageCombo01 = stateMachine.StatsData.AttackDamageBase * attackDamageModifier;
        EventsManager.TriggerSpecialEvent<float>("OnAttack01Enemy", attackDamageCombo01); // EVENTO: Crear evento de da�ar al enemigo con da�o del ComboAttack01.
        //Debug.Log("Da�o del ataque 1: " + " " + attackDamageCombo01);
    }

    public override void HandleInput()
    {
        if (stateMachine.Player.PlayerInput.PlayerActions.Attack.triggered && !isWaitingForInput)
        {
            canContinueCombo = true;
            isWaitingForInput = true;
        }

        if (attackFinish && canContinueCombo)
        {
            if (attackTimeElapsed < maxTimeToNextAttack && isWaitingForInput)
                stateMachine.ChangeState(stateMachine.Attack02State);
            else
                stateMachine.ChangeState(stateMachine.IdleState);
        }
    }

    public override void UpdateLogic()
    {
        FinishAnimation();
        attackTimeElapsed += Time.deltaTime;
    }

    public override void Exit()
    {
        canContinueCombo = false;
        isWaitingForInput = false;
        attackFinish = false;
        base.Exit();
        StopAnimation(stateMachine.Player.PlayerAnimationData.Attack01ParameterHash);
    }
    #endregion

    #region M�todos Propios Attack01State
    /*
     * M�todo para comprobar que la animaci�n del ataque 1 se ha terminado para pasar al siguiente estado requerido.
     */
    protected override void FinishAnimation()
    {
        if (stateMachine.Player.AnimPlayer.GetCurrentAnimatorStateInfo(0).IsName("Attack01") && stateMachine.Player.AnimPlayer.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            attackFinish = true;
    }

    protected override void Move()
    {
        if (!attackFinish) return;
    }
    #endregion
}
