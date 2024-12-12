using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{
	public class opencloseWindowApt : MonoBehaviour
	{

		public Animator openandclosewindow;
		public bool open;
		public bool beingOpened;
		public float timer = 20f;
		public Transform Player;
		public Light[] lightsInRoom;
		public LightSwitch lightSwitch;

		public AnimationClip openingClip;
		public AudioClip flickeringSFX;


        private void Awake()
        {
			beingOpened = false;
            open = false;
            Player = GameObject.Find("Player").transform;
        }

		void OnMouseOver()
		{
			{
				if (Player)
				{
					float dist = Vector3.Distance(Player.position, transform.position);
					if (dist < 5)
					{
						
						{
							if (open == true || beingOpened == true)
							{
								if (Input.GetMouseButtonDown(0))
								{
									StopCoroutine(opening());
									StartCoroutine(closing());

                                }
							}

						}

					}
				}

			}

		}


     public IEnumerator opening()
		{
			timer = 20f;
			print("you are opening the Window");
			StartCoroutine(lightFlickers());
			openandclosewindow.Play("Openingwindow");
            foreach (Light lights in lightsInRoom)
            {
				lights.GetComponent<AudioSource>().PlayOneShot(flickeringSFX);
            }
            yield return new WaitForSeconds(20f);
			beingOpened = false;
			open = true;
			yield return new WaitForSeconds(.5f);
		}

		IEnumerator closing()
		{
            timer = 20f;
            print("you are closing the Window");
			openandclosewindow.Play("Closingwindow");
			open = false;
			beingOpened = false;
            StopCoroutine(lightFlickers());
            foreach (Light lights in lightsInRoom)
            {
                lights.GetComponent<AudioSource>().Stop();
            }
            timer = 20f;

            yield return new WaitForSeconds(.5f);
		}

		public IEnumerator lightFlickers()
		{
			foreach (Light lights in lightsInRoom)
			{
				while (beingOpened && timer > 0f)
				{
					lights.intensity = Random.Range(0.1f, 0.49f);
					yield return new WaitForSeconds(0.1f);
					lights.intensity = Random.Range(0.5f, 2f);
					yield return new WaitForSeconds(0.1f);
				}
			}
		}


	}
}