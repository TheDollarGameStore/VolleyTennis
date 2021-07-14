using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    private RectTransform rt;

    public TextMeshProUGUI winOrLose;
    public TextMeshProUGUI prize;

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
        start = true;

        prizePool = GameManager.instance.world * 100 + (Random.Range(10, 20) * GameManager.instance.world);
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
