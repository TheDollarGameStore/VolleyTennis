using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    public TextMeshProUGUI hitFeedback;
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

    [HideInInspector]
    public int hitPrecision;

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
            destinationPos = new Vector3(Random.Range(-2f, 2f), 0.18f, Random.Range(-1f, -3f));
            float speedFactor = Vector3.Distance(startingPos, destinationPos) / 5f;
            ballSpeed = 2f + (3f * speedFactor * (Random.Range(0.5f, 0.8f)));
            rainbowTrail.emitting = false;
            normalTrail.emitting = true;
            enemyRacketAnimator.SetBool("swing", true);
            enemyRacketHolderAnimator.SetBool(GameManager.instance.enemy.transform.position.x > transform.position.x ? "swingRight" : "swingLeft", true);
            Invoke("ResetEnemyAnimatorBools", 0.15f);
            GameManager.instance.CheckWin();
        }
        else
        {
            bool firstHit = false;
            if (!started)
            {
                firstHit = true;
                started = true;
            }

            
            destinationPos = new Vector3(Random.Range(-2f, 2f), 0.18f, Random.Range(1f, 3f));
            float speedFactor = Vector3.Distance(startingPos, destinationPos) / 5f;
            if (!firstHit)
            {
                ballSpeed = 2f + (3f * speedFactor * (GameManager.instance.player.GetComponent<Player>().swingProgress / 100f));
                hitPrecision = GameManager.instance.player.GetComponent<Player>().swingProgress;
            }
            else
            {
                ballSpeed = 4.5f;
            }

            if (!firstHit)
            {
                DisplayHitFeedback(GameManager.instance.player.GetComponent<Player>().swingProgress);
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

            if (started && !firstHit)
            {
                GameManager.instance.CalculateScore();
                GameManager.instance.DamagePlayer(GameManager.instance.currentScore >= GameManager.instance.scoreGoal);
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

        if (ballYPos <= 0.41)
        {
            ballYPos = 0.4f;
        }
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

    private void DisplayHitFeedback(int precision)
    {
        if (precision == 25 || precision == 35 || precision == 45 || precision == 65)
        {
            hitFeedback.text = "Too Late...";
            hitFeedback.color = new Color32(250, 48, 100, 255);
            hitFeedback.gameObject.transform.GetComponent<Wobble>().DoTheWobble();
        }
        else if (precision == 100)
        {
            hitFeedback.text = "Perfect!";
            hitFeedback.color = new Color32(0, 171, 255, 255);
            hitFeedback.gameObject.transform.GetComponent<Wobble>().DoTheWobble();
        }
        else
        {
            hitFeedback.text = "Too Early...";
            hitFeedback.color = new Color32(255, 132, 52, 255);
            hitFeedback.gameObject.transform.GetComponent<Wobble>().DoTheWobble();
        }
    }
}
