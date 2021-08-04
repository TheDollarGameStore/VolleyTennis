using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectButton : MonoBehaviour
{
    private Wobble wobbler;

    private bool collected = false;

    public GameOverManager gameOverManager;

    private void Start()
    {
        wobbler = GetComponent<Wobble>();
    }

    public void Clicked()
    {
        if (!collected && !GameObject.FindGameObjectWithTag("Transition"))
        {
            wobbler.DoTheWobble();
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money", 0) + PlayerPrefs.GetInt("PrizePool", 0));
            collected = true;
            Invoke("NextRoom", 1f);
        }
    }

    void NextRoom()
    {
        if (gameOverManager.isWin)
        {
            if (GameManager.instance.level != 5)
            {
                PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level", 1) + 1);
            }
            else
            {
                PlayerPrefs.SetInt("level", 1);
                PlayerPrefs.SetInt("world", PlayerPrefs.GetInt("world", 1) + 1);
            }
        }

        GameManager.instance.transitioner.FadeIn("Upgrades");
    }
}
