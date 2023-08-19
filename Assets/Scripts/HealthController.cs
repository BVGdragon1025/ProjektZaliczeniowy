using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    //Public Variables
    public int CurrentHealth { get { return _currentHealth; } }
    public int maxHealth = 100;

    //Private Variables
    [SerializeField] private int _currentHealth;
    UIController _uIController;


    // Start is called before the first frame update
    void Start()
    {
        _uIController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UIController>();
        
        _currentHealth = maxHealth;
    }

    public void ChangeHealth(int deltaHealth)
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
        if (gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Character died! | Character name: " + gameObject.name);
            Destroy(gameObject);
        }
        else if(gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Died!");
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
