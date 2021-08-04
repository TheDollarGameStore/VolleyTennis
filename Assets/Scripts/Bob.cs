using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public float amplitude;

    [SerializeField]
    public float speed;

    private float x;
    private float originalY;

    private RectTransform rt;

    void Start()
    {
        rt = GetComponent<RectTransform>();
        x = 0;
    }

    // Update is called once per frame
    void Update()
    {
        x += speed * Time.deltaTime;
        float yOffset = Mathf.Sin(x) * amplitude;
        rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, originalY + yOffset);
    }
}
