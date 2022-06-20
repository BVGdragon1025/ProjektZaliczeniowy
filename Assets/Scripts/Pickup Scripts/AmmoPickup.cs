using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    //Private Variables
    [SerializeField] private int amount;
    [SerializeField] private AmmoHolder _gunAmmoHolder;
    private AudioController _audioController;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _audioController = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioController>();
        _audioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
    }

    private void Update()
    {
        transform.RotateAround(gameObject.transform.position, Vector3.up, 15 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<AmmoController>().ChangeAmmo(_gunAmmoHolder, amount);
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        _audioSource.PlayOneShot(_audioController.ammoPickUp);
    }
}
