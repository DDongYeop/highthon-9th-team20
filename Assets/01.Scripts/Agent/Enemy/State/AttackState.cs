using UnityEngine;

public class AttackState : IState
{
    private EnemyMono _enemy;
    private int _dieTrigger = Animator.StringToHash("Attack");

    private float _coolTime;
    
    public void OnEnterState()
    {
        //do nothing 
    }

    public void UpdateState()
    {
        _coolTime -= Time.deltaTime;
        if (_coolTime <= 0)
        {
            _enemy.EnemyAnimator.SetTrigger(_dieTrigger);
            _coolTime = _enemy.AttackCoolTime;
        }
        else
        {
            int dir = Mathf.RoundToInt(GameManager.Instance.Player.transform.position.x - _enemy.transform.position.x);
            dir = Mathf.Clamp(dir, -1, 1);
            _enemy.EnemyDirection(dir);
        }
    }

    public void OnExitState()
    {
        //do nothing  
    }

    public void Setup(EnemyMono enemyMono)
    {
        _enemy = enemyMono;
        _coolTime = 0;
    }
}
