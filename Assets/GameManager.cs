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

    public GameObject damageText;

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
    private int maxPlayerStamina, maxEnemyStamina;
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
        playerStamina = maxPlayerStamina = 1000;
        enemyStamina = maxEnemyStamina = 1000;
        playerDamage = 10;
        enemyDamage = 10;

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

    public void DamageEnemy()
    {
        float precision = ball.GetComponent<Ball>().hitPrecision;
        if (precision == 0)
        {
            precision = 50f;
        }

        float precisionMultiplier = 1 + (precision / 100f);
        float comboMultiplier = (Mathf.Max(1f, combo) * 0.5f);

        int damageToDo = (int)(playerDamage * comboMultiplier * precisionMultiplier);
        enemyStamina -= damageToDo;
        enemyFillGoal = (float)enemyStamina / maxEnemyStamina;
        enemyHp.text = enemyStamina.ToString();

        GameObject damageObject = Instantiate(damageText, enemy.transform.position + (Vector3.up * 1.6f) + (Vector3.forward * -0.25f), Quaternion.Euler(new Vector3(45, 0, 0)));
        damageObject.transform.Find("Text").GetComponent<TextFade>().displayText = "-" + damageToDo;

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

    public void DamagePlayer()
    {
        int damageToDo = (int)(enemyDamage * Random.Range(0.6f, 1.4f));
        playerStamina -= damageToDo;
        playerFillGoal = (float)playerStamina / maxPlayerStamina;
        playerHp.text = playerStamina.ToString();

        GameObject damageObject = Instantiate(damageText, player.transform.position + (Vector3.up * 1.2f), Quaternion.Euler(new Vector3(45, 0, 0)));
        damageObject.transform.Find("Text").GetComponent<TextFade>().displayText = "-" + damageToDo.ToString();

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
