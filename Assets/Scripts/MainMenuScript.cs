using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject creditPanel;
    public GameObject gamemodePanel;
    public GameObject levelSelectPanel;

    public Button levelSelectButton;

    [Header("Level Buttons")]
    public Button[] levelButtons;

    private void Start()
    {
        if (GameManager.instance.tutorialIsDone)
        {
            levelSelectButton.interactable = true;
        }
        
    }

    public void PlayButtonClicked()
    {
        ActivatePanel(gamemodePanel.name);
        
        UnlockLevelButtons();
    }
    public void CreditButtonClicked()
    {
        ActivatePanel(creditPanel.name);
    }
    public void QuitButtonClicked()
    {
        Application.Quit();
    }

    public void LevelSelectButtonClicked()
    {
        ActivatePanel(levelSelectPanel.name);
    }
    public void TutorialButtonClicked()
    {
        // Go to Tutorial Scene
        SceneManager.LoadScene("Tutorial");
    }

    public void BackButtonClicked(string previousPanelName)
    {
        ActivatePanel(previousPanelName);
    }

    void ActivatePanel(string panelToActivate)
    {
        mainMenuPanel.SetActive(mainMenuPanel.name.Equals(panelToActivate));
        creditPanel.SetActive(creditPanel.name.Equals(panelToActivate));
        gamemodePanel.SetActive(gamemodePanel.name.Equals(panelToActivate));
        levelSelectPanel.SetActive(levelSelectPanel.name.Equals(panelToActivate));
    }

    public void SelectLevel(int levelNumber)
    {
        GameManager.instance.levelManager.LoadLevel(levelNumber);
    }

    private void UnlockLevelButtons()
    {
        // unlock each levels
        for (int i = 0; i < GameManager.instance.levelManager.levelsUnlocked.ToArray().Length + 1; i++)
        {
            if (!levelButtons[i].interactable)
            {
                levelButtons[i].interactable = true;
                
                Debug.Log($"Unlocked button {levelButtons[i]}");
            }
        }
    }
}
