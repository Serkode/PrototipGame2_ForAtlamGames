using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainScreenPanel;
    [SerializeField] GameObject levelPanel;

    public TMP_Text levelLockedText;

    float levelLockedCounter;

    public bool soundOn = true;
    public GameObject soundOnButton, soundOffButton;
    public GameObject[] levelButtons, lockedImage;

    void Start()
    {
        if (PlayerPrefs.GetInt("savelevel") < 1)//if the game never played before, begin from level 1.
        {
            PlayerPrefs.SetInt("savelevel", 0);
        }

        levelPanel.SetActive(false);
        levelLockedText.enabled = false;

        if (PlayerPrefs.GetInt("sound") == 1)//is the sound on/off at the beginning?
        {
            AudioSettings.Mobile.StartAudioOutput();
            soundOnButton.SetActive(true);
            soundOffButton.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("sound") == 0)
        {
            AudioSettings.Mobile.StopAudioOutput();
            soundOnButton.SetActive(false);
            soundOffButton.SetActive(true);
        }

        for (int i = 0; i < PlayerPrefs.GetInt("savelevel"); i++)//Interactable situations of the buttons at the beginning.
        {
            levelButtons[i].GetComponent<Button>().interactable = true;
            lockedImage[i].SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (levelPanel.activeInHierarchy)//if level panel is open, go main screen
            {
                levelPanel.SetActive(false);
                mainScreenPanel.SetActive(true);
            }
            else
                Application.Quit();
        }

        if(levelLockedText.enabled)
        {
            levelLockedCounter += Time.deltaTime;
            if(levelLockedCounter >= 0.5f)
            {
                levelLockedText.enabled = false;
                levelLockedCounter = 0;
            }
        }
    }

    public void BeginGame()
    {
        if (PlayerPrefs.GetInt("savelevel") == 0)//if the game never played before, begin from level 1.
        {
            SceneManager.LoadScene(1);
        }
        else if (PlayerPrefs.GetInt("savelevel") != 0)//Start saved level when touch "play button".
        {
            for (int i = 0; i < PlayerPrefs.GetInt("savelevel"); i++)//if some levels played, unlock the same level buttons' lock.
            {
                levelButtons[i].GetComponent<Button>().interactable = true;
                lockedImage[i].SetActive(false);
            }
            SceneManager.LoadScene(PlayerPrefs.GetInt("savelevel"));
        }
    }

    public void Exit()//when touching "Back" on the phone, close the game.
    {
        Application.Quit();
    }


    public void LevelSelect(int level)
    {
        if (PlayerPrefs.GetInt("savelevel") >= level)//if the level that you want to play has been played before, it starts the level.
        {
            SceneManager.LoadScene(level);
        }
        else//if the level that you want to play has not been played before, it won't start the level.
        {
            levelLockedText.enabled = true;
            levelLockedText.text = "Level Locked!";
        }
    }

    public void LevelButton()//for open levels panel
    {
        mainScreenPanel.SetActive(false);
        levelPanel.SetActive(true);
    }

    public void BackToMainMenuButton()//for go to main screen from levels panel.
    {
        mainScreenPanel.SetActive(true);
        levelPanel.SetActive(false);
    }

    public void DeleteSaves()//It will delete all playerpres savings.
    {
        PlayerPrefs.DeleteAll();
        for (int i = 0; i < levelButtons.Length; i++)//Close all level buttons' interactability and activate all buttons' locks.
        {
            levelButtons[i].GetComponent<Button>().interactable = false;
            lockedImage[i].SetActive(true);
        }
    }

    public void SoundOnOrOff(int x)
    {
        if (x == 1)//for open the sound.
        {
            AudioSettings.Mobile.StartAudioOutput();
            PlayerPrefs.SetInt("sound", 1);
            soundOn = true;
            soundOnButton.SetActive(true);
            soundOffButton.SetActive(false);
        }
        else if (x == 0)//for close the sound.
        {
            AudioSettings.Mobile.StopAudioOutput();
            PlayerPrefs.SetInt("sound", 0);
            soundOn = false;
            soundOnButton.SetActive(false);
            soundOffButton.SetActive(true);
        }
    }
}
