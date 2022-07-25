using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameControl : MonoBehaviour
{

    PlayerAvatarControl player;
    public float  endGameTime;
    float endGameCounter;

    public TMP_Text levelText;

    void Start()
    {
        if (PlayerPrefs.GetInt("savelevel") < SceneManager.GetActiveScene().buildIndex)//if the current level is never saved befor, save it.
        {
            PlayerPrefs.SetInt("savelevel", SceneManager.GetActiveScene().buildIndex);
        }

        levelText.text = SceneManager.GetActiveScene().buildIndex.ToString();//for show the current level on the screen.

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAvatarControl>();
    }


    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))//go to main menu if touch "Back" on the phone during the game.
        {
            SceneManager.LoadScene(0);
        }

        PlayerInteractions();//Codes that coming from PlayerAvatarControl Script

    }

    void PlayerInteractions()
    {
        if (player.touchObstacle)//if player touch obstacles, go to main menu. You failed.
        {
            endGameCounter += Time.deltaTime;
            if (endGameCounter >= endGameTime)//go to main menu after "endGameTime" seconds.
            {
                SceneManager.LoadScene(0);
                endGameCounter = 0;
            }

        }
        else if (player.touchPendulum)//if player touch pendulum, go to main menu. You failed.
        {
            endGameCounter += Time.deltaTime;
            if (endGameCounter >= endGameTime)//go to main menu after "endGameTime" seconds.
            {
                SceneManager.LoadScene(0);
                endGameCounter = 0;
            }
        }
        else if (player.finish)//if player finish the level, go to next level. You did it.
        {
            endGameCounter += Time.deltaTime;
            if (endGameCounter >= endGameTime)//go to next level after "endGameTime" seconds.
            {
                endGameCounter = 0;
                Debug.Log(endGameTime + " saniye bitti!");
                if (SceneManager.GetActiveScene().buildIndex == 5)//if current level is 5 and you finish the level, you win the game. Go to main menu. You did it.
                {
                    SceneManager.LoadScene(0);
                    return;
                }
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

        if (player.transform.position.y <= -1)//if player falling down.
        {
            endGameCounter += Time.deltaTime;
            if (endGameCounter >= endGameTime)
            {
                SceneManager.LoadScene(0);
                endGameCounter = 0;
            }

        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
