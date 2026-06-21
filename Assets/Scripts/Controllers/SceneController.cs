using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class SceneController : MonoBehaviour
{
    //Public Variables
    public static SceneController Instance { get; private set; }

    public int playerScore;
    public GameObject[] pistolEnemySpawners;
    public GameObject[] shotgunEnemySpawners;
    public GameObject[] carbineEnemySpawners;
    public GameObject[] ammoPickups;
    public GameObject[] healthPickups;
    public GameObject[] gunPickups;
    public AmmoHolder[] ammoHolders;
    [HideInInspector]
    public bool isInMenu = false;
    public float PickupDelay { get { return _pickupsDelay; } }

    //Private Variables
    [SerializeField, FormerlySerializedAs("pickupsDelay")] private float _pickupsDelay;
    [SerializeField, FormerlySerializedAs("enemiesToSpeedUpSpawn")] private float _enemiesToSpeedUpSpawn;
    private int _killCount;
    private int _spawnCount;
    [SerializeField] private int _spawnLimit;
    [SerializeField] private int _killsToUnlockShotgun;
    [SerializeField] private int _killsToUnlockCarbine;
    private AudioController _audioController;
    private AudioSource _audioSource;
    private PlayerController _player;
    private PlayerInputActions _inputActions;

    public static event Action<bool> OnPauseMenu;
    public static event Action<int> OnScored;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        _inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _inputActions.Enable();
        _inputActions.Player.PauseMenu.performed += OnOpenMenu;
    }

    void Start()
    {
        _audioController = AudioController.Instance;
        _audioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        ResetGuns();
        ammoHolders[2].isWeaponUnlocked = true;
        Invoke(nameof(SpawnPickups), 0.0f);
    }

    void Update()
    {
        DecreaseSpawnTimer(pistolEnemySpawners, _enemiesToSpeedUpSpawn);
        DecreaseSpawnTimer(shotgunEnemySpawners, _enemiesToSpeedUpSpawn);
        DecreaseSpawnTimer(carbineEnemySpawners, _enemiesToSpeedUpSpawn);
        _spawnCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        UnlockWeapon();

        if(_spawnCount >= _spawnLimit)
            StopSpawners(true);
        else
            StopSpawners(false);
    }

    private void OnDisable() => _inputActions.Player.PauseMenu.performed -= OnOpenMenu;

    private void OnOpenMenu(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!isInMenu)
            ShowPauseMenu();
        else
            ContinueGame();
    }

    public void CountKill(int score) 
    {
        playerScore += score;
        _killCount++;
        OnScored?.Invoke(playerScore);
    }

    private void UnlockWeapon()
    {
        if(_killCount >= _killsToUnlockShotgun && !ammoHolders[0].isWeaponUnlocked && !gunPickups[0].activeInHierarchy)
            gunPickups[0].SetActive(true);

        if(_killCount >= _killsToUnlockCarbine && !ammoHolders[1].isWeaponUnlocked && !gunPickups[1].activeInHierarchy)
            gunPickups[1].SetActive(true);
    }

    private void ResetGuns()
    {
        for(int i = 0; i < ammoHolders.Length; i++)
        {
            ammoHolders[i].isWeaponUnlocked = false;
            if (ammoHolders[i].name == "PistolAmmoHolder")
            {
                ammoHolders[i].ammoCount = ammoHolders[i].maxAmmoCount / 5;
                Debug.Log(ammoHolders[i].name);
            }
            else
                ammoHolders[i].ammoCount = 0;
        }
    }

    private void StopSpawners(bool enabled)
    {
        for(int i = 0; i < pistolEnemySpawners.Length; i++)
            pistolEnemySpawners[i].SetActive(!enabled);

        for(int i = 0; i < shotgunEnemySpawners.Length; i++)
            if(_killCount >= _killsToUnlockShotgun)
                shotgunEnemySpawners[i].SetActive(!enabled);

        for(int i = 0; i < carbineEnemySpawners.Length; i++)
            if (_killCount >= _killsToUnlockCarbine)
                carbineEnemySpawners[i].SetActive(!enabled);
    }

    private void SpawnPickups()
    {
        for (int i = 0; i < healthPickups.Length; i++)
            if (!healthPickups[i].activeInHierarchy)
                healthPickups[i].SetActive(true);


        for (int i = 0; i < ammoPickups.Length; i++)
            if (ammoPickups[i].CompareTag("PistolAmmo") && !ammoPickups[i].activeInHierarchy)
                ammoPickups[i].SetActive(true);
        /*
        for (int i = 0; i < ammoPickups.Length; i++)
        {
            if (ammoPickups[i].CompareTag("ShotgunAmmo") && !ammoPickups[i].activeInHierarchy && ammoHolders[0].isWeaponUnlocked)
            {
                ammoPickups[i].SetActive(true);
            }
        }

        for (int i = 0; i < ammoPickups.Length; i++)
        {
            if (ammoPickups[i].CompareTag("CarbineAmmo") && !ammoPickups[i].activeInHierarchy && ammoHolders[1].isWeaponUnlocked)
            {
                ammoPickups[i].SetActive(true);
            }
        }
        */
    }


    //Activates all specific Ammo PickUps
    public IEnumerator ActivatePickups(AmmoType ammoType)
    {
        for(int i = 0; i < ammoPickups.Length; i++)
        {
            if (ammoPickups[i].GetComponent<AmmoPickup>().ammoType == ammoType)
            {
                Debug.Log($"Ammo pickup {ammoPickups[i].name} spawn start!");
                yield return new WaitForSeconds(_pickupsDelay);
                Debug.Log($"Ammo pickup {ammoPickups[i].name} spawn finished!");
                ammoPickups[i].SetActive(true);
            }
        }
        
    }

    //Activates specific Health PickUp
    public IEnumerator ActivateHealthPickup(GameObject pickUp)
    {
        for(int i = 0; i < healthPickups.Length; i++)
        {
            if(pickUp == healthPickups[i])
            {
                Debug.Log($"Health pickup {pickUp.name} spawn start!");
                yield return new WaitForSeconds(_pickupsDelay);
                Debug.Log($"Health pickup {pickUp.name} spawn finished!");
                healthPickups[i].SetActive(true);
            }
        }
    }

    public IEnumerator ActivateAmmoPickup(GameObject pickUp)
    {
        for (int i = 0; i < ammoPickups.Length; i++)
        {
            if (ammoPickups[i] == pickUp)
            {
                Debug.Log($"Ammo pickup {pickUp.name} spawn start!");
                yield return new WaitForSeconds(_pickupsDelay);
                Debug.Log($"Ammo pickup {pickUp.name} spawn finished!");
                ammoPickups[i].SetActive(true);
            }
        }
    }

    private void DecreaseSpawnTimer(GameObject[] spawnersList, float spawnDelay)
    {
        if(_killCount % spawnDelay == 0)
            for(int i = 0; i < spawnersList.Length; i++)
                if (spawnersList[i].activeInHierarchy && spawnersList[i].GetComponent<SpawnController>().spawnInterval >= spawnDelay)
                    spawnersList[i].GetComponent<SpawnController>().spawnInterval -= 0.5f;
    }

    public void ContinueGame()
    {
        OnPauseMenu?.Invoke(false);
        _player.canMove = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isInMenu = false;
        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ShowPauseMenu()
    {
        Time.timeScale = 0;
        _player.canMove = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isInMenu = true;
        OnPauseMenu?.Invoke(true);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void PlayHoverSound() => _audioSource.PlayOneShot(_audioController.menuHover);

    public void PlaySelectSound() => _audioSource.PlayOneShot(_audioController.menuSelect);

}
