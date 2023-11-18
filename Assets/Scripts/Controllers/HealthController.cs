using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    //Public Variables
    public float CurrentHealth { get { return _currentHealth; } }
    public float maxHealth = 100;

    //Private Variables
    [SerializeField] private float _currentHealth;
    private EnemyController _enemyController;
    UIController _uIController;


    // Start is called before the first frame update
    void Start()
    {
        _uIController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
        _enemyController = GetComponent<EnemyController>();

    }

    private void OnEnable()
    {
        _currentHealth = maxHealth;
    }

    public void ChangeHealth(float deltaHealth)
    {

        _currentHealth = Mathf.Clamp(_currentHealth + deltaHealth, 0, maxHealth);
        _uIController.DisplayHealth(_currentHealth);
        CheckHealth();

    }

    public void CheckHealth()
    {
        if(_currentHealth <= 0)
        {
            CharacterDie();
        }
    }

    private void CharacterDie()
    {
        SceneController instance = SceneController.Instance;

        if (gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("Character died! | Character name: " + gameObject.name);
            instance.CountKill();
            instance.playerScore += _enemyController.Score;
            gameObject.SetActive(false);
        }
        else if(gameObject.CompareTag("Player"))
        {
            //Debug.Log("Player Died!");
            _uIController.ShowDeathScreen();
            gameObject.GetComponent<PlayerController>().canMove = false;
            Time.timeScale = 0;
        }
        else
        {
            Debug.Log("Something went wrong with character dying. Maybe you forgot to add another tag?");
        }
        
    }

}
