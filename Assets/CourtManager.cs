using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CourtManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> courts;

    public List<Color> skyColors;
    
    void Start()
    {
        int world = 18;//PlayerPrefs.GetInt("world", 1);

        world = world % 20;

        world = Mathf.CeilToInt(world / 2f);

        Instantiate(courts[world - 1], transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
