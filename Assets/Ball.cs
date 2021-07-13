using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Vector3 startingPos;
    [HideInInspector]
    public Vector3 destinationPos;

    public Animator enemyRacketHolderAnimator;
    public Animator enemyRacketAnimator;

    private float totalDistance;

    private bool hitCheckReset;

    public float yOffset;
    public float amplitude;
    public GameObject ball;
    private float ballSpeed = 3f;

    [HideInInspector]
    public bool shouldChase;

    [HideInInspector]
    public bool onPlayerLeft = false;

    [HideInInspector]
    public bool onEnemyLeft = false;

    public TrailRenderer normalTrail;
    public TrailRenderer rainbowTrail;

    public ParticleSystem hitParticles;

    public GameObject bumpUpLight;
    public GameObject bumpUpHeavy;

    [HideInInspector]
    public bool started = false;

    // Start is called before the first frame update
    private void Start()
    {
        hitCheckReset = true;
        shouldChase = true;
        ball.transform.position = new Vector3(ball.transform.position.x, yOffset, ball.transform.position.z);
    }

    public void Hit(bool fromEnemy)
    {
        if (!hitCheckReset)
        {
            return;
        }
        hitParticles.Play();
        shouldChase = fromEnemy;
        yOffset = ball.transform.position.y;

        startingPos = transform.position;

        

        if (fromEnemy)
        {
            GameManager.instance.DamageEnemy((int)(ballSpeed * 5));
            destinationPos = new Vector3(Random.Range(-2f, 2f), 0.18f, Random.Range(-1f, -3f));
            float speedFactor = Vector3.Distance(startingPos, destinationPos) / 5f;
            ballSpeed = 2f + (3f * speedFactor * (Random.Range(0.5f, 0.8f)));
            rainbowTrail.emitting = false;
            normalTrail.emitting = true;
            enemyRacketAnimator.SetBool("swing", true);
            enemyRacketHolderAnimator.SetBool(GameManager.instance.enemy.transform.position.x > transform.position.x ? "swingRight" : "swingLeft", true);
            Invoke("ResetEnemyAnimatorBools", 0.15f);
        }
        else
        {
            bool firstHit = false;
            if (started)
            {
                GameManager.instance.DamagePlayer((int)(ballSpeed * 5));
            }
            else
            {
                firstHit = true;
                started = true;
            }

            
            destinationPos = new Vector3(Random.Range(-2f, 2f), 0.18f, Random.Range(1f, 3f));
            float speedFactor = Vector3.Distance(startingPos, destinationPos) / 5f;
            if (!firstHit)
            {
                ballSpeed = 2f + (3f * speedFactor * (GameManager.instance.player.GetComponent<Player>().swingProgress / 100f));
            }
            else
            {
                ballSpeed = 4.5f;
            }
            

            if (GameManager.instance.player.GetComponent<Player>().swingProgress == 100)
            {
                GameManager.instance.UpdateCombo(true);
                Instantiate(bumpUpHeavy, transform.position, Quaternion.identity);
                rainbowTrail.emitting = true;
                normalTrail.emitting = false;
            }
            else
            {
                GameManager.instance.UpdateCombo(false);
                Instantiate(bumpUpLight, transform.position, Quaternion.identity);
                rainbowTrail.emitting = false;
                normalTrail.emitting = true;
            }
        }

        if (GameManager.instance.player.transform.position.x < destinationPos.x)
        {
            onPlayerLeft = true;
        }
        else
        {
            onPlayerLeft = false;
        }

        if (GameManager.instance.enemy.transform.position.x < destinationPos.x)
        {
            onEnemyLeft = true;
        }
        else
        {
            onEnemyLeft = false;
        }

        totalDistance = Vector3.Distance(startingPos, destinationPos);

        hitCheckReset = false;

        Invoke("ResetHit", 0.25f);
    }

    // Update is called once per frame
    void Update()
    {

        if (!started && GameManager.instance.swipeControls.tap)
        {
            Hit(false);
        }

        if (!started)
        {
            return;
        }

        float xFactor = (Vector3.Distance(transform.position, destinationPos) / totalDistance) * Mathf.PI;
        float ballYPos = (Mathf.Sin(xFactor) * amplitude) + yOffset;
        ball.transform.position = new Vector3(transform.position.x, ballYPos, transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, destinationPos, ballSpeed * Time.deltaTime);

        if (ballYPos <= 0.4f)
        {
            if (shouldChase && GameManager.instance.player.GetComponent<Player>().swingProgress != -1 && shouldChase && onPlayerLeft == GameManager.instance.player.GetComponent<Player>().leftSwing)
            {
                Hit(false);
            }
            else if (!shouldChase)
            {
                Hit(true);
            }
        }
    }

    private void ResetHit()
    {
        hitCheckReset = true;
    }

    public void ResetEnemyAnimatorBools()
    {
        enemyRacketHolderAnimator.SetBool("swingRight", false);
        enemyRacketHolderAnimator.SetBool("swingLeft", false);
        enemyRacketAnimator.SetBool("swing", false);
    }
}
