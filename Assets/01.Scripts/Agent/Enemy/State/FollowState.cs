using UnityEngine;

public class FollowState : IState
{
    private int _direction;
    private EnemyMono _enemy;

    public void Setup(EnemyMono enemyMono)
    {
        _enemy = enemyMono;
    }
    
    public void OnEnterState()
    {
        int dir = Mathf.RoundToInt(_enemy.transform.position.x - GameManager.Instance.Player.transform.position.x);
        _direction = Mathf.Clamp(dir, -1, 1);
    }
    
    public void UpdateState()
    {
        _enemy.transform.position += new Vector3(_direction, 0) * (Time.deltaTime * _enemy.Speed);
    }

    public void OnExitState()
    {
        // do noting 
    }
}