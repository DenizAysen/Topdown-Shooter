using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(NavMeshAgent))]
public class EnemyBase : StateMachineBase
{
	#region Unity Fields

	#endregion
	#region Fields
	protected Rigidbody rb;
	protected NavMeshAgent navMeshAgent;
	protected Animator enemyAnimator;
	protected Transform playerTransform;
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
    }
    private void Start()
    {
        if (FindObjectOfType<Player>() == null)
        {
            Debug.LogError("There is no player");
            return;
        }
        playerTransform = FindObjectOfType<Player>().transform;
        InitStates();
        ChangeState(InitialState);
    }
    #endregion
    #region Private Methods
    private void InitStates()
    {
        InitialState = new InitialState(this, enemyAnimator);
        ChaseState = new ChaseState(enemyAnimator, navMeshAgent, playerTransform, this);
        AttackState = new AttackState();
    }
    #endregion
    #region Public Methods
    [ContextMenu("Start Chasing")]
    public void StartChase()
    {
        ChangeState(ChaseState);
    }
    #endregion
}
