using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LanguageObject", menuName = "Data Containers/Language Container Object")]

public class LanguageSO : ScriptableObject
{
    [Header("'Main Menu' scene strings")]
    public string startGameButton;
    public string creditsButton;
    public string controlsText;
    public string exitGameButton;
    public string backButton;
    [Header("'Credits' section")]
    public string createdByString;
    public string creditsSoundSourceText;
    public string licenseString;
    [Header("'Level Select' section")]
    public string selectLevelText;
    public string warehouseLevelButton;
    public string warehouseLevelDescription;
    public string whiskyLevelButton;
    public string whiskyLevelDescription;
    [Header("'Controls' section")]
    public string controlsHeaderText;
    public string walkText;
    public string sprintText;
    public string jumpText;
    public string jumpKeyText;
    public string shootText;
    public string shootKeyButton;
    public string selectWeaponText;
    public string selectWeaponKeysText;
    public string escapeText;
    [Header("'Gameplay' section")]
    public string scoreText;
    public string continueButtonText;
    public string restartButtonText;
    public string mainMenuButtonText;
    [Header("'Game over' section")]
    public string gameOverText;
    public string finalScoreText;
    [Header("'Pause Menu' section")]
    public string pauseMenuText;
    [Header("'Unlocks' section")]
    public string shotgunText;
    public string carbineText;
    public string unlockedText;

}
