using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float intensity;
    public float duration;

    private float fadeSpeed;

    private void Start()
    {
        fadeSpeed = intensity / duration;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (intensity > 0f)
        {
            intensity -= fadeSpeed;
            Camera.main.transform.position += new Vector3(Random.Range(-intensity, intensity), Random.Range(-intensity, intensity), Random.Range(-intensity, intensity));
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
