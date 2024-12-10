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


     public IEnumerator opening()
		{
			print("you are opening the Window");
			open = true;
			openandclosewindow.Play("Openingwindow");
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