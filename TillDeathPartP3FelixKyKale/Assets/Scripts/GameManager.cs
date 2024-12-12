using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting.FullSerializer.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum haunts { none, windowMonster, ventMonster, peekabooDemon}
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
    public bool currentlyHaunted;

    public haunts hauntTactic;

    public bool night;
    public int level;

    private int pickRandom;

    public float initialWindowTimer;
    public float initialVentTimer;
    public float initialPeekabooTimer;


    private void Awake()
    {
        windows = GameObject.FindGameObjectsWithTag("Window");
        hideMonster = GetComponent<HideMonster>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        hauntTactic = haunts.none;

        if (night)
        {
            RenderSettings.skybox = nightSkybox;
            StartCoroutine(InitialHauntingTimer());
            StartCoroutine(CurrentHaunting());
        }
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.Escape) && !paused) //Pause the Game
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused) //Unpause the Game
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
        gameOverScreen.SetActive(true);

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

        while (night)
        {
            RandomWindow();
            currentlyHaunted = true;
            hauntTactic = haunts.windowMonster;
            yield return new WaitForSeconds(20);
        }
    }

    public IEnumerator OpenVent()
    {
        while (night)
        {
            StartCoroutine(vent.GetComponent<VentOpen>().OpenVent());
            currentlyHaunted = true;
            hauntTactic = haunts.ventMonster;
            yield return new WaitForSeconds(Random.Range(30, 50));
        }
    }

    public IEnumerator PeekabooMonster()
    {
        while (night)
        {
            StartCoroutine(hideMonster.itsComing());
            currentlyHaunted = true;
            hauntTactic = haunts.peekabooDemon;
            yield return new WaitForSeconds(Random.Range(120, 300));
        }
    }

    public IEnumerator CurrentHaunting()
    {
        if (hauntTactic == haunts.windowMonster)
        {
            StopCoroutine(PeekabooMonster());
            yield return new WaitForSeconds(Random.Range(50, 130));
            StartCoroutine(PeekabooMonster());
        }

        if (hauntTactic == haunts.ventMonster)
        {
            StopCoroutine(PeekabooMonster());
            yield return new WaitForSeconds(Random.Range(20, 120));
            StartCoroutine(PeekabooMonster());
        }

        if (hauntTactic == haunts.peekabooDemon)
        {
            StopCoroutine(OpenVent());
            StopCoroutine(OpenRandomWindow());
            yield return new WaitForSeconds(Random.Range(20, 25));
            StartCoroutine(OpenVent());
            StartCoroutine(OpenRandomWindow());
        }
    }

    public IEnumerator InitialHauntingTimer()
    {
        yield return new WaitForSeconds(initialWindowTimer);
        StartCoroutine(OpenRandomWindow());
        yield return new WaitForSeconds(initialVentTimer);
        StartCoroutine(OpenVent());
        yield return new WaitForSeconds(initialPeekabooTimer);
        StartCoroutine(PeekabooMonster());
    }
}
