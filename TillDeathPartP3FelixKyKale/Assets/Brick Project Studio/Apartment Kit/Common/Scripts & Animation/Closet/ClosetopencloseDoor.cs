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
		public Transform Player;
		public Canvas hideCanvas;
		public TextMeshProUGUI hideCanvasText;

		void Start()
		{
			open = false;
		}

		void Update()
		{
			{
				if (Player)
				{
					float dist = Vector3.Distance(Player.position, transform.position);
					if (dist < 3)
					{
						inRange = true;
						hideCanvas.gameObject.SetActive(true);
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

					else if (dist > 3)
					{
						inRange=false;
						hideCanvas.gameObject.SetActive(false);
					}
				}

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