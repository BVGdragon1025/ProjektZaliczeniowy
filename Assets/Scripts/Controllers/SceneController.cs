using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
    public GameObject[] gunMessages;
    [HideInInspector]
    public bool isInMenu = false;

    //Private Variables
    [SerializeField] private float pickupsDelay;
    [SerializeField] private float enemiesToSpeedUpSpawn;
    [SerializeField] private int _killCount;
    [SerializeField] private int _spawnCount;
    [SerializeField] private int _spawnLimit;
    [SerializeField] private int _killsToUnlockShotgun;
    [SerializeField] private int _killsToUnlockCarbine;
    [SerializeField] private TextMeshProUGUI[] _scoreText;
    private AudioController _audioController;
    private AudioSource _audioSource;
    private PlayerController _player;
    [SerializeField] GameObject _pauseMenu;

    // Start is called before the first frame update

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        _audioController = AudioController.Instance;
        _audioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        ResetGuns();
        ammoHolders[2].isWeaponUnlocked = true;
        InvokeRepeating(nameof(SpawnPickups), 0, pickupsDelay);
    }

    // Update is called once per frame
    void Update()
    {
        DecreaseSpawnTimer(pistolEnemySpawners, enemiesToSpeedUpSpawn);
        DecreaseSpawnTimer(shotgunEnemySpawners, enemiesToSpeedUpSpawn);
        DecreaseSpawnTimer(carbineEnemySpawners, enemiesToSpeedUpSpawn);
        _spawnCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        UnlockWeapon();

        if(_spawnCount >= _spawnLimit)
        {
            StopSpawners(true);
        }
        else
        {
            StopSpawners(false);
        }
        
        for(int i = 0; i < _scoreText.Length; i++)
        {
            _scoreText[i].text = playerScore.ToString();
        }

        if(Input.GetKeyDown(KeyCode.Escape) && !isInMenu)
        {
            ShowPauseMenu();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isInMenu)
        {
            ContinueGame();
        }
    }


    public void CountKill()
    {
        _killCount++;
        
    }

    private void UnlockWeapon()
    {
        if(_killCount == _killsToUnlockShotgun && !ammoHolders[0].isWeaponUnlocked)
        {
            gunPickups[0].SetActive(true);

        }

        if(_killCount == _killsToUnlockCarbine && !ammoHolders[1].isWeaponUnlocked)
        {
            gunPickups[1].SetActive(true);

        }
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
            {
                ammoHolders[i].ammoCount = 0;
            }
            
        }
    }

    private void StopSpawners(bool enabled)
    {
        for(int i = 0; i < pistolEnemySpawners.Length; i++)
        {
            pistolEnemySpawners[i].SetActive(!enabled);
        }

        for(int i = 0; i < shotgunEnemySpawners.Length; i++)
        {
            if(_killCount >= _killsToUnlockShotgun)
            {
                shotgunEnemySpawners[i].SetActive(!enabled);
            }
            
        }

        for(int i = 0; i < carbineEnemySpawners.Length; i++)
        {
            if (_killCount >= _killsToUnlockCarbine)
            {
                carbineEnemySpawners[i].SetActive(!enabled);
            }
                
        }

    }

    private void SpawnPickups()
    {
        for (int i = 0; i < healthPickups.Length; i++)
        {
            if (!healthPickups[i].activeInHierarchy)
            {
                healthPickups[i].SetActive(true);
            }
        }

        for (int i = 0; i < ammoPickups.Length; i++)
        {
            if (ammoPickups[i].CompareTag("PistolAmmo") && !ammoPickups[i].activeInHierarchy)
            {
                ammoPickups[i].SetActive(true);
            }
        }

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
    }

    private void DecreaseSpawnTimer(GameObject[] spawnersList, float spawnDelay)
    {
        if(_killCount % spawnDelay == 0)
        {
            for(int i = 0; i < spawnersList.Length; i++)
            {
                if (spawnersList[i].activeInHierarchy && spawnersList[i].GetComponent<SpawnController>().spawnInterval >= spawnDelay)
                {
                    spawnersList[i].GetComponent<SpawnController>().spawnInterval -= 0.5f;
                }
            }
        }
    }

    public void ContinueGame()
    {
        _pauseMenu.SetActive(false);
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
        _pauseMenu.SetActive(true);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }



    public void PlayHoverSound()
    {
        _audioSource.PlayOneShot(_audioController.menuHover);
    }

    public void PlaySelectSound()
    {
        _audioSource.PlayOneShot(_audioController.menuSelect);
    }

}
