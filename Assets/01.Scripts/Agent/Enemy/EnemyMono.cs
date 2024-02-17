using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class EnemyMono : AgentMono
{
    [Header("Enemy Distance")]
    [SerializeField] private float _followDistance; //어느 거리부터 움직일지
    [SerializeField] private float _attackDistance; //어느 거리부터 공격할지

    [Header("Movement")] 
    [HideInInspector] public LayerMask WhatIsGround;
    private bool _isSameGround;
    public float Speed;

    [Header("AI State")] 
    private EnemyState _currentState;
    private Dictionary<EnemyState, IState> _stateDic = new Dictionary<EnemyState, IState>();

    [Header("Animation")] 
    [HideInInspector] public Animator EnemyAnimator;

    private void Awake()
    {
        EnemyAnimator = GetComponent<Animator>();
        
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

    public void ChangeState(EnemyState changeState)
    {
        _stateDic[_currentState].OnExitState();
        _currentState = changeState;
        _stateDic[_currentState].OnEnterState();
    }
}
