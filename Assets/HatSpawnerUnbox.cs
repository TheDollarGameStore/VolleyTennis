using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HatSpawnerUnbox : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> hatPrefabs;

    private GameObject spawnedHat;
    private float spawnedHatStartY;

    private TextMeshProUGUI resultText;
    private TextMeshProUGUI duplicateRewardText;

    private void Awake()
    {
        resultText = GameObject.FindGameObjectWithTag("UnboxedResult").GetComponent<TextMeshProUGUI>();
        duplicateRewardText = GameObject.FindGameObjectWithTag("UnboxedCashback").GetComponent<TextMeshProUGUI>();
    }

    public void ChangeHat()
    {
        int hatIndex = Random.Range(0, hatPrefabs.Count);
        spawnedHat = Instantiate(hatPrefabs[hatIndex], gameObject.transform, false);
        spawnedHat.transform.localScale = spawnedHat.transform.localScale / 100f;
        spawnedHatStartY = spawnedHat.transform.position.y;
        PlayerPrefs.SetInt("chest_progress", 0);

        if (PlayerPrefs.GetInt("hat_" + hatIndex.ToString(), 0) == 0)
        {
            //New Unlock
            PlayerPrefs.SetInt("hat_" + hatIndex.ToString(), 1);
            resultText.text = "NEW!";
        }
        else
        {
            //Duplicate
            resultText.text = "DUPLICATE...";
            

            int incomeLevelMult = (PlayerPrefs.GetInt("IncomeLevel", 1) - 1) * 5;
            int world = PlayerPrefs.GetInt("world", 1);
            int prizePool = (world * (100 + incomeLevelMult)) + (Random.Range(10, 20) * world);
            duplicateRewardText.text = "+$" + prizePool.ToString();

            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money", 0) + prizePool);
        }
    }

    private void Update()
    {
        if (spawnedHat != null)
        {
            spawnedHat.transform.Rotate(new Vector3(0f, 25f * Time.deltaTime, 0f));
            spawnedHat.transform.position = Vector3.Lerp(spawnedHat.transform.position, new Vector3(spawnedHat.transform.position.x, spawnedHatStartY + 2f, spawnedHat.transform.position.z), 10f * Time.deltaTime);
        }
    }
}
