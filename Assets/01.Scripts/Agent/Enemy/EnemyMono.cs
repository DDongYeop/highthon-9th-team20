using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyMono : AgentMono
{
    [Header("Enemy Distance")]
    [SerializeField] private float _followDistance; //어느 거리부터 움직일지
    [SerializeField] private float _attackDistance; //어느 거리부터 공격할지

    [Header("Movement")] 
    [HideInInspector] public LayerMask WhatIsGround;
    private bool _isSameGround;
    public float Speed;

    [Header("Attack")] 
    public float AttackCoolTime;

    [Header("AI State")] 
    private EnemyState _currentState;
    private Dictionary<EnemyState, IState> _stateDic = new Dictionary<EnemyState, IState>();

    [Header("Visual")] 
    private Transform _visualTrm;
    [HideInInspector] public Animator EnemyAnimator;

    [Header("Damage")] 
    public int Damage;

    private void Awake()
    {
        EnemyAnimator = transform.Find("Visual").GetComponent<Animator>();
        _visualTrm = EnemyAnimator.transform;
        
        _stateDic.Add(EnemyState.IDLE, new IdleState());
        _stateDic.Add(EnemyState.FOLLOW, new FollowState());
        _stateDic.Add(EnemyState.ATTACK, new AttackState());
        _stateDic.Add(EnemyState.DIE, new DieState());

        _currentState = EnemyState.IDLE;
        _stateDic[_currentState].OnEnterState();

        foreach (var state in _stateDic)
            state.Value.Setup(this);

        WhatIsGround = _whatIsGround;
    }

    protected override void Update()
    {
        base.Update();

        _isSameGround = CurrentGround == GameManager.Instance.Player.CurrentGround ? true : false;
        if (!_isSameGround)
        {
            if (_currentState != EnemyState.IDLE)
                ChangeState(EnemyState.IDLE);
            _stateDic[_currentState].UpdateState();
            return;
        }
        
        EnemyStateChange();
        
        _stateDic[_currentState].UpdateState();
    }

    protected override void Die()
    {
        ChangeState(EnemyState.DIE);
        Destroy(gameObject); // 차후 수정 
    }

    private void EnemyStateChange()
    {
        if (_currentState == EnemyState.DIE)
            return;
        
        float distance = Vector2.Distance(transform.position, GameManager.Instance.Player.transform.position);
        if (distance <= _attackDistance)
        {
            if (_currentState != EnemyState.ATTACK)
                ChangeState(EnemyState.ATTACK);
            return;
        }
        else if (distance <= _followDistance)
        {
            if (_currentState != EnemyState.FOLLOW)
                ChangeState(EnemyState.FOLLOW);
            return;
        }
        if (_currentState != EnemyState.IDLE)
            ChangeState(EnemyState.IDLE);
    }

    public void EnemyDirection(int value)
    {
        _visualTrm.localScale = new Vector3(value == 1 ? 1 : -1, 1, 1);
    }

    public void ChangeState(EnemyState changeState)
    {
        _stateDic[_currentState].OnExitState();
        _currentState = changeState;
        _stateDic[_currentState].OnEnterState();
    }

#if UNITY_EDITOR
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _followDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackDistance);
        Gizmos.color = Color.white;
    }
    
#endif
}
