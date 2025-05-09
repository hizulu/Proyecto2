using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * NOMBRE CLASE: EnemyAttackCombo
 * AUTOR: Sara Yue Madruga Mart�n
 * FECHA: 06/05/2025
 * DESCRIPCI�N: Clase que define el comportamiento espec�fico del Demonio del Aire.        
 * VERSI�N: 1.0.
 */

[CreateAssetMenu(fileName = "Attack-Combo", menuName = "Enemy Logic/Attack Logic/Combo")]
public class EnemyAttackCombo : EnemyStateSOBase
{
    [SerializeField] private EnemyComboAttacksSOBase[] comboAttacks;

    private int currentAttack = 0;
    private bool isComboFinished = false;

    public override void DoEnterLogic()
    {
        Debug.Log("El enemigo ha entrado en el estado de COMBO ATAQUE");
        base.DoEnterLogic();
        enemy.anim.SetBool("Attack", true);
        comboAttacks[0].Initialize(enemy);
        currentAttack = 0;
        isComboFinished = false;
        comboAttacks[currentAttack].EnemyAttack();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        if (isComboFinished) return;

        comboAttacks[currentAttack].FrameLogic();

        if (comboAttacks[currentAttack].IsFinished())
        {
            comboAttacks[currentAttack].Exit();
            currentAttack++;

            if (currentAttack < comboAttacks.Length)
            {
                comboAttacks[currentAttack].Initialize(enemy);
                comboAttacks[currentAttack].EnemyAttack();
            }
            else
                isComboFinished = true;
        }
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
        enemy.anim.SetBool("Attack", false);
        Debug.Log("El enemigo ha salido del estado de COMBO ATAQUE");
    }
}