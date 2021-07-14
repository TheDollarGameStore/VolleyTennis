using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transitioner : MonoBehaviour
{
    public GameObject transition;

    private void Start()
    {
        FadeOut();
    }

    // Start is called before the first frame update
    public void FadeIn(string targetRoom)
    {
        if (GameObject.FindGameObjectWithTag("Transition") == null)
        {
            Transition transCp = Instantiate(transition, transform, false).GetComponent<Transition>();
            transCp.fadeIn = true;
            transCp.targetRoom = targetRoom;
        }
    }

    public void FadeOut()
    {
        if (GameObject.FindGameObjectWithTag("Transition") == null)
        {
            Transition transCp = Instantiate(transition, transform, false).GetComponent<Transition>();
            transCp.fadeIn = false;
        }
    }


}
