using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    //Public Variables
    

    //Private Variables
    private HealthController _playerHealthController;
    [SerializeField] private int amountOfHealth;
    private AudioController _audioController;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _playerHealthController = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthController>();
        _audioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
    }

    private void Update()
    {
        transform.RotateAround(gameObject.transform.position, Vector3.up, 15 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && other.gameObject.GetComponent<HealthController>().CurrentHealth < other.gameObject.GetComponent<HealthController>().maxHealth)
        {
            _playerHealthController.ChangeHealth(amountOfHealth);
            gameObject.SetActive(false);

        }
        else
        {
            Debug.Log("Player has full health!");
        }

    }

    private void OnDisable()
    {
        _audioSource.PlayOneShot(AudioController.Instance.healthPickUp);
    }
}
