using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI moneyCount;

    private int money;

    private int attackLevel;
    private int staminaLevel;
    private int incomeLevel;

    private int attackPrice;
    private int staminaPrice;
    private int incomePrice;

    public TextMeshProUGUI attackLevelText;
    public TextMeshProUGUI staminaLevelText;
    public TextMeshProUGUI incomeLevelText;

    public TextMeshProUGUI attackPriceText;
    public TextMeshProUGUI staminaPriceText;
    public TextMeshProUGUI incomePriceText;

    void Start()
    {
        UpdateCounters();
    }

    // Update is called once per frame
    void UpdateCounters()
    {
        attackLevel = PlayerPrefs.GetInt("AttackLevel", 1);
        staminaLevel = PlayerPrefs.GetInt("StaminaLevel", 1);
        incomeLevel = PlayerPrefs.GetInt("IncomeLevel", 1);
        money = PlayerPrefs.GetInt("Money", 0);

        //Calculate Prices
        attackPrice = 100 + (int)(attackLevel * (attackLevel / 20f) * 100);
        staminaPrice = 100 + (int)(staminaLevel * (staminaLevel / 20f) * 100);
        incomePrice = 100 + (int)(incomeLevel * (incomeLevel / 20f) * 100);

        //Set Texts
        moneyCount.text = "$" + money.ToString();
        attackLevelText.text = "LVL: " + attackLevel.ToString();
        staminaLevelText.text = "LVL: " + staminaLevel.ToString();
        incomeLevelText.text = "LVL: " + incomeLevel.ToString();
        attackPriceText.text = "$" + attackPrice.ToString();
        incomePriceText.text = "$" + incomePrice.ToString();
        staminaPriceText.text = "$" + staminaPrice.ToString();
    }

    public void BuyAttack()
    {
        if (money >= attackPrice)
        {
            PlayerPrefs.SetInt("AttackLevel", PlayerPrefs.GetInt("AttackLevel", 1) + 1);
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money", 0) - attackPrice);
            UpdateCounters();
        }
        else
        {
            //Flash Money Red
        }
    }

    public void BuyStamina()
    {
        if (money >= staminaPrice)
        {
            PlayerPrefs.SetInt("StaminaLevel", PlayerPrefs.GetInt("StaminaLevel", 1) + 1);
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money", 0) - staminaPrice);
            UpdateCounters();
        }
        else
        {
            //Flash Money Red
        }
    }

    public void BuyIncome()
    {
        if (money >= incomePrice)
        {
            PlayerPrefs.SetInt("IncomeLevel", PlayerPrefs.GetInt("IncomeLevel", 1) + 1);
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money", 0) - incomePrice);
            UpdateCounters();
        }
        else
        {
            //Flash Money Red
        }
    }
}
