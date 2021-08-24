using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameAnalyticsSDK;
using Facebook.Unity;

public class Initializer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("InitAndTransition", 1f);
    }

    // Update is called once per frame
    void InitAndTransition()
    {
        FB.Init();
        GameAnalytics.Initialize();
        SceneManager.LoadScene("Upgrades");
    }
}
