using UnityEngine;

public class AttackState : MonoBehaviour, IFSMState
{
    //Public Variables
    public FSMStateType StateName { get { return FSMStateType.Attack; } }
    
    //Private Variables
    private EnemyWeapon _weaponController = null;
    private Transform _player = null;
    private HealthController _healthController = null;
    private ChaseState _chaseStateData;

    void Awake()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        _healthController = gameObject.GetComponent<HealthController>();
        _chaseStateData = gameObject.GetComponent<ChaseState>();
    }

    void Start()
    {
        _weaponController = gameObject.GetComponentInChildren<EnemyWeapon>();
    }

    private void Update()
    {
        if(_weaponController.gameObject.name == "AIShotgun")
        {
            _weaponController.Shoot();
        }
    }

    public void OnEnter()
    {
        return;
    }

    public void OnExit()
    {
        return;
    }

    public void DoAction()
    {
        Vector3 Dir = (_player.position - transform.position).normalized;
        Dir.y = 0;
        transform.rotation = Quaternion.LookRotation(Dir, Vector3.up);

        if(_healthController.CurrentHealth != 0)
        {
            _weaponController.Shoot();
        }
        
    }

    public FSMStateType ShouldTransitionToState()
    {
        float distanceToPlayer = Vector3.Distance(gameObject.transform.position, _player.position);

        if(distanceToPlayer >= _chaseStateData.minChaseDistance)
        {
            return FSMStateType.Chase;
        }

        return FSMStateType.Attack;
    }
}
