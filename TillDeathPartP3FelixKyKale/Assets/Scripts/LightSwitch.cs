using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{

    public GameObject player;
    public List<GameObject> lights;

    public bool lightOn;
    public float regularIntensity;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        lightOn = true;
        foreach (GameObject item in lights)
        {
            item.GetComponent<Light>().intensity = 1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist < 10)
        {

            {
                if (lightOn == true)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        lightOn = false;
                        Debug.Log("LightToggled");
                        foreach (var item in lights)
                        {
                            item.GetComponent<Light>().intensity = 0.1f;
                        }
                    }
                }

                else if (lightOn != true)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        lightOn = true;
                        Debug.Log("LightToggled");
                        foreach (var item in lights)
                        {
                            item.GetComponent<Light>().intensity = regularIntensity;
                        }
                    }
                }

            }

        }
    }
}
