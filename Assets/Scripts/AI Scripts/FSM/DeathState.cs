using UnityEngine;
using UnityEngine.AI;

public class DeathState : MonoBehaviour, IFSMState
{
    private NavMeshAgent _agent;
    private EnemyController _enemyController;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _enemyController = GetComponent<EnemyController>();
    }

    public FSMStateType StateName => FSMStateType.Death;

    public void DoAction() { }

    public void OnEnter() => _agent.enabled = false;

    public void OnExit() => _agent.enabled = true;

    public FSMStateType ShouldTransitionToState()
    {
        if (_enemyController.CurrentHealth > 0)
            return FSMStateType.Chase;
        else
            return StateName;

    }
}
