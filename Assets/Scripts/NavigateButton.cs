using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigateButton : MonoBehaviour
{
    // Start is called before the first frame update
    private Wobble wobble;
    public Transitioner transitioner;
    public string destination;

    void Start()
    {
        wobble = GetComponent<Wobble>();
    }

    // Update is called once per frame
    public void Clicked()
    {
        if (!GameObject.FindGameObjectWithTag("Transition"))
        {
            wobble.DoTheWobble();
            transitioner.FadeIn(destination);
        }
    }
}
