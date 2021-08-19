using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    // Start is called before the first frame update
    float alpha = 0f;

    public Image background;
    public Image icon;

    public Sprite tap;
    public Sprite swipeLeft;
    public Sprite swipeRight;

    public SwipeControls swipeControls;

    private bool fadeIn;

    public Ball ball;

    public void ShowTutorialTap()
    {
        icon.sprite = tap;
        fadeIn = true;
    }

    public void ShowTutorialSwipeLeft()
    {
        icon.sprite = swipeLeft;
        fadeIn = true;
    }

    public void ShowTutorialSwipeRight()
    {
        icon.sprite = swipeRight;
        fadeIn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIn && alpha != 1f)
        {
            alpha += 2f * Time.deltaTime;
            if (alpha > 1f)
            {
                alpha = 1f;
            }
        }

        if (!fadeIn && alpha != 0f)
        {
            alpha -= 2f * Time.deltaTime;

            if (alpha < 0f)
            {
                alpha = 0f;
            }
        }

        background.color = new Color(background.color.r, background.color.g, background.color.b, alpha / 2f);
        icon.color = new Color(icon.color.r, icon.color.g, icon.color.b, alpha);

        if (fadeIn)
        {
            if (icon.sprite == tap)
            {
                if (swipeControls.tap)
                {
                    fadeIn = false;
                }
            }
            else if (icon.sprite == swipeLeft)
            {
                if (swipeControls.swipeLeft)
                {
                    fadeIn = false;
                    ball.hasInvokedSwipeTutorial = false;
                }
            }
            else if (icon.sprite == swipeRight)
            {
                if (swipeControls.swipeRight)
                {
                    fadeIn = false;
                    //Might need to invoke reset variable here
                    ball.hasInvokedSwipeTutorial = false;
                }
            }
        }
        
    }
}
