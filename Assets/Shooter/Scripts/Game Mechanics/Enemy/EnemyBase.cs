using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(NavMeshAgent))]
public class EnemyBase : StateMachineBase, IDamageable
{
    #region Unity Fields
    [SerializeField] private EnemyTypes enemyType;
    [SerializeField] private Transform gunPoint;
    [SerializeField] private Image healtBar;
	#endregion
	#region Fields
	protected Rigidbody rb;
	protected NavMeshAgent navMeshAgent;
	protected Animator enemyAnimator;
	protected Transform playerTransform;
    public float CurrentHealth { get ; set; }
    public bool IsDead {  get; set; }
    #endregion
    #region States
    public InitialState InitialState { get; private set; }
    public ChaseState ChaseState {  get; private set; }
    public AttackState AttackState { get; private set; }
    #endregion
    #region Unity Methods
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
        CurrentHealth = enemyType.Health;
    }
    private void Start()
    {
        if (FindObjectOfType<Player>() == null)
        {
            Debug.LogError("There is no player");
            return;
        }
        navMeshAgent.speed = enemyType.MoveSpeed;
        playerTransform = FindObjectOfType<Player>().transform;
        InitStates();
        ChangeState(InitialState);
        StartCoroutine(AutoChase());
    }
    //private void Update()
    //{
    //    if (_isDead)
    //    {
    //        navMeshAgent.ResetPath();
    //        navMeshAgent.isStopped = true;
    //        return;
    //    }
    //}
    #endregion
    #region Private Methods
    private void InitStates()
    {
        InitialState = new InitialState(this, enemyAnimator);
        ChaseState = new ChaseState(enemyAnimator, navMeshAgent, playerTransform, this, enemyType);
        AttackState = new AttackState(rb,enemyAnimator,navMeshAgent,this,playerTransform,enemyType,gunPoint);
    }
    private IEnumerator AutoChase()
    {
        yield return new WaitForSeconds(1);
        StartChase();
    }
    #endregion
    #region Public Methods
    [ContextMenu("Start Chasing")]
    public void StartChase()
    {
        ChangeState(ChaseState);
    }
    [ContextMenu("Take Damage Simulator")]
    public void TakeDamageSim()
    {
        TakeDamage(20f);
    }
    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        healtBar.fillAmount = CurrentHealth / enemyType.Health;
        if(CurrentHealth <= 0)
        {
            navMeshAgent.ResetPath();
            navMeshAgent.isStopped = true;
            IsDead = true;
            enemyAnimator.SetBool(CommonVariables.PlayerAnimBools.Shooting.ToString(), false);
            enemyAnimator.SetBool(CommonVariables.PlayerAnimBools.Die.ToString(), true);
            Debug.Log("dusman oldu");
        }
        else if (CurrentHealth > 0)
        {
            enemyAnimator.SetTrigger(CommonVariables.PlayerAnimsTriggers.Hit.ToString());
        }
    }
    #endregion
}
