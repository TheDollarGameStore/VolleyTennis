using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<HatButton> hats;
    
    public void UpdateHatButtons()
    {
        for (int i = 0; i < hats.Count; i++)
        {
            hats[i].UpdateButton();
        }
    }
}
