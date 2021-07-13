using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBump : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 offset;
    void Start()
    {
        Camera.main.transform.position += offset;
    }
}
