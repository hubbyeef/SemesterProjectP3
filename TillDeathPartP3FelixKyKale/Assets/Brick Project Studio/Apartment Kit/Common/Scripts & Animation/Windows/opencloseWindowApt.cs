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
		public bool fullyOpen;
		public Transform Player;

		void Start()
		{
			open = false;
			Player = GameObject.Find("Player").transform;
		}

		void OnMouseOver()
		{
			{
				if (Player)
				{
					float dist = Vector3.Distance(Player.position, transform.position);
					if (dist < 15)
					{
						
						{
							if (open == true || fullyOpen == true)
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

        void FixedUpdate()
        {
			if (beingOpened)
			{
				StartCoroutine(opening());
			}
        }


    IEnumerator opening()
		{
			print("you are opening the Window");
			open = true;
			openandclosewindow.Play("Openingwindow");
			fullyOpen = true;
			yield return new WaitForSeconds(.5f);
		}

		IEnumerator closing()
		{
			print("you are closing the Window");
			openandclosewindow.Play("Closingwindow");
			open = false;
			yield return new WaitForSeconds(.5f);
		}


	}
}