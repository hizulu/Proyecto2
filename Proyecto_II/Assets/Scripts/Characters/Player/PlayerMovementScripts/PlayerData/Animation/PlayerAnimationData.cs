using System;
using UnityEngine;

/*
 * NOMBRE CLASE: PlayerAnimationData
 * AUTOR: Sara Yue Madruga Mart�n
 * FECHA: 09/03/2025
 * DESCRIPCI�N: Clase que gestiona los par�metros de animaci�n del jugador. Almacena los nombres y hashes de los par�metros utilizados por el Animator.
 * VERSI�N: 1.0
 */
[Serializable]
public class PlayerAnimationData
{
    [SerializeField] private string groundedParameterName = "Grounded";
    [SerializeField] private string movedParameterName = "Moved";
    [SerializeField] private string stopParameterName = "Stop";
    [SerializeField] private string attackParameterName = "Attack";
    [SerializeField] private string airborneParameterName = "Airborne";
    [SerializeField] private string deathParameterName = "Death";
    [SerializeField] private string interactionsParameterName = "InteractionsBeast";

    [SerializeField] private string idleParameterName = "isIdle";
    [SerializeField] private string walkParameterName = "isWalking";
    [SerializeField] private string runParameterName = "isRunning";
    [SerializeField] private string sprintParameterName = "isSprinting";
    [SerializeField] private string crouchPoseParameterName = "isCrouchPose";
    [SerializeField] private string crouchParameterName = "isCrouching";
    [SerializeField] private string attack01ParameterName = "isAttacking01";
    [SerializeField] private string attack02ParameterName = "isAttacking02";
    [SerializeField] private string attack03ParameterName = "isAttacking03";
    [SerializeField] private string defenseParameterName = "isDefending";
    [SerializeField] private string takeDamageParameterName = "isTakingDamage";
    [SerializeField] private string healParameterName = "isHealing";
    [SerializeField] private string jumpParameterName = "isJumping";
    [SerializeField] private string doubleJumpParameterName = "isDoubleJumping";
    [SerializeField] private string fallParameterName = "isFalling";
    [SerializeField] private string landParameterName = "isLanding";
    [SerializeField] private string hardLandParameterName = "isHardLanding";
    [SerializeField] private string halfDeadParameterName = "isHalfDeading";
    [SerializeField] private string idleHalfDeadParameterName = "isIdleHalfDead";
    [SerializeField] private string finalDeadParameterName = "isFinalDeading";
    [SerializeField] private string revivePlayerParameterName = "isRevivingPlayer";

    [SerializeField] private string callBeastParameterName = "isCallingBeast";
    [SerializeField] private string petBeastParameterName = "isPettingBeast";
    [SerializeField] private string rideBeastParameterName = "isRidingBeast";
    [SerializeField] private string dismountBeastParameterName = "isDismountingBeast";
    [SerializeField] private string healBeastParameterName = "isHealingBeast";
    [SerializeField] private string reviveBeastParameterName = "isRevivingBeast";

    [SerializeField] private string pickUpParameterName = "isPickUp";

    public int GroundedParameterHash { get; private set; }
    public int MovedParameterHash { get; private set; }
    public int StopParameterHash { get; private set; }
    public int AttackParameterHash { get; private set; }
    public int AirborneParameterHash { get; private set; }
    public int DeathParameterHash { get; private set; }
    public int InteractionsParameterHash { get; private set; }

    public int IdleParameterHash { get; private set; }
    public int WalkParameterHash { get; private set; }
    public int RunParameterHash { get; private set; }
    public int SprintParameterHash { get; private set; }
    public int CrouchPoseParameterHash { get; private set; }
    public int CrouchParameterHash { get; private set; }
    public int Attack01ParameterHash { get; private set; }
    public int Attack02ParameterHash { get; private set; }
    public int Attack03ParameterHash { get; private set; }
    public int DefenseParameterHash { get; private set; }
    public int TakeDamageParameterHash { get; private set; }
    public int HealParameterHash { get; private set; }
    public int JumpParameterHash { get; private set; }
    public int DoubleJumpParameterHash { get; private set; }
    public int FallParameterHash { get; private set; }
    public int LandParameterHash { get; private set; }
    public int HardLandParameterHash { get; private set; }
    public int HalfDeadParameterHash { get; private set; }
    public int IdleHalfDeadParameterHash { get; private set; }
    public int FinalDeadParameterHash { get; private set; }
    public int RevivePlayerParameterHash { get; private set; }

    public int CallBeastParameterHash { get; private set; }
    public int PetBeastParameterHash { get; private set; }
    public int RideBeastParameterHash { get; private set; }
    public int DismountBeastParameterHash { get; private set; }
    public int HealBeastParameterHash { get; private set; }
    public int ReviveBeastParameterHash { get; private set; }

    public int PickUpParameterHash { get; private set; }

    public void Initialize()
    {
        GroundedParameterHash = Animator.StringToHash(groundedParameterName);
        MovedParameterHash = Animator.StringToHash(movedParameterName);
        StopParameterHash = Animator.StringToHash(stopParameterName);
        AttackParameterHash = Animator.StringToHash(attackParameterName);
        AirborneParameterHash = Animator.StringToHash(airborneParameterName);
        DeathParameterHash = Animator.StringToHash(deathParameterName);
        InteractionsParameterHash = Animator.StringToHash(interactionsParameterName);

        IdleParameterHash = Animator.StringToHash(idleParameterName);
        WalkParameterHash = Animator.StringToHash(walkParameterName);
        RunParameterHash = Animator.StringToHash(runParameterName);
        SprintParameterHash = Animator.StringToHash(sprintParameterName);
        CrouchPoseParameterHash = Animator.StringToHash(crouchPoseParameterName);
        CrouchParameterHash = Animator.StringToHash(crouchParameterName);

        Attack01ParameterHash = Animator.StringToHash(attack01ParameterName);
        Attack02ParameterHash = Animator.StringToHash(attack02ParameterName);
        Attack03ParameterHash = Animator.StringToHash(attack03ParameterName);
        DefenseParameterHash = Animator.StringToHash(defenseParameterName);
        TakeDamageParameterHash = Animator.StringToHash(takeDamageParameterName);
        HealParameterHash = Animator.StringToHash(healParameterName);

        JumpParameterHash = Animator.StringToHash(jumpParameterName);
        DoubleJumpParameterHash = Animator.StringToHash(doubleJumpParameterName);
        FallParameterHash = Animator.StringToHash(fallParameterName);
        LandParameterHash = Animator.StringToHash(landParameterName);
        HardLandParameterHash = Animator.StringToHash(hardLandParameterName);

        HalfDeadParameterHash = Animator.StringToHash(halfDeadParameterName);
        IdleHalfDeadParameterHash = Animator.StringToHash(idleHalfDeadParameterName);
        FinalDeadParameterHash = Animator.StringToHash(finalDeadParameterName);
        RevivePlayerParameterHash = Animator.StringToHash(revivePlayerParameterName);

        CallBeastParameterHash = Animator.StringToHash(callBeastParameterName);
        PetBeastParameterHash = Animator.StringToHash(petBeastParameterName);
        RideBeastParameterHash = Animator.StringToHash(rideBeastParameterName);
        DismountBeastParameterHash = Animator.StringToHash(dismountBeastParameterName);
        HealBeastParameterHash = Animator.StringToHash(healBeastParameterName);
        ReviveBeastParameterHash = Animator.StringToHash(reviveBeastParameterName);

        PickUpParameterHash = Animator.StringToHash(pickUpParameterName);
    }
}