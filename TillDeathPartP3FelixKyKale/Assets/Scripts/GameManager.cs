using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pauseScreen;
    public Skybox nightSkybox;

    public List<GameObject> windows;

    public bool paused;

    public bool night;
    public int level;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused)
        {
            ResumeGame();
        }
    }

    private void FixedUpdate()
    {
        if (night)
        {
            OpenRandomWindow();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void PauseGame()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
        paused = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void ResumeGame()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
        paused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("TitleScreen");
    }

    public void LevelFinish()
    {
    
    }

    public void RandomWindow()
    {
        int pickRandom = Random.Range(0, windows.Count);
        if (windows[pickRandom].GetComponent<opencloseWindowApt>().beingOpened == false)
        {
            windows[pickRandom].GetComponent<opencloseWindowApt>().beingOpened = true;
        }
    }

    public IEnumerator OpenRandomWindow()
    {
        yield return null;

        RandomWindow(); yield return new WaitForSeconds(10 - level ^ 2);
    }
}
