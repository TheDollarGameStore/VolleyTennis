using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> hatPrefabs;

    private void Start()
    {
        ChangeHat();
    }

    public void ChangeHat()
    {
        int hatIndex = PlayerPrefs.GetInt("Equipped", -1);

        Destroy(GameObject.FindGameObjectWithTag("Hat"));

        if (hatIndex != -1)
        {
            Instantiate(hatPrefabs[hatIndex], gameObject.transform, false);
        }
    }
}
