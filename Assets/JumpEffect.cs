using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEffect : MonoBehaviour
{
    public float jumpHeight;
    public float jumpSpeed;
    public float jumpDelay;

    private float x;
    private float startY;

    private bool jumping;
    private bool stopped;
    // Start is called before the first frame update
    void Start()
    {
        stopped = false;
        startY = transform.position.y;
        jumping = false;
        Jump();
    }

    // Update is called once per frame
    void Update()
    {
        if (jumping)
        {
            x += jumpSpeed * Time.deltaTime;

            if (x >= Mathf.PI)
            {
                x = Mathf.PI;
                jumping = false;
                Invoke("Jump", jumpDelay);
            }

            float newY = Mathf.Sin(x) * jumpHeight;

            transform.position = new Vector3(transform.position.x, startY + newY, transform.position.z);
        }

        if (stopped)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, startY, transform.position.z), 10f * Time.deltaTime);
        }
    }

    void Jump()
    {
        x = 0;
        jumping = true;
    }

    public void StopJumping()
    {
        jumping = false;
        CancelInvoke("Jump");
        stopped = true;
    }
}
