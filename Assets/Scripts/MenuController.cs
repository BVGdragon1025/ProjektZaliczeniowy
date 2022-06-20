using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    //Private Variables
    [SerializeField] GameObject _mainMenu;
    [SerializeField] GameObject _creditsMenu;
    [SerializeField] GameObject _levelSelectionMenu;
    [SerializeField] Camera _camera;
    [SerializeField] GameObject _levelOneDescription;
    [SerializeField] GameObject _levelTwoDescription;
    private Vector3 _mainMenuPos;
    private Vector3 _creditsPos;
    private Vector3 _levelScreenPos;

    private void Awake()
    {
        StartCoroutine(FreezeTime());
    }

    // Start is called before the first frame update
    void Start()
    {
        _mainMenuPos = new Vector3(-4.94f, 1.18f, -5.55f);
        _levelScreenPos = new Vector3(-9.32f, 2.17f, 2.67f);
        _creditsPos = new Vector3(1.33f, 2.17f, -0.63f);
        MainMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenu()
    {
        _mainMenu.SetActive(true);
        _creditsMenu.SetActive(false);
        _levelSelectionMenu.SetActive(false);
        _camera.transform.position = _mainMenuPos;
        _camera.transform.rotation = Quaternion.Euler(-14.21f, 17.21f, 0f);
    }

    public void ShowCredits()
    {
        _mainMenu.SetActive(false);
        _creditsMenu.SetActive(true);
        _levelSelectionMenu.SetActive(false);
        _camera.transform.position = _creditsPos;
        _camera.transform.rotation = Quaternion.Euler(-9, 90, 0);
    }


    public void CloseGame()
    {
        Application.Quit();
    }

    public void ShowLevelSelectScreen()
    {
        _mainMenu.SetActive(false);
        //_creditsMenu.SetActive(false);
        _levelSelectionMenu.SetActive(true);
        _camera.transform.position = _levelScreenPos;
        _camera.transform.rotation = Quaternion.Euler(0, 90f, 0);
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

}
