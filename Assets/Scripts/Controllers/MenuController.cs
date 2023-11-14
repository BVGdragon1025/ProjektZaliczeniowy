using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    //Private Variables
    [SerializeField] GameObject _mainMenu;
    [SerializeField] GameObject _creditsMenu;
    [SerializeField] GameObject _controlsMenu;
    [SerializeField] GameObject _levelSelectionMenu;
    [SerializeField] Camera _camera;
    [SerializeField] GameObject _levelOneDescription;
    [SerializeField] GameObject _levelTwoDescription;
    private Vector3 _mainMenuPos;
    private Vector3 _creditsPos;
    private Vector3 _controlsMenuPos;
    private Vector3 _levelScreenPos;
    private Light _cameraSpotlight;
    private AudioController _audioController;
    private AudioSource _audioSource;
    private bool _isInMainMenu;

    public static MenuController Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        StartCoroutine(FreezeTime());
    }

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        _audioController = GameObject.FindGameObjectWithTag("AudioController").GetComponent<AudioController>();
        _mainMenuPos = new Vector3(-4.94f, 1.18f, -5.55f);
        _levelScreenPos = new Vector3(-9.32f, 2.17f, 2.67f);
        _controlsMenuPos = new Vector3(-3.47f, 6.28f, 1.44f);
        _creditsPos = new Vector3(1.33f, 2.17f, -0.63f);
        _cameraSpotlight = GameObject.FindGameObjectWithTag("MainCamera").GetComponentInChildren<Light>();
        MainMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_isInMainMenu)
        {
            MainMenu();
            _audioSource.PlayOneShot(_audioController.menuSelect);
        }
    }

    public void MainMenu()
    {
        _mainMenu.SetActive(true);
        _creditsMenu.SetActive(false);
        _controlsMenu.SetActive(false);
        _levelSelectionMenu.SetActive(false);
        //transform.SetLocalPositionAndRotation(_mainMenuPos, Quaternion.Euler(-14.21f, 17.21f, 0f));
        _camera.transform.position = _mainMenuPos;
        _camera.transform.rotation = Quaternion.Euler(-14.21f, 17.21f, 0f);
        _cameraSpotlight.enabled = false;
        _isInMainMenu = true;
    }

    public void ShowCredits()
    {
        _mainMenu.SetActive(false);
        _creditsMenu.SetActive(true);
        _controlsMenu.SetActive(false);
        _levelSelectionMenu.SetActive(false);
        //transform.SetLocalPositionAndRotation(_creditsPos, Quaternion.Euler(-9, 90, 0));
        _camera.transform.position = _creditsPos;
        _camera.transform.rotation = Quaternion.Euler(-9, 90, 0);
        _cameraSpotlight.enabled = false;
        _isInMainMenu = false;
    }

    public void ShowControls()
    {
        _mainMenu.SetActive(false);
        _creditsMenu.SetActive(false);
        _controlsMenu.SetActive(true);
        _levelSelectionMenu.SetActive(false);
        //transform.SetLocalPositionAndRotation(_controlsMenuPos, Quaternion.Euler(39f, 180f, 0f));
        _camera.transform.position = _controlsMenuPos;
        _camera.transform.rotation = Quaternion.Euler(39f, 180f, 0f);
        _cameraSpotlight.enabled = false;
        _isInMainMenu = false;
    }


    public void CloseGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void ShowLevelSelectScreen()
    {
        _mainMenu.SetActive(false);
        _creditsMenu.SetActive(false);
        _controlsMenu.SetActive(false);
        _levelSelectionMenu.SetActive(true);
        //transform.SetLocalPositionAndRotation(_levelScreenPos, Quaternion.Euler(0, 90f, 0));
        _camera.transform.position = _levelScreenPos;
        _camera.transform.rotation = Quaternion.Euler(0, 90f, 0);
        _cameraSpotlight.enabled = true;
        _isInMainMenu = false;
    }

    public void ChooseLevel(string levelName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(levelName);
    }

    IEnumerator FreezeTime()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0;
    }

    public void ShowLevelDesc(string levelName)
    {
        switch (levelName)
        {
            case ("Level1"):
                _levelOneDescription.SetActive(true);
                _levelTwoDescription.SetActive(false);
                break;
            case ("Level2"):
                _levelTwoDescription.SetActive(true);
                _levelOneDescription.SetActive(false);
                break;
            default:
                Debug.Log("Wrong level name!");
                break;

        }
        
    }

    public void PlaySelectSound()
    {
        _audioSource.PlayOneShot(_audioController.menuSelect);
    }

    public void PlayHoverSound()
    {
        _audioSource.PlayOneShot(_audioController.menuHover);
    }

}
