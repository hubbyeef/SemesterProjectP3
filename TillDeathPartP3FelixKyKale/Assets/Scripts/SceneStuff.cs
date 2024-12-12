using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStuff : MonoBehaviour
{
    public GameObject sceneTransition;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SceneTransitioning());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SceneTransitioning()
    {
        sceneTransition.SetActive(true);
        sceneTransition.GetComponent<Animator>().Play("FadingOut");
        yield return new WaitForSeconds(1.3f);
        sceneTransition.gameObject.SetActive(false);
    }
}
