using UnityEngine;

/*
 * NOMBRE CLASE: PlayerAttackState
 * AUTOR: Sara Yue Madruga Mart�n
 * FECHA: 09/03/2025
 * DESCRIPCI�N: Clase que hereda de PlayerGroundedState. Es un estado padre para gestionar el combo de ataque de Player.
 * VERSI�N: 1.0. 
 */

public class PlayerAttackState : PlayerGroundedState
{
    public PlayerAttackState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    #region Variables
    protected bool attackFinish;

    protected float attackTimeElapsed;
    protected float maxTimeToNextAttack = 1f;
    protected int currentNumAttack;

    protected bool canContinueCombo;
    protected bool isWaitingForInput;

    protected float detectionRange = 20f;

    protected float attackDamageModifierMin;
    protected float attackDamageModifierMax;

    #endregion

    #region M�todos Base de la M�quina de Estados
    public override void Enter()
    {
        //hasDashedToTarget = false;
        //dashTraveledDistance = 0f;
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

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        AlignPlayerToNearestEnemy();

        //if (!hasDashedToTarget)
        //    DashToPointTarget();
    }

    public override void Exit()
    {
        base.Exit();
        StopAnimation(stateMachine.Player.PlayerAnimationData.AttackParameterHash);
        //Debug.Log("Has salido del estado de ATACAR");
    }
    #endregion

    #region M�todos Propios AttackState
    /// <summary>
    /// M�todo para orientar al Player hacia los enemigos si ataca.
    /// Debe estar dentro de un rango limitado.
    /// </summary>
    protected virtual void AlignPlayerToNearestEnemy()
    {
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        Collider[] colliders = Physics.OverlapSphere(stateMachine.Player.transform.position, detectionRange);

        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Enemy"))
            {
                float distance = Vector3.Distance(stateMachine.Player.transform.position, col.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = col.transform;
                }
            }
        }

        if (closestEnemy != null)
        {
            Vector3 direction = closestEnemy.position - stateMachine.Player.transform.position;
            direction.y = 0f;

            if (direction.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                stateMachine.Player.transform.rotation = Quaternion.Slerp(stateMachine.Player.transform.rotation, targetRotation, Time.deltaTime * 10f);
            }
        }
    }

    #region M�TODO PROVISIONAL MINI-DASH HACIA ENEMIGO
    /*
     * M�todo que hace que el Player, si tiene marcado un enemigo, haga un mini-dash para acercarse a �l y golpearle (facilitar un poco el combate).
     */
    //protected bool hasDashedToTarget = false;
    //protected float dashSpeed = 10f;
    //protected float dashDistance = 1.5f;

    //private Vector3 dashDirection;
    //private float dashTraveledDistance = 0f;

    //protected virtual void DashToPointTarget()
    //{
    //    if (hasDashedToTarget || !stateMachine.Player.pointTarget.gameObject.activeInHierarchy)
    //        return;

    //    if (dashTraveledDistance == 0f)
    //    {
    //        Vector3 toTarget = stateMachine.Player.pointTarget.transform.position - stateMachine.Player.transform.position;
    //        toTarget.y = 0f;
    //        float distanceToTarget = toTarget.magnitude;

    //        if (distanceToTarget < 1f)
    //        {
    //            hasDashedToTarget = true;
    //            return;
    //        }

    //        dashDirection = toTarget.normalized;
    //    }

    //    float stepDistance = dashSpeed * Time.deltaTime;
    //    stateMachine.Player.RbPlayer.MovePosition(stateMachine.Player.transform.position + dashDirection * stepDistance);
    //    dashTraveledDistance += stepDistance;

    //    if (dashTraveledDistance >= dashDistance)
    //        hasDashedToTarget = true;
    //}
    #endregion
    #endregion
}
