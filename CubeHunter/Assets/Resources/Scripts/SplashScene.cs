using System.Collections;
using System.Collections.Generic;
using SupersonicWisdomSDK;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScene : MonoBehaviour
{
    public int TotalSceneLevelCount;
    public int NotRepeatableLevelsCount;
    int RoundCount;
    private void Awake()
    {
        // Subscribe
        SupersonicWisdom.Api.AddOnReadyListener(OnSupersonicWisdomReady);
        // Then initialize
        SupersonicWisdom.Api.Initialize();

        RoundCount = PlayerPrefs.GetInt("RoundCount", 0);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadLevel()
    {
        int CurrentSceneLevel = (PlayerPrefs.GetInt("Level", 1) - (RoundCount * (TotalSceneLevelCount - NotRepeatableLevelsCount)));
        SceneManager.LoadScene("Level " + CurrentSceneLevel.ToString());
    }

    public void OnSupersonicWisdomReady()
    {
        LoadLevel();
    }
}
