using UnityEngine;

public class AttackState : MonoBehaviour, IFSMState
{
    //Public Variables
    public FSMStateType StateName { get { return FSMStateType.Attack; } }
    
    //Private Variables
    private EnemyWeapon _weaponController = null;
    private Transform _player = null;
    private EnemyController _enemyController;
    private ChaseState _chaseStateData;

    void Awake()
    {
        _player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        _enemyController = GetComponent<EnemyController>();
        _chaseStateData = gameObject.GetComponent<ChaseState>();
    }

    void Start() => _weaponController = gameObject.GetComponentInChildren<EnemyWeapon>();

    private void Update()
    {
        if(gameObject.transform.GetChild(1).name == "AIShotgun")
            _weaponController.Shoot();
    }

    public void OnEnter() { }

    public void OnExit() { }

    public void DoAction()
    {
        Vector3 Dir = (_player.position - transform.position).normalized;
        Dir.y = 0;
        transform.rotation = Quaternion.LookRotation(Dir, Vector3.up);
        
        if(_enemyController.CurrentHealth != 0)
            _weaponController.Shoot();
    }

    public FSMStateType ShouldTransitionToState()
    {
        float distanceToPlayer = Vector3.Distance(gameObject.transform.position, _player.position);

        if (_enemyController.CurrentHealth <= 0)
            return FSMStateType.Death;

        if(distanceToPlayer >= _chaseStateData.minChaseDistance)
            return FSMStateType.Chase;

        return StateName;
    }
}
