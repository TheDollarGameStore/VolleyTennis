using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HatButton : MonoBehaviour
{
    public int hatIndex;

    public HatManager hatManager;

    private Image buttonImage;

    private bool unlocked;

    private bool equipped;

    private Wobble wobbler;
    // Start is called before the first frame update
    void Start()
    {
        buttonImage = GetComponent<Image>();
        wobbler = GetComponent<Wobble>();
        UpdateButton();
    }

    public void UpdateButton()
    {
        unlocked = PlayerPrefs.GetInt("hat_" + hatIndex.ToString(), 0) == 1;
        equipped = PlayerPrefs.GetInt("Equipped", -1) == hatIndex;

        if (equipped)
        {
            buttonImage.color = new Color32(70, 255, 125, 255);
        }
        else if (unlocked)
        {
            buttonImage.color = new Color32(255, 255, 255, 255);
        }
        else
        {
            buttonImage.color = new Color32(160, 160, 160, 255);
        }
    }

    public void Clicked()
    {
        if (unlocked)
        {
            if (!equipped)
            {
                PlayerPrefs.SetInt("Equipped", hatIndex);
            }
            else
            {
                PlayerPrefs.SetInt("Equipped", -1);
            }

            hatManager.UpdateHatButtons();

            wobbler.DoTheWobble();
        }
    }
}
