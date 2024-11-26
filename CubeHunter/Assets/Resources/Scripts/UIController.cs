using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Text.RegularExpressions;
using SupersonicWisdomSDK;

public class UIController : MonoBehaviour
{
    public LevelManager _LevelManager;
    public GameObject TopPanel;
    public GameObject UpgradePanel;
    public GameObject SuccessPanel;
    public GameObject FailPanel;
    public bool isGameStart;
    public Text CoinText;
    public TextMeshProUGUI EarnedCoinsText;
    public TextMeshProUGUI LevelText;
    int CollactedCoin;
    public Button NextButton;
    public Button TryAgainButton;
    public Button ResetButton;

    private void Awake()
    {
        isGameStart = false;
        //PlayerPrefs.SetInt("Coin", 71000);
        CoinText.text = PlayerPrefs.GetInt("Coin", 0).ToString();
        // string SceneName = SceneManager.GetActiveScene().name;
        // int levelId = int.Parse(Regex.Replace(SceneName, "[^0-9]", ""));
        // PlayerPrefs.SetInt("Level", levelId);
        LevelText.text = "LEVEL " + PlayerPrefs.GetInt("Level", 1).ToString();
        NextButton.onClick.AddListener(NextButtonClick);
        TryAgainButton.onClick.AddListener(TryAgainButtonClick);
        ResetButton.onClick.AddListener(ResetButtonClick);
        
    }
    // Start is called before the first frame update
    void Start()
    {
        //TinySauce.OnGameStarted(PlayerPrefs.GetInt("Level", 1).ToString());
        SupersonicWisdom.Api.NotifyLevelStarted(PlayerPrefs.GetInt("Level", 1), null);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        isGameStart = true;
        TopPanel.transform.GetChild(0).transform.gameObject.SetActive(false);
        UpgradePanel.SetActive(false);
    }

    public void SetActiveSuccesPanel()
    {
        SuccessPanel.SetActive(true);
        EarnedCoinsText.text = CollactedCoin.ToString();

    }
    public void SetActiveFailPanel()
    {
        SupersonicWisdom.Api.NotifyLevelFailed(PlayerPrefs.GetInt("Level", 1), null);
        FailPanel.SetActive(true);
    }

    public void AddCoin(int coin)
    {
        PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") + coin);
        // CoinText.text = PlayerPrefs.GetInt("Coin").ToString(); // Calling from CloinCoin When Icon Reach to Coin Icon
        CollactedCoin += coin;
    }

    public void NextButtonClick()
    {
        NextButton.enabled = false;
        //TinySauce.OnGameFinished(true, 1, PlayerPrefs.GetInt("Level", 1).ToString());
        SupersonicWisdom.Api.NotifyLevelCompleted(PlayerPrefs.GetInt("Level", 1), null);
        _LevelManager.LoadNextLevel();

    }
    public void TryAgainButtonClick()
    {
        SceneManager.LoadScene("Level " + _LevelManager.GetCurrentSceneLevel().ToString());
    }
    public void ResetButtonClick()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene("Level " + 1);
    }
}
