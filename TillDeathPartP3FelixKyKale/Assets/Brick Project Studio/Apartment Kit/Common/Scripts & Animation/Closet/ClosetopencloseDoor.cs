using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SojaExiles

{
    public class ClosetopencloseDoor : MonoBehaviour
    {

        public Animator Closetopenandclose;
        public bool open;
        public bool inRange;
        public GameObject Player;
        public GameObject hideTextUI;
        public TextMeshProUGUI hideCanvasText;

        void Start()
        {
            Player = GameObject.Find("Player");
            open = false;
        }

        void Update()
        {
            if (Player)
            {
                float dist = Vector3.Distance(Player.transform.position, this.transform.position);
                if (dist < 2)
                {
                    inRange = true;
                    if (open == false)
                    {
                        if (Input.GetKeyDown(KeyCode.E))
                        {
                            StartCoroutine(opening());
                        }
                    }
                    else
                    {
                        if (open == true)
                        {
                            if (Input.GetKeyDown(KeyCode.E))
                            {
                                StartCoroutine(closing());
                            }
                        }

                    }

                }

                else if (dist > 2)
                {
                    inRange = false;
                }

            
            }
            if (inRange == true)
            {

                hideTextUI.gameObject.SetActive(true);
            }
            else if (inRange == false)
            {
                hideTextUI.gameObject.SetActive(false);
            }


        }

        IEnumerator opening()
        {
            print("you are opening the door");
            Closetopenandclose.Play("ClosetOpening");
            open = true;
            hideCanvasText.text = "Close";
            yield return new WaitForSeconds(.5f);
        }

        IEnumerator closing()
        {
            print("you are closing the door");
            Closetopenandclose.Play("ClosetClosing");
            open = false;
            hideCanvasText.text = "Open";
            yield return new WaitForSeconds(.5f);
        }


    }
}