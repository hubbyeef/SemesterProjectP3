using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pauseScreen;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }
    public void ResumeButton()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenuButton()
    {

        SceneManager.LoadScene("TitleScreen");
    }

    public void LevelFinish()
    {
    
    }
}
