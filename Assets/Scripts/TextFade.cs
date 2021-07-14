using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextFade : MonoBehaviour
{
    // Start is called before the first frame update
    private TextMeshProUGUI text;
    private RectTransform rt;
    public string displayText;
    public Vector2 speed;
    public float fadeSpeed;

    private float alpha;
    void Start()
    {
        rt = GetComponent<RectTransform>();
        alpha = 1f;
        text = GetComponent<TextMeshProUGUI>();
        text.text = displayText;
    }

    // Update is called once per frame
    void Update()
    {
        rt.anchoredPosition += speed * Time.deltaTime;
        alpha -= fadeSpeed * Time.deltaTime;

        if (alpha > 0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, alpha);
        }
        else
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
