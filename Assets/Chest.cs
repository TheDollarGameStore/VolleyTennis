using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chest : MonoBehaviour
{
    public Image progressBar;
    public GameObject star;
    [HideInInspector]
    public int progress;
    private float fillProgress;
    public Bob chestBobber;
    // Start is called before the first frame update
    void Start()
    {
        progress = PlayerPrefs.GetInt("chest_progress", 0);

        if (progress >= 100)
        {
            star.SetActive(true);
            chestBobber.speed = 5;
        }

        fillProgress = progress / 100f;
    }

    // Update is called once per frame
    void Update()
    {
        progressBar.fillAmount = fillProgress;

        fillProgress = Mathf.Lerp(fillProgress, progress / 100f, Time.deltaTime * 5f);
    }

    public void AddProgress()
    {
        progress += Random.Range(10, 20);

        if (progress > 100)
        {
            progress = 100;
        }

        PlayerPrefs.SetInt("chest_progress", progress);

        if (progress >= 100)
        {
            star.SetActive(true);
            chestBobber.speed = 5;
        }
    }
}
