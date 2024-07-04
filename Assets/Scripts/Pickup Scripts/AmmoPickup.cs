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
    private SceneController _sceneController;

    public AmmoType ammoType;

    // Start is called before the first frame update
    void Start()
    {
        _audioController = AudioController.Instance;
        _sceneController = SceneController.Instance;
        _audioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
    }

    private void Update()
    {
        transform.RotateAround(gameObject.transform.position, Vector3.up, 15 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<AmmoController>().ChangeAmmo(_gunAmmoHolder, amount);
            _sceneController.StartCoroutine(_sceneController.ActivateAmmoPickup(gameObject));
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        _audioSource.PlayOneShot(_audioController.ammoPickUp);
        
    }
}
