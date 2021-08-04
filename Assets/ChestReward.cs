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

    // Start is called before the first frame update
    void Start()
    {
        shaking = false;
        shakeIntensity = 0f;
        opened = false;
        jumpEffect = GetComponent<JumpEffect>();
        wobbler = GetComponent<Wobble3D>();
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
            }
        }
    }
}
