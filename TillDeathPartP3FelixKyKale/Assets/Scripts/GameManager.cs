using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting.FullSerializer.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum haunts { none, windowMonster, ventMonster, peekabooDemon}
public class GameManager : MonoBehaviour
{
    public GameObject pauseScreen;
    public TextMeshProUGUI timeText;
    public GameObject gameOverScreen;
    public GameObject sceneTransition;

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
    public float currentTime;
    public int currentHour;
    public bool firstHour;

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
        firstHour = true;

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
        TimeOfDay();

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

        if (currentHour >= 6 && currentHour != 12)
        {
            StopAllCoroutines();
            LevelFinish();
        }
    }

    private void FixedUpdate()
    {

    }

    public void StartGame()
    {
        StartCoroutine(SceneTransitionStartGame());
    }
    public IEnumerator SceneTransitionStartGame()
    {
        sceneTransition.gameObject.SetActive(true);
        sceneTransition.GetComponent<Animator>().Play("FadingIn");
        yield return new WaitForSeconds(1.3f);
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
        StartCoroutine(SceneTransitioningMainMenu());
    }

    public IEnumerator GameOver()
    {
        Cursor.lockState = CursorLockMode.Confined;
        CrossHair.SetActive(false);
        gameOverScreen.SetActive(true);
        gameOverScreen.GetComponent<Animator>().Play("FadingIn");

        yield return new WaitForSeconds(2);

        StopAllCoroutines();
    }

    public void LevelFinish()
    {
        MainMenuButton();
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
            yield return new WaitForSeconds(Random.Range(25, 40));
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

    public IEnumerator SceneTransitioningMainMenu()
    {
        sceneTransition.gameObject.SetActive(true);
        sceneTransition.GetComponent<Animator>().Play("FadingIn");
        yield return new WaitForSeconds(1.3f);
        SceneManager.LoadScene("TitleScreen");
    }

    public void TimeOfDay()
    {
        currentTime += Time.deltaTime;
        UpdateTimeText();
        void UpdateTimeText()
        {
            if (Mathf.FloorToInt(currentTime / 45) + 12 == 12)
            {
                currentHour = 12;
            }
            else if (Mathf.FloorToInt(currentTime/45) + 12 > 12)
            {
                currentHour = Mathf.FloorToInt(currentTime / 45);
            }
            timeText.text = (currentHour + "AM");
        }
    }
}
