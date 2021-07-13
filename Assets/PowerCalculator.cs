using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerCalculator : MonoBehaviour
{
    // Start is called before the first frame update
    public void SetSwingProgress(int progress)
    {
        GameManager.instance.player.GetComponent<Player>().swingProgress = progress;
    }
}
