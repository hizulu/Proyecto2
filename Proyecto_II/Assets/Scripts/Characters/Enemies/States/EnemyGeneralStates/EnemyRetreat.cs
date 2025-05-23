/*
 * NOMBRE CLASE: EnemyRetreat
 * AUTOR: Sara Yue Madruga Mart�n, Jone Sainz Egea
 * FECHA: 11/03/2025
 * DESCRIPCI�N: Clase que define el estado de Retreat del enemigo.
 *              Hereda de EnemyStateTemplete, por lo que tiene acceso a la m�quina de estados y a Enemy.
 *              Se encarga de ejecutar la l�gica de la instancia espec�fica que contiene Enemy para el estado de Retreat.
 * VERSI�N: 1.0. Script base que ejecuta la l�gica de Retreat del enemigo
 */
public class EnemyRetreat : EnemyStateTemplate
{
    /*
     * Constructor del estado de Retreat del enemigo.
     * @param1 _stateMachine - Recibe una referencia de la m�quina de estados del enemigo para poder acceder a su informaci�n.
     */
    public EnemyRetreat(EnemyStateMachine _stateMachine) : base(_stateMachine) { }

    public override void Enter()
    {
        base.Enter();

        enemyStateMachine.Enemy.anim.SetBool("isMoving", true);
        enemyStateMachine.Enemy.EnemyRetreatBaseInstance.DoEnterLogic();
    }

    public override void Exit()
    {
        base.Exit();

        enemyStateMachine.Enemy.EnemyRetreatBaseInstance.DoExitLogic();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        enemyStateMachine.Enemy.EnemyRetreatBaseInstance.DoFrameUpdateLogic();
    }

    public override void UpdatePhysics()
    {
        base.UpdatePhysics();

        enemyStateMachine.Enemy.EnemyRetreatBaseInstance.DoPhysicsLogic();
    }
}
