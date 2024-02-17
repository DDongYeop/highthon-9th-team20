using UnityEngine;

public class DieState : IState
{
    private EnemyMono _enemy;
    private int _dieTrigger = Animator.StringToHash("Die");
    
    public void OnEnterState()
    {
        _enemy.EnemyAnimator.SetTrigger(_dieTrigger);
    }

    public void UpdateState()
    {
        // do notting
    }

    public void OnExitState()
    {
        // do notting
    }

    public void Setup(EnemyMono enemyMono)
    {
        _enemy = enemyMono;
    }
}
