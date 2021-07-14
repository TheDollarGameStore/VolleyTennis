using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;

    public Animator animator;
    public Ball ball;
    public GameObject characterModel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!ball.shouldChase)
        {
            Vector3 currentPos = transform.position;
            Vector3 move = Vector3.MoveTowards(transform.position, ball.destinationPos + (ball.onPlayerLeft ? Vector3.left * 0.5f : Vector3.right * 0.5f), speed * Time.deltaTime);

            Vector3 moveDiff = move - currentPos;

            if (moveDiff.magnitude == 0f)
            {
                animator.SetBool("isRunning", false);
            }
            else
            {
                animator.SetBool("isRunning", true);
            }

            transform.position += moveDiff;

            if (moveDiff.magnitude != 0f)
            {
                Quaternion _lookRotation = Quaternion.LookRotation(moveDiff.normalized);
                characterModel.transform.rotation = Quaternion.Slerp(characterModel.transform.rotation, _lookRotation, 15f * Time.deltaTime);
            }
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }
}
