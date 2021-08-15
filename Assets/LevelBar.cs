using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelBar : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI level1text;
    public TextMeshProUGUI level2text;
    public TextMeshProUGUI level3text;
    public TextMeshProUGUI level4text;
    public TextMeshProUGUI level5text;

    public Image levelProgressBar;
    void Start()
    {
        SetUpUI();
    }

    // Update is called once per frame
    private void SetUpUI()
    {
        int world = PlayerPrefs.GetInt("world", 1);
        int level = PlayerPrefs.GetInt("level", 1);

        level1text.text = (((world - 1) * 5) + 1).ToString();
        level2text.text = (((world - 1) * 5) + 2).ToString();
        level3text.text = (((world - 1) * 5) + 3).ToString();
        level4text.text = (((world - 1) * 5) + 4).ToString();
        level5text.text = (((world - 1) * 5) + 5).ToString();

        switch (level)
        {
            case 1:
                levelProgressBar.fillAmount = 0.165f;
                break;
            case 2:
                levelProgressBar.fillAmount = 0.365f;
                break;
            case 3:
                levelProgressBar.fillAmount = 0.57f;
                break;
            case 4:
                levelProgressBar.fillAmount = 0.78f;
                break;
            case 5:
                levelProgressBar.fillAmount = 1f;
                break;
        }
    }
}
