using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform playerTransform;

    private Vector3 playerDefaultPos;

    [HideInInspector]
    public Vector3 cameraGoalPos;

    private Vector3 cameraDefaultPos;

    void Start()
    {
        cameraDefaultPos = transform.position;
        playerDefaultPos = playerTransform.position;
        cameraGoalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        cameraGoalPos = new Vector3(cameraDefaultPos.x - ((playerDefaultPos.x - playerTransform.position.x) / 1.5f), cameraGoalPos.y, cameraDefaultPos.z - (playerDefaultPos.z - playerTransform.position.z));
        transform.position = Vector3.Lerp(transform.position, cameraGoalPos, 7.5f * Time.deltaTime);
    }
}
