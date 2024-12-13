using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting.FullSerializer.Internal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum haunts { none, windowMonster, ventMonster, peekabooDemon }
public class GameManager : MonoBehaviour
{
    public GameObject pauseScreen;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI closeWindowText;
    public GameObject hideTextUI;
    public TextMeshProUGUI controlsText;
    public GameObject gameOverScreen;
    public GameObject sceneTransition;
    public AudioClip morningAlarm;
    private AudioSource audioSource;
    public Image star;

    public Material nightSkybox;
    public Material daySkybox;
    public Material eveningSkybox;

    private opencloseWindowApt[] windows;
    public GameObject vent;
    public List<ClosetopencloseDoor> closets;

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

    public static bool won;


    private void Awake()
    {
        windows = FindObjectsOfType<opencloseWindowApt>();
        hideMonster = GetComponent<HideMonster>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        audioSource = Camera.main.GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        hauntTactic = haunts.none;
        firstHour = true;
        StartCoroutine(showControls());

        if (night)
        {
            RenderSettings.skybox = nightSkybox;
            StartCoroutine(InitialHauntingTimer());
            StartCoroutine(CurrentHaunting());
        }

        if (won)
        {
            if (star != null)
            {
                star.gameObject.SetActive(true);
            }

            if (star == null)
            {
                return;
            }
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

        foreach (opencloseWindowApt window in windows)
        {
            if (window.open == true && window.beingOpened == true)
            {
                window.timer -= Time.deltaTime;

                if (window.timer <= 0)
                {
                    StartCoroutine(GameOver());
                }
            }

            else if (window.open == false && window.beingOpened == false)
            {
                window.timer = 20f;
            }
        }

        if (vent.GetComponent<VentOpen>().opened == true)
        {
            vent.GetComponent<VentOpen>().timer -= Time.deltaTime;

            if (vent.GetComponent<VentOpen>().timer <= 0)
            {
                StartCoroutine(GameOver());
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

        CheckForHide();



        if (currentHour >= 6 && currentHour != 12 && won == false)
        {
            StopAllCoroutines();

            StartCoroutine(LevelFinish());
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

    public IEnumerator LevelFinish()
    {
        won = true;
        audioSource.PlayOneShot(morningAlarm);
        yield return new WaitForSeconds(morningAlarm.length);
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
        closeWindowText.gameObject.SetActive(true);
        yield return new WaitForSeconds(4);
        closeWindowText.gameObject.SetActive(false);
        yield return new WaitForSeconds(initialVentTimer);
        StartCoroutine(OpenVent());
        closeWindowText.text = "Close the vent in the bathroom when you hear it.";
        closeWindowText.gameObject.SetActive(true);
        yield return new WaitForSeconds(4);
        closeWindowText.gameObject.SetActive(false);
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
            else if (Mathf.FloorToInt(currentTime / 45) + 12 > 12)
            {
                currentHour = Mathf.FloorToInt(currentTime / 45);
            }
            timeText.text = (currentHour + "AM");
        }
    }

    public void CheckForHide()
    {
        foreach (ClosetopencloseDoor closet in closets)
        {
            float distance = Vector3.Distance(closet.transform.position, player.transform.position);
            if (distance < 2f)
            {
                hideTextUI.gameObject.SetActive(true);
            }
            else if (distance > 2f)
            {
                hideTextUI.gameObject.SetActive(false);
            }
        }
    }

    public IEnumerator showControls()
    {
        controlsText.gameObject.SetActive(true);
        controlsText.rectTransform.anchoredPosition = new Vector3(0, 0);
        controlsText.rectTransform.localScale = new Vector3(2, 2);
        yield return new WaitForSeconds(10);
        controlsText.rectTransform.anchoredPosition = new Vector3(830, -430);
        controlsText.rectTransform.localScale = new Vector3(1, 1);
    }
}
