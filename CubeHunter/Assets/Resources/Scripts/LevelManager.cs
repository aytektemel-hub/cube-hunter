using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;


public class LevelManager : MonoBehaviour
{
    public int TotalSceneLevelCount;
    public int NotRepeatableLevelsCount;
    public int RoundCount;
    //int CurrentSceneLevel;

    private void Awake()
    {
        string SceneName = SceneManager.GetActiveScene().name;
        //CurrentSceneLevel = int.Parse(Regex.Replace(SceneName, "[^0-9]", ""));
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

    public void LoadNextLevel()
    {
        if (GetCurrentSceneLevel() < TotalSceneLevelCount)
        {
            print(GetCurrentSceneLevel());
            SceneManager.LoadScene("Level " + (GetCurrentSceneLevel() + 1).ToString());
            PlayerPrefs.SetInt("Level", (PlayerPrefs.GetInt("Level", 1) + 1));
        }
        else
        {
            PlayerPrefs.SetInt("Level", (PlayerPrefs.GetInt("Level", 1) + 1));
            PlayerPrefs.SetInt("RoundCount", RoundCount + 1);
            SceneManager.LoadScene("Level " + (NotRepeatableLevelsCount + 1).ToString());
        }

    }

    public int GetCurrentSceneLevel()
    {
        int CurrentSceneLevel = (PlayerPrefs.GetInt("Level", 1) - (RoundCount * (TotalSceneLevelCount - NotRepeatableLevelsCount)));
        return CurrentSceneLevel;

    }
}
