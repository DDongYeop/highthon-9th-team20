using UnityEngine;

public class EnemyAttackArea : MonoBehaviour
{
    private EnemyMono _rootAgent;

    private void Awake()
    {
        _rootAgent = transform.root.GetComponent<EnemyMono>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.TryGetComponent(out AgentMono agent))
        {
            if (agent.IsPlayer)
                agent.CurrentHP -= _rootAgent.Damage;
        }
    }
}
