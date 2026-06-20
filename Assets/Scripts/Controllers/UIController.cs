using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _pistolAmmoCounter;
    [SerializeField] private TextMeshProUGUI _shotgunAmmoCounter;
    [SerializeField] private TextMeshProUGUI _carbineAmmoCounter;
    [SerializeField] private TextMeshProUGUI _healthCounter;
    [SerializeField] private GameObject _pistolCrosshair;
    [SerializeField] private GameObject _shotgunCrosshair;
    [SerializeField] private GameObject _carbineCrosshair;
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private TextMeshProUGUI[] _scoreTexts;

    private Color _orangeColor = new Color32(255, 127, 39, 255);

    public static UIController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);

        Instance = this;
    }

    private void OnEnable()
    { 
        SceneController.OnPauseMenu += SetPauseMenuVisibility;
        SceneController.OnScored += SetScoreTexts;
    }

    private void Start() => _healthCounter.gameObject.SetActive(true);

    private void OnDisable()
    { 
        SceneController.OnPauseMenu -= SetPauseMenuVisibility; 
        SceneController.OnScored -= SetScoreTexts;
    }

    private void SetPauseMenuVisibility(bool isVisible) => _pauseMenu.SetActive(isVisible);

    private void SetScoreTexts(int score)
    {
        for (int i = 0; i < _scoreTexts.Length; i++)
            _scoreTexts[i].SetText($"{score}");
    }

    public void DisplayHealth(float healthAmount)
    {
        _healthCounter.text = healthAmount.ToString();

        _healthCounter.color = healthAmount switch
        {
            >= 75 => Color.green,
            < 75 and >= 50 => Color.yellow,
            < 50 and >= 25 => _orangeColor,
            _ => Color.red,
        };
    }

    public void DisplayAmmoHUD(int weaponType, bool enabled, AmmoHolder weaponAmmoHolder)
    {
        switch (weaponType)
        {
            case 0:
                _pistolAmmoCounter.gameObject.SetActive(enabled);
                _pistolAmmoCounter.gameObject.GetComponent<TextMeshProUGUI>().text = weaponAmmoHolder.ammoCount.ToString();
                break;
            case 1:
                _shotgunAmmoCounter.gameObject.SetActive(enabled);
                _shotgunAmmoCounter.gameObject.GetComponent<TextMeshProUGUI>().text = weaponAmmoHolder.ammoCount.ToString();
                break;
            case 2:
                _carbineAmmoCounter.gameObject.SetActive(enabled);
                _carbineAmmoCounter.gameObject.GetComponent<TextMeshProUGUI>().text = weaponAmmoHolder.ammoCount.ToString();
                break;
            default:
                Debug.Log("Something went wrong with Ammo Display!");
                break;
        }
    }

    public void DisplayCrosshair(int weaponType, bool enabled)
    {
        switch (weaponType)
        {
            case 0:
                _pistolCrosshair.SetActive(enabled);
                break;
            case 1:
                _shotgunCrosshair.SetActive(enabled);
                break;
            case 2:
                _carbineCrosshair.SetActive(enabled);
                break;
            default:
                Debug.Log("Something went wrong with Crosshair Display!");
                break;
        }
    }

    public void UpdateAmmoDisplay(int weaponType, AmmoHolder weaponAmmoHolder)
    {

        switch (weaponType)
        {
            case 0:
                _pistolAmmoCounter.GetComponent<TextMeshProUGUI>().text = weaponAmmoHolder.ammoCount.ToString();
                ChangeAmmoDisplayColor(_pistolAmmoCounter.GetComponent<TextMeshProUGUI>(), weaponAmmoHolder);
                break;
            case 1:
                _shotgunAmmoCounter.GetComponent<TextMeshProUGUI>().text = weaponAmmoHolder.ammoCount.ToString();
                ChangeAmmoDisplayColor(_shotgunAmmoCounter.GetComponent<TextMeshProUGUI>(), weaponAmmoHolder);
                break;
            case 2:
                _carbineAmmoCounter.GetComponent<TextMeshProUGUI>().text = weaponAmmoHolder.ammoCount.ToString();
                ChangeAmmoDisplayColor(_carbineAmmoCounter.GetComponent<TextMeshProUGUI>(), weaponAmmoHolder);
                break;
            default:
                Debug.Log("Something went wrong with updating Ammo Count!");
                break;
        }

    }

    private void ChangeAmmoDisplayColor(TextMeshProUGUI ammoText, AmmoHolder ammoHolder)
    {
        if(ammoHolder.ammoCount <= 0)
            ammoText.color = Color.red;
        else
            ammoText.color = Color.white;
    }

    public void ShowDeathScreen()
    {
        _gameOverScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}
