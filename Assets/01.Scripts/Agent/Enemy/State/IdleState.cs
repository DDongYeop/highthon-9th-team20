using UnityEngine;

public class IdleState : IState
{
    private int _direction;
    private EnemyMono _enemy;
    private Transform _enemyTrm;
    
    public void OnEnterState()
    {
        _direction = 1;
    }
    
    public void UpdateState()
    {
        RaycastHit2D hit = Physics2D.Raycast(_enemyTrm.position + new Vector3(_direction, 0, 0), Vector2.down, 2,_enemy.WhatIsGround);
        if (!hit)
            _direction *= -1;
        _enemy.EnemyDirection(_direction);

        _enemyTrm.position += new Vector3(_direction, 0, 0) * (Time.deltaTime * _enemy.Speed);
    }

    public void OnExitState()
    {
        //do nothing 
    }

    public void Setup(EnemyMono enemyMono)
    {
        _enemy = enemyMono;
        _enemyTrm = enemyMono.transform;
    }
}
