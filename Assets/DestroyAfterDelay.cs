using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterDelay : MonoBehaviour
{
    // Start is called before the first frame update
    public float delay;
    void Start()
    {
        Invoke("DestroySelf", delay);
    }

    // Update is called once per frame
    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
