using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    private RectTransform rt;

    public TextMeshProUGUI winOrLose;
    public TextMeshProUGUI prize;
    public Chest chest;

    [HideInInspector]
    public bool isWin;

    private int prizePool;

    private bool start = false;
    // Start is called before the first frame update
    void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (start)
        {
            rt.anchoredPosition = Vector2.Lerp(rt.anchoredPosition, new Vector2(0f, rt.anchoredPosition.y), 10f * Time.deltaTime);
        }
    }

    // Update is called once per frame
    public void StartProcedure(bool isWin)
    {
        chest.Invoke("AddProgress", 1f);
        
        this.isWin = isWin;
        start = true;

        int incomeLevelMult = (PlayerPrefs.GetInt("IncomeLevel", 1) - 1) * 5;
        prizePool = (GameManager.instance.world * (100 + incomeLevelMult)) + (Random.Range(10, 20) * GameManager.instance.world);

        if (!isWin)
        {
            prizePool /= 2;
        }
        else
        {
            if (GameManager.instance.level == 5)
            {
                prizePool *= 2;
            }
        }

        PlayerPrefs.SetInt("PrizePool", prizePool);

        prize.text = "x" + prizePool.ToString();

        if (isWin)
        {
            winOrLose.text = "You Win!";
            winOrLose.color = new Color32(108, 255, 88, 255);
        }
        else
        {
            winOrLose.text = "You Lose...";
            winOrLose.color = new Color32(255, 52, 82, 255);
        }
    }
}
