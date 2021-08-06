using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestReward : MonoBehaviour
{
    private JumpEffect jumpEffect;
    private Wobble3D wobbler;

    private bool opened;
    private bool shaking;

    private float shakeIntensity;

    public GameObject closedChest;
    public GameObject openedChest;

    public GameObject hatReward;

    private bool collected;

    public Transitioner transitioner;

    // Start is called before the first frame update
    void Start()
    {
        shaking = false;
        shakeIntensity = 0f;
        opened = false;
        jumpEffect = GetComponent<JumpEffect>();
        wobbler = GetComponent<Wobble3D>();
        collected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!opened)
        {
            if (Input.GetMouseButtonDown(0))
            {
                opened = true;
                shaking = true;
                jumpEffect.StopJumping();
            }
        }

        if (collected)
        {
            if (Input.GetMouseButtonDown(0))
            {
                transitioner.FadeIn("Upgrades");
            }
        }
        
    }

    private void FixedUpdate()
    {
        if (opened && shaking)
        {
            shakeIntensity += Time.deltaTime / 100f;

            transform.position += new Vector3(Random.Range(-shakeIntensity, shakeIntensity), Random.Range(-shakeIntensity, shakeIntensity), Random.Range(-shakeIntensity, shakeIntensity));

            if (shakeIntensity >= 0.02f)
            {
                shaking = false;
                closedChest.SetActive(false);
                openedChest.SetActive(true);
                wobbler.DoTheWobble();
                GameObject unboxedHatObject = Instantiate(hatReward, transform.position, Quaternion.identity);
                unboxedHatObject.GetComponent<HatSpawnerUnbox>().ChangeHat();
                Camera.main.transform.GetComponent<UnboxingCamera>().followHat = true;
                collected = true;
            }
        }
    }
}
