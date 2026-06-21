using UnityEngine;

public class EnemyEyes : MonoBehaviour
{
    private EnemyController _enemyController;

    private void Awake() => _enemyController = GetComponentInParent<EnemyController>();

    public void ChangeHealth(float health) => _enemyController.ChangeHealth(health);

}
