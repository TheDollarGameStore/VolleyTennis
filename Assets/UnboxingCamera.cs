using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnboxingCamera : MonoBehaviour
{
    // Start is called before the first frame update
    private float goalY;

    [HideInInspector]
    public bool followHat;

    void Start()
    {
        goalY = transform.position.y + 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (followHat)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, goalY, transform.position.z), 10f * Time.deltaTime);
        }
    }
}
