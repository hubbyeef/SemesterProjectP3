using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMonster : MonoBehaviour
{
    public float timer = 10f;
    public bool active;
    public GameObject player;
    public LightSwitch[] lightSwitch;
    public GameObject hideText;
    public GameObject itsComingText;
    public GameManager gameManager;
    public AudioClip lightsOut;
    public AudioClip footsteps;
    public AudioClip lightsBack;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator itsComing()
    {
        foreach (LightSwitch lightswitch in lightSwitch)
        {
            foreach (GameObject light in lightswitch.lights)
            {
                light.GetComponent<Light>().intensity = 0.1f;
                light.GetComponent<AudioSource>().PlayOneShot(lightsOut);
            }
        }

        foreach (ClosetopencloseDoor hideSpots in gameManager.closets)
        {
            hideSpots.GetComponent<Light>().intensity = 0.5f;
        }

        yield return new WaitForSeconds(0.5f);
        hideText.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        itsComingText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.25f);
        hideText.gameObject.SetActive(false);
        timer = 10f;
        active = true;

        yield return new WaitForSeconds(3);
        foreach (LightSwitch lightswitch in lightSwitch)
        {
            lightswitch.GetComponent<AudioSource>().PlayOneShot(footsteps);
        }
    }

    public void Survived()
    {
        foreach (LightSwitch lightswitch in lightSwitch)
        {
            foreach (GameObject light in lightswitch.lights)
            {
                light.GetComponent<Light>().intensity = lightswitch.regularIntensity;
                light.GetComponent<AudioSource>().PlayOneShot(lightsBack);
            }
        }
        foreach (ClosetopencloseDoor hideSpots in gameManager.closets)
        {
            hideSpots.GetComponent<Light>().intensity = 0;
        }



        active = false;
        timer = 10f;
    }
}
