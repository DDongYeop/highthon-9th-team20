public interface IState 
{
    public void OnEnterState();
    public void UpdateState();
    public void OnExitState();
    public void Setup(EnemyMono enemyMono);
}
