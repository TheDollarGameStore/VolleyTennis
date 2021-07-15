using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI moneyCount;

    private int money;

    void Start()
    {
        money = PlayerPrefs.GetInt("Money", 0);
        moneyCount.text = "$" + money.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
