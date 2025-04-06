using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack-Zig Zag Jump", menuName = "Enemy Logic/Attack Logic/Ziz Zag Jump")]
public class EnemyAttackZigZagJump : EnemyAttackSOBase
{
    [SerializeField] private float attackDamage = 20f;
    [SerializeField] private float distanceToStopAttackState = 5f;

    [SerializeField] private float jumpDuration = 0.6f;
    [SerializeField] private float lateralOffset = 2f;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float stopDistance = 0.5f;

    private bool isAttacking = false;

    private float distanceToStopAttackStateSQR = 0f;

    public override void DoEnterLogic()
    {
        base.DoEnterLogic();

        distanceToStopAttackStateSQR = distanceToStopAttackState * distanceToStopAttackState;

        if (!isAttacking)
        {
            isAttacking = true;
            enemy.StartCoroutine(DoThreeZigZags());
        }
    }

    public override void DoExitLogic()
    {
        base.DoExitLogic();
    }

    public override void DoFrameUpdateLogic()
    {
        base.DoFrameUpdateLogic();

        float distanceToPlayerSQR = (enemy.transform.position - playerTransform.position).sqrMagnitude;      

        if (distanceToPlayerSQR > distanceToStopAttackStateSQR)
        {
            enemy.doAttack = false;
            enemy.doChase = true;
        }
    }

    public override void DoPhysicsLogic()
    {
        base.DoPhysicsLogic();
    }

    public override void Initialize(GameObject gameObject, Enemy enemy)
    {
        base.Initialize(gameObject, enemy);
    }

    public override void ResetValues()
    {
        base.ResetValues();
    }

    private IEnumerator DoThreeZigZags()
    {
        Vector3 startPosition = enemy.transform.position;
        Vector3 directionToTarget = (playerTransform.position - startPosition).normalized;
        Vector3 right = Vector3.Cross(Vector3.up, directionToTarget).normalized;

        float[] distances = { 0.25f, 0.5f, 0.75f };
        float[] lateralOffsets = { lateralOffset, lateralOffset * 0.66f, lateralOffset * 0.33f };

        for (int i = 0; i < 3; i++)
        {
            Vector3 jumpTarget = startPosition + directionToTarget * distances[i] * Vector3.Distance(startPosition, playerTransform.position);
            jumpTarget += (i % 2 == 0 ? right : -right) * lateralOffsets[i];

            yield return MoveInArc(enemy.transform.position, jumpTarget, jumpHeight);
        }

        Vector3 finalJumpTarget = playerTransform.position - directionToTarget * stopDistance;
        yield return MoveInArc(enemy.transform.position, finalJumpTarget, jumpHeight);

        Attack();
    }

    private IEnumerator MoveInArc(Vector3 start, Vector3 end, float height)
    {
        float elapsedTime = 0;
        while (elapsedTime < jumpDuration)
        {
            float t = elapsedTime / jumpDuration;
            Vector3 position = Vector3.Lerp(start, end, t);
            position.y += Mathf.Sin(t * Mathf.PI) * height;
            transform.position = position;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = end;
    }

    private void Attack()
    {
        Debug.Log("El enemigo ataca al jugador");
        // Called after three zig zags done
        // TODO: enemy.anim.SetTrigger("ataca");
        // TODO: play enemy attack sound depending on enemy
        player.TakeDamage(50f);
        isAttacking = false;
        enemy.doAttack = false;
        enemy.doRetreat = true;
    }
}
