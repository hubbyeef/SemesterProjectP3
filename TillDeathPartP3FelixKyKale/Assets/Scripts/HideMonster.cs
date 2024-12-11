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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
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
                light.GetComponent<Light>().intensity = 0.05f;
            }

            
            yield return new WaitForSeconds(0.5f);
            hideText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            itsComingText.gameObject.SetActive(true);
            yield return new WaitForSeconds(1.25f);
            hideText.gameObject.SetActive(false);
            timer = 10f;
            active = true;
        }
    }

    public void Survived()
    {
        foreach (LightSwitch lightswitch in lightSwitch)
        {
            foreach (GameObject light in lightswitch.lights)
            {
                light.GetComponent<Light>().intensity = lightswitch.regularIntensity;
            }
        }
    }
}
