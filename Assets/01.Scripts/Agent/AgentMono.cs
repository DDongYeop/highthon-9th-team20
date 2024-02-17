using System;
using UnityEngine;
using UnityEngine.Serialization;

public class AgentMono : MonoBehaviour
{
    [Header("Ground")] 
    [SerializeField] protected LayerMask _whatIsGround;
    [SerializeField] private float _groundCheckRayDistance;
    [HideInInspector] public string CurrentGround;

    [Header("Health")] 
    [SerializeField] private int _maxHP;
    [SerializeField] private int _currentHP;
    public int CurrentHP
    {
        get => _currentHP;
        set
        {
            _currentHP += value;
            if (_currentHP <= 0)
            {
                Debug.Log($"{gameObject.name} is die");
                Die();
            }
        }
    }

    [Header("Other")] 
    public bool IsPlayer;

    protected virtual void Update()
    {
        GroundCheck();
    }

    protected virtual void Die()
    {
        
    }
    
    private void GroundCheck()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _groundCheckRayDistance, _whatIsGround);
        if (hit)
            CurrentGround = hit.transform.name;
        else
            CurrentGround = null;
    }

#if UNITY_EDITOR
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -_groundCheckRayDistance, 0));
        Gizmos.color = Color.white;
    }
    
#endif
}
