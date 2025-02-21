using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
        private GameManager gameManager;

        void Start()
        {
            Player = GameObject.Find("Player");
            open = false;

            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        void OnMouseOver()
        {
            if (Player)
            {
                float dist = Vector3.Distance(Player.transform.position, this.transform.position);
                if (dist < 2)
                {
                    gameManager.CrossHair.color = Color.red;
                    inRange = true;
                    if (open == false)
                    {
                        if (Input.GetMouseButtonDown(0))
                        {
                            StartCoroutine(opening());
                        }
                    }
                    else
                    {
                        if (open == true)
                        {
                            if (Input.GetMouseButtonDown(0))
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


        }

        private void OnMouseExit()
        {
            gameManager.CrossHair.color = Color.white;
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