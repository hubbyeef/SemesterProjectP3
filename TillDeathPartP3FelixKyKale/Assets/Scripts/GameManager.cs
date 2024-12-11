using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting.FullSerializer.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject gameOverScreen;

    public Material nightSkybox;
    public Material daySkybox;
    public Material eveningSkybox;

    private GameObject[] windows;
    public GameObject vent;

    public GameObject CrossHair;

    private HideMonster hideMonster;
    private PlayerController player;

    public bool paused;

    public bool night;
    public int level;

    private int pickRandom;


    private void Awake()
    {
        windows = GameObject.FindGameObjectsWithTag("Window");
        hideMonster = GetComponent<HideMonster>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        

        if (night)
        {
            RenderSettings.skybox = nightSkybox;
            StartCoroutine(OpenRandomWindow());
            StartCoroutine(OpenVent());
            StartCoroutine(PeekabooMonster());
        }
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

        if (vent.GetComponent<VentOpen>().open == true)
        {
            vent.GetComponent<VentOpen>().timer -= Time.deltaTime;

            if (vent.GetComponent<VentOpen>().timer <= 0)
            {
                StartCoroutine(GameOver());
            }
        }

        foreach (GameObject window in windows)
        {
            if (window.GetComponent<opencloseWindowApt>().open == true)
            {
                window.GetComponent<opencloseWindowApt>().timer -= Time.deltaTime;

                if (window.GetComponent<opencloseWindowApt>().timer <= 0)
                {
                    StartCoroutine(GameOver());
                }
            }
        }

        if (hideMonster.active == true)
        {
            hideMonster.timer -= Time.deltaTime;
            if (hideMonster.timer <= 0 && player.safe == true)
            {
                hideMonster.Survived();
            }
            else if (hideMonster.timer <= 0 && player.safe == false)
            {
                StartCoroutine(GameOver());
            }
        }
    }

    private void FixedUpdate()
    {
        
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

    public IEnumerator GameOver()
    {
        Cursor.lockState = CursorLockMode.Confined;
        CrossHair.SetActive(false);
        gameOverScreen.SetActive(true );

        yield return new WaitForSeconds(2);

        StopAllCoroutines();
    }

    public void LevelFinish()
    {
    
    }

    public void RandomWindow()
    {
        pickRandom = Random.Range(0, windows.Length);
        if (windows[pickRandom].GetComponent<opencloseWindowApt>().beingOpened == false)
        {
            windows[pickRandom].GetComponent<opencloseWindowApt>().beingOpened = true;
            StartCoroutine(windows[pickRandom].GetComponent<opencloseWindowApt>().opening());
        }
    }

    public IEnumerator OpenRandomWindow()
    {
        yield return new WaitForSeconds(10);

        while (night)
        {
            RandomWindow(); yield return new WaitForSeconds(20);
        }
    }

    public IEnumerator OpenVent()
    {
        yield return new WaitForSeconds(5);

        while (night)
        {
            StartCoroutine(vent.GetComponent<VentOpen>().OpenVent());
            yield return new WaitForSeconds(Random.Range(30, 50));
        }
    }

    public IEnumerator PeekabooMonster()
    {
        yield return new WaitForSeconds(5);
        while (night)
        {
            StartCoroutine(hideMonster.itsComing());
            yield return new WaitForSeconds(Random.Range(120, 300));
        }
    }
}
