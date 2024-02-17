using System.Collections;
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

    [Header("Attack")] 
    public float AttackCoolTime;

    [Header("AI State")] 
    private EnemyState _currentState;
    private Dictionary<EnemyState, IState> _stateDic = new Dictionary<EnemyState, IState>();

    [Header("Visual")] 
    private Transform _visualTrm;
    private SpriteRenderer _spriteRenderer;
    [HideInInspector] public Animator EnemyAnimator;

    [Header("Damage")] 
    public int Damage;

    [Header("Die")] 
    [SerializeField] private float _blinkingTime;

    [Header("Other")] 
    [HideInInspector] public AudioSource AttackSound;

    private void Awake()
    {
        EnemyAnimator = transform.Find("Visual").GetComponent<Animator>();
        _spriteRenderer = EnemyAnimator.GetComponent<SpriteRenderer>();
        _visualTrm = EnemyAnimator.transform;
        AttackSound = GetComponent<AudioSource>();
        
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
        StartCoroutine(EnemyDieCo());
    }

    private IEnumerator EnemyDieCo()
    {
        int x = 1;
        int y = 0;
        
        for (int i = 0; i < 5; ++i)
        {
            float currentTime = 0;
            while (currentTime <= _blinkingTime)
            {
                yield return null;
                currentTime += Time.deltaTime;
                float time = currentTime / _blinkingTime;
                _spriteRenderer.color = new Color(1, 1, 1, Mathf.Lerp(x, y, time));
            }

            (x, y) = (y, x);
        }
        
        Destroy(gameObject);
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
