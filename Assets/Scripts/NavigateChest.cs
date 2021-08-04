using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigateChest : MonoBehaviour
{
    // Start is called before the first frame update
    private Wobble wobble;
    public Transitioner transitioner;
    public string destination;
    public Chest chest;
    public GameOverManager gameOverManager;

    void Start()
    {
        wobble = GetComponent<Wobble>();
    }

    // Update is called once per frame
    public void Clicked()
    {
        if (!GameObject.FindGameObjectWithTag("Transition") && chest.progress == 100)
        {
            //If it's on the playfield, collect money and go to chest room
            if (SceneManager.GetActiveScene().name == "Play")
            {
                PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money", 0) + PlayerPrefs.GetInt("PrizePool", 0));

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
            }

            wobble.DoTheWobble();
            transitioner.FadeIn(destination);
        }
    }
}
