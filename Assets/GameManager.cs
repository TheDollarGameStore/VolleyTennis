using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance = null;

    private int combo = 0;

    public GameObject player;
    public GameObject enemy;
    public SwipeControls swipeControls;
    public GameObject ball;

    public Image enemyHpBar;
    public Image playerHpBar;

    public TextMeshProUGUI playerHp;
    public TextMeshProUGUI enemyHp;
    public TextMeshProUGUI comboText;

    [HideInInspector]
    private int maxPlayerStamina, maxEnemyStamina, maxPlayerDamage, maxEnemyDamage;
    [HideInInspector]
    public int playerStamina, enemyStamina, playerDamage, enemyDamage;

    private float playerFillGoal;
    private float enemyFillGoal;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    void Start()
    {
        playerStamina = maxPlayerStamina = 80;
        enemyStamina = maxEnemyStamina = 80;
        playerDamage = maxPlayerDamage = 10;
        enemyDamage = maxEnemyDamage = 10;

        enemyFillGoal = (float)enemyStamina / maxEnemyStamina;
        playerFillGoal = (float)playerStamina / maxPlayerStamina;
        playerHp.text = playerStamina.ToString();
        enemyHp.text = enemyStamina.ToString();
    }

    private void Update()
    {
        playerHpBar.fillAmount = Mathf.Lerp(playerHpBar.fillAmount, playerFillGoal, 5f * Time.deltaTime);
        enemyHpBar.fillAmount = Mathf.Lerp(enemyHpBar.fillAmount, enemyFillGoal, 5f * Time.deltaTime);
    }

    public void DamageEnemy(int damage)
    {
        enemyStamina -= damage;
        enemyFillGoal = (float)enemyStamina / maxEnemyStamina;
        enemyHp.text = enemyStamina.ToString();

        if (enemyStamina <= 0)
        {
            enemyStamina = 0;
            enemyHp.text = enemyStamina.ToString();
            player.GetComponent<Player>().animator.SetBool("isWin", true);
            enemy.GetComponent<Enemy>().animator.SetBool("isLose", true);
            Destroy(ball.GetComponent<Ball>().enemyRacketHolderAnimator.gameObject);
            Destroy(player.GetComponent<Player>().racketHolderAnimator.gameObject);
            Vector3 cameraGoalPos = Camera.main.transform.GetComponent<CameraBehaviour>().cameraGoalPos;
            Camera.main.transform.GetComponent<CameraBehaviour>().cameraGoalPos = new Vector3(cameraGoalPos.x, 2.5f, cameraGoalPos.z);
            Destroy(ball);
        }
    }

    public void DamagePlayer(int damage)
    {
        playerStamina -= damage;
        playerFillGoal = (float)playerStamina / maxPlayerStamina;
        playerHp.text = playerStamina.ToString();

        if (playerStamina <= 0)
        {
            playerStamina = 0;
            playerHp.text = playerStamina.ToString();
            player.GetComponent<Player>().animator.SetBool("isLose", true);
            enemy.GetComponent<Enemy>().animator.SetBool("isWin", true);
            Destroy(ball.GetComponent<Ball>().enemyRacketHolderAnimator.gameObject);
            Destroy(player.GetComponent<Player>().racketHolderAnimator.gameObject);
            Vector3 cameraGoalPos = Camera.main.transform.GetComponent<CameraBehaviour>().cameraGoalPos;
            Camera.main.transform.GetComponent<CameraBehaviour>().cameraGoalPos = new Vector3(cameraGoalPos.x, 2.5f, cameraGoalPos.z);
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
}
