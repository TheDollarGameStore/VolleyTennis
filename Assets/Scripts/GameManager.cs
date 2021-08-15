using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    public GameOverManager gameOverManager;

    private int combo = 0;

    public GameObject plusScoreText;

    public GameObject player;
    public GameObject enemy;
    public SwipeControls swipeControls;
    public GameObject ball;
    public GameObject explosion;
    public GameObject explosionShake;
    public GameObject darken;

    public Image scoreGoalBar;
    public Image playerHpBar;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI comboText;
    public TextMeshProUGUI scoreGoalText;


    public Transitioner transitioner;

    [HideInInspector]
    public bool gameOver;

    [HideInInspector]
    public int maxPlayerStamina, scoreGoal;
    [HideInInspector]
    public int playerStamina, currentScore, playerDamage, enemyDamage;

    private float playerFillGoal;
    private float enemyFillGoal;

    [HideInInspector]
    public int world;
    [HideInInspector]
    public int level;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        gameOver = false;

        world = PlayerPrefs.GetInt("world", 1);
        level = PlayerPrefs.GetInt("level", 1);

        playerStamina = maxPlayerStamina = 300 + (PlayerPrefs.GetInt("StaminaLevel", 1) * 25);
        if (world == 1)
        {
            scoreGoal = (int)(100f * (level + (world * 1.5f)));
        }
        else if (world == 2)
        {
            scoreGoal = (int)(100f * (level + (world * 2f)));
        }
        else
        {
            scoreGoal = (int)(100f * (level + (world * 2.5f)));
        }
        playerDamage = 10 + (PlayerPrefs.GetInt("AttackLevel", 1) * 5);
        enemyDamage = 10 + (world * level);

        playerStamina = maxPlayerStamina /= 2;
        currentScore = 0;

        enemyFillGoal = (float)currentScore / scoreGoal;
        playerFillGoal = (float)playerStamina / maxPlayerStamina;
        scoreText.text = currentScore.ToString();
        scoreGoalText.text = scoreGoal.ToString();
    }

    private void Update()
    {
        playerHpBar.fillAmount = Mathf.Lerp(playerHpBar.fillAmount, playerFillGoal, 5f * Time.deltaTime);
        scoreGoalBar.fillAmount = Mathf.Lerp(scoreGoalBar.fillAmount, enemyFillGoal, 5f * Time.deltaTime);
    }

    public void CalculateScore()
    {
        float precision = ball.GetComponent<Ball>().hitPrecision;
        if (precision == 0)
        {
            precision = 50f;
        }

        float precisionMultiplier = precision / 100f;
        float comboMultiplier = 1 + (combo * 0.5f);

        int damageToDo = (int)(playerDamage * comboMultiplier * precisionMultiplier);
        currentScore += damageToDo;
        enemyFillGoal = (float)currentScore / scoreGoal;
        scoreText.text = currentScore.ToString();

        GameObject damageObject = Instantiate(plusScoreText, player.transform.position + (Vector3.up * 1.6f) + (Vector3.forward * -0.25f), Quaternion.Euler(new Vector3(45, 0, 0)));
        damageObject.transform.Find("Text").GetComponent<TextFade>().displayText = "+" + damageToDo;

        if (currentScore >= scoreGoal)
        {
            scoreText.text = currentScore.ToString();
            player.GetComponent<Player>().animator.SetBool("isWin", true);
            enemy.GetComponent<Enemy>().animator.SetBool("isLose", true);
            Destroy(ball.GetComponent<Ball>().enemyRacketHolderAnimator.gameObject);
            Destroy(player.GetComponent<Player>().racketHolderAnimator.gameObject);
            Vector3 cameraGoalPos = Camera.main.transform.GetComponent<CameraBehaviour>().cameraGoalPos;
            Camera.main.transform.GetComponent<CameraBehaviour>().cameraGoalPos = new Vector3(cameraGoalPos.x, 2.5f, cameraGoalPos.z);
            Destroy(ball.GetComponent<Ball>().hitFeedback);
            comboText.text = "";
            Instantiate(explosion, ball.transform.position, Quaternion.identity);
            Instantiate(explosionShake, ball.transform.position, Quaternion.identity);
            gameOver = true;
            Invoke("GameOver", 2.5f);
            Destroy(ball);
        }
    }

    public void DamagePlayer(bool playerAlreadyWon)
    {
        int damageToDo = (int)(enemyDamage * Random.Range(0.6f, 1.4f));
        playerStamina -= damageToDo;
        playerFillGoal = (float)playerStamina / maxPlayerStamina;
        


        if (!playerAlreadyWon && playerStamina <= 0)
        {
            playerStamina = 0;
            player.GetComponent<Player>().animator.SetBool("isLose", true);
            enemy.GetComponent<Enemy>().animator.SetBool("isWin", true);
            Destroy(ball.GetComponent<Ball>().enemyRacketHolderAnimator.gameObject);
            Destroy(player.GetComponent<Player>().racketHolderAnimator.gameObject);
            Vector3 cameraGoalPos = Camera.main.transform.GetComponent<CameraBehaviour>().cameraGoalPos;
            Camera.main.transform.GetComponent<CameraBehaviour>().cameraGoalPos = new Vector3(cameraGoalPos.x, 2.5f, cameraGoalPos.z);
            Destroy(ball.GetComponent<Ball>().hitFeedback);
            comboText.text = "";
            Instantiate(explosion, ball.transform.position, Quaternion.identity);
            Instantiate(explosionShake, ball.transform.position, Quaternion.identity);
            gameOver = true;
            Invoke("GameOver", 2.5f);
            Destroy(ball);
        }
    }

    public void UpdateCombo(bool add)
    {
        if (add)
        {
            combo += 1;
            comboText.text = "X" + combo.ToString();
            comboText.gameObject.GetComponent<Wobble>().DoTheWobble();
        }
        else
        {
            combo = 0;
            comboText.text = "";
        }
    }

    void GameOver()
    {
        darken.SetActive(true);
        gameOverManager.StartProcedure(playerStamina == 0 ? false : true);
    }
}
