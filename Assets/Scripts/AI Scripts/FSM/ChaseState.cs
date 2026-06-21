using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ChaseState : MonoBehaviour, IFSMState
{
    public FSMStateType StateName { get { return FSMStateType.Chase; } }
    public float minChaseDistance = 2.0f;

    private Transform _player = null;
    private NavMeshAgent ThisAgent = null;
    private EnemyController _enemyController;

    void Awake()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        _enemyController = GetComponent<EnemyController>();
        ThisAgent = GetComponent<NavMeshAgent>();
    }

    public void OnEnter()
    {
        if (!ThisAgent.enabled)
            ThisAgent.enabled = true;
        ThisAgent.isStopped = false;
    }

    public void OnExit() => ThisAgent.isStopped = true;

    public void DoAction() => ThisAgent.SetDestination(_player.position);

    public FSMStateType ShouldTransitionToState()
    {
        float distanceToDest = Vector3.Distance(transform.position, _player.position);

        if (_enemyController.CurrentHealth <= 0)
            return FSMStateType.Death;

        if (distanceToDest <= minChaseDistance)
            return FSMStateType.Attack;

        return StateName;
    }
}
