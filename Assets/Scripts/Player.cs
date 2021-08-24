using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;
    public GameObject characterModel;

    public float speed;

    public Animator racketAnimator;
    public Animator racketHolderAnimator;

    public TrailRenderer tr;

    public Ball ball;

    private bool swingReady = true;

    [HideInInspector]
    public bool leftSwing;

    [HideInInspector]
    public int swingProgress;


    private void Start()
    {
        tr.emitting = false;
        swingProgress = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (ball.shouldChase && ball.started)
        {
            Vector3 currentPos = transform.position;

            Vector3 move;

            if (!GameObject.FindGameObjectWithTag("Ball"))
            {
                move = transform.position;
            }
            else
            {
                move = Vector3.MoveTowards(transform.position, ball.destinationPos + (ball.onPlayerLeft ? Vector3.left * 0.5f : Vector3.right * 0.5f), speed * Time.deltaTime);
            }


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
        

        if (!GameManager.instance.gameOver)
        {
            if (swingReady)
            {
                if (GameManager.instance.swipeControls.swipeLeft || Input.GetKeyDown(KeyCode.RightArrow))
                {
                    leftSwing = true;
                    racketAnimator.SetBool("swing", true);
                    racketHolderAnimator.SetBool("swingRight", true);
                    swingReady = false;
                    Invoke("ResetBool", 0.15f);
                    tr.emitting = true;
                    Invoke("SwingCooldownReset", 0.5f);
                }

                if (GameManager.instance.swipeControls.swipeRight || Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    leftSwing = false;
                    racketAnimator.SetBool("swing", true);
                    racketHolderAnimator.SetBool("swingLeft", true);
                    swingReady = false;
                    Invoke("ResetBool", 0.15f);
                    tr.emitting = true;
                    Invoke("SwingCooldownReset", 0.5f);
                }
            }
        }
    }

    private void ResetBool()
    {
        if (racketAnimator != null)
        {
            racketAnimator.SetBool("swing", false);
            racketHolderAnimator.SetBool("swingRight", false);
            racketHolderAnimator.SetBool("swingLeft", false);
        }
        racketAnimator.SetBool("swing", false);
        racketHolderAnimator.SetBool("swingRight", false);
        racketHolderAnimator.SetBool("swingLeft", false);
    }

    private void SwingCooldownReset()
    {
        swingReady = true;
        if (tr != null)
        {
            tr.emitting = false;
        }
    }
}
