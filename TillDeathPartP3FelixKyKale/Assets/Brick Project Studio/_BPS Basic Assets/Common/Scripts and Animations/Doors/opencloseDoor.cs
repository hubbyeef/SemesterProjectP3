using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{
	public class opencloseDoor : MonoBehaviour
	{

		public Animator openandclose;
		public bool open;
		public Transform Player;
		private GameManager manager;

		void Start()
		{
			open = false;
			Player = GameObject.Find("Player").transform;
			manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

		void OnMouseOver()
		{
			{
				if (Player)
				{
                    manager.CrossHair.color = Color.red;
                    float dist = Vector3.Distance(Player.position, transform.position);
					if (dist < 5)
					{
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
				}

			}

		}

        private void OnMouseExit()
        {
            manager.CrossHair.color = Color.white;
        }

        IEnumerator opening()
		{
			print("you are opening the door");
			openandclose.Play("Opening");
			open = true;
			yield return new WaitForSeconds(.5f);
		}

		IEnumerator closing()
		{
			print("you are closing the door");
			openandclose.Play("Closing");
			open = false;
			yield return new WaitForSeconds(.5f);
		}


	}
}