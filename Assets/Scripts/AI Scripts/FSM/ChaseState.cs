using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class ChaseState : MonoBehaviour, IFSMState
{
    public FSMStateType StateName { get { return FSMStateType.Chase; } }
    public float minChaseDistance = 2.0f;

    private Transform _player = null;
    private NavMeshAgent ThisAgent = null;

    void Awake()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        ThisAgent = GetComponent<NavMeshAgent>();
    }

    public void OnEnter()
    {
        ThisAgent.isStopped = false;
    }

    public void OnExit()
    {
        ThisAgent.isStopped = true;
    }

    public void DoAction()
    {
        ThisAgent.SetDestination(_player.position);
    }

    public FSMStateType ShouldTransitionToState()
    {
        float distanceToDest = Vector3.Distance(transform.position, _player.position);
        if (distanceToDest <= minChaseDistance)
        {
            return FSMStateType.Attack;
        }

        return FSMStateType.Chase;
    }
}
