using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectButton : MonoBehaviour
{
    private Wobble wobbler;

    private bool hovered = false;

    private bool collected = false;

    private void Start()
    {
        wobbler = GetComponent<Wobble>();
    }

    public void Clicked()
    {
        if (!collected)
        {
            wobbler.DoTheWobble();
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money", 0) + PlayerPrefs.GetInt("PrizePool", 0));
            collected = true;
            Invoke("NextRoom", 1f);
        }
    }

    void NextRoom()
    {
        GameManager.instance.transitioner.FadeIn("Play");
    }
}
