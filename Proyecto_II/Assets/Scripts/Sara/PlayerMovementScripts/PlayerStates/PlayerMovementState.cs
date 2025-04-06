using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * NOMBRE SCRIPT: PlayerMovementState
 * AUTOR: Sara Yue Madruga Martín
 * FECHA: 09/03/2025
 * DESCRIPCIÓN: Clase que hereda de IState
 * VERSIÓN: 1.0.
 */
public class PlayerMovementState : IState
{
    protected PlayerStateMachine stateMachine;

    protected readonly PlayerGroundedData groundedData;
    protected readonly PlayerAirborneData airborneData;
    //protected readonly PlayerStatsData statsData;

    public PlayerMovementState(PlayerStateMachine _stateMachine)
    {
        stateMachine = _stateMachine;

        groundedData = stateMachine.Player.Data.GroundedData;
        airborneData = stateMachine.Player.Data.AirborneData;
        //statsData = stateMachine.Player.Data.StatsData;
    }

    public virtual void Enter()
    {
        AddInputActionsCallbacks();
    }

    public virtual void HandleInput()
    {
        ReadMovementInput();
    }

    public virtual void UpdateLogic()
    {
        //Debug.Log("Actualizando");
    }

    public virtual void UpdatePhysics()
    {
        Move();
    }

    public virtual void Exit()
    {
        RemoveInputActionsCallbacks();
    }

    public virtual void OnTriggerEnter(Collider collider)
    {
        if (stateMachine.Player.LayerData.IsGroundLayer(collider.gameObject.layer))
        {
            ContactWithGround(collider);
            //Debug.Log(collider.gameObject.name);
            //return;
        }

        if (collider.CompareTag("Enemy")) ;
            //TakeDamage(50);
    }

    public virtual void OnTriggerExit(Collider collider)
    {
        if (stateMachine.Player.LayerData.IsGroundLayer(collider.gameObject.layer))
        {
            NoContactWithGround(collider);
            //return;
        }
    }

    public void ReadMovementInput()
    {
        stateMachine.MovementData.MovementInput = stateMachine.Player.PlayerInput.PlayerActions.Movement.ReadValue<Vector2>();
    }

    protected void StartAnimation(int hashNumAnimation)
    {
        stateMachine.Player.AnimPlayer.SetBool(hashNumAnimation, true);
    }

    protected void StopAnimation(int hashNumAnimation)
    {
        stateMachine.Player.AnimPlayer.SetBool(hashNumAnimation, false);
    }

    protected virtual void AddInputActionsCallbacks()
    {
        stateMachine.Player.PlayerInput.PlayerActions.Movement.canceled += OnMovementCanceled;
        stateMachine.Player.PlayerInput.PlayerActions.Run.canceled += OnMovementCanceled;
        
        
        
        stateMachine.Player.PlayerInput.PlayerActions.Crouch.canceled -= OnMovementCanceled;
    }

    protected virtual void RemoveInputActionsCallbacks()
    {
        stateMachine.Player.PlayerInput.PlayerActions.Movement.canceled -= OnMovementCanceled;
        stateMachine.Player.PlayerInput.PlayerActions.Run.canceled -= OnMovementCanceled;
    }

    protected virtual void Move()
    {
        if (stateMachine.MovementData.MovementInput == Vector2.zero || stateMachine.MovementData.MovementSpeedModifier == 0f)
            return;

        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0;
        cameraForward.Normalize();

        Vector3 movementDirection = cameraForward * stateMachine.MovementData.MovementInput.y + Camera.main.transform.right * stateMachine.MovementData.MovementInput.x;
        movementDirection.Normalize();

        float movSpeed = GetMovementSpeed();
        movementDirection.Normalize();
        stateMachine.Player.RbPlayer.MovePosition(stateMachine.Player.RbPlayer.position + movementDirection * movSpeed * Time.deltaTime);
        Rotate(movementDirection);
    }

    public void Rotate(Vector3 movementDirection)
    {
        if (movementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection); // Hace que el personaje gire en la dirección donde se produce el movimiento.
            stateMachine.Player.RbPlayer.rotation = Quaternion.Slerp(stateMachine.Player.RbPlayer.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    protected Vector3 GetMovementInputDirection()
    {
        return new Vector3(stateMachine.MovementData.MovementInput.x, 0f, stateMachine.MovementData.MovementInput.y);
    }

    protected float GetMovementSpeed()
    {
        float movementSpeed = groundedData.BaseSpeed * stateMachine.MovementData.MovementSpeedModifier;

        return movementSpeed;
    }

    protected virtual void OnMovementCanceled(InputAction.CallbackContext context)
    {

    }

    protected virtual void ContactWithGround(Collider collider)
    {

    }

    protected virtual void NoContactWithGround(Collider collider)
    {

    }


    //private void TakeDamage(float _enemyDamage)
    //{
    //    statsData.CurrentHealth -= _enemyDamage;
    //    Debug.Log(statsData.CurrentHealth);
    //    if (statsData.CurrentHealth <= 0)
    //        stateMachine.ChangeState(stateMachine.HalfDeadState);
    //}
}
